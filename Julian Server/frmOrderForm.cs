using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2021.Excel.NamedSheetViews;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Julian.Database.DTO;
using Julian.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian_Server
{
    public partial class frmOrderForm : Form
    {
        List<OrderForm> _lstOrderForm = null;
        public List<OrderForm> LstOrderForm => _lstOrderForm;
        BindingSource _bindingSource = new BindingSource();
        private List<OrderForm> _lstProductionReport = null;
        private List<OrderForm> _lstHoiHang = null;
        public frmOrderForm()
        {
            InitializeComponent();
            tcMain.AutoGenerateColumns = false;
            tcMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvProductionReport.AutoGenerateColumns = false;
            dgvProductionReport.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        private void frmOrderForm_Shown(object sender, EventArgs e)
        {
            frmLoadOrderForm frmLoadOrder = new frmLoadOrderForm();
            frmLoadOrder.StartPosition = FormStartPosition.CenterParent;
            frmLoadOrder.ShowDialog();
            if (frmLoadOrder.LstOrder.Count == 0)
                Application.Exit();
            //SetDataSourceControl();
        }
        public void SetDataSourceControl(List<OrderForm> lstOrderForm)
        {
            _lstOrderForm = lstOrderForm;
            var sw = Stopwatch.StartNew();
            var dt = ConvertData.ToDataTable(_lstOrderForm);
            var a = sw.ElapsedMilliseconds;
            _bindingSource.DataSource = dt;
            var a1 = sw.ElapsedMilliseconds;
            tcMain.DataSource = _bindingSource;
            var b = sw.ElapsedMilliseconds;
            tcMain.AutoGenerateContextFilters = true;

            tcMain.FilterStringChanged += (s, ee) =>
            {
                _bindingSource.Filter = tcMain.FilterString;
                tcMain.Refresh();
            };

            tcMain.SortStringChanged += (s, ee) =>
            {
                _bindingSource.Sort = tcMain.SortString;
            };
            var sw1 = Stopwatch.StartNew();

            var lst = _lstOrderForm.GroupBy(o => o.MaKH).Select(o => o.First().MaKH).ToList();
            //Production Report
            filterProductionReport_MaKH.SetDataSource(lst);
            //HoiHang
            filterHoiHang_MaKH.SetDataSource(lst);
            var c = sw1.ElapsedMilliseconds;
        }
        private void frmOrderForm_Load(object sender, EventArgs e)
        {

        }

        private void btnFilterByExcelText_Click(object sender, EventArgs e)
        {
            frmFilterByText frm = new frmFilterByText();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            var lstFilter = frm.LstFilterByText;
            if (lstFilter.Count == 0) return;
            var curSource = tcMain.DataSource as SortableBindingList<OrderForm>;
            var filtered = curSource.Where(order =>
                lstFilter.Any(f =>
                     order.MaDonKH == f.MaDonKH &&
                    order.MaHangKH == f.MaHangKH &&
                    (f.SlDat == -1 || order.SLDat == f.SlDat) &&
                    (f.NgayGiao == DateTime.MinValue || order.NgayXuat?.Date == f.NgayGiao.Date)
                )
            ).ToList();
            tcMain.DataSource = new SortableBindingList<OrderForm>(filtered);
            lblProductionReport_TotalRows.Text = tcMain.Rows.Count.ToString();
        }
        private void btnProductionReport_Apply_Click(object sender, EventArgs e)
        {
            _lstProductionReport = _lstOrderForm.Where(o =>
                filterProductionReport_MaKH.GetItemsChecked().Any(maKh => maKh == o.MaKH) &&
                o.NgayDat?.Date >= dtpProductionReport_FromDate.Value.Date &&
                o.NgayDat?.Date <= dtpProductionReport_ToDate.Value.Date
            ).ToList();
            UpdateProductionReport();
        }
        private void UpdateProductionReport()
        {
            if (_lstProductionReport == null || _lstProductionReport.Count == 0)
                return;
            var data = new List<OrderForm>();

            if (chkProductionReport_FilerUnitPrice.Checked)
            {
                data = _lstProductionReport.Where(order => order.DonGia > 0).ToList();
            }
            else
                data = _lstProductionReport;
            dgvProductionReport.DataSource = new SortableBindingList<OrderForm>(data);
            lblProductionReport_TotalRows.Text = data.Count.ToString("#,##0");
            var newData = data.GroupBy(o => o.LieuKH).Select(o => new
            {
                MaKH = o.First().MaKH,
                Lieu = o.First().LieuKH,
                Qty = o.Sum(x => x.SLDat),
                SoTien = o.Sum(x => x.TongTien),
            });
            lblProductionReport_TotalQty.Text = newData.Sum(order => order.Qty).ToString("#,##0.00");
            lblProductionReport_TotalAmount.Text = newData.Sum(order => order.SoTien).ToString("#,##0.00");
            lsvProductionReport.Items.Clear();
            foreach (var item in newData)
            {
                var lsvItem = new ListViewItem(item.MaKH);
                lsvItem.SubItems.Add(item.Lieu);
                lsvItem.SubItems.Add(item.Qty.ToString("#,##0.00"));
                lsvItem.SubItems.Add(item.SoTien.ToString("#,##0.00"));
                lsvProductionReport.Items.Add(lsvItem);
            }
        }
        private void chkFilerUnitPrice_CheckedChanged(object sender, EventArgs e)
        {
            UpdateProductionReport();
        }
        private void UpdateHoiHang()
        {
            if (_lstHoiHang == null || _lstHoiHang.Count == 0)
                return;
            var newData = _lstHoiHang.Where(o => !string.IsNullOrEmpty(o.PONhuomMoi) && o.PONhuomMoi.Length >= 10 && o.PONhuomMoi.First() == 'P').Select(o => new
            {
                o.MaKH,
                PoNhuom = o.PONhuomMoi,
                o.NgayDat,
                Qty = o.SLDat,
                Lieu = !string.IsNullOrEmpty(o.LieuThayThe) && o.LieuThayThe.Length >= 2 ? o.LieuThayThe : o.LieuKH,
                Mau = !string.IsNullOrEmpty(o.MauThayThe) && o.MauThayThe.Length >= 2 ? o.MauThayThe : o.MauKH,
            }).ToList();
            dgvHoiHang.DataSource = ConvertData.ToDataTable(newData);
            lblHoiHang_TotalRows.Text = dgvHoiHang.RowCount.ToString("#,##0");
        }
        private void btnHoiHang_Apply_Click(object sender, EventArgs e)
        {
            _lstHoiHang = _lstOrderForm.Where(o =>
                filterProductionReport_MaKH.GetItemsChecked().Any(maKh => maKh == o.MaKH) &&
                o.NgayDat?.Date >= dtpProductionReport_FromDate.Value.Date &&
                o.NgayDat?.Date <= dtpProductionReport_ToDate.Value.Date &&
                (o.NgayXuat?.Date == DateTime.MinValue || o.NgayXuat == null)
            ).ToList();
            UpdateHoiHang();
        }

        private void btnHoiHang_ThemTienDo_Click(object sender, EventArgs e)
        {
            if (dgvHoiHang.RowCount == 0)
            {
                return;
            }
        }
    }
}
