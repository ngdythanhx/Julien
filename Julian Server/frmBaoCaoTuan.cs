using Julian.Database.DTO;
using Julian.Helper;
using Julian.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian_Server
{
    public partial class frmBaoCaoTuan : Form
    {
        frmReporter _frmReporter = null;
        List<OrderForm> _lstOrderForm => _frmReporter?.LstOrderForm;
        List<BaoCaoTuan> _lstBaoCaoTuan = null;
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public frmBaoCaoTuan(frmReporter frmReporter)
        {
            InitializeComponent();
            _frmReporter = frmReporter;
            foreach (var col in dgvSubTotal.Columns.Cast<DataGridViewColumn>())
            {
                /*if (col.Name == "col_checked")
                    col.ReadOnly = false;
                else
                    col.ReadOnly = true;*/
            }
            dgvSubTotal.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvSubTotal.IsCurrentCellDirty)
                {
                    dgvSubTotal.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            dgvSubTotal.CellValueChanged += (obj, e) =>
            {
                /*if (e.RowIndex < 0)
                    return;

                if (dgvSubTotal.Columns[e.ColumnIndex].Name == "col_checked")
                {
                    var source = dgvSubTotal.DataSource as BindingList<SubtotalData>;
                    var lstCheked = source.ToList().Where(s => s.Checked).ToList();
                    var lstVita = _lstBaoCaoTuan.Where(v => lstCheked.Any(c => c.MaKH == v.MaKH && c.NgayXuat == v.NgayXuat)).OrderBy(v => v.MaKH).ThenBy(v => v.NgayXuat).ToList();
                    dgvMain.DataSource = lstVita;
                    var totalQty = lstVita.Sum(order => order.Qty1);
                    var totalAmount = lstVita.Sum(order => order.TongTien);
                    var rowCount = lstVita.Count;
                    lblTotalQty.Text = totalQty.ToString("#,##0.00");
                    lblTotalAmount.Text = totalAmount.ToString("#,##0.00");
                    lblTotalRows.Text = rowCount.ToString("#,##0");
                }*/
            };
        }
        private class BaoCaoTuan
        {
            public bool Checked { get; set; }
            public string MaKH { get; set; }
            public double DaXuat { get; set; }
            public double ChuaGiaiQuyet { get; set; }
            public double TienTonKho { get; set; }
            public double DonMoi { get; set; }

        }
        public void SetDataSource()
        {
            var lstMaKH = _lstOrderForm.GroupBy(o => o.MaKH).Select(o => o.First().MaKH).ToList();
            filterMaKH.SetDataSource(lstMaKH);
            var minDate = _lstOrderForm.Min(o => o.NgayDat);
            var maxDate = _lstOrderForm.Max(o => o.NgayDat);
            var nowDate = DateTime.Now;
            //var fromDate = nowDate.Day >= 26 ? new DateTime(nowDate.Year, nowDate.Month, 26) : new DateTime(nowDate.AddMonths(-1).Year, nowDate.AddMonths(-1).Month, 26);
            int daysSinceFriday = ((int)nowDate.DayOfWeek - (int)DayOfWeek.Friday + 7) % 7;

            var fromDate = nowDate.Date.AddDays(-daysSinceFriday);
            dtpFromDate.Value = fromDate;
            dtpToDate.Value = nowDate;
        }
        private async void btnApply_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            _lstBaoCaoTuan?.Clear();
            var fromDate = dtpFromDate.Value.Date;
            var toDate = dtpToDate.Value.Date;
            var checkedItems = filterMaKH.GetItemsChecked().ToList();

            await Task.Run(() =>
            {
                var lstDaXuat = _lstOrderForm.Where(o => o.NgayXuat?.Date >= fromDate && o.NgayXuat?.Date <= toDate && o.DonGia > 0).ToList();
                var a = _lstOrderForm.Where(o =>
                     o.NgayXuat == null
                ).ToList();
                var lstChuaGiaiQuyet = _lstOrderForm.Where(o =>
                    o.MaKH == "HSV" && o.DonGia > 0 &&
                    o.NgayXuat == null
                //(o.NgayXuat == null || o.NgayXuat?.Date > toDate)
                // && o.NgayDat <= toDate
                ).ToList();
                var aa = lstChuaGiaiQuyet.Sum(o => o.SLDat);
                var aaa = lstChuaGiaiQuyet.GroupBy(x => x.NgayDat).Select(x => x.First().NgayDat + "\t" + x.Sum(y => y.SLDat));
                var x = lstChuaGiaiQuyet.Where(o => o.MaKH == "HSV" && o.NgayXuat != DateTime.MinValue && (o.NgayXuat == null || o.NgayXuat?.Date > toDate) && o.DonGia > 0 && o.NgayDat <= toDate).ToList();
                var yy = x.Sum(n => n.SLDat);

                foreach (var n in x)
                {
                    File.AppendAllLines("testxxxx.txt", new[] { n.MaDonKH + n.MaHangKH + "\t" + n.TongTien });
                }
                foreach (var n in aaa)
                {
                    File.AppendAllLines("test.txt", new[] { n });
                }
                var lstDonMoi = _lstOrderForm.Where(o => o.NgayDat?.Date >= fromDate && o.NgayDat?.Date <= toDate && o.DonGia > 0).ToList();
                var checkDate = toDate.Day >= 26 ? new DateTime(toDate.Year, toDate.Month, 26) : new DateTime(toDate.AddMonths(-1).Year, toDate.AddMonths(-1).Month, 26);
                _lstBaoCaoTuan = _lstOrderForm
                    .Where(o => checkedItems.Any(maKh => maKh == o.MaKH))
                    .GroupBy(o => o.MaKH)
                    .Select(g => new BaoCaoTuan
                    {
                        Checked = true,
                        MaKH = g.First().MaKH,
                        DaXuat = g.Where(o => o.NgayXuat?.Date >= checkDate.Date && o.NgayXuat?.Date <= toDate && o.DonGia > 0).Sum(o => o.TongTien),
                        ChuaGiaiQuyet = g.Where(o => o.NgayXuat != DateTime.MinValue && (o.NgayXuat == null || o.NgayXuat?.Date > toDate) && o.DonGia > 0 && o.NgayDat <= toDate).Sum(o => o.TongTien),
                        TienTonKho = g.Where(o => o.NgayXuat != DateTime.MinValue && (o.NgayXuat == null || o.NgayXuat?.Date > toDate) && o.DonGia > 0 && o.NgayDat <= toDate && string.IsNullOrEmpty(o.PONhuomMoi)).Sum(o => o.TongTien),
                        DonMoi = g.Where(o => o.NgayDat?.Date >= fromDate && o.NgayDat?.Date <= toDate && o.DonGia > 0).Sum(o => o.TongTien),
                    }).ToList();
            });

            dgvSubTotal.DataSource = ConvertData.ToDataTable(_lstBaoCaoTuan);
            btnApply.Enabled = true;
        }
    }
}
