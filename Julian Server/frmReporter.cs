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

namespace Julian_Server
{
    public partial class frmReporter : Form
    {
        public List<OrderForm> LstOrderForm => _lstOrderForm;
        List<OrderForm> _lstOrderForm = null;
        BindingSource _bindingSource = new BindingSource();
        private List<OrderForm> _lstProductionReport = null;
        private List<OrderForm> _lstHoiHang = null;

        frmVita _frmVita = null;
        frmSanLuong _frmSanLuong = null;
        public frmReporter()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
            dgvMain.AutoGenerateColumns = false;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;



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
            //HoiHang
            filterHoiHang_MaKH.SetDataSource(lst);
            dtpHoiHang_FromDate.Value = _lstOrderForm.Min(o => o.NgayDat);
            dtpHoiHang_ToDate.Value = _lstOrderForm.Max(o => o.NgayDat);

            _frmVita.SetDataSource();
            _frmSanLuong.SetDataSource();
        }
        private void frmReporter_Load(object sender, EventArgs e)
        {
            _frmVita = new frmVita(this);
            LoadForm(_frmVita);
            _frmSanLuong = new frmSanLuong(this);
            LoadForm(_frmSanLuong);
        }
        private void LoadForm(Form frm)
        {
            string tabPageName = "tp" + frm.Name.Substring(3);
            if (tcMain.TabPages.ContainsKey(tabPageName))
            {
                var tp = tcMain.TabPages[tabPageName];
                Panel pnlScroll = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = SystemColors.Window,
                    AutoScroll = true
                };

                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Width = tp.Width;
                frm.Height = tp.Height;
                //frm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                frm.Dock = DockStyle.Fill;
                frm.Location = new Point(0, 0);
                frm.AutoScroll = false;
                pnlScroll.Controls.Add(frm);
                tp.Controls.Add(pnlScroll);
                frm.Show();
            }
            tcMain.SelectedTab = tcMain.TabPages[tabPageName];
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
            //lblProductionReport_TotalRows.Text = dgvMain.Rows.Count.ToString();
        }
        private void chkFilerUnitPrice_CheckedChanged(object sender, EventArgs e)
        {
            
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
          /*  _lstHoiHang = _lstOrderForm.Where(o =>
                filterProductionReport_MaKH.GetItemsChecked().Any(maKh => maKh == o.MaKH) &&
                o.NgayDat.Date >= dtpProductionReport_FromDate.Value.Date &&
                o.NgayDat.Date <= dtpProductionReport_ToDate.Value.Date &&
                (o.NgayXuat.Date == DateTime.MinValue || o.NgayXuat == null)
            ).ToList();
            UpdateHoiHang();*/
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
