using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian.Forms
{
    public partial class frmHoiHang : Form
    {
        XLWorkbook workbook1;
        XLWorkbook workbook2;
        string filePath1 = "";
        string filePath2 = "";
        int totalFind = 0;
        bool allowRun = false;
        public frmHoiHang()
        {
            InitializeComponent();
            txtCotGhiDuLieu.KeyPress += new KeyPressEventHandler(txtOnlyLetter_KeyPress);
            txtCotSaoChep.KeyPress += new KeyPressEventHandler(txtOnlyLetter_KeyPress);
            txtCotSoSanh1.KeyPress += new KeyPressEventHandler(txtOnlyLetter_KeyPress);
            txtCotSoSanh2.KeyPress += new KeyPressEventHandler(txtOnlyLetter_KeyPress);
            txtCotNgayNhanMau.KeyPress += new KeyPressEventHandler(txtOnlyLetter_KeyPress);
        }
        private void txtOnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtOnlyLetter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtOnlyLetter_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            int selStart = txt.SelectionStart;

            string newText = new string(txt.Text.ToUpper().Where(char.IsLetter).ToArray());

            if (txt.Text != newText)
            {
                txt.Text = newText;
                txt.SelectionStart = selStart > newText.Length ? newText.Length : selStart;
            }
        }
        private async void btnSelectFile1_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                gb1.Enabled = false;
                var dl = new OpenFileDialog();
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!");
                        return;
                    }
                    if (selectedPath != lblFileName1.Text)
                    {
                        WriteLog($"Đang tải dữ liệu file {System.IO.Path.GetFileName(selectedPath)}");
                        await Task.Run(() =>
                        {
                            workbook1?.Dispose();
                            filePath1 = selectedPath;
                            //var steam = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            workbook1 = new XLWorkbook(filePath1);
                            var worksheets = workbook1.Worksheets.ToList();
                            this.Invoke((MethodInvoker)delegate
                            {
                                lblFileName1.Text = fileName;
                                cbSheets1.DataSource = worksheets;
                                cbSheets1.DisplayMember = "Name";
                            });
                        });
                        WriteLog($"Tải xong dữ liệu file {System.IO.Path.GetFileName(selectedPath)}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!");
                WriteLog("Error: " + ex.Message);
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
                this.ActiveControl = label1;
                var dl = new OpenFileDialog();
                gb2.Enabled = false;
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!");
                        return;
                    }
                    if (selectedPath != lblFileName2.Text)
                    {

                        WriteLog($"Đang tải dữ liệu file {System.IO.Path.GetFileName(selectedPath)}");
                        await Task.Run(() =>
                        {
                            workbook2?.Dispose();
                            filePath2 = selectedPath;
                            FileStream stream = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            workbook2 = new XLWorkbook(stream);
                            var worksheets = workbook2.Worksheets.ToList();
                            this.Invoke((MethodInvoker)delegate
                            {
                                lblFileName2.Text = fileName;
                                cbSheets2.DataSource = worksheets;
                                cbSheets2.DisplayMember = "Name";
                            });
                        });
                        WriteLog($"Tải xong dữ liệu file {System.IO.Path.GetFileName(selectedPath)}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!");
                WriteLog("Error: " + ex.Message);
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
        private bool CheckReadWriteFile1()
        {
            try
            {
                using (FileStream fs = File.Open(filePath1, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return true;
                }
            }
            catch (IOException)
            {

                return false;
            }
        }
        private int state = 0;
        private void EnableTextbox(bool enable)
        {
            txtCotSoSanh1.Enabled = enable;
            txtCotSoSanh2.Enabled = enable;
            txtCotGhiDuLieu.Enabled = enable;
            txtCotNgayNhanMau.Enabled = enable;
            txtCotSaoChep.Enabled = enable;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
            if (state == 0)
            {
                if (workbook1 == null)
                {
                    MessageBox.Show("Vui lòng chọn file1!");
                    return;
                }
                if (workbook2 == null)
                {
                    MessageBox.Show("Vui lòng chọn file2!");
                    return;
                }
                if (!CheckReadWriteFile1())
                {
                    MessageBox.Show("File đang được mở trong Excel. Vui lòng đóng file trước khi tiếp tục.");
                    return;
                }
                if (!IsOnlyLetters(txtCotSoSanh1.Text))
                {
                    ToolTip tip = new ToolTip();
                    tip.Show("Dữ liệu không hợp lệ!", txtCotSoSanh1, 0, txtCotSoSanh1.Height, 1000);
                    txtCotSoSanh1.Clear();
                    txtCotSoSanh1.Focus();
                    return;
                }
                if (!IsOnlyLetters(txtCotGhiDuLieu.Text))
                {
                    ToolTip tip = new ToolTip();
                    tip.Show("Dữ liệu không hợp lệ!", txtCotGhiDuLieu, 0, txtCotGhiDuLieu.Height, 1000);
                    txtCotGhiDuLieu.Clear();
                    txtCotGhiDuLieu.Focus();
                    return;
                }
                if (!IsOnlyLetters(txtCotSoSanh2.Text))
                {
                    ToolTip tip = new ToolTip();
                    tip.Show("Dữ liệu không hợp lệ!", txtCotSoSanh2, 0, txtCotSoSanh2.Height, 1000);
                    txtCotSoSanh2.Clear();
                    txtCotSoSanh2.Focus();
                    return;
                }
                if (!IsOnlyLetters(txtCotNgayNhanMau.Text))
                {
                    ToolTip tip = new ToolTip();
                    tip.Show("Dữ liệu không hợp lệ!", txtCotNgayNhanMau, 0, txtCotNgayNhanMau.Height, 1000);
                    txtCotNgayNhanMau.Clear();
                    txtCotNgayNhanMau.Focus();
                    return;
                }
                if (!IsOnlyLetters(txtCotSaoChep.Text))
                {
                    ToolTip tip = new ToolTip();
                    tip.Show("Dữ liệu không hợp lệ!", txtCotSoSanh1, 0, txtCotSaoChep.Height, 1000);
                    txtCotSaoChep.Clear();
                    txtCotSaoChep.Focus();
                    return;
                }

                Start(txtCotSoSanh1.Text, txtCotSoSanh2.Text, txtCotNgayNhanMau.Text, txtCotSaoChep.Text, txtCotGhiDuLieu.Text);
            }
            else
            {
                Stop();
            }
        }

        private async void Start(string inputColumnName, string patternColumnName, string dateColumnName, string copyColumnName, string storeColumnName)
        {
            try
            {
                if (workbook1 == null || workbook2 == null)
                {
                    MessageBox.Show("Dữ liệu không hợp lệ!");
                    return;
                }
                state = 1;
                btnStart.Text = "Stop";
                btnStart.ForeColor = System.Drawing.Color.Red;
                allowRun = true;

                var sheet1 = (IXLWorksheet)cbSheets1.SelectedValue;
                var sheet2 = (IXLWorksheet)cbSheets2.SelectedValue;
                List<IXLRangeRow> list1 = new List<IXLRangeRow>();
                WriteLog("Đang lọc dữ liệu trống...");
                await Task.Run(() =>
                {
                    list1 = sheet1.RangeUsed().RowsUsed().Where(row =>
                    {
                        string key = GetStringCellFromRangeRow(row, inputColumnName);
                        if (string.IsNullOrWhiteSpace(key))
                        {
                            return false;
                        }
                        return true;
                    }).ToList();
                });
                int totalRows = list1.Count;
                WriteLog($"Tổng số dòng {totalRows}");
                totalFind = 0;
                Reporter(0, totalRows);

                WriteLog("Đang tạo Dictionary...");
                var patternDict = new Dictionary<string, List<IXLRangeRow>>();
                await Task.Run(() =>
                {
                    foreach (var row2 in sheet2.RangeUsed().RowsUsed())
                    {
                        string key = GetStringCellFromRangeRow(row2, patternColumnName).Replace("\r", "").Replace("\n", "");
                        if (!string.IsNullOrEmpty(key))
                        {
                            if (!patternDict.ContainsKey(key))
                                patternDict[key] = new List<IXLRangeRow>();
                            patternDict[key].Add(row2);
                        }
                    }
                });

                var locker = new object();
                int processed = 0;
                WriteLog("Đang chạy...");
                await Task.Run(() =>
                {
                    Parallel.ForEach(list1, (row1, state) =>
                    {
                        if (!allowRun)
                        {
                            state.Stop();
                            return;
                        }

                        string cellValue1 = GetStringCellFromRangeRow(row1, inputColumnName).Replace("\r", "").Replace("\n", "");

                        if (!string.IsNullOrEmpty(cellValue1) && patternDict.TryGetValue(cellValue1, out var matchedRows))
                        {
                            lock (locker)
                            {
                                totalFind++;
                                SaoChep(matchedRows.ToArray(), row1, dateColumnName, copyColumnName, storeColumnName);
                            }
                        }
                        //Reporter(totalRows, totalRows);
                        int done = Interlocked.Increment(ref processed);
                        Reporter(done, totalRows);
                    });
                });

                Reporter(totalRows, totalRows);
                WriteLog("Đang lưu dữ liệu...");
                await Task.Run(() =>
                {
                    string path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Output");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    workbook1.SaveAs(System.IO.Path.Combine(path, System.IO.Path.GetFileName(filePath1)));
                });
                totalFind = 0;
                WriteLog("Xong!");
                Stop();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                Stop();
            }
        }


        private void Stop()
        {
            allowRun = false;
            if (totalFind > 0)
            {
                if (MessageBox.Show($"Đã lọc được {totalFind}\n Bạn có muốn lưu?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Out");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    workbook1.SaveAs(System.IO.Path.Combine(path, System.IO.Path.GetFileName(filePath1)));
                    totalFind = 0;
                }
                ;
            }
            Reset();
            state = 0;
            btnStart.Text = "Start";
            btnStart.ForeColor = System.Drawing.Color.Green;
        }
        private string GetStringCellFromSheet(string sheetName, string cellName)//string rowName,string columnName
        {
            var sheet = workbook1.Worksheet(sheetName);
            var cell = sheet.Cell(cellName);
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
        private void Reset()
        {
            //workbook1?.Dispose();
            //workbook2?.Dispose();
            //filePath1 = "";
            //filePath2 = "";
            //lblFileName1.Text = "";
            //lblFileName2.Text = "";
            totalFind = 0;
            Reporter(0, 0);
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
        private async void SaoChep(IXLRangeRow[] resultRows, IXLRangeRow storeRow, string dateColumn, string copyColumnName, string storeColumnName)
        {
            List<string> data = new List<string>();
            List<string> list_etd = new List<string>();
            List<string> list_ketqua = new List<string>();
            foreach (var row in resultRows)
            {
                var cell = row.Cell(dateColumn);
                DateTime createDate;

                if (!DateTime.TryParse(cell.GetValue<string>(), CultureInfo.InvariantCulture, DateTimeStyles.None, out createDate))
                {
                    // Nếu không parse được, gán giá trị mặc định
                    createDate = DateTime.MinValue;
                }
                var startCell = row.Cell(copyColumnName);
                string ketqua = startCell.GetString().Replace("\r", "").Replace("\n", "");
                string ghichu = row.Cell(startCell.Address.ColumnNumber + 1).GetString();
                data.Add($"{createDate:dd/MM/yyyy}-{ketqua}-{ghichu}");
                string etd = row.Cell(startCell.Address.ColumnNumber + 2).GetString();
                list_etd.Add(etd);
                list_ketqua.Add(ketqua);
            }
            var storeCell1 = storeRow.Cell(storeColumnName);
            var storeCell2 = storeRow.Cell(storeCell1.Address.ColumnNumber + 1);
            var storeCell3 = storeRow.Cell(storeCell1.Address.ColumnNumber + 2);

            storeCell2.Value = string.Join(Environment.NewLine, data);
            storeCell2.Style.Alignment.WrapText = true;
            //luu cot ETD
            storeCell3.Value = list_etd.OrderByDescending(x => x.Length).FirstOrDefault().ToString();
            //luu cot ket qua
            storeCell1.Value = list_ketqua.Last();
            //Chỉnh định dạng
            storeRow.Worksheet.Row(storeRow.RowNumber()).AdjustToContents();
            while (!CheckReadWriteFile1())
            {
                MessageBox.Show("Không thể ghi dữ liệu, vui lòng đóng file " + lblFileName1.Text);
                await Task.Delay(2000);
            }
            //workbook1.Save();
        }
        void Reporter(int curRows, int totalRows)
        {
            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = curRows;
                progressBar1.Maximum = totalRows;
                lblTotalFind.Text = totalFind.ToString();
                lblReport.Text = $"{curRows}/{totalRows}";
            });
        }
        void WriteLog(string log)
        {
            txtLogs.Invoke((MethodInvoker)delegate
            {
                txtLogs.Text += log + Environment.NewLine;
                txtLogs.SelectionStart = txtLogs.Text.Length;
                txtLogs.ScrollToCaret();
            });
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;
        }

        private void lblFileName1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblFileName1_Click(object sender, EventArgs e)
        {

        }
    }
}
