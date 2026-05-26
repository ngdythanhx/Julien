using Julian.Database.DTO;
using Julian.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Julian_Server
{
    public partial class frmReporter : Form
    {
        List<OrderForm> _lstOrderForm = null;
        BindingSource _bindingSource = new BindingSource();
        private List<OrderForm> _lstProductionReport = null;
        private List<OrderForm> _lstHoiHang = null;
        public frmReporter()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(btnProductionReport_Apply, true, null);
            dgvMain.AutoGenerateColumns = false;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvProductionReport.AutoGenerateColumns = false;
            dgvProductionReport.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvMain.EnableHeadersVisualStyles = false;
        }

        private void frmReporter_Shown(object sender, EventArgs e)
        {
            frmLoadOrderForm frmLoadOrder = new frmLoadOrderForm(this);
            frmLoadOrder.StartPosition = FormStartPosition.CenterParent;
            frmLoadOrder.ShowDialog();
            if (frmLoadOrder.LstOrder.Count == 0)
                Application.Exit();
            //SetDataSourceControl();
        }
        public void SetDataSourceControl(List<OrderForm> lstOrderForm)
        {
            _lstOrderForm = lstOrderForm;
            var dt = ConvertData.ToDataTable(_lstOrderForm);
            _bindingSource.DataSource = dt;
            dgvMain.DataSource = _bindingSource;

            var lst = _lstOrderForm.GroupBy(o => o.MaKH).Select(o => o.First().MaKH).ToList();
            //Production Report
            filterProductionReport_MaKH.SetDataSource(lst);
            dtpProductionReport_FromDate.Value = _lstOrderForm.Min(o => o.NgayDat);
            dtpProductionReport_ToDate.Value = _lstOrderForm.Max(o => o.NgayDat);
            //HoiHang
            filterHoiHang_MaKH.SetDataSource(lst);
            dtpHoiHang_FromDate.Value = _lstOrderForm.Min(o => o.NgayDat);
            dtpHoiHang_ToDate.Value = _lstOrderForm.Max(o => o.NgayDat);
        }
        private void frmReporter_Load(object sender, EventArgs e)
        {

        }

        private void btnFilterByExcelText_Click(object sender, EventArgs e)
        {
            frmFilterByText frm = new frmFilterByText();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            var lstFilter = frm.LstFilterByText;
            if (lstFilter.Count == 0) return;
            var curSource = dgvMain.DataSource as SortableBindingList<OrderForm>;
            var filtered = curSource.Where(order =>
                lstFilter.Any(f =>
                     order.MaDonKH == f.MaDonKH &&
                    order.MaHangKH == f.MaHangKH &&
                    (f.SlDat == -1 || order.SLDat == f.SlDat) &&
                    (f.NgayGiao == DateTime.MinValue || order.NgayXuat.Date == f.NgayGiao.Date)
                )
            ).ToList();
            dgvMain.DataSource = new SortableBindingList<OrderForm>(filtered);
            lblProductionReport_TotalRows.Text = dgvMain.Rows.Count.ToString();
        }
        private async void btnProductionReport_Apply_Click(object sender, EventArgs e)
        {
            btnProductionReport_Apply.Enabled = false;

            var fromDate = dtpProductionReport_FromDate.Value.Date;
            var toDate = dtpProductionReport_ToDate.Value.Date;
            var checkedItems = filterProductionReport_MaKH.GetItemsChecked().ToList();
            var filterUnitPrice = chkProductionReport_FilerUnitPrice.Checked;

            var result = await Task.Run(() =>
            {
                var productionReport = _lstOrderForm.Where(o =>
                    checkedItems.Any(maKh => maKh == o.MaKH) &&
                    o.NgayDat.Date >= fromDate &&
                    o.NgayDat.Date <= toDate
                ).ToList();

                List<OrderForm> data;

                if (filterUnitPrice)
                    data = productionReport.Where(order => order.DonGia > 0).ToList();
                else
                    data = productionReport;

                var newData = data.GroupBy(o => o.LieuKH)
                    .Select(o => new
                    {
                        MaKH = o.First().MaKH,
                        Lieu = o.First().LieuKH,
                        Qty = o.Sum(x => x.SLDat),
                        SoTien = o.Sum(x => x.TongTien),
                    })
                    .OrderBy(x => x.Lieu)
                    .ToList();

                var totalQty = newData.Sum(order => order.Qty);
                var totalAmount = newData.Sum(order => order.SoTien);

                var dt = ConvertData.ToDataTable(newData);

                return new
                {
                    Data = data,
                    Subtotal = dt,
                    TotalQty = totalQty,
                    TotalAmount = totalAmount,
                    TotalRows = data.Count
                };
            });

            dgvProductionReport.DataSource =
                new SortableBindingList<OrderForm>(result.Data);

            dgvProductionReport_Subtotal.DataSource = result.Subtotal;

            lblProductionReport_TotalRows.Text =
                result.TotalRows.ToString("#,##0");

            lblProductionReport_TotalQty.Text =
                result.TotalQty.ToString("#,##0.00");

            lblProductionReport_TotalAmount.Text =
                result.TotalAmount.ToString("#,##0.00");

            btnProductionReport_Apply.Enabled = true;
        }
        private async void UpdateProductionReport()
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
            }).OrderBy(x => x.Lieu).ToList();
            lblProductionReport_TotalQty.Text = newData.Sum(order => order.Qty).ToString("#,##0.00");
            lblProductionReport_TotalAmount.Text = newData.Sum(order => order.SoTien).ToString("#,##0.00");
            var dt = ConvertData.ToDataTable(newData);
            dgvProductionReport_Subtotal.DataSource = dt;
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
                o.NgayDat.Date >= dtpProductionReport_FromDate.Value.Date &&
                o.NgayDat.Date <= dtpProductionReport_ToDate.Value.Date &&
                (o.NgayXuat.Date == DateTime.MinValue || o.NgayXuat == null)
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
