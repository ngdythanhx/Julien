using DocumentFormat.OpenXml.Office2010.Drawing;
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
        frmBaoCaoTuan _frmBaoCaoTuan = null;
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

            dgvMain.ColumnWidthChanged += (obj, e) =>
            {
                UpdateFilterPosition();
            };
            dgvMain.Scroll += (obj, e) =>
            {
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    UpdateFilterPosition();
                }
            }; 
        }
        private void UpdateFilterPosition()
        {
            var lstTextBox = pnlFilter.Controls.OfType<TextBox>().ToList();
            for (int i = 0; i < lstTextBox.Count; i++)
            {
                TextBox textBox = lstTextBox[i];
                var col = dgvMain.Columns[i];
                Rectangle rect = dgvMain.GetCellDisplayRectangle(col.Index, -1, true);
                textBox.SetBounds(rect.X + 3, 0, rect.Width - 2, pnlFilter.Height);
                textBox.Visible = rect.Width > 0;
            }
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
            dt.CaseSensitive = false;
            _bindingSource.DataSource = dt;
            dgvMain.DataSource = _bindingSource;

            _frmVita.SetDataSource();
            _frmSanLuong.SetDataSource();
            _frmBaoCaoTuan.SetDataSource();
        }
        private void frmReporter_Load(object sender, EventArgs e)
        {
            _frmVita = new frmVita(this);
            LoadForm(_frmVita);
            _frmSanLuong = new frmSanLuong(this);
            LoadForm(_frmSanLuong);
            _frmBaoCaoTuan = new frmBaoCaoTuan(this);
            LoadForm(_frmBaoCaoTuan);
            foreach (DataGridViewColumn col in dgvMain.Columns)
            {
                Rectangle rect = dgvMain.GetCellDisplayRectangle(col.Index, -1, true);
                var textbox = new TextBox();
                textbox.Tag = col.DataPropertyName;
                textbox.Width = rect.Width - 2;
                textbox.Location = new Point(rect.X + 3, 0);
                textbox.TextChanged += textBox_TextChanged;
                pnlFilter.Controls.Add(textbox);
            }
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
                    (f.NgayGiao == DateTime.MinValue || order.NgayXuat?.Date == f.NgayGiao.Date)
                )
            ).ToList();
            dgvMain.DataSource = new SortableBindingList<OrderForm>(filtered);
            //lblProductionReport_TotalRows.Text = dgvMain.Rows.Count.ToString();
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

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (_bindingSource.DataSource is not DataTable dt)
                return;

            List<string> conditions = new();

            foreach (Control ctrl in pnlFilter.Controls)
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
            }

            dt.DefaultView.RowFilter = string.Join(" AND ", conditions);
        }
    }
}
