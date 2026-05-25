using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  Julian_Solution.Forms
{
    public partial class frmCompare : Form
    {
        XLWorkbook workbook1;
        XLWorkbook workbook2;
        IXLWorksheet worksheet1;
        IXLWorksheet worksheet2;
        private string _filePath1;
        private string _filePath2;
        public frmCompare()
        {
            InitializeComponent();
            btnStart.Enabled = false;
            chkFilterData.Checked = true;
            chkFilterData.CheckedChanged += (sender, e) =>
            {
                txtColumnsFilter.Enabled = chkFilterData.Checked;
            };
        }
        private async void btnSelectFile1_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                gb1.Enabled = false;
                btnStart.Enabled = false;
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
                    workbook1?.Dispose();
                    workbook1 = null;
                    await Task.Run(() =>
                    {
                        workbook1?.Dispose();
                        _filePath1 = selectedPath;
                        workbook1 = new XLWorkbook(selectedPath);
                    });
                    var worksheets = workbook1.Worksheets.ToList();
                    lblFileName1.Text = fileName;
                    cbSheets1.DataSource = worksheets;
                    cbSheets1.DisplayMember = "Name";
                    if (workbook1 != null && workbook2 != null)
                    {
                        btnStart.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                gb1.Enabled = true;
            }

        }

        private async void btnSelectFile2_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label2;
                gb2.Enabled = false;
                btnStart.Enabled = false;
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
                    workbook2?.Dispose();
                    workbook2 = null;
                    await Task.Run(() =>
                    {
                        workbook2?.Dispose();
                        _filePath2 = selectedPath;
                        workbook2 = new XLWorkbook(selectedPath);
                    });
                    var worksheets = workbook2.Worksheets.ToList();
                    lblFileName2.Text = fileName;
                    cbSheets2.DataSource = worksheets;
                    cbSheets2.DisplayMember = "Name";
                    if (workbook1 != null && workbook2 != null)
                    {
                        btnStart.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                gb2.Enabled = true;
            }

        }
        bool IsOnlyLetters(string input)
        {
            return Regex.IsMatch(input, @"^[a-zA-Z]+$");
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
        private string GetStringCellFromRangeRow(IXLRangeRow rangeRow, string columnName)
        {
            var cell = rangeRow.Cell(columnName);
            string cellValue = string.Empty;
            if (cell.IsMerged())
            {
                var mergedRange = cell.MergedRange();
                var topLeft = mergedRange.FirstCell();
                cellValue = topLeft.GetString();
            }
            else
            {
                cellValue = cell.GetString();
            }
            return cellValue;
        }
        private bool ValidateInputs()
        {
            if (workbook1 == null)
            {
                MessageBox.Show("File1 không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (workbook2 == null)
            {
                MessageBox.Show("File2 không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!CheckReadWriteFile(_filePath1))
            {
                MessageBox.Show("File1 không thể ghi dữ liệu, nếu đang mở bằng tiến trình khác vui lòng đóng trước khi thao tác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!CheckReadWriteFile(_filePath2))
            {
                MessageBox.Show("File1 không thể ghi dữ liệu, nếu đang mở bằng tiến trình khác vui lòng đóng trước khi thao tác", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string[] columns1 = txtColumns1.Text.Replace(" ", "").Split(';');
            if (columns1.Length == 0 || columns1.Any(c => !IsOnlyLetters(c)))
            {
                toolTip1.Show("Định dạng: A;B;C", txtColumns1, txtColumns1.Width, 0, 1500);
                txtColumns1.Focus();
                return false;
            }
            string[] columns2 = txtColumns2.Text.Replace(" ", "").Split(';');
            if (columns2.Length == 0 || columns2.Any(c => !IsOnlyLetters(c)))
            {
                toolTip1.Show("Định dạng: A;B;C", txtColumns2, txtColumns2.Width, 0, 1500);
                txtColumns2.Focus();
                return false;
            }
            if (columns1.Length != columns2.Length)
            {
                toolTip1.Show("Số cột cần lọc của 2 sheet phải bằng nhau", txtColumns2, txtColumns2.Width, 0, 1500);
                txtColumns2.Focus();
                return false;
            }
            if (chkFilterData.Checked)
            {
                string[] columnsFilter = txtColumnsFilter.Text.Replace(" ", "").Split(';');
                if (columnsFilter.Length == 0 || columnsFilter.Any(c => !IsOnlyLetters(c)))
                {
                    toolTip1.Show("Định dạng: A;B;C", txtColumnsFilter, txtColumnsFilter.Width, 0, 1500);
                    txtColumnsFilter.Focus();
                    return false;
                }
            }
            return true;
        }
        private void EnableControl(bool enable)
        {
            gb1.Enabled = enable;
            gb2.Enabled = enable;
            btnStart.Enabled = enable;
            chkFilterData.Enabled = enable;
            txtColumnsFilter.Enabled = enable;
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            lblRowsCount.Text = "_";
            dgvData.DataSource = null;
            try
            {
                if (!ValidateInputs())
                {
                    return;
                }

                IXLRangeRow[] rangeRows1 = null;
                IXLRangeRow[] rangeRows2 = null;
                worksheet1 = (IXLWorksheet)cbSheets1.SelectedValue;
                worksheet2 = (IXLWorksheet)cbSheets2.SelectedValue;
                var task1 = Task.Run(() =>
                {
                    rangeRows1 = worksheet1.RangeUsed().RowsUsed().ToArray();
                });
                var task2 = Task.Run(() =>
                {
                    rangeRows2 = worksheet2.RangeUsed().RowsUsed().ToArray();
                });
                await Task.WhenAll(task1, task2);

                string[] columns1 = txtColumns1.Text.Replace(" ", "").Split(';');
                string[] columns2 = txtColumns2.Text.Replace(" ", "").Split(';');

                Dictionary<string, IXLRangeRow> dictSheet1 = new Dictionary<string, IXLRangeRow>();
                await Task.Run(() =>
                {
                    foreach (var row in rangeRows1)
                    {
                        var list = columns1.Select(c => GetStringCellFromRangeRow(row, c));
                        if (list.All(c => string.IsNullOrWhiteSpace(c)))
                            continue;
                        var key = string.Join("|", list);
                        if (!dictSheet1.ContainsKey(key))
                            dictSheet1[key] = row;
                    }
                });
                List<IXLRangeRow> foundRows = new List<IXLRangeRow>();
                foreach (var row2 in rangeRows2)
                {
                    var key = string.Join("|", columns2.Select(c => GetStringCellFromRangeRow(row2, c)));
                    if (dictSheet1.TryGetValue(key, out var row1))
                    {
                        foundRows.Add(row1);
                    }
                }
                lblRowsCount.Text = foundRows.Count.ToString();
                var data = ToDataTable(foundRows);
                dgvData.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblRowsCount.Text = "_";
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }
        private DataTable ToDataTable(List<IXLRangeRow> rows)
        {
            DataTable dt = new DataTable();

            if (rows == null || rows.Count == 0)
                return dt;

            int colCount = rows[0].CellCount();

            if (chkFilterData.Checked)
            {
                var columns = txtColumnsFilter.Text.Replace(" ", "").Split(';');
                for (int i = 1; i <= colCount; i++)
                {
                    string columnName = rows[0].Cell(i).Address.ColumnLetter;
                    if (columns.Contains(columnName))
                    {
                        dt.Columns.Add(rows[0].Cell(i).Address.ColumnLetter, typeof(string));
                    }
                }
                foreach (var row in rows)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < columns.Length; i++)
                    {
                        dr[i] = row.Cell(columns[i]).GetValue<string>();
                    }
                    dt.Rows.Add(dr);
                }
            }
            else
            {
                for (int i = 1; i <= colCount; i++)
                {
                    dt.Columns.Add(rows[0].Cell(i).Address.ColumnLetter, typeof(string));
                }

                foreach (var row in rows)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 1; i <= colCount; i++)
                    {
                        dr[i - 1] = row.Cell(i).GetValue<string>();
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}
