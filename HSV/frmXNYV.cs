using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Presentation;
using Julian.Database.DTO;
using Julian.Helper;
using Julian.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSV
{
    public partial class frmXNYV : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        XLWorkbook _workbook;
        public List<OrderForm> LstOrder => _lstOrder.ToList();
        ConcurrentBag<OrderForm> _lstOrder = new ConcurrentBag<OrderForm>();
        List<CS> _lstCS = new List<CS>();
        BindingSource _bindingSource = new BindingSource();
        public frmXNYV()
        {
            InitializeComponent();
        }

        private void frmXNYV_Load(object sender, EventArgs e)
        {
            string section = "XNYV";
            txtFileInput.Text = Encoding.UTF8.GetString(Encoding.Default.GetBytes(_iniManager.GetString(section, "FileInput", "")));
        }
        private class CS
        {
            public string KhachHang { get; set; }
            public string Lieu { get; set; }
            public string Mau { get; set; }
            public string Art { get; set; }
            public string So { get; set; }
            public string FlSo { get; set; }
        }
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileInput.Text) || !File.Exists(txtFileInput.Text))
            {
                MessageBox.Show("Đường dẫn không hợp lệ!");
                return;
            }
            using (var fs = new FileStream(txtFileInput.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                _workbook = new XLWorkbook(fs);
            }
            //_iniManager.WriteString("XNYV", "FileInput", txtFileInput.Text);
            var lstFile = new string[] { "HSV", "HWK" };
            var lstTask = new List<Task>();
            foreach (string iniFile in lstFile)
            {
                lstTask.Add(Task.Run(() =>
                {
                    var sheet = _workbook.Worksheet(iniFile);
                    var ini = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "OrderForms", iniFile + ".ini"));
                    int startIndex = ini.GetInt("Info", "StartIndex");
                    if (startIndex > 1)
                    {
                        var lastColumnUsed = sheet.LastColumnUsed();
                        var lastRowUsedIndex = sheet.LastRowUsed().RowNumber();
                        if (startIndex > lastRowUsedIndex) return;
                        //var rangeRows = sheet.Range($"A{startIndex}:{lastColumnUsed.ColumnLetter()}{lastRowUsedIndex}").Rows().ToArray();
                        var rangeRows = sheet.Range($"A{startIndex}:{"AZ"}{lastRowUsedIndex}").Rows().ToArray();
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
                                lieuKh = lieuKh.Replace(" ", "").Replace("\"", "").Replace("'", "");
                                float slDat = -1;
                                var tryParseSlDat = row.Cell(c.SLDat).TryGetValue<float>(out slDat);
                                float unitPrice = -1;
                                row.Cell(c.DonGia).TryGetValue<float>(out unitPrice);
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
                                    SLDat = slDat,
                                    DonGia = unitPrice,
                                    TongTien = slDat == -1 || unitPrice == -1 ? 0 : slDat * unitPrice,
                                    //ETD = row.Cell(c.ETD).TryGetValue<DateTime>(out var etd) ? etd : DateTime.MinValue,
                                    ETD = row.Cell(c.ETD).TryGetValue<DateTime>(out var etd) ? etd.ToString("yyyy-MM-dd") : "",
                                    NgayXuat = row.Cell(c.NgayXuat).GetString() == "" ? null : row.Cell(c.NgayXuat).TryGetValue<DateTime>(out var ngayXuat) ? ngayXuat : DateTime.MinValue,
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
                            foreach (var item in lst)
                            {
                                _lstOrder.Add(item);
                            }
                        }
                    }
                }));
            }
            lstTask.Add(Task.Run(() =>
            {
                var sheet = _workbook.Worksheet("CS");
                var rangeRows = sheet.Range("A3:Q1000").Rows().ToList();
                foreach (var row in rangeRows)
                {
                    var cs = new CS()
                    {
                        KhachHang = row.Cell("A").GetString(),
                        Lieu = row.Cell("D").GetString(),
                        Mau = row.Cell("E").GetString(),
                        Art = row.Cell("G").GetString(),
                        So = row.Cell("N").GetString(),
                        FlSo = row.Cell("O").GetString(),
                    };
                    _lstCS.Add(cs);
                }
            }));
            await Task.WhenAll(lstTask);
            var result = _lstCS.Join(
                _lstOrder,
                cs => cs.Art,
                po => po.Article.Length >= 6 ? po.Article.Substring(po.Article.Length - 6) : po.Article,
                (cs, po) => new
                {
                    po.PONhuom,
                    po.SLDat,
                    cs.Art,
                    cs.So,
                }).ToList();
            var dt = ConvertData.ToDataTable(_lstOrder.ToList());
            _bindingSource.DataSource = dt;
            dgvMain.DataSource = _bindingSource;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (_bindingSource.DataSource is not DataTable dt)
                return;
            string kh = txtKH.Text;
            string po = txtKH.Text;
            string art = txtKH.Text;
            List<string> conditions_orderform = new();
            conditions_orderform.Add(
                $"([PONhuom] IS NULL OR Convert([PO], 'System.String') = '' OR Convert([PONhuom], 'System.String') LIKE '%{po}%'"
            );
            List<string> conditions_art = new();
            dt.DefaultView.RowFilter = string.Join(" AND ", conditions_orderform);
            /* foreach (Control ctrl in pnlFilter.Controls)
             {
                 if (string.IsNullOrWhiteSpace(ctrl.Text))
                     continue;

                 string columnName = ctrl.Tag.ToString();
                 string value = ctrl.Text.Trim().Replace("'", "''");

                 if (value.Equals("null", StringComparison.OrdinalIgnoreCase))
                 {
                     conditions.Add(
                         $"([{columnName}] IS NULL OR Convert([{columnName}], 'System.String') = '' OR Convert([{columnName}], 'System.String') = 'null')"
                     );
                 }
                 else if (value.Equals("blank", StringComparison.OrdinalIgnoreCase))
                 {
                     conditions.Add(
                         $"([{columnName}] IS NULL OR Convert([{columnName}], 'System.String') = '' OR Convert([{columnName}], 'System.String') = 'blank')"
                     );
                 }
                 else
                 {
                     conditions.Add(
                         $"Convert([{columnName}], 'System.String') LIKE '%{value}%'"
                     );
                 }
             }*/
        }
    }
}
