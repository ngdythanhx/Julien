using ClosedXML.Excel;
using Julian.Database.DTO;
using Julian.Utils;
using Julian.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Julian_Server
{
    public partial class frmLoadOrderForm : Form
    {
        public Dictionary<string, XLWorkbook> Workbooks => _workbooks;
        Dictionary<string, XLWorkbook> _workbooks = new Dictionary<string, XLWorkbook>();
        private Dictionary<string, SheetInfo> _dictSheetInfo = new Dictionary<string, SheetInfo>();
        private List<SheetInfo> _lstSheetInfo = new List<SheetInfo>();
        public List<OrderForm> LstOrder => _lstOrder;
        List<OrderForm> _lstOrder = new List<OrderForm>();
        frmReporter _frmReporter = null;
        ListView listView1 = new ListView();
        public frmLoadOrderForm()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(listView1, true, null);
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
        }
        public frmLoadOrderForm(frmReporter frmReporter)
        {
            _frmReporter = frmReporter;
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(listView1, true, null);
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
        }
        private void frmLoadOrderForm_Load(object sender, EventArgs e)
        {
            string pathOrders = Path.Combine(Directory.GetCurrentDirectory(), "OrderForms");
            if (Directory.Exists(pathOrders))
            {
                var files = Directory.GetFiles(pathOrders, "*.ini");
                foreach (var file in files)
                {
                    var ini = new IniManager(file);
                    var sheetInfo = new SheetInfo()
                    {
                        SheetID = Path.GetFileNameWithoutExtension(file),
                        Employee = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ini.GetString("Info", "Employee"))),
                        SheetName = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ini.GetString("Info", "Sheet"))),
                        FilePath = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ini.GetString("Info", "Path"))),
                    };
                    if (!_dictSheetInfo.ContainsKey(sheetInfo.SheetID))
                    {
                        _dictSheetInfo[sheetInfo.SheetID] = sheetInfo;
                    }
                    _lstSheetInfo.Add(sheetInfo);
                    var item = new ListViewItem(sheetInfo.SheetID);
                    item.Name = sheetInfo.SheetID;
                    item.SubItems.Add(sheetInfo.SheetName);
                    item.SubItems.Add(sheetInfo.FilePath);
                    item.SubItems.Add("");
                    listView1.Items.Add(item);
                }
                dgvMain.DataSource = new SortableBindingList<SheetInfo>(_lstSheetInfo);
                foreach (var col in dgvMain.Columns.Cast<DataGridViewColumn>())
                {
                    if (col.Name == "col_checked")
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                }
                //col_checked.ReadOnly = false;
                timer1.Start();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = checkBox1.Checked;
            }
            foreach (var s in _lstSheetInfo)
            {
                s.Checked = checkBox1.Checked;
            }
        }

        private async void LoadX()
        {
            _workbooks.Clear();
            btnLoad.Enabled = false;
            checkBox1.Enabled = false;
            col_checked.ReadOnly = true;
            dgvMain.Enabled = false;
            //try
            //{
                var tasks1 = new List<Task>();
                var lstChecked = _lstSheetInfo.Where(s => s.Checked).ToList();
                foreach (var sheetInfoGroupByFilePath in lstChecked.GroupBy(s => s.FilePath))
                {
                    foreach (var s in sheetInfoGroupByFilePath)
                    {
                        s.Status = "Đường dẫn không hợp lệ!";
                    }
                    var sheetInfo = sheetInfoGroupByFilePath.First();
                    if (!File.Exists(sheetInfo.FilePath))
                    {
                        sheetInfo.Status = "Đường dẫn không hợp lệ!";
                        return;
                    }
                    foreach (var s in sheetInfoGroupByFilePath)
                    {
                        s.Status = "Đang load file...";
                    }
                    //if (!_lstSheetInfo.Any(s => s.SheetName == sheetInfo.SheetName && s.FilePath == sheetInfo.FilePath))
                    //    newLstSheetInfo.Add(sheetInfo);
                    if (!_workbooks.ContainsKey(sheetInfo.FilePath))
                    {
                        tasks1.Add(Task.Run(() =>
                        {
                            using (var fs = new FileStream(
                                sheetInfo.FilePath,
                                FileMode.Open,
                                FileAccess.Read,
                                FileShare.ReadWrite))
                            {
                                _workbooks[sheetInfo.FilePath] = new XLWorkbook(fs);
                            }
                        }));
                        await Task.Delay(50);
                    }
                }
                await Task.WhenAll(tasks1);
                var tasks2 = new List<Task>();
                foreach (var sheetInfoGroupBySheetName in lstChecked.GroupBy(s => new { s.SheetName, s.FilePath }))
                {
                    var sheetInfo = sheetInfoGroupBySheetName.First();
                    var worksheets = _workbooks[sheetInfo.FilePath].Worksheets.ToArray();
                    //var lstItem = listView1.Items.Cast<ListViewItem>().Where(item => item.SubItems[2].Text == sheetInfo.SheetName).ToArray();
                    var sheet = worksheets.FirstOrDefault(s => s.Name == sheetInfo.SheetName && s.Visibility == XLWorksheetVisibility.Visible);
                    if (sheet == null)
                    {
                        foreach (var s in sheetInfoGroupBySheetName)
                        {
                            s.Status = $"Không tìm thấy Sheet [{s.SheetName}] trong file!";
                        }
                        return;
                    }
                    tasks2.Add(Task.Run(() =>
                    {
                        //try
                        //{
                            var ini = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "OrderForms", sheetInfo.SheetID + ".ini"));
                            int startIndex = ini.GetInt("Info", "StartIndex");
                            if (startIndex > 1)
                            {
                                var lastColumnUsed = sheet.LastColumnUsed();
                                var lastRowUsedIndex = sheet.LastRowUsed().RowNumber();
                                if (startIndex > lastRowUsedIndex) return;
                                var rangeRows = sheet.Range($"A{startIndex}:{lastColumnUsed.ColumnLetter()}{lastRowUsedIndex}").Rows().ToArray();
                                if (rangeRows.Length > 0)
                                {
                                    var c = new
                                    {
                                        MaKH = ini.GetString("OrderForm", "MaKH"),
                                        NgayDat = ini.GetString("OrderForm", "NgayDat"),
                                        Brand = ini.GetString("OrderForm", "Brand"),
                                        PONhuom = ini.GetString("OrderForm", "PONhuom"),
                                        PONhuomMoi = ini.GetString("OrderForm", "PONhuomMoi"),
                                        MaDonKH = ini.GetString("OrderForm", "MaDonKH"),
                                        MaHangKH = ini.GetString("OrderForm", "MaHangKH"),
                                        LieuKH = ini.GetString("OrderForm", "LieuKH"),
                                        LieuThayThe = ini.GetString("OrderForm", "LieuThayThe"),
                                        Kho = ini.GetString("OrderForm", "Kho"),
                                        MauKH = ini.GetString("OrderForm", "MauKH"),
                                        MauThayThe = ini.GetString("OrderForm", "MauThayThe"),
                                        MoTaMau = ini.GetString("OrderForm", "MoTaMau"),
                                        SLDat = ini.GetString("OrderForm", "SLDat"),
                                        DonGia = ini.GetString("OrderForm", "DonGia"),
                                        TongTien = ini.GetString("OrderForm", "TongTien"),
                                        ETD = ini.GetString("OrderForm", "ETD"),
                                        NgayXuat = ini.GetString("OrderForm", "NgayXuat"),
                                        InvoiceHoaDon = ini.GetString("OrderForm", "InvoiceHoaDon"),
                                        InvoicePGH = ini.GetString("OrderForm", "InvoicePGH"),
                                        Article = ini.GetString("OrderForm", "Article"),
                                        BillNumber = ini.GetString("OrderForm", "BillNumber"),
                                        ShippingMethod = ini.GetString("OrderForm", "ShippingMethod"),
                                        T1 = ini.GetString("OrderForm", "T1"),
                                        Season = ini.GetString("OrderForm", "Season"),
                                        ETDNote = ini.GetString("OrderForm", "ETDNote"),
                                    };
                                    var lst = new ConcurrentBag<OrderForm>();
                                    ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                                    Parallel.For(0, rangeRows.Length, i =>
                                    {
                                        var row = rangeRows[i];
                                        string lieuKh = row.Cell(c.LieuKH).GetString();
                                        lieuKh = lieuKh.Replace(" ", "").Replace("\"", "").Replace("'","");
                                        var order = new OrderForm()
                                        {
                                            MaKH = row.Cell(c.MaKH).GetString(),
                                            NgayDat = row.Cell(c.NgayDat).TryGetValue<DateTime>(out var ngaydat) ? ngaydat : DateTime.MinValue,
                                            Brand = row.Cell(c.Brand).GetString(),
                                            PONhuom = row.Cell(c.PONhuom).GetString(),
                                            PONhuomMoi = row.Cell(c.PONhuomMoi).GetString(),
                                            MaHangKH = row.Cell(c.MaHangKH).GetString(),
                                            MaDonKH = row.Cell(c.MaDonKH).GetString(),
                                            LieuKH = lieuKh == "YHM1093EPM558" ? "YHM1093EPM5" : lieuKh,
                                            LieuThayThe = row.Cell(c.LieuThayThe).GetString(),
                                            Kho = row.Cell(c.Kho).TryGetValue<int>(out var w) ? w : -1,
                                            MauKH = row.Cell(c.MauKH).GetString(),
                                            MauThayThe = row.Cell(c.MauThayThe).GetString(),
                                            SLDat = row.Cell(c.SLDat).TryGetValue<float>(out var slDat) ? slDat : -1,
                                            DonGia = row.Cell(c.DonGia).TryGetValue<float>(out var unitPrice) ? unitPrice : -1,
                                            TongTien = row.Cell(c.TongTien).TryGetValue<double>(out var amount) ? amount : -1,
                                            //ETD = row.Cell(c.ETD).TryGetValue<DateTime>(out var etd) ? etd : DateTime.MinValue,
                                            ETD = row.Cell(c.ETD).TryGetValue<DateTime>(out var etd) ? etd.ToString("yyyy-MM-dd") : "",
                                            NgayXuat = row.Cell(c.NgayXuat).TryGetValue<DateTime>(out var ngayXuat) ? ngayXuat : null,
                                            InvoiceHoaDon = row.Cell(c.InvoiceHoaDon).GetString(),
                                            InvoicePGH = row.Cell(c.InvoicePGH).GetString(),
                                            Article = row.Cell(c.Article).GetString(),
                                            BillNumber = row.Cell(c.BillNumber).GetString(),
                                            ShippingMethod = row.Cell(c.ShippingMethod).GetString(),
                                            T1 = row.Cell(c.T1).GetString(),
                                            Season = row.Cell(c.Season).GetString(),
                                            ETDNote = row.Cell(c.ETDNote).GetString(),
                                        };
                                        if (!string.IsNullOrEmpty(order.MaKH))
                                            lst.Add(order);
                                    });
                                    _lstOrder.AddRange(lst);
                                }
                            }
                        //}
                        //catch
                        //{

                        //}
                    }));
                    foreach (var s in sheetInfoGroupBySheetName)
                    {
                        s.Status = $"Đang load sheet {s.SheetName}...";
                    }
                    await Task.Delay(10);
                }
                await Task.WhenAll(tasks2);
                if (_frmReporter != null)
                {
                    _frmReporter.SetDataSourceControl(_lstOrder);
                }
                this.Close();
            /*}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLoad.Enabled = true;
                checkBox1.Enabled = true;
                col_checked.ReadOnly = false;
                dgvMain.Enabled = true;
            }*/
        }
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            LoadX();
           /* _workbooks.Clear();
            btnLoad.Enabled = false;
            listView1.Enabled = false;
            checkBox1.Enabled = false;
            try
            {
                var lstSheet = new List<SheetInfo>();
                var tasks1 = new List<Task>();
                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.Checked)
                    {
                        var info = _dictSheetInfo[item.SubItems[0].Text];
                        if (!File.Exists(info.FilePath))
                        {
                            item.SubItems[3].Text = "Đường dẫn không hợp lệ!";
                            return;
                        }
                        item.SubItems[3].Text = "Đang load file...";
                        if (!lstSheet.Any(s => s.SheetName == info.SheetName && s.FilePath == info.FilePath))
                            lstSheet.Add(info);
                        if (!_workbooks.ContainsKey(info.FilePath))
                        {
                            tasks1.Add(Task.Run(() =>
                            {
                                using (var fs = new FileStream(
                                    item.SubItems[2].Text,
                                    FileMode.Open,
                                    FileAccess.Read,
                                    FileShare.ReadWrite))
                                {
                                    _workbooks[item.SubItems[2].Text] = new XLWorkbook(fs);
                                }
                            }));
                            await Task.Delay(50);
                        }
                    }
                }
                await Task.WhenAll(tasks1);
                var tasks2 = new List<Task>();
                foreach (var sheetInfo in lstSheet)
                {
                    var sheets = _workbooks[sheetInfo.FilePath].Worksheets.ToArray();
                    var lstItem = listView1.Items.Cast<ListViewItem>().Where(item => item.SubItems[2].Text == sheetInfo.SheetName).ToArray();
                    var sheet = sheets.FirstOrDefault(s => s.Name == sheetInfo.SheetName);
                    if (sheet == null)
                    {
                        foreach (var item in lstItem)
                        {
                            item.SubItems[0].Text = $"Không tìm thấy Sheet [{sheetInfo.SheetName}] trong file!";
                        }
                        return;
                    }
                    tasks2.Add(Task.Run(() =>
                    {
                        try
                        {
                            var ini = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "OrderForms", sheetInfo.SheetID + ".ini"));
                            int startIndex = ini.GetInt("Info", "StartIndex");
                            if (startIndex > 1)
                            {
                                var lastColumnUsed = sheet.LastColumnUsed();
                                var lastRowUsedIndex = sheet.LastRowUsed().RowNumber();
                                if (startIndex > lastRowUsedIndex) return;
                                var rangeRows = sheet.Range($"A{startIndex}:{lastColumnUsed.ColumnLetter()}{lastRowUsedIndex}").Rows().ToArray();
                                if (rangeRows.Length > 2)
                                {
                                    var c = new
                                    {
                                        MaKH = ini.GetString("OrderForm", "MaKH"),
                                        NgayDat = ini.GetString("OrderForm", "NgayDat"),
                                        Brand = ini.GetString("OrderForm", "Brand"),
                                        PONhuom = ini.GetString("OrderForm", "PONhuom"),
                                        PONhuomMoi = ini.GetString("OrderForm", "PONhuomMoi"),
                                        MaDonKH = ini.GetString("OrderForm", "MaDonKH"),
                                        MaHangKH = ini.GetString("OrderForm", "MaHangKH"),
                                        LieuKH = ini.GetString("OrderForm", "LieuKH"),
                                        LieuThayThe = ini.GetString("OrderForm", "LieuThayThe"),
                                        Kho = ini.GetString("OrderForm", "Kho"),
                                        MauKH = ini.GetString("OrderForm", "MauKH"),
                                        MauThayThe = ini.GetString("OrderForm", "MauThayThe"),
                                        MoTaMau = ini.GetString("OrderForm", "MoTaMau"),
                                        SLDat = ini.GetString("OrderForm", "SLDat"),
                                        DonGia = ini.GetString("OrderForm", "DonGia"),
                                        TongTien = ini.GetString("OrderForm", "TongTien"),
                                        ETD = ini.GetString("OrderForm", "ETD"),
                                        NgayXuat = ini.GetString("OrderForm", "NgayXuat"),
                                        InvoiceHoaDon = ini.GetString("OrderForm", "InvoiceHoaDon"),
                                        InvoicePGH = ini.GetString("OrderForm", "InvoicePGH"),
                                        Article = ini.GetString("OrderForm", "Article"),
                                        BillNumber = ini.GetString("OrderForm", "BillNumber"),
                                        ShippingMethod = ini.GetString("OrderForm", "ShippingMethod"),
                                        T1 = ini.GetString("OrderForm", "T1"),
                                        Season = ini.GetString("OrderForm", "Season"),
                                    };
                                    var lst = new ConcurrentBag<OrderForm>();
                                    ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                                    Parallel.For(0, rangeRows.Length, i =>
                                    {
                                        var row = rangeRows[i];
                                        var order = new OrderForm()
                                        {
                                            MaKH = row.Cell(c.MaKH).GetString(),
                                            NgayDat = row.Cell(c.NgayDat).TryGetValue<DateTime>(out var ngaydat) ? ngaydat : DateTime.MinValue,
                                            Brand = row.Cell(c.Brand).GetString(),
                                            PONhuom = row.Cell(c.PONhuom).GetString(),
                                            PONhuomMoi = row.Cell(c.PONhuomMoi).GetString(),
                                            MaHangKH = row.Cell(c.MaHangKH).GetString(),
                                            MaDonKH = row.Cell(c.MaDonKH).GetString(),
                                            LieuKH = row.Cell(c.LieuKH).GetString(),
                                            LieuThayThe = row.Cell(c.LieuThayThe).GetString(),
                                            Kho = row.Cell(c.Kho).TryGetValue<int>(out var w) ? w : -1,
                                            MauKH = row.Cell(c.MauKH).GetString(),
                                            MauThayThe = row.Cell(c.MauThayThe).GetString(),
                                            SLDat = row.Cell(c.SLDat).TryGetValue<float>(out var slDat) ? slDat : -1,
                                            DonGia = row.Cell(c.DonGia).TryGetValue<float>(out var unitPrice) ? unitPrice : -1,
                                            TongTien = row.Cell(c.TongTien).TryGetValue<double>(out var amount) ? amount : -1,
                                            ETD = row.Cell(c.ETD).TryGetValue<DateTime>(out var etd) ? etd : DateTime.MinValue,
                                            NgayXuat = row.Cell(c.NgayXuat).TryGetValue<DateTime>(out var ngayXuat) ? ngayXuat : DateTime.MinValue,
                                            InvoiceHoaDon = row.Cell(c.InvoiceHoaDon).GetString(),
                                            InvoicePGH = row.Cell(c.InvoicePGH).GetString(),
                                            Article = row.Cell(c.Article).GetString(),
                                            BillNumber = row.Cell(c.BillNumber).GetString(),
                                            ShippingMethod = row.Cell(c.ShippingMethod).GetString(),
                                            T1 = row.Cell(c.T1).GetString(),
                                            Season = row.Cell(c.Season).GetString(),
                                        };
                                        if (!string.IsNullOrEmpty(order.MaKH))
                                            lst.Add(order);
                                    });
                                    _lstOrder.AddRange(lst);
                                }
                            }
                        }
                        catch
                        {

                        }
                    }));
                    foreach (var item in lstItem)
                    {
                        item.SubItems[0].Text = $"Đang load sheet {sheetInfo.SheetName}...";
                    }
                    await Task.Delay(100);
                }
                await Task.WhenAll(tasks2);
                if (_frmReporter != null)
                {
                    _frmReporter.SetDataSourceControl(_lstOrder);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLoad.Enabled = true;
                listView1.Enabled = true;
                checkBox1.Enabled = true;
            }
           */

        }
        public class SheetInfo
        {
            public bool Checked { get; set; } = true;
            public string SheetID { get; set; }
            public string Employee { get; set; }
            public string SheetName { get; set; }
            public string FilePath { get; set; }
            public string Status { get; set; }
        }
        public class OrderFormInfo
        {
            public string FilePath { get; set; }
            public List<string> Sheets { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadX();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dgvMain.Refresh();
        }
    }

}
