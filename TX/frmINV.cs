using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Julian.Helper;
using Julian.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TX
{
    public partial class frmINV : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public frmINV()
        {
            InitializeComponent();
            dgvMain.AutoGenerateColumns = false;
        }

        private void frmINV_Load(object sender, EventArgs e)
        {
            cbCus.DataSource = new string[] { "TX", "HWK" };
            // txtFilePath.Text = _iniManager.GetString("Default", "OrderFormHSVPath", "");
        }
        private bool CheckReadWriteFile(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return true;
                }
            }
            catch (IOException)
            {

                return false;
            }
        }
        private async void btnCreateINVPKL_Click(object sender, EventArgs e)
        {
            txtInput.Enabled = false;
            cbCus.Enabled = false;
            txtFilename.Enabled = false;
            btnCreate.Enabled = false;

            FileStream fs_template = null;
            XLWorkbook wb_template = null;

            FileStream fs_Main = null;
            XLWorkbook wb_Main = null;
            try
            {
                if (!int.TryParse(txtExchangRate.Text.Trim(), out int exchangRate))
                {
                    toolTip1.Show("Tỷ giá không hợp lệ!", txtExchangRate);
                    return;
                }
                string output = txtFilename.Text;
                string filePath = $@"OUTPUT\INV\{txtFilename.Text}.xlsx";
                if (string.IsNullOrWhiteSpace(output) ||
                    output.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    toolTip1.Show("Tên file không hợp lệ!", txtFilename);
                    return;
                }
                if (File.Exists(filePath) && !CheckReadWriteFile(filePath))
                {
                    toolTip1.Show("Có file đã tồn tại đang mở!", txtFilename);
                    return;
                }
                string cusName = cbCus.SelectedValue.ToString();
                await Task.Run(() =>
                {
                    fs_template = new FileStream($@"Default\INVPKLHD_{cusName}.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreate);
                    return;
                }

                var lstDiem = (dgvMain.DataSource as BindingList<ListGuiDiem2>).ToList();
                /* var task1 = await CreateINV(cusName, lstINV, wb_template);
                 if (!task1.success)
                 {
                     MessageBox.Show(task1.msg);
                     return;
                 }
                */
                var task2 = await CreateINVHD(cusName, exchangRate, lstDiem, wb_template);
                if (!task2.success)
                {
                    MessageBox.Show(task2.msg);
                    return;
                }
                await CreatePKLHD(cusName, lstDiem, wb_template);
                //await CreatePKLXE(cusName, lstINV, wb_template);
                //await CreatePKLXE_CLAIM(cusName, lstINV, wb_template);
                await CreatePHIEUGIAOHANG(cusName, lstDiem, wb_template);
                await CreatePHIEUGIAOHANG_CLAIM(cusName, lstDiem, wb_template);
                await CreateDEBIT(cusName, exchangRate, lstDiem, wb_template);
                await CreateTEMLST(cusName, lstDiem, wb_template);
                await Task.Run(() => wb_template.SaveAs(filePath));
                if (File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start("explorer.exe", @"/select, " + filePath);
                }
                else
                {
                    MessageBox.Show("Lỗi không xác định!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                txtInput.Enabled = true;
                cbCus.Enabled = true;
                txtFilename.Enabled = true;
                btnCreate.Enabled = true;

                wb_template?.Dispose();
                fs_template?.Dispose();

                wb_Main?.Dispose();
                fs_Main?.Dispose();
            }
        }
        private async Task<(bool success, string msg)> CreatePHIEUGIAOHANG(string cusName, List<ListGuiDiem2> lst, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PGH");
                    sheet.Cell("A8").Value = "Khách hàng: " + cusName.ToUpper();
                    sheet.Cell("J8").Value = dtpCreateDate.Value.ToString("dd/MM/yyyy");
                    var keywords = new[] { "CLAIM", "FOC", "BU", "BÙ" };
                    var lstNew = lst.Where(x => keywords.All(k => !x.CusPO.ToUpper().Contains(k))).ToList();
                    int n = 10;
                    int i = 1;
                    foreach (var inv in lst)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        sheet.Row(n).Height = 80;
                        var row = sheet.Range($"A{n}:K{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        row.Style.Font.Bold = false;
                        row.Style.Font.FontColor = XLColor.Black;
                        row.Cell("A").Value = i++;
                        row.Cell("B").Value = inv.CusPO;
                        row.Cell("C").Value = inv.CusItemCode;
                        row.Cell("D").Value = inv.Lieu;
                        row.Cell("E").Value = inv.MauKD;
                        row.Cell("F").Value = inv.Width;
                        row.Cell("G").Value = inv.Qty;
                        row.Cell("G").Style.Font.Bold = true;
                        row.Cell("H").Value = "YD";
                        row.Cell("I").Value = inv.RollQty;
                        row.Cell("J").Value = inv.Roll;
                        row.Cell("J").Style.Font.Bold = true;
                        row.Cell("K").Value = inv.ID;
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd = sheet.Range($"A{n}:J{n}").Row(1);
                    sheet.Range($"A{n}:F{n}").Merge();
                    sheet.Row(n).Height = 50;
                    rowEnd.Style.Font.Bold = true;
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL";
                    rowEnd.Cell("G").FormulaA1 = $"=SUBTOTAL(109,G{n - lst.Count}:G{n - 1})";
                    rowEnd.Cell("G").Style.Font.FontColor = XLColor.Red;
                    rowEnd.Cell("I").FormulaA1 = $"=SUBTOTAL(109,I{n - lst.Count}:I{n - 1})";

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreatePHIEUGIAOHANG_CLAIM(string cusName, List<ListGuiDiem2> lst, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PGH CLAIM|FOC");
                    sheet.Cell("A8").Value = "Khách hàng: " + cusName.ToUpper();
                    sheet.Cell("J8").Value = dtpCreateDate.Value.ToString("dd/MM/yyyy");
                    var keywords = new[] { "CLAIM", "FOC", "BU", "BÙ" };
                    var lstNew = lst.Where(x => keywords.Any(k => x.CusPO.ToUpper().Contains(k))).ToList();
                    if (lstNew.Count == 0)
                    {
                        return (false, "Không có đơn hàng CLAIM|FOC");
                    }
                    int n = 10;
                    int i = 1;
                    foreach (var inv in lstNew)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        sheet.Row(n).Height = 80;
                        var row = sheet.Range($"A{n}:K{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        row.Style.Font.Bold = false;
                        row.Style.Font.FontColor = XLColor.Black;
                        row.Cell("A").Value = i++;
                        row.Cell("B").Value = inv.CusPO;
                        row.Cell("C").Value = inv.CusItemCode;
                        row.Cell("D").Value = inv.Lieu;
                        row.Cell("E").Value = inv.MauKD;
                        row.Cell("F").Value = inv.Width;
                        row.Cell("G").Value = inv.Qty;
                        row.Cell("G").Style.Font.Bold = true;
                        row.Cell("H").Value = "YD";
                        row.Cell("I").Value = inv.RollQty;
                        row.Cell("J").Value = inv.Roll;
                        row.Cell("J").Style.Font.Bold = true;
                        row.Cell("K").Value = inv.ID;
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd = sheet.Range($"A{n}:J{n}").Row(1);
                    sheet.Range($"A{n}:F{n}").Merge();
                    sheet.Row(n).Height = 50;
                    rowEnd.Style.Font.Bold = true;
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL";
                    rowEnd.Cell("G").FormulaA1 = $"=SUBTOTAL(109,G{n - lstNew.Count}:G{n - 1})";
                    rowEnd.Cell("G").Style.Font.FontColor = XLColor.Red;
                    rowEnd.Cell("I").FormulaA1 = $"=SUBTOTAL(109,I{n - lstNew.Count}:I{n - 1})";

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreateINVHD(string cusName, int exchangeRate, List<ListGuiDiem2> lst, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("INV");
                    //sheet.Cell("E3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    var keywords = new[] { "CLAIM", "FOC", "BU", "BÙ" };
                    var lstNew = lst.Where(x => !keywords.Any(k => x.CusPO.ToUpper().Contains(k))).ToList();
                    var lstINVHD = lstNew.GroupBy(x => x.Lieu).Select(x => new INVHD()
                    {
                        DESCRIPTION = x.First().Lieu,
                        //COLOR = x.Sum(y => y.Qty).ToString("#,##0.000"),
                        SOLUONG = x.Sum(y => y.Qty),
                        DONGIA = x.First().UnitPrice * exchangeRate
                    }).OrderBy(x => x.DESCRIPTION).ToList();
                    if (lstINVHD.Count == 0)
                        return (true, "");
                    //sheet.Row(19).InsertRowsBelow(lstINVHD.Count);
                    int n = 17;
                    for (int i = 0; i < lstINVHD.Count; i++)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        sheet.Row(n).Height = 30;
                        var row = sheet.Range($"A{n}:F{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Style.Font.Bold = false;
                        row.Cell("A").Value = (i + 1).ToString();
                        row.Cell("B").Value = lstINVHD[i].DESCRIPTION;
                        row.Cell("C").Value = lstINVHD[i].COLOR;
                        row.Cell("D").Value = lstINVHD[i].SOLUONG;
                        row.Cell("D").Style.Font.Bold = true;
                        row.Cell("E").Value = lstINVHD[i].DONGIA;
                        row.Cell("F").FormulaA1 = $"=ROUND(D{n}*E{n},0)";
                        row.Cell("F").Style.NumberFormat.Format = "#,##0";
                        row.Cell("F").Style.Font.Bold = true;
                    }
                    sheet.Cell($"D{18 + lstINVHD.Count}").FormulaA1 = $"=SUM(D18:D{17 + lstINVHD.Count})";
                    sheet.Cell($"F{18 + lstINVHD.Count}").FormulaA1 = $"=SUM(F18:F{17 + lstINVHD.Count})";
                    sheet.Cell($"F{19 + lstINVHD.Count}").FormulaA1 = $"=ROUND(F{18 + lstINVHD.Count}*0.08,0)";
                    sheet.Cell($"F{20 + lstINVHD.Count}").FormulaA1 = $"=SUM(F{18 + lstINVHD.Count}+F{19 + lstINVHD.Count})";
                    // var rowEnd = sheet.Range($"C{24 + lstINVHD.Count}:F{24 + lstINVHD.Count}");
                    //rowEnd.Merge();
                    // sheet.Cell($"C{24 + lstINVHD.Count}").Value = "JULIEN (VN) METAL POWDER CO.,LTD";
                    sheet.Cell("E3").Value = $"INV{dtpCreateDate.Value.ToString("yyyyMMdd")}TX-SL";
                    sheet.Cell("F3").Value = dtpCreateDate.Value.ToString("yyyy/MM/dd");
                    sheet.Cell("E14").Value = $"Tỷ giá tháng {dtpCreateDate.Value.ToString("MM")} : {exchangeRate} vnd/usd";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreatePKLHD(string cusName, List<ListGuiDiem2> lstDiem, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PKL");
                    //sheet.Cell("F3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    int n = 20;
                    int descriptionIndex = 1;
                    var keywords = new[] { "CLAIM", "FOC", "BU", "BÙ" };
                    var lstNew = lstDiem.Where(x => !keywords.Any(k => x.CusPO.ToUpper().Contains(k))).ToList();
                    var lstG = lstNew.GroupBy(x => new { x.Lieu, x.Width }).OrderBy(g => g.Key.Lieu).ThenBy(g => g.Key.Width).ToList();
                    var lstIndex = new List<int>();
                    foreach (var lst in lstG)
                    {
                        for (int i = 0; i <= lst.Count(); i++)
                        {
                            sheet.Row(n++).InsertRowsBelow(1);
                            sheet.Row(n).Height = 30;
                            var row = sheet.Range($"A{n}:M{n}").Row(1);
                            row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            row.Style.Font.Bold = false;
                            row.Style.Font.FontColor = XLColor.Black;
                            if (i != lst.Count())
                            {
                                var inv = lst.ToList()[i];
                                row.Style.Font.Bold = false;

                                row.Cell("A").Value = descriptionIndex;
                                row.Cell("H").Style.Font.Bold = true;
                                row.Cell("B").Value = inv.CusPO;
                                row.Cell("C").Value = inv.CusItemCode;
                                row.Cell("D").Value = inv.Lieu;
                                row.Cell("E").Value = inv.MauKD;
                                row.Cell("F").Value = inv.Width;
                                row.Cell("G").Value = 210;
                                row.Cell("H").Value = inv.Qty;
                                row.Cell("H").Style.Font.Bold = true;
                                row.Cell("I").Value = "Y";
                                row.Cell("J").Value = 1;
                                row.Cell("K").Value = 1;
                                row.Cell("L").FormulaA1 = $"=H{n}*0.24";
                                row.Cell("M").FormulaA1 = $"=L{n}+0.25";
                            }
                            else
                            {
                                row.Style.Font.Bold = true;
                                row.Style.Font.FontColor = XLColor.Red;
                                sheet.Range($"A{n}:G{n}").Merge();
                                sheet.Row(n).Height = 30;
                                row.Cell("A").Value = lst.First().Lieu;
                                row.Cell("H").FormulaA1 = $"=SUM(H{n - lst.Count()}:H{n - 1})";
                                row.Cell("J").FormulaA1 = $"=SUM(J{n - lst.Count()}:J{n - 1})";
                                row.Cell("K").FormulaA1 = $"=SUM(K{n - lst.Count()}:K{n - 1})";
                                row.Cell("L").FormulaA1 = $"=SUM(L{n - lst.Count()}:L{n - 1})";
                                row.Cell("M").FormulaA1 = $"=SUM(M{n - lst.Count()}:M{n - 1})";
                                descriptionIndex++;
                                lstIndex.Add(n);
                            }

                        }
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd = sheet.Range($"A{n}:M{n}").Row(1);
                    sheet.Range($"A{n}:G{n}").Merge();
                    sheet.Row(n).Height = 40;
                    rowEnd.Style.Font.FontSize = rowEnd.Style.Font.FontSize * 1.4;
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL:";
                    rowEnd.Cell("H").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "H" + x))})";
                    rowEnd.Cell("J").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "J" + x))})";
                    rowEnd.Cell("K").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "K" + x))})";
                    rowEnd.Cell("L").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "L" + x))})";
                    rowEnd.Cell("M").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "M" + x))})";

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreateDEBIT(string cusName, int exchangeRate, List<ListGuiDiem2> lst, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("DEBIT");
                    sheet.Cell("A4").Value = $"Debit Note - {cusName} - Production ({dtpCreateDate.Value.ToString("yyyy")}年{dtpCreateDate.Value.ToString("MM")}月份請款明細 )";
                    sheet.Cell("H5").Value = $"Currency T{dtpCreateDate.Value.ToString("MM")}: {exchangeRate}VNĐ";
                    var keywords = new[] { "CLAIM", "FOC", "BU", "BÙ" };
                    var lstNew = lst.Where(x => keywords.All(k => !x.CusPO.ToUpper().Contains(k))).ToList();
                    int n = 6;
                    string invNo = $"{dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}";
                    foreach (var inv in lstNew)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        sheet.Row(n).Height = 30;
                        var row = sheet.Range($"A{n}:K{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        row.Style.Font.Bold = false;
                        row.Style.Font.FontColor = XLColor.Black;
                        row.Cell("A").Value = dtpCreateDate.Value.Date;
                        row.Cell("A").Style.DateFormat.Format = "yyyy/mm/dd";
                        row.Cell("B").Value = invNo;
                        row.Cell("B").Style.Font.Bold = true;
                        row.Cell("C").Value = inv.CusPO;
                        row.Cell("D").Value = inv.CusItemCode;
                        row.Cell("E").Value = inv.Lieu;
                        row.Cell("F").Value = inv.MauKD;
                        row.Cell("G").Value = inv.Width;
                        row.Cell("H").Value = inv.Qty;
                        row.Cell("H").Style.Font.Bold = true;
                        row.Cell("I").Value = inv.UnitPrice;
                        row.Cell("J").Value = inv.UnitPrice * exchangeRate;
                        row.Cell("K").FormulaA1 = $"=ROUND(H{n}*J{n},0)";
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd1 = sheet.Range($"A{n}:K{n}").Row(1);
                    sheet.Range($"A{n}:G{n}").Merge();
                    sheet.Row(n).Height = 40;
                    rowEnd1.Style.Font.Bold = true;
                    rowEnd1.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd1.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd1.Cell("A").Value = "Total Amount (總計)";
                    rowEnd1.Cell("H").FormulaA1 = $"=SUBTOTAL(109,H{n - lst.Count}:H{n - 1})";
                    rowEnd1.Cell("H").Style.Font.FontColor = XLColor.Red;
                    rowEnd1.Cell("K").FormulaA1 = $"=SUBTOTAL(109,K{n - lst.Count}:K{n - 1})";

                    sheet.Cell($"H{n + 1}").FormulaA1 = $"=H{n}";
                    sheet.Cell($"K{n + 1}").FormulaA1 = $"=ROUND(K{n}*0.08,0)";
                    sheet.Cell($"K{n + 2}").FormulaA1 = $"=SUM(K{n}+K{n + 1})";

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreateTEMLST(string cusName, List<ListGuiDiem2> lstDiem, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("TEM");
                    var rangeTem = sheet.Range("A1:E16");
                    int temHeight = 16;
                    int index = 0;

                    foreach (var diem in lstDiem)
                    {
                        var l1 = diem.Roll.Split('+');

                        foreach (var i1 in l1)
                        {
                            int n = i1.Contains("*")
                                ? int.Parse(i1.Substring(0, i1.IndexOf('*')))
                                : 1;

                            decimal qty = i1.Contains("*")
                                ? decimal.Parse(i1.Substring(i1.IndexOf('*') + 1))
                                : decimal.Parse(i1);

                            for (int i = 0; i < n; i++)
                            {
                                CreateTem(
                                    rangeTem,
                                    sheet.Range($"A{index * temHeight + 1}:E{index * temHeight + temHeight}"),
                                    cusName,
                                    diem.CusPO,
                                    diem.CusItemCode,
                                    diem.Lieu,
                                    diem.MauKD,
                                    diem.Width,
                                    qty,
                                    index + 1,
                                    dtpCreateDate.Value
                                );

                                index++;
                            }
                        }
                    }
                    sheet.PageSetup.PrintAreas.Clear();
                    sheet.PageSetup.PrintAreas.Add($"A1:E{index * temHeight}");

                    for (int i = 1; i < index; i++)
                    {
                        sheet.PageSetup.AddHorizontalPageBreak(i * temHeight);
                    }

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private void CreateTem(IXLRange rangeTem, IXLRange rangeNewTem,string cusName, string po, string code,string material, string color, int width, decimal qty, int index, DateTime deliveryDate)
        {
            if (rangeTem.RangeAddress.ToString() != rangeNewTem.RangeAddress.ToString())
            {
                rangeTem.CopyTo(rangeNewTem);

                for (int i = 0; i < rangeTem.RowCount(); i++)
                {
                    rangeNewTem.Worksheet.Row(rangeNewTem.FirstRow().RowNumber() + i).Height =
                        rangeTem.Worksheet.Row(rangeTem.FirstRow().RowNumber() + i).Height;
                }
            }
            rangeNewTem.Cell("C2").Value = material;
            rangeNewTem.Cell("C3").Value = color;
            rangeNewTem.Cell("C4").Value = width;
            rangeNewTem.Cell("C5").Value = qty;
            rangeNewTem.Cell("C6").Value = po;
            rangeNewTem.Cell("C7").Value = code;
            rangeNewTem.Cell("C8").Value = cusName;
            rangeNewTem.Cell("C9").Value = deliveryDate.ToString("yyyy.MM.dd");
            rangeNewTem.Cell("C10").Value = index;
        }
        private string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13)
                return day + "th";

            switch (day % 10)
            {
                case 1:
                    return day + "st";
                case 2:
                    return day + "nd";
                case 3:
                    return day + "rd";
                default:
                    return day + "th";
            }
        }





        private async void txtInput2_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtInput.Text;
            if (string.IsNullOrEmpty(inputText))
            {
                dgvMain.DataSource = null;
                return;
            }
            //var inputData = inputText.Split('\n').Select(x => x.Split('\t')).Where(x => x.Length >= 19).ToArray();
            var excelData = JHelper.ConvertData.ConvertExcelDataToRows(inputText);
            var inputData = excelData.Data;
            // var inputData = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(x => x.Split('\t')).Where(x => x.Length >= 24).ToArray();
            if (excelData.ColumnCount < 24)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                dgvMain.DataSource = null;
                return;
            }
            var lst = new List<ListGuiDiem2>();
            await Task.Run(() =>
            {
                foreach (var item in inputData)
                {
                    string qtyString = item[19].Replace(",", "");
                    qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                    if (
                        decimal.TryParse(qtyString, out decimal qty)
                    // int.TryParse(item[13], out int id) &&
                    // int.TryParse(item[15], out int width) &&
                    //int.TryParse(item[14], out int weight)
                    )
                    {
                        var pgh = new ListGuiDiem2()
                        {
                            ID = int.TryParse(item[13], out int id) ? id : -1,
                            Lieu = item[0],
                            MauKD = item[1],
                            MauKho = item[2],
                            NgayDatHang = DateTime.TryParse(item[3], out DateTime ngaydathang) ? ngaydathang : DateTime.MinValue,
                            Brand = item[4],
                            CusName = item[5],
                            CusPO = item[6],
                            CusItemCode = item[7],
                            PONhuom = item[8],
                            PONhuomMoi = item[9],
                            ETD = item[10],
                            ETDNote = item[11],
                            CusNote = item[12],
                            //Article = item[24],
                            Width = int.TryParse(item[15], out int width) ? width : -1,
                            UnitPrice = decimal.TryParse(item[14], out decimal unitPrice) ? unitPrice : 0,
                            //Weight = int.TryParse(item[47], out int weight) ? weight : -1,
                            Qty = qty,
                            Roll = item[23].Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\"", "")
                        };
                        lst.Add(pgh);
                    }
                }
            });
            if (lst.Count == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                dgvMain.DataSource = null;
                return;
            }
            dgvMain.DataSource = new SortableBindingList<ListGuiDiem2>(lst);
            lblRows.Text = lst.Count.ToString("#,##0");
            lblTotalQty.Text = lst.Sum(x => x.Qty).ToString("#,##0.000");
            lblTotalAmount.Text = lst.Sum(x => x.AmountVND).ToString("#,##0.000");
            lblGTGT.Text = (lst.Sum(x => x.AmountVND) * 0.08m).ToString("#,##0.000");
            lblGrandTotal.Text = (lst.Sum(x => x.AmountVND) * 1.08m).ToString("#,##0.000");
        }
    }
}
