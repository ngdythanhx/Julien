using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using Julian_Solution;
using Julian_Solution.Database.DTO;
using Julian_SolutionDatabase.DTO;
using Julian_SolutionHelper;
using Julian_SolutionUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian
{
    public partial class frmAddOrder : Form
    {
        string _sourceFilePath = string.Empty;
        string _resultFile = string.Empty;
        string _linkOutput = string.Empty;
        Customer _curCustomer;
        public frmAddOrder()
        {
            InitializeComponent();
            EnableControl(false);
            linkResult.Visible = false;
        }
        private void EnableControl(bool enable)
        {
            foreach (System.Windows.Forms.Control control in groupBox1.Controls)
            {
                if (!(control is Label))
                {
                    control.Enabled = enable;
                }
            }
        }
        private async Task<List<Customer>> LoadCustomerAll()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(Config.ConnectionString);
                var lstRs = await Task.FromResult(db.GetListSP<Customer>("SP_Customer_GetAll"));
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }
        private async void frmAddOrder_Load(object sender, EventArgs e)
        {
            var customers = await LoadCustomerAll();
            if (customers != null && customers.Count > 0)
            {
                EnableControl(true);
                this.ActiveControl = label2;
                cbCustomer.SelectedIndexChanged += (s, ee) =>
                {
                    if (cbCustomer.SelectedIndex >= 0)
                    {
                        _curCustomer = (Customer)cbCustomer.SelectedValue;
                        txtCustomerName.Text = _curCustomer.CustomerName;
                    }
                    else
                        _curCustomer = null;
                };
                cbCustomer.DisplayMember = "CustomerCode";
                cbCustomer.DataSource = customers;
            }
            else
            {
                EnableControl(false);
            }
        }
        private void SelectFile()
        {
            try
            {
                this.ActiveControl = label2;
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
                    txtFileName.Text = fileName;
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
        private async Task<List<Material>> GetMaterialByCode(string materialCode)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_MaterialCode", materialCode);
                db = new DBHelper(Config.ConnectionString);
                var lstRs = await Task.FromResult(db.GetListSP<Material>("SP_Material_Get_ByCode", pars));
                return lstRs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
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
                // Trường hợp lỗi do merge lỗi trong Excel
                return cell.GetString();
            }
        }
        private async Task<List<Dictionary<string, string>>> Export(IXLWorksheet worksheet, Customer customer)
        {
            var list = new List<Dictionary<string, string>>();
            var rangeUsed = worksheet.RangeUsed();
            if (rangeUsed != null)
            {
                IXLRangeRow[] rowsUseds = worksheet.RangeUsed().RowsUsed().ToArray();
                var ini = new IniManager(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
                if (ini.GetInt("Customer", customer.CustomerCode) == 1)
                {
                    ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                    var rowsFields = rowsUseds[5];
                    string[] fields = new string[21];
                    for (int i = 0; i < 21; i++)
                    {
                        fields[i] = GetStringCell(rowsFields, i + 1);
                    }
                    if (!fields.Any(f => string.IsNullOrEmpty(f)))
                    {
                        await Task.Run(() =>
                        {
                            for (int i = 6; i < rowsUseds.Length; i++)
                            {
                                var row = rowsUseds[i];
                                var a = row.RangeAddress.FirstAddress.RowNumber;
                                if (DateTime.TryParse(GetStringCell(row, 1), out DateTime date))
                                {
                                    Dictionary<string, string> data = new Dictionary<string, string>();
                                    data.Add(fields[0], date.ToString("dd/MM/yyyy"));
                                    for (int j = 1; j < 21; j++)
                                    {
                                        data.Add(fields[j], GetStringCell(row, j + 1));
                                    }
                                    list.Add(data);
                                }
                            }
                        });
                    }
                }
            }
            return list;
        }
        private async void btnExport_Click(object sender, EventArgs e)
        {
            this.ActiveControl = label2;
            btnExport.Enabled = false;
            linkResult.Visible = false;
            _linkOutput = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(_sourceFilePath))
                {
                    SelectFile();
                    btnExport.Enabled = true;
                    return;
                }
                XLWorkbook workbook_DefaultOrderForm = null;
                if (!File.Exists("DefaultOrderForm.xlsx"))
                {
                    MessageBox.Show("Không tìm thấy file DefaultOrderForm.xlsx!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                await Task.Run(() => workbook_DefaultOrderForm = new XLWorkbook("DefaultOrderForm.xlsx"));
                if (!(workbook_DefaultOrderForm.Worksheets.FirstOrDefault() is IXLWorksheet worksheet_DefaultOrderForm))
                {
                    MessageBox.Show("Không tìm thấy Sheets[0] trong file DefaultOrderForm.xlsx!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        Customer customer = cbCustomer.SelectedItem as Customer;
                        var resultList = await Export(sheet, customer);
                        if (resultList != null && resultList.Count > 0)
                        {
                            var rows = worksheet_DefaultOrderForm.Rows(2, resultList.Count + 1).ToArray();
                            for (int i = 0; i < rows.Length; i++)
                            {
                                var row = rows[i];
                                row.Cell("C").Value = resultList[i]["Date"];
                                row.Cell("E").Value = customer.CustomerCode;
                                string materialCode = resultList[i]["Mt'l code"];
                                row.Cell("J").Value = materialCode;
                                string materialName = resultList[i]["Material name"];
                                var match = Regex.Match(materialName, @"(\d+)\s*\""");
                                row.Cell("O").Value = match.Success ? match.Groups[1].Value : "";
                                row.Cell("P").Value = materialName;
                                var materials = await GetMaterialByCode(materialCode);
                                if (materials != null && materials.Count > 0)
                                {
                                    row.Cell("Q").Value = materials[0].CustomerColor;
                                    row.Cell("AR").Value = materials[0].StandardColor;
                                    row.Cell("AS").Value = materials[0].Description;
                                }
                                row.Cell("V").Value = resultList[i]["Order Q'ty"];
                                row.Cell("X").Value = resultList[i]["Order Q'ty"];
                                row.Cell("AC").Value = resultList[i]["ETD (Rtd)"];
                                row.Cell("AL").Value = resultList[i]["Demand Season"];
                            }
                            if (!Directory.Exists("Output"))
                            {
                                Directory.CreateDirectory("Output");
                            }
                            string fileName = "Order_" + DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.Ticks + ".xlsx";
                            workbook_DefaultOrderForm.SaveAs(System.IO.Path.Combine("Output", fileName));
                            btnExport.Enabled = true;
                            linkResult.Visible = true;
                            linkResult.Text = fileName;
                            _linkOutput = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Output", fileName);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm được dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnExport.Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Worksheets.Count = 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnExport.Enabled = true;
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
    }
}
