using ClosedXML.Excel;
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
        public List<OrderForm> LstOrder => _lstOrder;
        List<OrderForm> _lstOrder = new List<OrderForm>();
        frmReporter _frmReporter = null;
        public frmLoadOrderForm()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(listView1, true, null);
        }
        public frmLoadOrderForm(frmReporter frmReporter)
        {
            _frmReporter = frmReporter;
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(listView1, true, null);
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
                        SheetName = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ini.GetString("Info", "Sheet"))),
                        FilePath = Encoding.UTF8.GetString(Encoding.Default.GetBytes(ini.GetString("Info", "Path"))),
                    };
                    if (!_dictSheetInfo.ContainsKey(sheetInfo.SheetID))
                    {
                        _dictSheetInfo[sheetInfo.SheetID] = sheetInfo;
                    }
                    var item = new ListViewItem(sheetInfo.SheetID);
                    item.Name = sheetInfo.SheetID;
                    item.SubItems.Add(sheetInfo.SheetName);
                    item.SubItems.Add(sheetInfo.FilePath);
                    item.SubItems.Add("");
                    listView1.Items.Add(item);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = checkBox1.Checked;
            }
        }


        private async void btnLoad_Click(object sender, EventArgs e)
        {
            _workbooks.Clear();
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
        }
        public class SheetInfo
        {
            public string SheetID { get; set; }
            public string SheetName { get; set; }
            public string FilePath { get; set; }
        }
    }

}
