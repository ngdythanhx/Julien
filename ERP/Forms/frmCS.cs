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
namespace ERP.Forms
{
    public partial class frmCS : Form
    {
        public frmCS()
        {
            InitializeComponent();
        }
        private class Material
        {
            public string MaterialName { get; set; }
            public int Size { get; set; }
            public bool Checked { get; set; } = true;
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var lstSearch = txtSearch.Text
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .Distinct(StringComparer.OrdinalIgnoreCase);

            var lstMaterial = new List<Material>();
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
                        lstMaterial.Add(new Material { MaterialName = name, Size = width });
                    }
                }
            }
            if (lstMaterial.Count > 0)
            {
                dgvMaterial.DataSource = lstMaterial;
                dgvMain.DataSource = dt;
            }
        }
        private void ApplyTexboxFilter()
        {
            if (dgvMaterial.DataSource == null || dgvMain.DataSource == null)
                return;
            var lstMaterial = new List<Material>();
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
        private void frmCS_Load(object sender, EventArgs e)
        {
            dgvMain.AutoGenerateColumns = false;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgvMain.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvMain.EnableHeadersVisualStyles = false;

            dgvMain.ColumnWidthChanged += (obj, ee) =>
            {
                UpdateFilterPosition();
            };
            dgvMain.Scroll += (obj, ee) =>
            {
                if (ee.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    UpdateFilterPosition();
                }
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
            foreach (TextBox textBox in panel2.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
            }
        }
    }
}
