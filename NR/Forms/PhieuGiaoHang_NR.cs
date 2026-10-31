using ClosedXML.Excel;
using Julian.Helper;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NR.Forms
{
    public partial class PhieuGiaoHang_NR : Form
    {
        string _custName = "NR";
        public PhieuGiaoHang_NR()
        {
            InitializeComponent();
        }
        private async void txtInput_TextChanged(object sender, EventArgs e)
        {
            txtInput.Enabled = false;
            try
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
                var lst = new List<PhieuGiaoHang>();
                await Task.Run(() =>
                {
                    foreach (var item in inputData)
                    {
                        string qtyString = item[19].Replace(",", "");
                        qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                        if (
                            decimal.TryParse(qtyString, out decimal qty) &&
                            int.TryParse(item[13], out int id) &&
                            int.TryParse(item[15], out int width) &&
                            int.TryParse(item[14], out int weight)
                        )
                        {
                            var pgh = new PhieuGiaoHang()
                            {
                                ID = id,
                                PO = item[6],
                                MaterialCode = item[7],
                                Description = item[0],
                                Color = item[1],
                                Width = width,
                                Weight = weight,
                                Qty = qty,
                                Roll = item[23]
                            };
                            lst.Add(pgh);
                        }
                    }
                });
                if (lst.Count == 0)
                {
                    toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                    return;
                }
                /*lst = lst.GroupBy(x => new { x.POOLD, x.Material, x.Color, x.Follow }).OrderBy(x => x.Key.Material).ThenBy(x => x.Key.Color).Select(x =>
                    new SQ()
                    {
                        OrderDate = x.Max(a => a.OrderDate),
                        Brand = x.First().Brand,
                        CusName = x.First().CusName,
                        POOLD = x.First().POOLD.Trim('"'),
                        PONEW = x.First().PONEW,
                        Material = x.First().Material,
                        Color = x.First().Color,
                        Qty = x.Sum(a => a.Qty),
                        Unit = "YD",
                        ETD = x.Min(a => a.ETD),
                        Season = x.First().Season,
                        Art = x.First().Art,
                        Follow = x.First().Follow.Trim('"'),
                        PurchaseSpecification = x.First().PurchaseSpecification.Trim('"'),
                        PurchaserName = x.First().PurchaserName,
                        Width = x.First().Width,
                    }
                ).ToList();*/
                dgvMain.DataSource = lst;
                //lblTotalRows.Text = lst.Count.ToString("#,##0");
                //lblTotalQty.Text = lst.Sum(x => x.Qty).ToString("#,##0.000");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                txtInput.Enabled = true;

            }
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
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            txtInput.Enabled = false;
            txtFilename.Enabled = false;
            btnCreate.Enabled = false;

            FileStream fs_template = null;
            XLWorkbook wb_template = null;

            FileStream fs_Main = null;
            XLWorkbook wb_Main = null;
            try
            {
                string output = txtFilename.Text;
                string filePath = $@"OUTPUT\PHIEUGIAOHANG\{txtFilename.Text}.xlsx";
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
                await Task.Run(() =>
                {
                    fs_template = new FileStream($@"Default\PHIEUGIAOHANG_{_custName}.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreate);
                    return;
                }
                var lst = dgvMain.DataSource as List<PhieuGiaoHang>;
                var task1 = await CreatePHIEUGIAOHANG(_custName, lst, wb_template);
                if (!task1.success)
                {
                    MessageBox.Show(task1.msg);
                    return;
                }
                var task2 = await CreateTEMLST(_custName, lst, wb_template);
                if (!task2.success)
                {
                    MessageBox.Show(task2.msg);
                    return;
                }
                /*var task3 = await CreateDEBIT(cusName, lstINV, wb_template);
                if (!task3.success)
                {
                    MessageBox.Show(task3.msg);
                    return;
                }*/
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
                txtFilename.Enabled = true;
                btnCreate.Enabled = true;

                wb_template?.Dispose();
                fs_template?.Dispose();

                wb_Main?.Dispose();
                fs_Main?.Dispose();
            }
        }
        private async Task<(bool success, string msg)> CreatePHIEUGIAOHANG(string cusName, List<PhieuGiaoHang> lstPGH, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("PGH");
                    sheet.Cell("H8").Value = $"出貨日期:{dtpCreateDate.Value.ToString("yyyy-MM-dd")}";
                    int n = 11;
                    int i = 1;
                    foreach (var inv in lstPGH)
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
                        row.Cell("B").Value = inv.PO;
                        row.Cell("C").Value = inv.MaterialCode;
                        row.Cell("D").Value = inv.Description;
                        row.Cell("E").Value = inv.Color;
                        row.Cell("F").Value = inv.Width;
                        row.Cell("G").Value = inv.Qty;
                        row.Cell("G").Style.Font.Bold = true;
                        row.Cell("H").Value = inv.Unit;
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
                    rowEnd.Cell("G").FormulaA1 = $"=SUBTOTAL(109,G{n - lstPGH.Count}:G{n - 1})";
                    rowEnd.Cell("I").FormulaA1 = $"=SUBTOTAL(109,I{n - lstPGH.Count}:I{n - 1})";

                    //sheet.Cell("A4").Value = $"Debit Note - SPARKLIN (NEWRICH) ({dtpCreateDate.Value.ToString("yyyy/MM")})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private async Task<(bool success, string msg)> CreateTEMLST(
            string cusName,
            List<PhieuGiaoHang> lstPGH,
            XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("TEM");
                    var rangeTem = sheet.Range("A1:C10");

                    int index = 0;

                    foreach (var pgh in lstPGH)
                    {
                        var l1 = pgh.Roll.Split('+');

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
                                    sheet.Range($"A{index * 10 + 1}:C{index * 10 + 10}"),
                                    pgh.PO,
                                    pgh.Description.Replace("EPM5", "EPM5 ").Replace("EPM3", "EPM3 "),
                                    pgh.Color == "STANDARDBLACK" ? "STANDARD BLACK" : pgh.Color,
                                    pgh.Width,
                                    qty,
                                    index + 1,
                                    dtpCreateDate.Value
                                );

                                index++;
                            }
                        }
                    }
                    sheet.PageSetup.PrintAreas.Clear();
                    sheet.PageSetup.PrintAreas.Add($"A1:C{index * 10}");

                    for (int i = 1; i < index; i++)
                    {
                        sheet.PageSetup.AddHorizontalPageBreak(i * 10);
                    }

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private void CreateTem(IXLRange rangeTem, IXLRange rangeNewTem, string po, string code, string color, int width, decimal qty, int index, DateTime deliveryDate)
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
            rangeNewTem.Cell("B2").Value = po;
            rangeNewTem.Cell("B3").Value = code;
            rangeNewTem.Cell("B5").Value = color;
            rangeNewTem.Cell("B6").Value = width;
            rangeNewTem.Cell("B7").Value = qty;
            rangeNewTem.Cell("B9").Value = index;
            rangeNewTem.Cell("B10").Value = deliveryDate.ToString("yyyy.MM.dd");
        }
    }
}
