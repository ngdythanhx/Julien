using ClosedXML.Excel;
using  Julian_SolutionDatabase.DTO;
using Julian;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Julian_Solution.Database.DTO;

namespace  Julian_Solution.Forms
{
    public partial class frmGroupByPO : Form
    {
        string _sourceFilePath = string.Empty;
        string _resultFile = string.Empty;
        string _linkOutput = string.Empty;
        Customer _curCustomer;
        public frmGroupByPO()
        {
            InitializeComponent();
            linkResult.Visible = false;
            btnExport.Enabled = false;
        }
        private void SelectFile()
        {
            try
            {
                this.ActiveControl = label3;
                var dl = new OpenFileDialog();
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    _sourceFilePath = selectedPath;
                    lblFileName1.Text = fileName;
                    btnExport.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                groupBox1.Enabled = true;
            }
        }
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            SelectFile();
        }
        private string GetStringCell(IXLRangeRow rangeRow, object column)
        {
            var cell = column is int
                ? rangeRow.Cell((int)column)
                : rangeRow.Cell(column.ToString());

            try
            {
                if (cell.IsMerged())
                    return cell.MergedRange().FirstCell().GetString();

                return cell.GetString();
            }
            catch
            {
                return cell.GetString();
            }
        }
        private Object GetCellValue(IXLRangeRow rangeRow, object column)
        {
            var cell = column is int
                ? rangeRow.Cell((int)column)
                : rangeRow.Cell(column.ToString());

            try
            {
                if (cell.IsMerged())
                    return cell.MergedRange().FirstCell().GetString();

                return cell.GetString();
            }
            catch
            {
                return cell.GetString();
            }
        }
        private Dictionary<int, IXLRangeRow> Export(IXLWorksheet worksheet)
        {
            var list = new Dictionary<int, IXLRangeRow>();
            var rangeUsed = worksheet.RangeUsed();
            if (rangeUsed != null)
            {
                IXLRangeRow[] rowsUseds = worksheet.RangeUsed().RowsUsed().Skip(3).ToArray();
                var grouped = rowsUseds.Where(r => DateTime.TryParse(GetStringCell(r, "C"), out _)).GroupBy(r => new
                {
                    ColB = r.Cell("D").GetString(),
                    ColE = r.Cell("E").GetString(),
                    ColI = r.Cell("I").GetString(),
                    ColL = r.Cell("L").GetString(),
                    ColQ = r.Cell("Q").GetString(),
                }).ToArray();
                foreach (var group in grouped)
                {
                    double totalD = group.Sum(r =>
                    {
                        try
                        {
                            return r.Cell("V").GetDouble();
                        }
                        catch
                        {
                            string valString = r.Cell("V").GetString();
                            int valInt;
                            if (int.TryParse(valString, out valInt))
                                return valInt;
                            return 0;
                        }
                    });
                    var firstRow = group.First();
                    firstRow.Cell("V").Value = totalD;
                    //foreach (var row in group.Skip(1))
                    //{
                    //    row.Delete();
                    //}
                }
                list = grouped.ToDictionary(g => g.First().RangeAddress.FirstAddress.RowNumber, g => g.First());
            }
            return list;
        }
        private List<ProductionReport> Export2(IXLWorksheet worksheet)
        {
            var list = new List<ProductionReport>();
            var rangeUsed = worksheet.RangeUsed();
            if (rangeUsed != null)
            {
                IXLRangeRow[] rowsUseds = worksheet.RangeUsed().RowsUsed().ToArray();
                for (int i = 3; i < rowsUseds.Length; i++)
                {
                    var row = rowsUseds[i];
                    int rowIndex = row.RangeAddress.FirstAddress.RowNumber;
                    if (DateTime.TryParse(GetStringCell(row, 1), out DateTime date))
                    {
                        ProductionReport ProductionReport = new ProductionReport()
                        {
                            OrderDate = date,
                            Brand = GetStringCell(row, "D"),
                            CustomerCode = GetStringCell(row, "E"),
                            CustomerPO = GetStringCell(row, "I"),
                            CustomerMaterial = GetStringCell(row, "L"),
                            MaterialSizeWith = GetStringCell(row, "O"),
                            CustomerColor = GetStringCell(row, "Q"),
                            Quantity = decimal.TryParse(GetStringCell(row, "V"), out decimal qty) ? qty : 0,
                            UnitPrice = decimal.TryParse(GetStringCell(row, "AA"), out decimal uPrice) ? uPrice : 0,
                            Amount = decimal.TryParse(GetStringCell(row, "AB"), out decimal amount) ? amount : 0,
                            CustomerSeason = GetStringCell(row, "AL"),
                            Weight = int.TryParse(GetStringCell(row, "BB"), out int wei) ? wei : 0,
                            EcoRatio = GetStringCell(row, "BC"),
                            WarehousePO = GetStringCell(row, "BE"),
                        };
                        list.Add(ProductionReport);
                    }
                }
            }
            return list;
        }
        private async void btnExport_Click(object sender, EventArgs e)
        {
            this.ActiveControl = label3;
            btnExport.Enabled = false;
            linkResult.Visible = false;
            _linkOutput = string.Empty;
            string fileDefaultPath = "DefaultGroupByPO.xlsx";
            try
            {
                if (string.IsNullOrEmpty(_sourceFilePath))
                {
                    SelectFile();
                    return;
                }
                XLWorkbook workbook_DefaultOrderForm = null;
                if (!File.Exists(fileDefaultPath))
                {
                    MessageBox.Show($"Không tìm thấy file {fileDefaultPath}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await Task.Run(() => workbook_DefaultOrderForm = new XLWorkbook(fileDefaultPath));
                if (!(workbook_DefaultOrderForm.Worksheets.FirstOrDefault() is IXLWorksheet worksheet_DefaultOrderForm))
                {
                    MessageBox.Show($"Không tìm thấy Sheets[0] trong file {fileDefaultPath}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(_sourceFilePath) || !File.Exists(_sourceFilePath))
                {
                    MessageBox.Show("Đường dẫn không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (System.IO.Path.GetExtension(_sourceFilePath) != ".xlsx")
                {
                    MessageBox.Show("Định dạng file không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    XLWorkbook workbook = null;
                    IXLWorksheet sheet = null;
                    await Task.Run(() => workbook = new XLWorkbook(_sourceFilePath));
                    var worksheets = workbook.Worksheets.ToArray();
                    if (worksheets.Length > 0)
                    {
                        sheet = worksheets[0];
                        Dictionary<int, IXLRangeRow> resultList = Export(sheet);
                        if (resultList != null && resultList.Count > 0)
                        {
                            var rowsInsert = worksheet_DefaultOrderForm.Rows(4, resultList.Count + 1).ToArray();
                            /* foreach(var rowResult in resultList)
                             {
                                 var rowInsert = worksheet_DefaultOrderForm.Row(rowResult.Key);
                                 foreach (IXLCell cellResult in rowResult.Value.Cells().ToArray())
                                 {
                                     rowInsert.Cell(cellResult.Address.ColumnNumber).Value = cellResult.Value;
                                 }
                             }*/
                            var list = resultList.Values.ToArray();
                            for (int i = 0; i < rowsInsert.Length; i++)
                            {
                                var row = rowsInsert[i];
                                var rowResult = list[i];
                                foreach (IXLCell cellResult in rowResult.Cells().ToArray())
                                {
                                    if (!cellResult.Value.IsBlank)
                                    {
                                        if (cellResult.DataType == XLDataType.DateTime)
                                        {
                                            try
                                            {
                                                DateTime date = cellResult.GetDateTime();
                                                row.Cell(cellResult.Address.ColumnLetter).Value = date;
                                            }
                                            catch
                                            {
                                                row.Cell(cellResult.Address.ColumnLetter).Value = cellResult.GetString();
                                            }
                                        }
                                        else if (cellResult.DataType == XLDataType.Number)
                                        {
                                            try
                                            {
                                                row.Cell(cellResult.Address.ColumnLetter).Value = cellResult.GetDouble();
                                            }
                                            catch
                                            {
                                                row.Cell(cellResult.Address.ColumnLetter).Value = cellResult.GetString();
                                            }
                                        }
                                        else
                                        {
                                            row.Cell(cellResult.Address.ColumnLetter).Value = cellResult.Value;
                                        }
                                    }
                                }
                            }

                            if (!Directory.Exists("Output"))
                            {
                                Directory.CreateDirectory("Output");
                            }
                            string fileName = "Order_" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.Ticks + ".xlsx";
                            workbook_DefaultOrderForm.SaveAs(System.IO.Path.Combine("Output", fileName));
                            linkResult.Visible = true;
                            linkResult.Text = fileName;
                            _linkOutput = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Output", fileName);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm được dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Worksheets.Count = 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnExport.Enabled = true;
            }
        }

        private void linkResult_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_linkOutput) && File.Exists(_linkOutput))
            {
                //string filePath = System.IO.Path.Combine("Output", fileName));
                System.Diagnostics.Process.Start("explorer.exe", @"/select, " + _linkOutput);
            }
        }
        public class ProductionReport
        {
            public DateTime OrderDate { get; set; }
            public string Brand { get; set; }
            public string CustomerCode { get; set; }
            public string CustomerPO { get; set; }
            public string CustomerMaterial { get; set; }
            public string MaterialSizeWith { get; set; }
            public string CustomerColor { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Amount { get; set; }
            public string CustomerSeason { get; set; }
            public int Weight { get; set; }
            public string EcoRatio { get; set; }
            public string WarehousePO { get; set; }
        }
    }
}
