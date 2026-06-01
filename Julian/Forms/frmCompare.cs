using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Julian.Utils;
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

namespace Julian.Forms
{
    public partial class frmCompare : Form
    {
        XLWorkbook workbook1;
        XLWorkbook workbook2;
        IXLWorksheet worksheet1;
        IXLWorksheet worksheet2;
        //private string _filePath1;
        //private string _filePath2;
        IniManager _iniManager = new IniManager(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public frmCompare()
        {
            InitializeComponent();
            btnStart.Enabled = false;
            txtFilterString.Enabled = false;
            chkFilterData.Checked = true;
            chkFilterData.CheckedChanged += (sender, e) =>
            {
                txtFilterColumns.Enabled = chkFilterData.Checked;
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
                    if (!extension.Contains(".xls"))
                    {
                        MessageBox.Show("File không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    workbook1?.Dispose();
                    workbook1 = null;
                    await Task.Run(() =>
                    {
                        workbook1?.Dispose();
                        //_filePath1 = selectedPath;
                        using (var stream = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            workbook1 = new XLWorkbook(stream);
                        }
                    });
                    var worksheets = workbook1.Worksheets.ToList();
                    lblFileName1.Text = fileName;
                    cbSheets1.DataSource = worksheets;
                    cbSheets1.DisplayMember = "Name";
                    if (workbook1 != null && workbook2 != null)
                    {
                        btnStart.Enabled = true;
                        txtFilterString.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
                        //_filePath2 = selectedPath;
                        using (var stream = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            workbook2 = new XLWorkbook(stream);
                        }
                    });
                    var worksheets = workbook2.Worksheets.ToList();
                    lblFileName2.Text = fileName;
                    cbSheets2.DataSource = worksheets;
                    cbSheets2.DisplayMember = "Name";
                    if (workbook1 != null && workbook2 != null)
                    {
                        btnStart.Enabled = true;
                        txtFilterString.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            if (chkFilterData.Checked)
            {
                string[] columnsFilter = txtFilterColumns.Text.Replace(" ", "").Split(';');
                if (columnsFilter.Length == 0 || columnsFilter.Any(c => !IsOnlyLetters(c)))
                {
                    toolTip1.Show("Định dạng: A;B;C", txtFilterColumns, txtFilterColumns.Width, 0, 1500);
                    txtFilterColumns.Focus();
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
            txtFilterString.Enabled = enable;
            chkFilterData.Enabled = enable;
            txtFilterColumns.Enabled = enable;
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            lblRowsCount.Text = "_";
            dgvData.DataSource = null;
            _iniManager.WriteString("Default", "Compare_FilterColumns",txtFilterColumns.Text);
            _iniManager.WriteString("Default", "Compare_FilterString", txtFilterString.Text);
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

                //string[] columns1 = txtColumns1.Text.Replace(" ", "").Split(';');
                //string[] columns2 = txtColumns2.Text.Replace(" ", "").Split(';');
                List<string> columns1 = new List<string>();
                List<string> columns2 = new List<string>();
                string[] a = txtFilterString.Text.Split(';');
                foreach (var b in a)
                {
                    string[] c = b.Split('=');
                    if (c.Length == 2 && Regex.IsMatch(c[0], "^[a-zA-Z]$") && Regex.IsMatch(c[1], "^[a-zA-Z]$"))
                    {
                        columns1.Add(c[0]);
                        columns2.Add(c[1]);
                    }
                }
                if (columns1.Count == 0)
                {
                    toolTip1.Show("Định dạng: A=B;N=O", txtFilterString, txtFilterString.Width, 0, 1500);
                    txtFilterString.Focus();
                    return;
                }
                Dictionary<string, List<IXLRangeRow>> dictSheet1 = new Dictionary<string, List<IXLRangeRow>>();
                await Task.Run(() =>
                {
                    foreach (var row in rangeRows1)
                    {
                        var list = columns1.Select(c => GetStringCellFromRangeRow(row, c));
                        if (list.All(c => string.IsNullOrWhiteSpace(c)))
                            continue;
                        var key = string.Join("|", list);
                        if (!dictSheet1.ContainsKey(key))
                        {
                            var lst = new List<IXLRangeRow>();
                            lst.Add(row);
                            dictSheet1[key] = lst;
                        }
                        else
                        {
                            dictSheet1[key].Add(row);
                        }
                    }
                });
                List<IXLRangeRow> foundRows = new List<IXLRangeRow>();
                foreach (var row2 in rangeRows2)
                {
                    var key = string.Join("|", columns2.Select(c => GetStringCellFromRangeRow(row2, c)));
                    if (dictSheet1.TryGetValue(key, out var row1))
                    {
                        foundRows.AddRange(row1);
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
                var columns = txtFilterColumns.Text.Replace(" ", "").Split(';');
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
                        dr[i] = row.Cell(columns[i].ToUpper()).GetValue<string>();
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

        private void frmCompare_Load(object sender, EventArgs e)
        {
            txtFilterColumns.Text = _iniManager.GetString("Default", "Compare_FilterColumns");
            txtFilterString.Text = _iniManager.GetString("Default", "Compare_FilterString");
        }
        private void CopyDataGridViewToClipboard(DataGridView dgv)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i = 0; i < dgvData.Columns.Count; i++)
                {
                    string value = row.Cells[i].Value?.ToString() ?? "";

                    // nếu có xuống dòng thì đặt trong ""
                    if (value.Contains("\n"))
                        value = "\"" + value + "\"";

                    sb.Append(value);

                    if (i < dgvData.Columns.Count - 1)
                        sb.Append("\t");
                }

                sb.Append("\r\n");
            }

            Clipboard.SetText(sb.ToString());
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                toolTip1.Show("Không có dữ liệu để sao chép", btnCopy, btnCopy.Width, 0, 1500);
            }
            else
            {
                CopyDataGridViewToClipboard(dgvData);
                toolTip1.Show("Đã sao chép", btnCopy, btnCopy.Width, 0, 1500);
            }
        }
    }
}
