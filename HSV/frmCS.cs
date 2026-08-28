using Julian.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSV
{
    public partial class frmCS : Form
    {
        private bool _lockItemCheck = false;
        private bool _lockCheckAll = false;
        BindingSource _bindingSource = new BindingSource();
        private static readonly Lazy<CSDATA> _instance = new Lazy<CSDATA>(() => new CSDATA());
        private int _action = 0;
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        Dictionary<string, bool> _dictSource = new Dictionary<string, bool>();
        public frmCS()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
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
        private void ApplyTexboxFilter()
        {
            if (_bindingSource.DataSource is not DataTable dt)
                return;

            List<string> conditions = new();

            foreach (TextBox textBox in pnlFilter.Controls.OfType<TextBox>())
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
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            /*  if (_lockCheckAll)
                  return;
              _lockItemCheck = true;

              for (int i = 0; i < lsbData.Items.Count; i++)
              {
                  _dictSource[lsbData.Items[i].ToString()] = chkAll.Checked;
                  lsbData.SetItemChecked(i, chkAll.Checked);
              }
              _lockItemCheck = false;
              ItemCheckedChange?.Invoke();*/
        }

        private void frmCS_Load(object sender, EventArgs e)
        {
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
            LockInputs();
        }

        private void dgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvMain.Columns[e.ColumnIndex].DataPropertyName == "Confirmed")
            {
                if (e.Value?.ToString() == "1" || e.Value?.ToString() == "True")
                    e.Value = "Yes";
                else if (e.Value?.ToString() == "0")
                    e.Value = "No";
            }
        }
        private void EnableTextBox(bool enable)
        {
            txtArticle.ReadOnly = enable;
            txtMaterial.ReadOnly = enable;
            txtColor.ReadOnly = enable;
            txtFollow.ReadOnly = enable;
            txtNote.ReadOnly = enable;
        }
        public void LockInputs()
        {
            foreach (TextBox textBox in gb1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
                //textBox.BorderStyle = BorderStyle.None;
                //textBox.Margin = new Padding(6, 6, 1, 0);
            }
            rbYes.AutoCheck = false;
            rbNo.AutoCheck = false;
        }

        public void UnlockInputs()
        {
            foreach (TextBox textBox in gb1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
                textBox.BorderStyle = BorderStyle.None;
                //textBox.Margin = new Padding(6, 6, 1, 0);
            }
            rbYes.AutoCheck = true;
            rbNo.AutoCheck = true;
        }
        public void ResetInputs()
        {
            foreach (TextBox textBox in gb1.Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            rbYes.Enabled = false;
            rbNo.Enabled = true;
        }
        private void EnableButton(bool load, bool add, bool update, bool del, bool save, bool cancel)
        {
            btnLoadData.Enabled = load;
            btnAdd.Enabled = add;
            btnUpdate.Enabled = update;
            btnDel.Enabled = del;
            btnSave.Enabled = save;
            btnCancel.Enabled = cancel;
        }

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null)
                return;

            txtArticle.Text = dgvMain.CurrentRow.Cells[0].Value?.ToString() ?? "";
            txtMaterial.Text = dgvMain.CurrentRow.Cells[1].Value?.ToString() ?? "";
            txtColor.Text = dgvMain.CurrentRow.Cells[2].Value?.ToString() ?? "";
            txtFollow.Text = dgvMain.CurrentRow.Cells[3].Value?.ToString() ?? "";
            txtNote.Text = dgvMain.CurrentRow.Cells[4].Value?.ToString() ?? "";
            txtLocation.Text = dgvMain.CurrentRow.Cells[5].Value?.ToString() ?? "";
            bool confirmed = dgvMain.CurrentRow.Cells[6].Value?.ToString() == "True" ? true : false;
            rbYes.Checked = confirmed;
            rbNo.Checked = !confirmed;
        }

        private bool _isUpdateLstMaterial = false;
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            lsbMaterial.Items.Clear();
            //_isUpdateLstMaterial = true;
            var lstMaterial = _iniManager.GetString("Data", "Materials").Split(';');
            foreach (var mtl in lstMaterial)
            {
                var a = mtl.Replace(" ", "");
                if (a.Length >= 5)
                    lsbMaterial.Items.Add(mtl, true);
            }
            //_isUpdateLstMaterial = false;
        }

        private async void lsbMaterial_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_isUpdateLstMaterial) return;

            var lst = lsbMaterial.CheckedItems
                .Cast<string>()
                .ToList();

            // Item vừa click chưa được cập nhật trong CheckedItems
            if (e.NewValue == CheckState.Checked)
            {
                lst.Add(lsbMaterial.Items[e.Index].ToString());
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                lst.Remove(lsbMaterial.Items[e.Index].ToString());
            }

            var dt = await CSDATA.Instance.Get(lst.ToArray());
            _bindingSource.DataSource = dt;
            dgvMain.DataSource = _bindingSource;

            ResetInputs();

            if (dt.Rows.Count == 0)
            {
                EnableButton(true, true, false, false, false, false);
            }
            else
            {
                EnableButton(true, true, true, true, false, false);
            }
            _action = 0;
            ApplyTexboxFilter();
        }
    }
}
