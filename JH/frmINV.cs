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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JH
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
            cbCus.DataSource = new string[] { "JH" };
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
            txtInput2.Enabled = false;
            cbCus.Enabled = false;
            txtFilename.Enabled = false;
            btnCreateINVPKL.Enabled = false;

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
                    toolTip1.Show("Lỗi load template!", btnCreateINVPKL);
                    return;
                }
                var lstINV = (dgvMain.DataSource as BindingList<INV>).ToList();
                var task1 = await CreateINVHD(cusName, exchangRate, lstINV, wb_template);
                if (!task1.success)
                {
                    MessageBox.Show(task1.msg);
                    return;
                }
                var task2 = await CreatePKLHD(cusName, lstINV, wb_template);
                if (!task2.success)
                {
                    MessageBox.Show(task2.msg);
                    return;
                }
                var task3 = await CreateDEBIT(cusName, exchangRate, lstINV, wb_template);
                if (!task3.success)
                {
                    MessageBox.Show(task3.msg);
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
                txtInput2.Enabled = true;
                cbCus.Enabled = true;
                txtFilename.Enabled = true;
                btnCreateINVPKL.Enabled = true;

                wb_template?.Dispose();
                fs_template?.Dispose();

                wb_Main?.Dispose();
                fs_Main?.Dispose();
            }
        }
        private async Task<(bool success, string msg)> CreateINVHD(string cusName, int exchangeRate, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("INV HĐ");
                    sheet.Cell("D3").Value = $"INV{dtpCreateDate.Value.ToString("yyyyMMdd")}JH-SL";
                    sheet.Cell("F3").Value = dtpCreateDate.Value.ToString("yyyy-MM-dd");
                    var lstINVHD = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && !x.PO.ToUpper().Contains("FOC") && !x.PO.ToUpper().Contains("BU") && !x.PO.ToUpper().Contains("BÙ") && x.UnitPrice > 0).GroupBy(x => new { x.Description, x.UnitPrice }).Select(x => new INVHD()
                    {
                        DESCRIPTION = x.First().Description,
                        COLOR = x.Sum(y => y.Qty).ToString("#,##0.000"),
                        SOLUONG = x.Sum(y => y.Qty),
                        DONGIA = x.First().UnitPrice
                    }).OrderBy(x => x.DESCRIPTION).ToList();
                    if (lstINVHD.Count == 0)
                        return (true, "");
                    sheet.Row(19).InsertRowsBelow(lstINVHD.Count);
                    for (int i = 0; i < lstINVHD.Count; i++)
                    {
                        var row = sheet.Range($"A{i + 20}:F{i + 20}").Row(1);
                        sheet.Row(i + 20).Height = 50;
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Cell("A").Value = (i + 1).ToString();
                        row.Cell("B").Value = $"{lstINVHD[i].DESCRIPTION}\nHS CODE: 60069000";
                        //row.Cell("C").Value = lstINVHD[i].COLOR;
                        row.Cell("D").Value = lstINVHD[i].SOLUONG;
                        row.Cell("E").FormulaA1 = $"=ROUND( {lstINVHD[i].DONGIA}*{exchangeRate},0)";
                        row.Cell("F").FormulaA1 = $"=ROUND(D{i + 20}*E{i + 20},0)";
                        row.Cell("F").Style.NumberFormat.Format = "#,##0.00";
                        row.Cell("F").Style.Font.Bold = true;
                    }
                    sheet.Row(20 + lstINVHD.Count).Cell("D").FormulaA1 = $"=SUM(D20:D{19 + lstINVHD.Count})";
                    sheet.Row(20 + lstINVHD.Count).Cell("F").FormulaA1 = $"=SUM(F20:F{19 + lstINVHD.Count})";
                    sheet.Row(21 + lstINVHD.Count).Cell("F").FormulaA1 = $"=ROUND(F{20 + lstINVHD.Count}*0.08,0)";
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
                    var lstPKLs = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && !x.PO.ToUpper().Contains("FOC") && !x.PO.ToUpper().Contains("BU") && !x.PO.ToUpper().Contains("BÙ") && x.UnitPrice > 0).GroupBy(x => new { x.Description, x.Width, x.UnitPrice }).OrderBy(g => g.Key.Description).Select(a =>
                        a.Select(y => new PKLHD()
                        {
                            PO = y.PO,
                            MaterialCode = y.MaterialCode,
                            Description = y.Description,
                            Color = y.Color,
                            Thickness = y.Width,
                            Qty = y.Qty,
                            Roll = y.Qty,
                        }).ToList()
                    ).ToList();
                    var rowCount = lstPKLs.Sum(a => a.Count);
                    int n = 20;
                    int itemIndex = 1;
                    var lstIndex = new List<int>();
                    foreach (var lstPKL in lstPKLs)
                    {
                        for (int i = 0; i < lstPKL.Count; i++)
                        {
                            sheet.Row(n++).InsertRowsBelow(1);
                            sheet.Row(n).Height = 40;
                            var row = sheet.Range($"A{n}:K{n}").Row(1);
                            row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            row.Style.Font.FontColor = XLColor.Black;
                            row.Style.Font.Bold = false;
                            var item = lstPKL[i];

                            row.Cell("A").Value = itemIndex;
                            row.Cell("B").Value = lstPKL[i].PO;
                            row.Cell("C").Value = lstPKL[i].Description;
                            row.Cell("D").Value = lstPKL[i].Color;
                            row.Cell("E").Value = lstPKL[i].Thickness;
                            //row.Cell("E").Value = lstPKL[i].DyeLot.Contains("\n") ? $"\"{lstPKL[i].DyeLot}\"" : lstPKL[i].DyeLot;
                            row.Cell("F").Value = lstPKL[i].Qty;
                            row.Cell("G").Value = "YARD";
                            row.Cell("H").Value = 1;
                            row.Cell("I").Value = 1;
                            row.Cell("J").FormulaA1 = $"=F{n}*0.2";
                            row.Cell("K").FormulaA1 = $"=J{n}+0.25";
                            if (i == lstPKL.Count - 1)
                            {
                                sheet.Row(n++).InsertRowsBelow(1);
                                sheet.Row(n).Height = 40;
                                row = sheet.Range($"A{n}:K{n}").Row(1);
                                row.Style.Font.Bold = true;
                                row.Style.Font.FontColor = XLColor.Red;
                                sheet.Range($"A{n}:E{n}").Merge();
                                row.Cell("A").Value = lstPKL[i].Description;
                                row.Cell("F").FormulaA1 = $"=SUM(F{n - lstPKL.Count}:F{n - 1})";
                                row.Cell("G").Value = "YARD";
                                row.Cell("H").FormulaA1 = $"=SUM(H{n - lstPKL.Count}:H{n - 1})";
                                row.Cell("I").FormulaA1 = $"=SUM(I{n - lstPKL.Count}:I{n - 1})";
                                row.Cell("J").FormulaA1 = $"=SUM(J{n - lstPKL.Count}:J{n - 1})";
                                row.Cell("K").FormulaA1 = $"=SUM(K{n - lstPKL.Count}:K{n - 1})";
                                lstIndex.Add(n);
                            }
                        }
                        itemIndex++;
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    sheet.Row(n).Height = 50;
                    var rowEnd = sheet.Range($"A{n}:K{n}").Row(1);
                    rowEnd.Style.Font.FontColor = XLColor.Black;
                    rowEnd.Style.Font.FontSize = rowEnd.Style.Font.FontSize * 1.2;
                    sheet.Range($"A{n}:E{n}").Merge();
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL:";
                    rowEnd.Cell("F").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "F" + x))})";
                    rowEnd.Cell("G").Value = "YARD";
                    rowEnd.Cell("H").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "H" + x))})";
                    rowEnd.Cell("I").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "I" + x))})";
                    rowEnd.Cell("J").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "J" + x))})";
                    rowEnd.Cell("K").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "K" + x))})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreateDEBIT(string cusName, int exchangeRate, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("DEBIT NOTE");
                    var lstNew = lstINV.Where(x => !x.PO.ToUpper().Contains("CLAIM") && !x.PO.ToUpper().Contains("FOC") && !x.PO.ToUpper().Contains("BU") && !x.PO.ToUpper().Contains("BÙ") && x.UnitPrice > 0).ToList();
                    //sheet.Cell("F3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    int n = 6;
                    int i = 1;
                    foreach (var inv in lstNew)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        sheet.Row(n).Height = 24;
                        var row = sheet.Range($"A{n}:J{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        row.Style.Font.Bold = false;
                        row.Style.Font.FontColor = XLColor.Black;
                        row.Cell("A").Value = i++;
                        row.Cell("B").Value = inv.DeliveryDate;
                        row.Cell("C").Value = "JH-" + inv.DeliveryDate.ToString("yyyy.MM.dd");
                        row.Cell("D").Value = inv.PO;
                        row.Cell("E").Value = inv.Description;
                        row.Cell("F").Value = inv.Color;
                        row.Cell("G").Value = inv.Width;
                        row.Cell("H").Value = inv.Qty;
                        row.Cell("H").Style.Font.Bold = true;
                        row.Cell("I").FormulaA1 = $"=Round({inv.UnitPrice}*{exchangeRate},0)";
                        row.Cell("J").FormulaA1 = $"=Round(I{n}*H{n},0)";
                        //row.Cell("J").Style.NumberFormat.Format = @"_(\$* #,##0.00_);_(\$* (#,##0.00);_(\$* -??_);_(@_)";
                        row.Cell("K").Value = inv.ID;
                    }
                    sheet.Row(n + 1).Cell("H").FormulaA1 = $"=SUBTOTAL(109,H{n - lstNew.Count + 1}:H{n})";
                    sheet.Row(n + 1).Cell("J").FormulaA1 = $"=SUBTOTAL(109,J{n - lstNew.Count + 1}:J{n})";
                    sheet.Row(n + 2).Cell("J").FormulaA1 = $"=ROUND(J{n + 1}*0.08,0)";
                    sheet.Row(n + 3).Cell("J").FormulaA1 = $"=J{n + 1}+J{n + 2}";
                    /* sheet.Row(n++).InsertRowsBelow(1);
                     var rowEnd = sheet.Range($"A{n}:K{n}").Row(1);
                     sheet.Range($"A{n}:G{n}").Merge();
                     sheet.Row(n).Height = 40;
                     rowEnd.Style.Font.Bold = true;
                     rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                     rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                     rowEnd.Cell("A").Value = "TOTAL:";
                     rowEnd.Cell("I").FormulaA1 = $"=SUM(I{n - lstINV.Count()}:I{n - 1})";
                     rowEnd.Cell("K").FormulaA1 = $"=SUM(K{n - lstINV.Count()}:K{n - 1})";
                    */
                    sheet.Cell("A5").Value = $"Debit Note-{cusName}({dtpCreateDate.Value.ToString("yyyy")}年{dtpCreateDate.Value.ToString("MM")}月份請款明細)";
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

        private void txtInput2_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtInput2.Text;
            string[] lines = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length == 0)
                return;
            var a = JHelper.ConvertData.ConvertExcelDataToRows(inputText);
            var data = a.Data;
            if (data.Count == 0 || a.ColumnCount < 52)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput2);
                return;
            }
            var lstINV = new List<INV>();
            foreach (var item in data)
            {
                string qtyString = item[13].Replace(",", "");
                qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                string unitPriceString = item[17].Replace(",", "");
                unitPriceString = unitPriceString.LastOrDefault() == '.' ? unitPriceString.Substring(0, unitPriceString.Length - 1) : unitPriceString;
                if (
                    decimal.TryParse(qtyString, out decimal qty) &&
                    decimal.TryParse(unitPriceString, out decimal unitPrice)

                )
                {
                    var inv = new INV()
                    {
                        ID = int.TryParse(item[51], out int id) ? id : -1,
                        PO = item[4],
                        MaterialCode = item[5],
                        Description = item[8].Replace(" ", ""),
                        Color = item[11].Replace(" ", ""),
                        Width = int.TryParse(item[46], out int width) ? width : -1,
                        Weight = int.TryParse(item[47], out int wei) ? wei : -1,
                        Qty = qty,
                        Unit = "YD",
                        UnitPrice = unitPrice,
                        DeliveryDate = DateTime.TryParseExact(item[19], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var rDate) ? rDate : DateTime.MinValue,
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
            lblTotalAmount.Text = lstINV.Sum(x => x.Amount).ToString("#,##0.000");
            //lblGTGT.Text = (lstINV.Sum(x => x.AmountVND) * 0.08m).ToString("#,##0.000");
            //lblGrandTotal.Text = (lstINV.Sum(x => x.AmountVND) * 1.08m).ToString("#,##0.000");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private string Socay(int qty)
        {
            int number = qty;

            int x = number / 100;
            int y = number % 100;

            if (x == 0)
            {
                return y == 0 ? "" : y.ToString();
            }

            string result = x == 1
                ? (y == 0 ? "100" : $"100+{y}")
                : (y == 0 ? $"{x}*100" : $"{x}*100+{y}");

            return result;
        }
        private int Socuon(int qty)
        {
            return (qty / 100) + (qty % 100 > 0 ? 1 : 0);
        }
    }
}
