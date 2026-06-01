using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace Julian.Forms
{
    public partial class frmBaoCaoSanLuong : Form
    {
        List<string> _lstFile = new List<string>();
        public frmBaoCaoSanLuong()
        {
            InitializeComponent();
            btnStart.Enabled = false;
        }
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                var dl = new OpenFileDialog();
                dl.Multiselect = true;
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                _lstFile.Clear();
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string[] files = dl.FileNames;
                    foreach (string file in files)
                    {
                        string fileName = System.IO.Path.GetFileName(file);
                        string extension = System.IO.Path.GetExtension(file);
                        if (extension == ".xlsx")
                        {
                            _lstFile.Add(file);
                            listBox1.Items.Add(fileName);
                        }
                    }
                    if (_lstFile.Count > 0)
                    {
                        btnStart.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReportInProcessing(int value)
        {

        }
        private Task<bool> ExcelFileHandle(string file, IXLTable table_Write)
        {
            return Task.Run(() =>
            {
                using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var sheet_Read = workbook.Worksheet(1);
                        var rangeRows = sheet_Read.RangeUsed().RowsUsed().Skip(3);

                        foreach (var row_Read in rangeRows)
                        {
                            string customerCode = row_Read.Cell("E").GetString();

                            if (!string.IsNullOrEmpty(customerCode) && customerCode.Length >= 2)
                            {
                                var rowData = new object[60];

                                for (int j = 1; j <= 60; j++)
                                    rowData[j - 1] = row_Read.Cell(j).Value;

                                table_Write.AppendData(new[] { rowData });
                            }
                        }
                    }

                    table_Write.DataRange.FirstRow().Delete();

                    return true;
                }
            });

        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            progressBar1.Maximum = _lstFile.Count;
            progressBar1.Value = 0;
            //XLWorkbook workbook_test = new XLWorkbook("Output\\BaoCaoSanLuongAll_05_03_2026_639083432477979306.xlsx");
            //var data = workbook_test.Worksheet(1).Cell("BE51").GetString();
            XLWorkbook workbook_Write = null;
            try
            {
                string fileWrite = "Default_BaoCaoSanLuong.xlsx";
                if (!File.Exists(fileWrite))
                {
                    MessageBox.Show($"Không tìm thấy file {fileWrite}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblCurProcessingFile.Text = fileWrite;
                await Task.Run(() => workbook_Write = new XLWorkbook("Default_BaoCaoSanLuong.xlsx"));
                if (workbook_Write == null)
                {
                    MessageBox.Show($"Lỗi file {fileWrite}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var table_Write = workbook_Write.Worksheet(1).Table("tbData");
                int totalSuccess = 0;
                foreach (string file in _lstFile)
                {
                    lblCurProcessingFile.Text = Path.GetFileName(file);
                    var success = await ExcelFileHandle(file, table_Write);
                    if (success)
                    {
                        totalSuccess++;
                        progressBar1.Value = totalSuccess;
                        lblTotalRows.Text = table_Write.DataRange.Rows().Count().ToString();
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi file {Path.GetFileName(file)}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                string fileName = DateTime.Now.ToString("dd_MM_yyyy_") + DateTime.Now.Ticks + ".xlsx";
                workbook_Write.SaveAs($"Output\\BaoCaoSanLuongAll_{fileName}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            {
                btnStart.Enabled = true;
            }
        }
    }
}
