using Julian.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
namespace HSV
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            string inputText = textBox.Text;
            var inputData = inputText.Split('\n').Select(x => x.Split('\t')).Where(x => x.Length >= 18).ToArray();
            if (inputData.Length == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                return;
            }
            var lstINV = new List<INV>();
            foreach (var item in inputData)
            {
                if (
                    decimal.TryParse(item[11].Last() == '.' ? item[11].Substring(0, item[11].Length - 1) : item[11], out decimal qty) &&
                    int.TryParse(item[13].Replace(",", ""), out int unitPriceVND)
                )
                {
                    var inv = new INV()
                    {
                        PO = item[0],
                        BuyMonth = item[1],
                        Article = item[2],
                        MaterialCode = item[3],
                        DyeLot = item[4],
                        HSCode = item[5],
                        SaleNo = item[6],
                        Description = item[7],
                        Color = item[8],
                        Pantone = item[9],
                        Thickness = item[10],
                        Qty = qty,
                        Unit = item[12],
                        UnitPriceVND = unitPriceVND,
                        //AmountVND = item[14],
                        DeliveryDate = item[15],
                        TemCode = item[16],
                        UnitPriceUSD = item[17],
                    };
                    lstINV.Add(inv);
                }
            }
            if (lstINV.Count == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                return;
            }
            dgvMain.DataSource = new SortableBindingList<INV>(lstINV);
            lblRows.Text = lstINV.Count.ToString("#,##0");
            lblTotalQty.Text = lstINV.Sum(x => x.Qty).ToString("#,##0.000");
            lblTotalAmount.Text = lstINV.Sum(x => x.AmountVND).ToString("#,##0.000");
            lblGTGT.Text = (lstINV.Sum(x => x.AmountVND) * 0.08m).ToString("#,##0.000");
            lblGrandTotal.Text = (lstINV.Sum(x => x.AmountVND) * 1.08m).ToString("#,##0.000");
        }

        private async void btnCreateINVPKL_Click(object sender, EventArgs e)
        {
            txtInput.Enabled = false;
            cbCus.Enabled = false;
            txtFilename.Enabled = false;
            btnCreateINVPKL.Enabled = false;
            try
            {
                var dt = dgvMain.DataSource as BindingList<INV>;
                if (dt == null)
                {
                    toolTip1.Show("Không có dữ liệu!", btnCreateINVPKL);
                    return;
                }
                var lstINV = dt.ToList();
                using var fs = new FileStream($@"Default\INVPKLHD_{cbCus.SelectedValue.ToString()}.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var wb = new XLWorkbook(fs);
                string cusName = cbCus.SelectedValue.ToString();
                var tasks = await Task.WhenAll(CreateINV(cusName, lstINV, wb), CreatePKL(cusName, lstINV, wb));
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
                string filePath = $@"OUTPUT\INV\{txtFilename.Text}.xlsx";
                await Task.Run(() => wb.SaveAs(filePath));
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
                btnCreateINVPKL.Enabled = true;
            }
        }
        private async Task<(bool success, string msg)> CreateINV(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("INV HĐ");
                    sheet.Cell("E3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    var lstINVHD = lstINV.GroupBy(x => x.Description).Select(x => new INVHD()
                    {
                        DESCRIPTION = x.First().Description,
                        COLOR = x.Sum(y => y.Qty).ToString("#,##0.000"),
                        SOLUONG = x.Sum(y => y.Qty),
                        DONGIA = x.First().UnitPriceVND
                    }).ToList();
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
        private async Task<(bool success, string msg)> CreatePKL(string cusName, List<INV> lstINV, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PKL HĐ");
                    sheet.Cell("I3").Value = $"INV NO.: {dtpCreateDate.Value.ToString("yyyyMMdd")}{cusName}   Date : {dtpCreateDate.Value:MMMM} {GetDaySuffix(dtpCreateDate.Value.Day)}, {dtpCreateDate.Value:yyyy}";
                    var lstPKLs = lstINV.GroupBy(x => new { x.Description, x.Thickness }).OrderBy(g => g.Key.Description).Select(a =>
                        a.Select(y => new PKLHD()
                        {
                            PO = y.PO,
                            BuyMonth = y.BuyMonth,
                            Article = y.Article,
                            MaterialCode = y.MaterialCode,
                            DyeLot = y.DyeLot,
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
                            row.Cell("F").Value = lstPKL[i].HSCode;
                            row.Cell("G").Value = lstPKL[i].Description;
                            row.Cell("H").Value = lstPKL[i].Color;
                            row.Cell("I").Value = lstPKL[i].Thickness;
                            row.Cell("J").Value = lstPKL[i].Qty;
                            row.Cell("J").Style.NumberFormat.Format = "#,##0.000";
                            row.Cell("K").Value = lstPKL[i].Roll;
                            row.Cell("J").Style.NumberFormat.Format = "#,##0.000";
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
                                row.Cell("N").FormulaA1 = $"=SUM(N{n - 15}:N{n - 1})";
                                row.Cell("O").FormulaA1 = $"=SUM(O{n - 15}:O{n - 1})";
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
        private void frmMain_Load(object sender, EventArgs e)
        {
            cbCus.DataSource = new string[] { "HSV", "HWK" };
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
    }
}
