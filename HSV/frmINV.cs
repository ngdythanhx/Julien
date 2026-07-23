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

namespace HSV
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
            cbCus.DataSource = new string[] { "HSV", "HWK" };
            txtFilePath.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(_iniManager.GetString("Default", "OrderFormHSVPath", "")));
            rb1.CheckedChanged += rb_CheckedChanged;
            rb2.CheckedChanged += rb_CheckedChanged;
            rb1.Checked = true;
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
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            var data = txtInput1.Text.Replace("\r", "").Split('\n');
            foreach (var item in data)
            {
                if (int.TryParse(item.ToString(), out int id))
                {
                    lsbInput.Items.Add(id);
                }
            }
        }
        private async void btnCreateINVPKL_Click(object sender, EventArgs e)
        {
            txtInput1.Enabled = false;
            cbCus.Enabled = false;
            txtFilename.Enabled = false;
            btnCreateINVPKL.Enabled = false;

            FileStream fs_template = null;
            XLWorkbook wb_template = null;

            FileStream fs_Main = null;
            XLWorkbook wb_Main = null;
            try
            {
                string output = txtFilename.Text;
                string filePath = $@"OUTPUT\INV\{txtFilename.Text}.xlsx";
                if (string.IsNullOrEmpty(output) || output.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                {
                    toolTip1.Show("Tên file không hợp lệ!", txtFilename);
                    return;
                }
                if (File.Exists(filePath) && !CheckReadWriteFile(filePath))
                {
                    toolTip1.Show("Có file đã tồn tại đang mở!", txtFilename);
                    return;
                }
                var lstINV = new List<INV>();
                string cusName = cbCus.SelectedValue.ToString();
                await Task.Run(() =>
                {
                    fs_template = new FileStream($@"Default\INVPKLHD_{cusName}.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreateINVPKL);
                    return;
                }
                if (rb1.Checked)
                {
                    var lstID = new List<int>();
                    lstID = lstID.OrderBy(x => x).ToList();
                    foreach (var item in lsbInput.Items)
                    {
                        int id = (int)item;
                        if (!lstID.Contains(id))
                        {
                            lstID.Add(id);
                        }
                    }
                    if (lstID.Count == 0)
                    {
                        toolTip1.Show("Chưa có dữ liệu ID!", lsbInput);
                        return;
                    }

                    IXLWorksheet sheet_Main = null;
                    await Task.Run(() =>
                    {
                        fs_Main = new FileStream(txtFilePath.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        wb_Main = new XLWorkbook(fs_Main);
                        sheet_Main = wb_Main.Worksheet(cusName);
                    });
                    if (sheet_Main == null)
                    {
                        toolTip1.Show("Lỗi load dữ liệu!", btnCreateINVPKL);
                        return;
                    }
                    //var rangeRows = sheet_Main.Range($"A1:BA{lstID.Max()}").Rows().ToList().Where(r => lstID.Contains(r.RangeAddress.LastAddress.RowNumber)).ToList();
                    var rangeRows = sheet_Main.Range($"A1:BA{lstID.Max() + 1}").Rows().ToList();

                    foreach (var id in lstID)
                    {
                        var row = rangeRows[id - 1];
                        var uPriceUSD = row.Cell("R").TryGetValue<decimal>(out decimal unitPriceUSD) ? unitPriceUSD : -1;
                        var inv = new INV()
                        {
                            ID = id,
                            PO = row.Cell("E").GetString(),
                            BuyMonth = row.Cell("AX").GetString(),
                            Article = row.Cell("Y").GetString(),
                            MaterialCode = row.Cell("F").GetString(),
                            DyeLot = row.Cell("G").GetString(),
                            HSCode = "60069000",
                            SaleNo = row.Cell("AT").GetString(),
                            Description = row.Cell("I").GetString(),
                            Color = row.Cell("K").GetString(),
                            Pantone = row.Cell("L").GetString(),
                            Thickness = row.Cell("AW").GetString(),
                            Qty = row.Cell("N").TryGetValue<decimal>(out decimal qty) ? qty : -1,
                            Unit = "YARD",
                            //UnitPriceVND = row.Cell("AV").TryGetValue<int>(out int unitPriceVND) ? unitPriceVND : -1,
                            UnitPriceVND = Convert.ToInt32(uPriceUSD * 25000),
                            //AmountVND=
                            DeliveryDate = "",
                            TemCode = "",
                            UnitPriceUSD = uPriceUSD,
                            SeasonSMTT = row.Cell("AY").GetString()
                        };
                        lstINV.Add(inv);
                    }
                    dgvMain.DataSource = new SortableBindingList<INV>(lstINV);
                    if (dgvMain.RowCount == 0)
                    {
                        toolTip1.Show("Không có dữ liệu!", btnCreateINVPKL);
                        return;
                    }
                }
                else
                {
                    lstINV = (dgvMain.DataSource as BindingList<INV>).ToList();
                }
                var tasks = await Task.WhenAll(
                    CreateINV(cusName, lstINV, wb_template),
                    CreateINVHD(cusName, lstINV, wb_template),
                    CreatePKLHD(cusName, lstINV, wb_template),
                    CreatePKLXE(cusName, lstINV, wb_template),
                    CreatePKLXE_CLAIM(cusName, lstINV, wb_template)
                );
                if (!tasks[0].success)
                {
                    MessageBox.Show(tasks[0].msg);
                    return;
                }
                else if (!tasks[01].success)
                {
                    MessageBox.Show(tasks[1].msg);
                    return;
                }
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
                txtInput1.Enabled = true;
                cbCus.Enabled = true;
                txtFilename.Enabled = true;
                btnCreateINVPKL.Enabled = true;

                wb_template?.Dispose();
                fs_template?.Dispose();

                wb_Main?.Dispose();
                fs_Main?.Dispose();
            }
        }
        private async Task<(bool success, string msg)> CreateINV(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("INV");
                    sheet.Row(19).InsertRowsBelow(lstINV.Count);
                    sheet.Cell("B1").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    sheet.Row(2).InsertRowsBelow(lstINV.Count);
                    var rangeRows = sheet.Range($"A3:U{lstINV.Count + 2}").Rows().ToList();
                    for (int i = 0; i < rangeRows.Count; i++)
                    {
                        var row = rangeRows[i];
                        var inv = lstINV[i];
                        var rowIndex = row.RangeAddress.LastAddress.RowNumber;
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        row.Cell("A").Value = inv.ID;
                        row.Cell("B").Value = inv.PO;
                        row.Cell("C").Value = inv.BuyMonth;
                        row.Cell("D").Value = inv.Article;
                        row.Cell("E").Value = inv.MaterialCode;
                        row.Cell("F").Value = inv.DyeLot.Contains("\n") ? $"\"{inv.DyeLot}\"" : inv.DyeLot;
                        row.Cell("G").Value = "'60069000";
                        row.Cell("H").Value = "'" + inv.SaleNo;
                        row.Cell("I").Value = inv.Description;
                        row.Cell("J").Value = inv.Color;
                        row.Cell("K").Value = inv.Pantone;
                        row.Cell("L").Value = inv.Thickness;
                        row.Cell("M").Value = inv.Qty;
                        row.Cell("M").Style.Font.Bold = true;
                        row.Cell("M").Style.NumberFormat.Format = "#,##0.##############";
                        row.Cell("N").Value = inv.Unit;
                        row.Cell("O").Value = inv.UnitPriceVND;
                        row.Cell("O").Style.NumberFormat.Format = "#,##0";
                        row.Cell("O").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        row.Cell("O").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Cell("O").Style.Alignment.Indent = 1;
                        row.Cell("P").FormulaA1 = $"M{rowIndex}*O{rowIndex}";
                        row.Cell("P").Style.NumberFormat.Format = "#,##0.00";
                        row.Cell("P").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        row.Cell("P").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Cell("P").Style.Alignment.Indent = 1;
                        row.Cell("Q").Value = "";
                        row.Cell("R").Value = "";
                        row.Cell("S").Value = inv.UnitPriceUSD;
                        row.Cell("S").Style.NumberFormat.Format = "#,##0.00";
                        row.Cell("S").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                        row.Cell("S").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Cell("S").Style.Alignment.Indent = 1;
                        row.Cell("T").Value = inv.SeasonSMTT;
                        row.Cell("U").Value = "";
                    }
                    sheet.Row(3 + lstINV.Count).Cell("M").FormulaA1 = $"=SUBTOTAL(109,M3:M{2 + lstINV.Count})";
                    sheet.Row(3 + lstINV.Count).Cell("P").FormulaA1 = $"=SUBTOTAL(109,P3:P{2 + lstINV.Count})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreateINVHD(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("INV HĐ");
                    //sheet.Cell("E3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    var lstINVHD = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && x.UnitPriceUSD > 0).GroupBy(x => x.Description).Select(x => new INVHD()
                    {
                        DESCRIPTION = x.First().Description,
                        COLOR = x.Sum(y => y.Qty).ToString("#,##0.000"),
                        SOLUONG = x.Sum(y => y.Qty),
                        DONGIA = x.First().UnitPriceVND
                    }).OrderBy(x => x.DESCRIPTION).ToList();
                    if (lstINVHD.Count == 0)
                        return (true, "");
                    sheet.Row(19).InsertRowsBelow(lstINVHD.Count);
                    for (int i = 0; i < lstINVHD.Count; i++)
                    {
                        var row = sheet.Range($"A{i + 20}:F{i + 20}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Cell("O").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Cell("O").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Cell("A").Value = (i + 1).ToString();
                        row.Cell("B").Value = cusName == "HSV" ? $"{lstINVHD[i].DESCRIPTION}\nHS CODE : 60069000" : lstINVHD[i].DESCRIPTION;
                        row.Cell("C").Value = lstINVHD[i].COLOR;
                        row.Cell("D").Value = lstINVHD[i].SOLUONG;
                        row.Cell("E").Value = lstINVHD[i].DONGIA;
                        row.Cell("F").FormulaA1 = $"=D{i + 20}*E{i + 20}";
                        row.Cell("F").Style.NumberFormat.Format = "#,##0.00";
                        row.Cell("F").Style.Font.Bold = true;
                    }
                    sheet.Row(20 + lstINVHD.Count).Cell("D").FormulaA1 = $"=SUM(D20:D{19 + lstINVHD.Count})";
                    sheet.Row(20 + lstINVHD.Count).Cell("F").FormulaA1 = $"=SUM(F20:F{19 + lstINVHD.Count})";
                    sheet.Row(21 + lstINVHD.Count).Cell("F").FormulaA1 = $"=F{20 + lstINVHD.Count}*0.08";
                    sheet.Row(22 + lstINVHD.Count).Cell("F").FormulaA1 = $"=SUM(F{20 + lstINVHD.Count}+F{21 + lstINVHD.Count})";
                    var rowEnd = sheet.Range($"C{24 + lstINVHD.Count}:F{24 + lstINVHD.Count}");
                    rowEnd.Merge();
                    sheet.Cell($"C{24 + lstINVHD.Count}").Value = "JULIEN (VN) METAL POWDER CO.,LTD";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreatePKLHD(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PKL HĐ");
                    //sheet.Cell("I3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    var lstPKLs = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && x.UnitPriceUSD > 0).GroupBy(x => new { x.Description, x.Thickness }).OrderBy(g => g.Key.Description).Select(a =>
                        a.Select(y => new PKLHD()
                        {
                            PO = y.PO,
                            BuyMonth = y.BuyMonth,
                            Article = y.Article,
                            MaterialCode = y.MaterialCode,
                            DyeLot = y.DyeLot,
                            //DyeLot = y.DyeLot.Contains("\n") ? $"\"{y.DyeLot}\"" : y.DyeLot,
                            HSCode = y.HSCode,
                            SaleNo = y.SaleNo,
                            Description = y.Description,
                            Color = y.Color,
                            Thickness = y.Thickness,
                            Qty = y.Qty,
                            Roll = y.Qty,
                        }).ToList()
                    ).ToList();
                    var rowCount = lstPKLs.Sum(a => a.Count);
                    int n = 20;
                    var lstIndex = new List<int>();
                    foreach (var lstPKL in lstPKLs)
                    {
                        for (int i = 0; i < lstPKL.Count; i++)
                        {
                            sheet.Row(n++).InsertRowsBelow(1);
                            var row = sheet.Range($"A{n}:O{n}").Row(1);
                            row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            row.Cell("M").Style.NumberFormat.Format = "#,##0";
                            row.Style.Font.Bold = false;
                            var item = lstPKL[i];

                            row.Cell("A").Value = lstPKL[i].PO;
                            row.Cell("B").Value = lstPKL[i].BuyMonth;
                            row.Cell("C").Value = lstPKL[i].Article;
                            row.Cell("D").Value = lstPKL[i].MaterialCode;
                            row.Cell("E").Value = lstPKL[i].DyeLot;
                            //row.Cell("E").Value = lstPKL[i].DyeLot.Contains("\n") ? $"\"{lstPKL[i].DyeLot}\"" : lstPKL[i].DyeLot;
                            row.Cell("F").Value = lstPKL[i].HSCode;
                            row.Cell("G").Value = lstPKL[i].Description;
                            row.Cell("H").Value = lstPKL[i].Color;
                            row.Cell("I").Value = lstPKL[i].Thickness;
                            row.Cell("J").Value = lstPKL[i].Qty;
                            row.Cell("J").Style.NumberFormat.Format = "#,##0.000";
                            row.Cell("K").Value = lstPKL[i].Roll;
                            row.Cell("K").Style.NumberFormat.Format = "#,##0.000";
                            row.Cell("L").Value = lstPKL[i].Unit;
                            row.Cell("M").Value = 1;
                            row.Cell("N").FormulaA1 = $"=J{n}*0.24";
                            row.Cell("O").FormulaA1 = $"=N{n}+0.2";
                            if (i == lstPKL.Count - 1)
                            {
                                sheet.Row(n++).InsertRowsBelow(1);
                                row = sheet.Range($"A{n}:O{n}").Row(1);
                                row.Style.Font.Bold = true;
                                sheet.Range($"A{n}:I{n}").Merge();
                                row.Cell("A").Value = "TOTAL:";
                                row.Cell("J").Value = lstPKL.Sum(x => x.Qty);
                                row.Cell("L").Value = "YARD";
                                row.Cell("M").Value = lstPKL.Count;
                                row.Cell("M").Style.NumberFormat.Format = "#,##0\" Roll\"";
                                row.Cell("N").FormulaA1 = $"=SUM(N{n - lstPKL.Count}:N{n - 1})";
                                row.Cell("O").FormulaA1 = $"=SUM(O{n - lstPKL.Count}:O{n - 1})";
                                lstIndex.Add(n);
                            }

                        }
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd = sheet.Range($"A{n}:O{n}").Row(1);
                    sheet.Range($"A{n}:I{n}").Merge();
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL:";
                    rowEnd.Cell("J").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "J" + x))})";
                    rowEnd.Cell("L").Value = "YARD";
                    rowEnd.Cell("M").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "M" + x))})";
                    rowEnd.Cell("N").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "N" + x))})";
                    rowEnd.Cell("O").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "O" + x))})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreatePKLXE(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PKL KHAI THEO XE");
                    //var lst = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && x.UnitPriceUSD > 0).OrderBy(inv => inv.MaterialCode).ThenBy(inv => inv.Article).ThenBy(inv => inv.PO).ToList();
                    var lst = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && x.UnitPriceUSD > 0).ToList();
                    if (lst.Count == 0)
                        return (true, "");
                    int n = 20;
                    for (int i = 0; i < lst.Count; i++)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        var row = sheet.Range($"A{n}:Q{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Style.Font.Bold = false;
                        var item = lst[i];

                        row.Cell("A").Value = item.PO;
                        row.Cell("B").Value = item.BuyMonth;
                        row.Cell("C").Value = item.Article;
                        row.Cell("D").Value = item.MaterialCode;
                        row.Cell("E").Value = item.DyeLot;
                        //row.Cell("E").Value = item.DyeLot.Contains("\n") ? $"\"{item.DyeLot}\"" : item.DyeLot;
                        row.Cell("F").Value = item.HSCode;
                        row.Cell("G").Value = item.Description;
                        row.Cell("H").Value = item.Color;
                        row.Cell("I").Value = item.Thickness;
                        row.Cell("J").Value = item.Qty;
                        row.Cell("J").Style.NumberFormat.Format = "#,##0.000";
                        row.Cell("K").Value = "";
                        row.Cell("L").Value = "YARD";
                        row.Cell("M").Value = "";
                        row.Cell("N").FormulaA1 = $"=J{n}*0.24";
                        row.Cell("O").FormulaA1 = $"=N{n}+0.2";
                        row.Cell("P").Value = item.ID;
                        row.Cell("Q").Value = item.Pantone;
                    }
                    n++;
                    var rowEnd = sheet.Range($"A{n}:P{n}").Row(1);
                    rowEnd.Cell("J").FormulaA1 = $"=SUBTOTAL(109,J21:J{n - 1})";
                    rowEnd.Cell("M").FormulaA1 = $"=SUBTOTAL(109,M21:M{n - 1})";
                    rowEnd.Cell("N").FormulaA1 = $"=SUBTOTAL(109,N21:N{n - 1})";
                    rowEnd.Cell("O").FormulaA1 = $"=SUBTOTAL(109,O21:O{n - 1})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreatePKLXE_CLAIM(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PKL KHAI THEO XE - CLAIM");
                    //var lst = lstINV.Where(x => x.PO.ToUpper().Contains("CLAIM") && x.UnitPriceUSD <= 0).OrderBy(inv => inv.MaterialCode).ThenBy(inv => inv.Article).ThenBy(inv => inv.PO).ToList();
                    var lst = lstINV.Where(x => x.PO.ToUpper().Contains("CLAIM") && x.UnitPriceUSD <= 0).ToList();
                    if (lst.Count == 0)
                        return (true, "");
                    int n = 20;
                    for (int i = 0; i < lst.Count; i++)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        var row = sheet.Range($"A{n}:Q{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Style.Font.Bold = false;
                        var item = lst[i];

                        row.Cell("A").Value = item.PO;
                        row.Cell("B").Value = item.BuyMonth;
                        row.Cell("C").Value = item.Article;
                        row.Cell("D").Value = item.MaterialCode;
                        row.Cell("E").Value = item.DyeLot;
                        //row.Cell("E").Value = item.DyeLot.Contains("\n") ? $"\"{item.DyeLot}\"" : item.DyeLot;
                        row.Cell("F").Value = item.HSCode;
                        row.Cell("G").Value = item.Description;
                        row.Cell("H").Value = item.Color;
                        row.Cell("I").Value = item.Thickness;
                        row.Cell("J").Value = item.Qty;
                        row.Cell("J").Style.NumberFormat.Format = "#,##0.000";
                        row.Cell("K").Value = "";
                        row.Cell("L").Value = "YARD";
                        row.Cell("M").Value = "";
                        row.Cell("N").FormulaA1 = $"=J{n}*0.24";
                        row.Cell("O").FormulaA1 = $"=N{n}+0.2";
                        row.Cell("P").Value = item.ID;
                        row.Cell("Q").Value = item.Pantone;
                    }
                    n++;
                    var rowEnd = sheet.Range($"A{n}:P{n}").Row(1);
                    rowEnd.Cell("J").FormulaA1 = $"=SUBTOTAL(109,J21:J{n - 1})";
                    rowEnd.Cell("M").FormulaA1 = $"=SUBTOTAL(109,M21:M{n - 1})";
                    rowEnd.Cell("N").FormulaA1 = $"=SUBTOTAL(109,N21:N{n - 1})";
                    rowEnd.Cell("O").FormulaA1 = $"=SUBTOTAL(109,O21:O{n - 1})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
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

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1.Checked)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
            }
        }

        private void txtInput1_TextChanged(object sender, EventArgs e)
        {
            var data = txtInput1.Text.Replace("\r", "").Split('\n');
            lsbInput.Items.Clear();
            dgvMain.DataSource = null;
            foreach (var item in data)
            {
                if (int.TryParse(item.ToString(), out int id))
                {
                    lsbInput.Items.Add(id);
                }
            }
        }

        private void txtInput2_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtInput2.Text;
            var inputData = inputText.Split('\n').Select(x => x.Split('\t')).Where(x => x.Length >= 19).ToArray();
            if (inputData.Length == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput2);
                return;
            }
            var lstINV = new List<INV>();
            foreach (var item in inputData)
            {
                if (
                    decimal.TryParse(item[12].Last() == '.' ? item[12].Substring(0, item[12].Length - 1) : item[12], out decimal qty) &&
                    int.TryParse(item[14].Replace(",", ""), out int unitPriceVND) &&
                    decimal.TryParse(item[18].Last() == '.' ? item[18].Substring(0, item[18].Length - 1) : item[18], out decimal unitPriceUSD)
                )
                {
                    var inv = new INV()
                    {
                        ID = int.TryParse(item[0], out int id) ? id : 0,
                        PO = item[01],
                        BuyMonth = item[2],
                        Article = item[3],
                        MaterialCode = item[4],
                        DyeLot = item[5],
                        HSCode = item[6],
                        SaleNo = item[7],
                        Description = item[8],
                        Color = item[9],
                        Pantone = item[10],
                        Thickness = item[11],
                        Qty = qty,
                        Unit = item[13],
                        UnitPriceVND = unitPriceVND,
                        //AmountVND = item[14],
                        DeliveryDate = item[16],
                        TemCode = item[17],
                        UnitPriceUSD = unitPriceUSD,
                    };
                    lstINV.Add(inv);
                }
            }
            if (lstINV.Count == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput2);
                return;
            }
            dgvMain.DataSource = new SortableBindingList<INV>(lstINV);
            lblRows.Text = lstINV.Count.ToString("#,##0");
            lblTotalQty.Text = lstINV.Sum(x => x.Qty).ToString("#,##0.000");
            lblTotalAmount.Text = lstINV.Sum(x => x.AmountVND).ToString("#,##0.000");
            lblGTGT.Text = (lstINV.Sum(x => x.AmountVND) * 0.08m).ToString("#,##0.000");
            lblGrandTotal.Text = (lstINV.Sum(x => x.AmountVND) * 1.08m).ToString("#,##0.000");
        }
    }
}
