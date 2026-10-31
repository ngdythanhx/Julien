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

namespace NR
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
            cbCus.DataSource = new string[] { "NR" };
            txtFilePath.Text = _iniManager.GetString("Default", "OrderFormHSVPath", "");
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
                    fs_template = new FileStream($@"Default\INVPKL_{cusName}.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreateINVPKL);
                    return;
                }
                var lstINV = (dgvMain.DataSource as BindingList<INV>).ToList();
                var task1 = await CreateINV(cusName, lstINV, wb_template);
                if (!task1.success)
                {
                    MessageBox.Show(task1.msg);
                    return;
                }
                var task2 = await CreatePKL(cusName, lstINV, wb_template);
                if (!task2.success)
                {
                    MessageBox.Show(task2.msg);
                    return;
                }
                var task3 = await CreateDEBIT(cusName, lstINV, wb_template);
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
                    sheet.Cell("F3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    int n = 24;
                    int descriptionIndex = 1;
                    var lstG = lstINV.GroupBy(x => x.Description).ToList();
                    var lstIndex = new List<int>();
                    foreach (var lst in lstG)
                    {
                        for (int i = 0; i <= lst.Count(); i++)
                        {
                            sheet.Row(n++).InsertRowsBelow(1);
                            sheet.Row(n).Height = 50;
                            var row = sheet.Range($"A{n}:L{n}").Row(1);
                            row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            if (i != lst.Count())
                            {
                                var inv = lst.ToList()[i];
                                row.Style.Font.Bold = false;

                                row.Cell("A").Value = descriptionIndex;
                                row.Cell("B").Value = inv.PO;
                                row.Cell("C").Value = inv.MaterialCode;
                                row.Cell("D").Value = inv.Description;
                                row.Cell("E").Value = inv.Color;
                                row.Cell("F").Value = inv.Width;
                                row.Cell("G").Value = inv.Weight;
                                row.Cell("H").Value = inv.Qty;
                                row.Cell("I").Value = inv.Unit;
                                row.Cell("J").Value = inv.UnitPrice;
                                row.Cell("K").Value = inv.Amount;
                                row.Cell("L").Value = inv.DeliveryDate;
                            }
                            else
                            {
                                row.Style.Font.Bold = true;
                                sheet.Range($"A{n}:G{n}").Merge();
                                sheet.Row(n).Height = 50;
                                row.Cell("A").Value = lst.First().Description;
                                row.Cell("H").FormulaA1 = $"=SUM(H{n - lst.Count()}:H{n - 1})";
                                row.Cell("K").FormulaA1 = $"=SUM(K{n - lst.Count()}:K{n - 1})";
                                descriptionIndex++;
                                lstIndex.Add(n);
                            }

                        }
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd = sheet.Range($"A{n}:K{n}").Row(1);
                    sheet.Range($"A{n}:G{n}").Merge();
                    sheet.Row(n).Height = 60;
                    rowEnd.Style.Font.FontSize = rowEnd.Style.Font.FontSize * 1.4;
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL:";
                    rowEnd.Cell("H").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "H" + x))})";
                    rowEnd.Cell("K").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "K" + x))})";

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreatePKL(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PKL");
                    //sheet.Cell("F3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    int n = 24;
                    int descriptionIndex = 1;
                    var lstG = lstINV.GroupBy(x => x.Description).ToList();
                    var lstIndex = new List<int>();
                    foreach (var lst in lstG)
                    {
                        for (int i = 0; i <= lst.Count(); i++)
                        {
                            sheet.Row(n++).InsertRowsBelow(1);
                            sheet.Row(n).Height = 50;
                            var row = sheet.Range($"A{n}:M{n}").Row(1);
                            row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            if (i != lst.Count())
                            {
                                var inv = lst.ToList()[i];
                                row.Style.Font.Bold = false;

                                row.Cell("A").Value = descriptionIndex;
                                row.Cell("B").Value = inv.PO;
                                row.Cell("C").Value = inv.MaterialCode;
                                row.Cell("D").Value = inv.Description;
                                row.Cell("E").Value = inv.Color;
                                row.Cell("F").Value = inv.Width;
                                row.Cell("G").Value = inv.Weight;
                                row.Cell("H").Value = inv.Qty;
                                row.Cell("H").Style.Font.Bold = true;
                                row.Cell("I").Value = inv.Unit;
                                row.Cell("J").Value = Socay((int)Math.Ceiling(inv.Qty));
                                row.Cell("J").Style.Font.Bold = true;
                                row.Cell("K").Value = Socuon((int)Math.Ceiling(inv.Qty));
                                row.Cell("L").FormulaA1 = $"=H{n}*G{n}/1000";
                                row.Cell("M").FormulaA1 = $"=L{n}+0.25*K{n}";
                            }
                            else
                            {
                                row.Style.Font.Bold = true;
                                sheet.Range($"A{n}:G{n}").Merge();
                                sheet.Row(n).Height = 50;
                                row.Cell("A").Value = lst.First().Description;
                                row.Cell("H").FormulaA1 = $"=SUM(H{n - lst.Count()}:H{n - 1})";
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
                    sheet.Row(n).Height = 50;
                    rowEnd.Style.Font.FontSize = rowEnd.Style.Font.FontSize * 1.4;
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL:";
                    rowEnd.Cell("H").FormulaA1 = $"=SUM({string.Join("+", lstIndex.Select(x => "H" + x))})";
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
        private async Task<(bool success, string msg)> CreateDEBIT(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("DEBIT");
                    //sheet.Cell("F3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    int n = 6;
                    foreach (var inv in lstINV)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        sheet.Row(n).Height = 50;
                        var row = sheet.Range($"A{n}:L{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        row.Style.Font.Bold = false;
                        row.Style.Font.FontColor = XLColor.Black;
                        row.Cell("A").Value = inv.DeliveryDate.ToString("yyyy-MM-dd");
                        row.Cell("B").Value = "";
                        row.Cell("B").Style.Font.Bold = true;
                        row.Cell("C").Value = inv.PO;
                        row.Cell("D").Value = inv.MaterialCode;
                        row.Cell("E").Value = inv.Description;
                        row.Cell("F").Value = inv.Color;
                        row.Cell("G").Value = inv.Width;
                        row.Cell("H").Value = inv.Weight;
                        row.Cell("I").Value = inv.Qty;
                        row.Cell("J").Value = inv.UnitPrice;
                        row.Cell("J").Style.Font.Bold = true;
                        row.Cell("K").FormulaA1 = $"=I{n}*J{n}";
                        row.Cell("K").Style.NumberFormat.Format = @"_(\$* #,##0.00_);_(\$* (#,##0.00);_(\$* -??_);_(@_)";
                        row.Cell("L").Value = inv.ID;
                    }
                    sheet.Row(n++).InsertRowsBelow(1);
                    var rowEnd = sheet.Range($"A{n}:K{n}").Row(1);
                    sheet.Range($"A{n}:G{n}").Merge();
                    sheet.Row(n).Height = 40;
                    rowEnd.Style.Font.Bold = true;
                    rowEnd.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    rowEnd.Cell("A").Value = "TOTAL:";
                    rowEnd.Cell("I").FormulaA1 = $"=SUM(I{n - lstINV.Count()}:I{n - 1})";
                    rowEnd.Cell("K").FormulaA1 = $"=SUM(K{n - lstINV.Count()}:K{n - 1})";

                    sheet.Cell("A4").Value = $"Debit Note - SPARKLIN (NEWRICH) ({dtpCreateDate.Value.ToString("yyyy/MM")})";
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
            string[] lines = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length == 0)
                return;
            var a = lines.Select(l => l.Split('\t'));
            var data = lines.Select(l => l.Split('\t')).Where(x => x.Length >= 52).ToList();
            if (data.Count == 0)
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
                    decimal.TryParse(unitPriceString, out decimal unitPrice) &&
                    int.TryParse(item[51], out int id)
                )
                {
                    var inv = new INV()
                    {
                        ID = id,
                        PO = item[4],
                        MaterialCode = item[5],
                        Description = item[8].Replace(" ", ""),
                        Color = item[11].Replace(" ", ""),
                        Width = int.TryParse(item[46], out int width) ? width : -1,
                        Weight = int.TryParse(item[46], out int wei) ? wei : -1,
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
