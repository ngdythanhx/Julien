using JHelper;
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
using Trustrace;
using Trustrace.SS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SMTTV2
{
    public partial class frmTC : Form
    {
        TrustraceAPI _trustraceAPI;
        BindingSource _bindingSource = new BindingSource();
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public frmTC()
        {
            InitializeComponent();
        }

        private void frmTC_Load(object sender, EventArgs e)
        {
            txtUsername.Text = _iniManager.GetString("Login", "Username", "jenny@yeechain.com");
            txtPassword.Text = _iniManager.GetString("Login", "Password", "WUN2bioxMjM0");

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
            Button btnX = new Button();
            btnX.Text = "X";
            btnX.Width = dgvMain.RowHeadersWidth;
            btnX.Height = pnlSearch.Height - 1;
            btnX.Top = 0;
            btnX.Left = 0;
            btnX.Click += (ss, ee) =>
            {
                foreach (TextBox textBox in pnlSearch.Controls.OfType<TextBox>())
                {
                    textBox.Text = "";
                }
            };
            pnlSearch.Controls.Add(btnX);
            foreach (DataGridViewColumn col in dgvMain.Columns)
            {
                Rectangle rect = dgvMain.GetCellDisplayRectangle(col.Index, -1, true);
                var textbox = new TextBox();
                textbox.Tag = col.DataPropertyName;
                textbox.Width = rect.Width - 2;
                textbox.Location = new System.Drawing.Point(rect.X + 3, 0);
                textbox.TextChanged += textBox_TextChanged;
                pnlSearch.Controls.Add(textbox);
            }
            foreach (TextBox textBox in groupBox3.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
            }
        }
        private void UpdateFilterPosition()
        {
            var lstTextBox = pnlSearch.Controls.OfType<TextBox>().ToList();
            for (int i = 0; i < lstTextBox.Count; i++)
            {
                TextBox textBox = lstTextBox[i];
                var col = dgvMain.Columns[i];
                Rectangle rect = dgvMain.GetCellDisplayRectangle(col.Index, -1, true);
                textBox.SetBounds(rect.X + 3, 0, rect.Width - 2, pnlSearch.Height);
                textBox.Visible = rect.Width > 0;
            }
        }
        private void ApplyTexboxFilter()
        {
            if (_bindingSource.DataSource is not DataTable dt)
                return;

            List<string> conditions = new();

            foreach (TextBox textBox in pnlSearch.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    continue;

                string columnName = textBox.Tag.ToString();
                string value = textBox.Text.Trim().Replace("'", "''");

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
            UpdateQty(dt.DefaultView.ToTable());
        }
        private void UpdateQty(DataTable dt)
        {
            decimal remainingQty = dt.AsEnumerable().Select(row =>
            {
                var a = row.Field<string>("AvailableQuantityString");
                return decimal.TryParse(a.Substring(0, a.IndexOf(' ')), out decimal remainingQty) ? remainingQty : 0;
            }).Sum();
            lblRemainingQty.Text = remainingQty.ToString("#,##0.00");

            decimal totalQuantity = dt.AsEnumerable().Select(row =>
            {
                var a = row.Field<string>("TotalQuantityString");
                return decimal.TryParse(a.Substring(0, a.IndexOf(' ')), out decimal totalQuantity) ? totalQuantity : 0;
            }).Sum();
            lblTotalQty.Text = totalQuantity.ToString("#,##0.00");

            decimal usedQuantity = dt.AsEnumerable().Select(row =>
            {
                var a = row.Field<string>("UsedQuantityString");
                return decimal.TryParse(a.Substring(0, a.IndexOf(' ')), out decimal usedQuantity) ? usedQuantity : 0;
            }).Sum();
            lblUsedQty.Text = usedQuantity.ToString("#,##0.00");
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            ApplyTexboxFilter();
        }
        private async Task<bool> LoginTrustrace()
        {
            if (_trustraceAPI == null)
                _trustraceAPI = new TrustraceAPI();
            if (!_trustraceAPI.IsLogin)
            {
                var login = await _trustraceAPI.Login(txtUsername.Text, txtPassword.Text);
                if (!login.success)
                {
                    MessageBox.Show(login.msg);
                    return false;
                }
            }
            return true;
        }
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLoad.Enabled = false;
            groupBox1.Enabled = false;
            lbl.Enabled = false;
            groupBox3.Enabled = false;
            try
            {
                if (await LoginTrustrace())
                {

                    var lst = await _trustraceAPI.SmartSegregation.GetInventoryList();
                    //var a = lst.Where(x => x.TcReferenceNumber == "BVC-2026-00029384-V1.0");
                    var dt = ConvertData.ToDataTable<Trustrace.SS.Inventory>(lst);
                    _bindingSource.DataSource = dt;
                    dgvMain.DataSource = _bindingSource;
                    UpdateQty(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                btnLoad.Enabled = true;
                groupBox1.Enabled = true;
                lbl.Enabled = true;
                groupBox3.Enabled = true;
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var lst = await _trustraceAPI.SmartSegregation.GetTCCertificateList();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var a = await _trustraceAPI.SmartSegregation.GetTCCertificate("6abf62a69e17846bc310a14c", "");
        }
    }
}
