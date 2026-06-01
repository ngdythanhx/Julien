using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Julian.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian.Forms
{
    public partial class frmBaoCaoTienDo : Form
    {
        XLWorkbook workbook_Customer = null;
        List<string> _lstPathSourceHoiHang = new List<string>();
        private string _filePath_Customer = string.Empty;
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        FileStream _fileStream_Customer = null;
        public frmBaoCaoTienDo()
        {
            InitializeComponent();
            btnStart.Enabled = false;
        }

        private void frmBaoCaoTienDo_Load(object sender, EventArgs e)
        {
            string section = "SourceHoiHang";
            int count = _iniManager.GetInt(section, "count");
            for (int i = 0; i < count; i++)
            {
                string path = _iniManager.GetString(section, i.ToString());
                if (!string.IsNullOrEmpty(path) && path.Length > 4)
                {
                    string fixedPath = Encoding.UTF8.GetString(Encoding.Default.GetBytes(path));
                    _lstPathSourceHoiHang.Add(fixedPath);
                }
            }
            if (_lstPathSourceHoiHang.Count == 0)
            {
                WriteLog("Không có dữ liệu nguồn hối hàng!");
                gb_Customer.Enabled = false;
                gb_OrderForm.Enabled = false;
                btnStart.Enabled = false;
            }
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
        private async void btnSelectFile_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                btnStart.Enabled = false;
                gb_Customer.Enabled = false;
                var dl = new OpenFileDialog();
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension.ToLower() != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!");
                        return;
                    }
                    if (selectedPath != lblFileName_Customer.Text)
                    {
                        WriteLog($"Đang tải dữ liệu file {System.IO.Path.GetFileName(selectedPath)}");
                        await Task.Run(() =>
                        {
                             _fileStream_Customer = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            LoadOptions loadOptions = new LoadOptions();
                            workbook_Customer = new XLWorkbook(_fileStream_Customer);
                            var worksheets = workbook_Customer.Worksheets.ToList();
                            this.Invoke((MethodInvoker)delegate
                            {
                                lblFileName_Customer.Text = fileName;
                                cbSheets_Customer.DataSource = worksheets;
                                cbSheets_Customer.DisplayMember = "Name";
                            });
                            _filePath_Customer = selectedPath;
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

            {
                gb_Customer.Enabled = true;
                if (cbSheets_Customer.DataSource != null &&
                    cbSheets_OrderForm.DataSource != null)
                {
                    btnStart.Enabled = true;
                }
            }
        }

        private async void btnSelectFile_OrderForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                btnStart.Enabled = false;
                gb_OrderForm.Enabled = false;
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
                    if (selectedPath != lblFileName_Customer.Text)
                    {
                        WriteLog($"Đang tải dữ liệu file {System.IO.Path.GetFileName(selectedPath)}");
                        await Task.Run(() =>
                        {
                            using (var stream = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                XLWorkbook workbook = new XLWorkbook(stream);
                                var worksheets = workbook.Worksheets.ToList();
                                this.Invoke((MethodInvoker)delegate
                                {
                                    lblFileName_OrderForm.Text = fileName;
                                    cbSheets_OrderForm.DataSource = worksheets;
                                    cbSheets_OrderForm.DisplayMember = "Name";
                                });
                            }
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

            {
                gb_OrderForm.Enabled = true;
                if (cbSheets_Customer.DataSource != null &&
                    cbSheets_OrderForm.DataSource != null)
                {
                    btnStart.Enabled = true;
                }
            }
        }
        void Reporter(int curRows, int totalRows)
        {
            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value = curRows;
                progressBar1.Maximum = totalRows;
                lblReport.Text = $"{curRows}/{totalRows}";
            });
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            try
            {
                Reporter(0, 0);
                IXLRangeRow[] rangeRows_Customer = null;
                IXLRangeRow[] rangeRows_OrderForm = null;
                var sheet_Customer = (IXLWorksheet)cbSheets_Customer.SelectedValue;
                var sheet_OrderForm = (IXLWorksheet)cbSheets_OrderForm.SelectedValue;
                await Task.Run(() =>
                {
                    rangeRows_Customer = sheet_Customer.RangeUsed().RowsUsed().Skip(1).ToArray();
                    rangeRows_OrderForm = sheet_OrderForm.RangeUsed().RowsUsed().Skip(1).ToArray();
                });

                var lstTasks = new List<Task>();
                var dictCustomer = new Dictionary<string, List<IXLRangeRow>>();
                WriteLog("Tạo thư viện file KH...");
                var task1 = Task.Run(() =>
                {
                    string cotMaDonHang_Customer = txtMaDonKH_Customer.Text;
                    string cotMaLieu_Customer = txtMaHangKH_Customer.Text;
                    foreach (var row in rangeRows_Customer)
                    {
                        string madon_cus = row.Cell(cotMaDonHang_Customer).GetString();
                        string malieu_cus = row.Cell(cotMaLieu_Customer).GetString();
                        string key = madon_cus + "|" + malieu_cus;
                        if (!string.IsNullOrEmpty(madon_cus) && key.Length > 1)
                        {
                            if (!dictCustomer.ContainsKey(key))
                            {
                                dictCustomer[key] = new List<IXLRangeRow>();

                            }
                            dictCustomer[key].Add(row);
                        }
                    }
                });
                lstTasks.Add(task1);

                progressBar1.Style = ProgressBarStyle.Marquee;
                ConcurrentDictionary<string, string> dictHoiHang = new ConcurrentDictionary<string, string>();
                foreach (var path in _lstPathSourceHoiHang)
                {
                    var task = Task.Run(() =>
                    {
                        if (!File.Exists(path))
                        {
                            WriteLog("Không tìm thấy file: " + path);
                            return;
                        }

                        WriteLog($"Load dữ liệu file {Path.GetFileName(path)}...");
                        try
                        {
                            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                using (XLWorkbook workbook = new XLWorkbook(stream))
                                {

                                    var sheet = workbook.Worksheet("總表");
                                    var rangeRows = sheet.RangeUsed().RowsUsed().ToArray();
                                    if (rangeRows.Length >= 5)
                                    {
                                        string colTienDo = string.Empty;
                                        for (int i = 0; i < 5; i++)
                                        {
                                            var cells = rangeRows[i].Cells();
                                            foreach (var cell in cells)
                                            {
                                                string text = cell.GetString();
                                                if (text.Contains("廠商回覆\nTIẾN ĐỘ"))
                                                {
                                                    colTienDo = cell.Address.ColumnLetter;
                                                    break;
                                                }
                                            }
                                        }
                                        if (colTienDo.Length > 0)
                                        {
                                            for (int i = 1; i < rangeRows.Length; i++)
                                            {
                                                var row = rangeRows[i];
                                                string poNhuom = row.Cell("B").GetString();
                                                string tiendo = row.Cell(colTienDo).GetString();
                                                if (poNhuom.Length > 5)
                                                {
                                                    dictHoiHang.TryAdd(poNhuom, tiendo);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            WriteLog($"Load dữ liệu file {Path.GetFileName(path)}...");
                            Console.WriteLine(ex.Message);
                        }
            
                        {
                            WriteLog($"Load xong: {Path.GetFileName(path)}");
                        }
                    });
                    lstTasks.Add(task);
                }
                await Task.WhenAll(lstTasks);

                WriteLog($"Lọc dữ liệu OrderForm: số dòng[{rangeRows_OrderForm.Length}]");
                var locker = new object();
                int processed = 0;
                int totalRows = rangeRows_OrderForm.Length;
                progressBar1.Style = ProgressBarStyle.Blocks;
                WriteLog($"Tổng số dòng {totalRows}");
                Reporter(0, totalRows);
                await Task.Run(() =>
                {
                    ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                    Parallel.ForEach(rangeRows_OrderForm, parallelOptions, row_order =>
                    {
                        string madon_Order = row_order.Cell(txtMaDonKH_OrderForm.Text).GetString();
                        string malieu_Order = row_order.Cell(txtMaHangKH_OrderForm.Text).GetString();
                        string key_Order = madon_Order + "|" + malieu_Order;
                        if (!string.IsNullOrEmpty(madon_Order) && key_Order.Length > 1 && dictCustomer.ContainsKey(key_Order))
                        {
                            string poNhuom_order = row_order.Cell(txtPoNhuom_OrderForm.Text).GetString();
                            lock (locker)
                            {
                                if (dictHoiHang.TryGetValue(poNhuom_order, out string tiendo))
                                {
                                    var lst = dictCustomer[key_Order];
                                    foreach (var row_cus in lst)
                                    {
                                        var cell1 = row_cus.Cell(txtCotGhiDuLieu.Text);
                                        cell1.Value = poNhuom_order;
                                        var cell2 = row_cus.Cell(cell1.Address.ColumnNumber + 1);
                                        cell2.Value = tiendo;
                                    }
                                }
                                else
                                {
                                    var lst = dictCustomer[key_Order];
                                    foreach (var row_cus in lst)
                                    {
                                        var cell1 = row_cus.Cell(txtCotGhiDuLieu.Text);
                                        cell1.Value = poNhuom_order;
                                        var cell2 = row_cus.Cell(cell1.Address.ColumnNumber + 1);
                                        cell2.Value = $"Was not found in HoiHang";
                                    }
                                }
                            }
                        }
                        int done = Interlocked.Increment(ref processed);
                        Reporter(done, totalRows);
                    });
                });
                await Task.Run(() =>
                {
                    string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
                    if (!Directory.Exists(outputFolder))
                    {
                        Directory.CreateDirectory(outputFolder);
                    }
                    string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + "_" + Path.GetFileName(_filePath_Customer);
                    string outputFilePath = Path.Combine(outputFolder, fileName);
                    workbook_Customer.SaveAs(outputFilePath);
                    WriteLog($"Output: {outputFilePath}");
                });
                WriteLog("Xong");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            {
                btnStart.Enabled = true;
            }
        }
    }
}
