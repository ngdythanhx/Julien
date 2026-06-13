using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Julian.Database.DTO;
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
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Path = System.IO.Path;

namespace Julian_Server
{
    public partial class frmThoaBeSHC : Form
    {
        string _filePath1 = "";
        string _filePath_Cus = string.Empty;
        XLWorkbook workbook_Order = null;
        IniManager _iniManager = new IniManager(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        ConcurrentBag<ThoaBeSHC> _lstSHC = new ConcurrentBag<ThoaBeSHC>();
        public frmThoaBeSHC()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            dgvData.AutoGenerateColumns = false;
            dgvData.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            EnableControl(false);
        }
        private void frmThoaBeSHC_Load(object sender, EventArgs e)
        {
            txtMaDonKH.Text = _iniManager.GetString("ThoaBeSHC", "MaDonKH");
            txtMaHang.Text = _iniManager.GetString("ThoaBeSHC", "MaHang");
            txtNgayXuatHang.Text = _iniManager.GetString("ThoaBeSHC", "NgayXuatHang");
            txtGhiChuXuatHang.Text = _iniManager.GetString("ThoaBeSHC", "GhiChuXuatHang");
            chkBackup.Checked = true;
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
        private async void btnSelectFile_OrderForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                btnSelectFile_OrderForm.Enabled = false;
                EnableControl(false);
                var dl = new OpenFileDialog();
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension.ToLower() != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (!CheckReadWriteFile(selectedPath))
                    {
                        MessageBox.Show("File đang mở, đóng lại trước khi thao tác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }




                    workbook_Order?.Dispose();
                    workbook_Order = null;
                    SetStatus("Đang load OrderForm...");
                    await Task.Run(() =>
                    {
                        workbook_Order?.Dispose();
                        _filePath1 = selectedPath;
                        workbook_Order = new XLWorkbook(selectedPath);
                    });
                    //dsadasdsa
                    foreach (var ws in workbook_Order.Worksheets)
                    {
                        foreach (var cell in ws.CellsUsed())
                        {
                            try
                            {
                                if(cell.DataType== XLDataType.DateTime)
                                {
                                    var a = cell.Value;
                                    DateTime d;
                                    if(!cell.TryGetValue(out d))
                                    {
                                        Console.WriteLine(
                                            $"Sheet={ws.Name}, Cell={cell.Address}");
                                    }
                                    if(d.Month==2 && d.Day>28)
                                    {
                                        Console.WriteLine(
    $"Sheet={ws.Name}, Cell={cell.Address}");
                                    }
                                }    
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(
                                    $"Sheet={ws.Name}, Cell={cell.Address}");
                            }
                        }
                    }
                    //dsadsadsa
                    var worksheets = workbook_Order.Worksheets.ToList();
                    lblFileName_OrderForm.Text = fileName;
                    cbSheet.DataSource = worksheets;
                    cbSheet.DisplayMember = "Name";
                    if (workbook_Order != null)
                    {
                        EnableControl(true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSelectFile_OrderForm.Enabled = true;
                SetStatus("...");
            }

        }
        private string SelectFile_Cus()
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
                    if (extension.ToLower() == ".xlsx")
                    {
                        return selectedPath;
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }

        }
        private bool ValidateInputs()
        {
            if (workbook_Order == null)
            {
                return false;
            }
            return true;
        }
        private void EnableControl(bool enable)
        {
            cbSheet.Enabled = enable;
            btnStart.Enabled = enable;
            txtMaDonKH.Enabled = enable;
            txtMaHang.Enabled = enable;
            txtNgayXuatHang.Enabled = enable;
            txtGhiChuXuatHang.Enabled = enable;
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            EnableControl(false);
            try
            {
                _filePath_Cus = SelectFile_Cus();
                if (string.IsNullOrEmpty(_filePath_Cus))
                {
                    SetStatus("Lỗi chọn file KH!");
                    return;
                }
                if (!ValidateInputs())
                {
                    SetStatus("Lỗi file OrderForm!");
                    return;
                }
                this.ActiveControl = label1;
                _iniManager.WriteString("ThoaBeSHC", "MaDonKH", txtMaDonKH.Text);
                _iniManager.WriteString("ThoaBeSHC", "MaHang", txtMaHang.Text);
                _iniManager.WriteString("ThoaBeSHC", "NgayXuatHang", txtNgayXuatHang.Text);
                _iniManager.WriteString("ThoaBeSHC", "GhiChuXuatHang", txtGhiChuXuatHang.Text);
                if (chkBackup.Checked)
                {
                    if (!await Backup())
                    {
                        SetStatus("Lỗi Backup!");
                        return;
                    }
                }
                _lstSHC = new ConcurrentBag<ThoaBeSHC>();
                await Task.Run(async () =>
                {
                    using (FileStream stream = new FileStream(_filePath_Cus, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var workbook = new XLWorkbook(stream))
                        {
                            var sheets = workbook.Worksheets;
                            foreach (var sheet in sheets)
                            {
                                if (sheet.Visibility == XLWorksheetVisibility.Visible)
                                {
                                    SetStatus($"Đang lọc sheet: {sheet.Name}");
                                    GetListCustomer(sheet);
                                }
                            }
                        }
                    }
                    //_lstSHC.Reverse();
                });
                dgvData.DataSource = _lstSHC.ToList();
                if (_lstSHC.Count == 0)
                {
                    SetStatus($"Không có dữ liệu file: {_filePath_Cus}");
                    return;
                }
                dgvData.Refresh();
                SetStatus($"Đang lấy dữ liệu sheet '{cbSheet.DisplayMember}' OrderForm...");
                IXLWorksheet sheet_OrderForm = (IXLWorksheet)cbSheet.SelectedValue;
                IXLRangeRow[] rangeRows_Order = null;
                await Task.Run(async () =>
                {
                    rangeRows_Order = sheet_OrderForm.RangeUsed().RowsUsed().ToArray();
                });
                if (rangeRows_Order.Length == 0)
                {
                    SetStatus($"Không có dữ liệu trong sheet '{cbSheet.DisplayMember}' OrderForm!");
                }



                int totalRows = rangeRows_Order.Length;
                int count = 0;
                int found = 0;
                await Task.Run(async () =>
                {
                    ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                    Parallel.ForEach(rangeRows_Order, parallelOptions, row =>
                    {
                        string madon = row.Cell(txtMaDonKH.Text).GetString();
                        string mahang = row.Cell(txtMaHang.Text).GetString();
                        if (_lstSHC.FirstOrDefault(a => a.PurchOrder == madon && a.Material == mahang) is ThoaBeSHC shc)
                        {
                            row.Cell(txtNgayXuatHang.Text).Value = shc.PurchOrderDate.Date;
                            row.Cell(txtGhiChuXuatHang.Text).Value = shc.SHCRTD;
                            shc.Found = "Yes";
                            Interlocked.Increment(ref found);
                        }
                        Interlocked.Increment(ref count);
                        SetStatus($"Tiến trình: {count}/{totalRows} Found: {found}");
                    });
                });

                dgvData.Refresh();
                SetStatus("Đang lưu dữ liệu...");
                await Task.Run(() =>
                {
                    string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
                    if (!Directory.Exists(outputFolder))
                    {
                        Directory.CreateDirectory(outputFolder);
                    }
                    string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + "_" + Path.GetFileName(_filePath1);
                    string outputFilePath = Path.Combine(outputFolder, fileName);
                    workbook_Order.CalculateMode = XLCalculateMode.Manual;
                    // workbook_Order.SaveAs(outputFilePath);
                    using var ms = new MemoryStream();
                    workbook_Order.SaveAs(ms);

                    File.WriteAllBytes(outputFilePath, ms.ToArray());
                });
                SetStatus($"Xong! Rows:{totalRows} Found:{found}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControl(true);
            }
        }
        public static string GetMD5(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        private async Task<bool> Backup()
        {
            SetStatus("Đang tạo backup...");
            string backupFolder = Path.Combine(Directory.GetCurrentDirectory(), "Backup");
            if (!Directory.Exists(backupFolder))
            {
                Directory.CreateDirectory(backupFolder);
            }
            string fileName = DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + "_" + Path.GetFileName(_filePath1);
            string backupFilePath = Path.Combine(backupFolder, fileName);
            await Task.Run(() =>
            {
                File.Copy(_filePath1, backupFilePath, true);
            });
            return File.Exists(backupFilePath);
        }
        private async void GetListCustomer(IXLWorksheet worksheet)
        {
            var rangeRows = worksheet.RangeUsed().RowsUsed().ToArray();
            if (rangeRows.Length <= 1)
                return;
            var columns = rangeRows[0];
            if (columns.Cell("C").GetString().Contains("Purch.Order") &&
                columns.Cell("D").GetString().Contains("Material") &&
                columns.Cell("G").GetString().Contains("Purch.Order Date"))
            {
                for (int i = 1; i < rangeRows.Length; i++)
                {
                    try
                    {
                        var row = rangeRows[i];
                        var item = new ThoaBeSHC()
                        {
                            SheetName = worksheet.Name,
                            PurchOrder = row.Cell("C").GetString(),
                            Material = row.Cell("D").GetString(),
                            PurchOrderDate = row.Cell("G").GetDateTime(),
                            SHCRTD = row.Cell("I").GetString(),
                            OpenQty = row.Cell("J").GetDouble(),
                            Cutstartdate = row.Cell("K").GetFormattedString(),
                            Model = row.Cell("L").GetString(),
                            ModelName = row.Cell("M").GetString(),
                            Article = row.Cell("N").GetString(),
                        };
                        if (item.PurchOrder == "" || item.Material == "")
                            continue;
                        if (_lstSHC.FirstOrDefault(shc => shc.PurchOrder == item.PurchOrder && shc.Material == item.Material) is ThoaBeSHC thoabeSHC)
                        {
                            thoabeSHC.SheetName = worksheet.Name;
                            thoabeSHC.OpenQty += item.OpenQty;
                        }
                        else
                        {
                            _lstSHC.Add(item);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        private DataTable ToDataTable(List<IXLRangeRow> rows)
        {
            DataTable dt = new DataTable();

            if (rows == null || rows.Count == 0)
                return dt;

            int colCount = rows[0].CellCount();

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
            return dt;
        }
        private void SetStatus(string status)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Text = status;
            });
        }
        public class mlm
        {
            public string code { get; set; }
            public string name { get; set; }
            public int id { get; set; }
            public int width { get; set; }
            public string Composition { get; set; }
            public string rate { get; set; }
            public int weight { get; set; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var wb = new XLWorkbook(@"C:\Users\Admin\Desktop\Cong viec\MLM.xlsx");
            var s1 = wb.Worksheet("Sheet1");
            var s2 = wb.Worksheet("MLM");
            var rangeRows1 = s1.RangeUsed().RowsUsed().ToArray();
            var lstM = new List<mlm>();
            foreach (var row in rangeRows1)
            {
                string code = row.Cell(1).GetString().Replace(" ", "").Replace("\"", "").Replace("-", "");
                if (code.Length > 3 && !lstM.Any(m => m.code == code) && row.Cell(2).TryGetValue<int>(out int mId) && row.Cell(3).TryGetValue<int>(out int mWidth) && row.Cell(6).TryGetValue<int>(out int mWeight))
                {
                    var m = new mlm()
                    {
                        code = code,
                        name = row.Cell(1).GetString(),
                        id = mId,
                        width = mWidth,
                        Composition = row.Cell(4).GetString(),
                        rate = row.Cell(5).GetString(),
                        weight = mWeight,
                    };
                    lstM.Add(m);
                }
            }
            var rangeRows2 = s2.Range($"A1:F{lstM.Count + 1}").Rows().ToArray();
            for (int i = 0; i < lstM.Count; i++)
            {
                var m = lstM[i];
                var row = rangeRows2[i + 1];
                row.Cell(1).Value = m.code;
                row.Cell(2).Value = m.name;
                row.Cell(3).Value = m.id;
                row.Cell(4).Value = m.width;
                row.Cell(5).Value = m.Composition;
                row.Cell(6).Value = m.rate;
                row.Cell(6).Value = m.weight;
            }
            wb.Save();
        }
    }
    public class ThoaBeSHC
    {
        public string SheetName { get; set; }
        public string PurchOrder { get; set; }
        public string Material { get; set; }
        public DateTime PurchOrderDate { get; set; }
        public string SHCRTD { get; set; }
        public double OpenQty { get; set; }
        public string Cutstartdate { get; set; }
        public string Model { get; set; }
        public string ModelName { get; set; }
        public string Article { get; set; }
        public string Found { get; set; } = "No";
    }
}
