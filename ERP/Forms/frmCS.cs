using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ERP.Database.DAO;
using JServer;
using JServer.Models;
namespace ERP.Forms
{
    public partial class frmCS : Form
    {
        public frmCS()
        {
            InitializeComponent();
        }
        private class MaterialSearch
        {
            public string MaterialName { get; set; }
            public int Width { get; set; }
            public bool Checked { get; set; } = true;
        }
        private List<JServer.Models.Material> _lstMaterialFromServer = new List<JServer.Models.Material>();
        private List<JServer.Models.Customer> _lstCustFromServer = new List<JServer.Models.Customer>();
        public bool IsLoadDataFromServer { get; set; } = false;
        public async Task<bool> LoadDataFromServer()
        {
            IsLoadDataFromServer = true;
            try
            {
                ServerAPI serverAPI = new ServerAPI();
                var login = await serverAPI.Login("b019", "b019");
                if (login.Success)
                {
                    var task1 = serverAPI.GetMaterialListAsync();
                    var task2 = serverAPI.GetCustomerListAsync();
                    await Task.WhenAll(task1, task2);
                    _lstMaterialFromServer = task1.Result;
                    _lstCustFromServer = task2.Result;
                    if (_lstMaterialFromServer != null && _lstMaterialFromServer.Count > 0 && _lstCustFromServer != null && _lstCustFromServer.Count > 0)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            txtSearch.Enabled = false;
            _isBulkUpdating = true;
            chkALL.Checked = true;
            try
            {  //if (!IsLoadDataFromServer)
               //    await LoadDataFromServer();
                var lstSearch = txtSearch.Text
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => s.Length > 0)
                    .Distinct(StringComparer.OrdinalIgnoreCase);

                var lstMaterialSearch = new List<MaterialSearch>();
                var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                DataTable dt = null;

                foreach (var item in lstSearch)
                {
                    var dtResult = await CSDataDAO.Instance.GetByCustCode(item.ToUpper());
                    if (dtResult == null) continue;

                    if (dt == null) dt = dtResult;
                    else dt.Merge(dtResult);

                    foreach (DataRow row in dtResult.Rows)
                    {
                        string name = row["MaterialName"]?.ToString()?.Trim();
                        if (string.IsNullOrEmpty(name)) continue;

                        if (seen.Add(name))
                        {
                            int width = row["Width"] == DBNull.Value ? 0 : Convert.ToInt32(row["Width"]);
                            lstMaterialSearch.Add(new MaterialSearch { MaterialName = name, Width = width });
                        }
                    }
                }
                if (lstMaterialSearch.Count > 0)
                {
                    dgvMaterial.DataSource = lstMaterialSearch;
                    dgvMain.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
                txtSearch.Enabled = true;
                _isBulkUpdating = false;
            }
        }
        private bool _isBulkUpdating;
        private void dgvMaterial_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMaterial.IsCurrentCellDirty && dgvMaterial.CurrentCell is DataGridViewCheckBoxCell)
                dgvMaterial.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvMaterial_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvMaterial.Columns[e.ColumnIndex].Name != "colChecked") return;
            ApplyTexboxFilter();
        }

        private void SetAllChecked(bool value)
        {
            var lst = dgvMaterial.DataSource as List<MaterialSearch>;
            if (lst == null) return;

            _isBulkUpdating = true;
            try
            {
                dgvMaterial.EndEdit();
                foreach (var m in lst) m.Checked = value;
                dgvMaterial.Refresh();
            }
            finally
            {
                _isBulkUpdating = false;
            }
            ApplyTexboxFilter();
        }
        private void ApplyTexboxFilter()
        {
            if (dgvMaterial.DataSource == null || dgvMain.DataSource == null || _isBulkUpdating)
                return;
            var lstMaterial = dgvMaterial.DataSource as List<MaterialSearch>;
            var dt = dgvMain.DataSource as DataTable;
            List<string> conditions = new List<string>();
            var all = lstMaterial.ToList();
            var checkedNames = all
                .Where(m => m.Checked)
                .Select(m => "'" + m.MaterialName.Replace("'", "''") + "'")
                .ToList();

            if (all.Count > 0 && checkedNames.Count == 0)
                conditions.Add("1 = 0");
            else if (checkedNames.Count < all.Count)
                conditions.Add($"Trim([MaterialName]) IN ({string.Join(",", checkedNames)})");
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
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            ApplyTexboxFilter();
        }
        private void UpdateFilterPosition()
        {
            int x = (dgvMain.RowHeadersVisible ? dgvMain.RowHeadersWidth : 0)
                    - dgvMain.HorizontalScrollingOffset;

            foreach (DataGridViewColumn col in dgvMain.Columns.Cast<DataGridViewColumn>()
                                                        .OrderBy(c => c.DisplayIndex))
            {
                var tb = _filterBoxes[col];
                tb.Visible = col.Visible;
                if (!col.Visible) continue;

                tb.SetBounds(x + 2, 0, Math.Max(col.Width - 2, 10), tb.Height);
                x += col.Width;
            }
        }
        private readonly Dictionary<DataGridViewColumn, TextBox> _filterBoxes = new Dictionary<DataGridViewColumn, TextBox>();


        private void frmCS_Load(object sender, EventArgs e)
        {
            dgvMaterial.AutoGenerateColumns = false;
            dgvMaterial.CurrentCellDirtyStateChanged += dgvMaterial_CurrentCellDirtyStateChanged;
            dgvMaterial.CellValueChanged += dgvMaterial_CellValueChanged;

            dgvMain.AutoGenerateColumns = false;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvMain.EnableHeadersVisualStyles = false;

            dgvMain.ColumnWidthChanged += (s, ee) => UpdateFilterPosition();
            dgvMain.SizeChanged += (s, ee) => UpdateFilterPosition();
            dgvMain.ColumnDisplayIndexChanged += (s, ee) => UpdateFilterPosition();
            dgvMain.Scroll += (s, ee) =>
            {
                if (ee.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                    UpdateFilterPosition();
            };
            /*ucFilter1.ItemCheckedChange += () =>
            {
                if (_isUpdateLstMaterial)
                    return;
                LoadCSData();
            };*/
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
            /*foreach (DataGridViewColumn col in dgvMain.Columns)
            {
                Rectangle rect = dgvMain.GetCellDisplayRectangle(col.Index, -1, true);
                var textbox = new TextBox();
                textbox.Tag = col.DataPropertyName;
                textbox.Width = rect.Width - 2;
                textbox.Location = new System.Drawing.Point(rect.X + 3, 0);
                textbox.TextChanged += textBox_TextChanged;
                pnlSearch.Controls.Add(textbox);
            }
            */
            foreach (DataGridViewColumn col in dgvMain.Columns)
            {
                var textbox = new TextBox();
                textbox.Tag = col.DataPropertyName;
                textbox.TextChanged += textBox_TextChanged;
                pnlSearch.Controls.Add(textbox);
                _filterBoxes[col] = textbox;
            }
            UpdateFilterPosition();
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = false;
            //if (!IsLoadDataFromServer)
            //    await LoadDataFromServer();
            if (_lstCustFromServer != null && _lstCustFromServer.Count > 0)
            {
                MessageBox.Show("Không có dữ liệu khách hàng từ máy chủ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmCS_Modify frmCS_Modify = new frmCS_Modify();
            frmCS_Modify.Text = "Thêm CS";
            frmCS_Modify.StartPosition = FormStartPosition.CenterParent;
            frmCS_Modify.ShowDialog();
            btnInsert.Enabled = true;
        }
        private void ViewUpdate()
        {

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var row = dgvMain.CurrentRow;
            if (row == null) return;

            var drv = row.DataBoundItem as DataRowView;
            if (drv == null) return;
            btnUpdate.Enabled = false;
            DataRow dr = drv.Row;
            frmCS_Modify frmCS_Modify = new frmCS_Modify(CSDataDAO.Instance.FromRow(dr));
            frmCS_Modify.Text = "Sửa CS";
            frmCS_Modify.StartPosition = FormStartPosition.CenterParent;
            frmCS_Modify.ShowDialog();
            btnUpdate.Enabled = true;
        }

        private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvMain.Rows[e.RowIndex];
            var drv = row.DataBoundItem as DataRowView;
            if (drv == null) return;
            btnUpdate.Enabled = false;
            DataRow dr = drv.Row;
            frmCS_Modify frmCS_Modify = new frmCS_Modify(CSDataDAO.Instance.FromRow(dr));
            frmCS_Modify.Text = "Sửa CS";
            frmCS_Modify.StartPosition = FormStartPosition.CenterParent;
            frmCS_Modify.ShowDialog();
            btnUpdate.Enabled = true;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            try
            {
                var dt = dgvMain.DataSource as DataTable;
                if (dt == null) return;
                var row = dgvMain.CurrentRow;
                if (row == null) return;

                var drv = row.DataBoundItem as DataRowView;
                if (drv == null) return;
                DataRow dr = drv.Row;
                if (MessageBox.Show($"Dữ liệu sẽ bị xóa vĩnh viễn:\n{dr.Field<string>("CustCode")}\n{dr.Field<string>("MaterialName")}\n{dr.Field<string>("Color")}\n{dr.Field<string>("Follow")}", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var rp = await CSDataDAO.Instance.Delete(dr.Field<int>("ID"));
                    if (rp.IsSuccess)
                    {
                        DataRow[] rows = dt.Select($"ID = {dr.Field<int>("ID")}");
                        foreach (DataRow r in rows)
                            r.Delete();
                        dt.AcceptChanges();
                    }
                    else
                        MessageBox.Show(rp.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDelete.Enabled = true;
            }
        }

        private void chkALL_CheckedChanged(object sender, EventArgs e)
        {
            SetAllChecked(chkALL.Checked);
        }

        private void btnMultiInsert_Click(object sender, EventArgs e)
        {
            btnInsert.Enabled = false;
            //if (!IsLoadDataFromServer)
            //    await LoadDataFromServer();
            if (_lstCustFromServer != null && _lstCustFromServer.Count > 0)
            {
                MessageBox.Show("Không có dữ liệu khách hàng từ máy chủ", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmCS_MultiInsert frmCS_MultiInsert = new frmCS_MultiInsert();
            frmCS_MultiInsert.StartPosition = FormStartPosition.CenterParent;
            frmCS_MultiInsert.Text = "Thêm hàng loạt CS";
            frmCS_MultiInsert.ShowDialog();
            btnInsert.Enabled = true;

        }
    }
}
