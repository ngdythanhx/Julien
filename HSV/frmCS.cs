using Julian.Utils;
using Julian_Server;
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
using System.Windows;
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
        private int _actionType = 1;
        private CS _cusCS = null;
        public frmCS()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
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
            ucFilter1.ItemCheckedChange += () =>
            {
                if (_isUpdateLstMaterial)
                    return;
                LoadCSData();
            };
            Button btnX = new Button();
            btnX.Text= "X";
            btnX.Width = dgvMain.RowHeadersWidth;
            btnX.Height = pnlSearch.Height - 1;
            btnX.Top = 0;
            btnX.Left = 0;
            btnX.Click += (ss, ee) =>
            {
                foreach(TextBox textBox in pnlSearch.Controls.OfType<TextBox>())
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
            foreach (TextBox textBox in gb1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
            }
            _isUpdateLstMaterial = true;
            ucFilter1.ClearDataSource();
            var lstMaterial = _iniManager.GetString("Data", "Materials").Split(';').Select(x => x.Replace(" ", "")).Where(x => x.Length >= 5).ToList();
            ucFilter1.SetDataSource(lstMaterial,false);
            _isUpdateLstMaterial = false;

            textBox1.ReadOnly = false;
        }
        private async void LoadCSData()
        {
            var a = ucFilter1.GetItemsChecked().ToArray();
            DataTable dt = null;
            if (a.Length != 0)
                dt = await CSDATA.Instance.Get(a);
            _bindingSource.DataSource = dt;
            dgvMain.DataSource = _bindingSource;

            if (dt == null || dt.Rows.Count == 0)
            {
                EnableButton(true, true, false, false);
            }
            else
            {
                EnableButton(true, true, true, true);
            }
            _action = 0;
            ApplyTexboxFilter();
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
        private void EnableButton(bool load, bool add, bool update, bool del)
        {
            btnLoadData.Enabled = load;
            btnAdd.Enabled = add;
            btnUpdate.Enabled = update;
            btnDel.Enabled = del;
        }

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow == null)
            {
                _cusCS = null;
                btnUpdate.Enabled = false;
                lblID.Text = "N/A";
                txtArticle.Text = "";
                txtMaterial.Text = "";
                txtColor.Text = "";
                txtFollow.Text = "";
                txtPurchaseSpecification.Text = "";
                txtNote.Text = "";
                txtLocation.Text = "";
                lblConfirmed.Text = "N/A";
                return;
            }
            lblID.Text = dgvMain.CurrentRow.Cells[0].Value?.ToString() ?? "";
            txtArticle.Text = dgvMain.CurrentRow.Cells[1].Value?.ToString() ?? "";
            txtMaterial.Text = dgvMain.CurrentRow.Cells[2].Value?.ToString() ?? "";
            txtColor.Text = dgvMain.CurrentRow.Cells[3].Value?.ToString() ?? "";
            txtFollow.Text = dgvMain.CurrentRow.Cells[4].Value?.ToString() ?? "";
            txtPurchaseSpecification.Text = dgvMain.CurrentRow.Cells[5].Value?.ToString() ?? "";
            txtNote.Text = dgvMain.CurrentRow.Cells[6].Value?.ToString() ?? "";
            txtLocation.Text = dgvMain.CurrentRow.Cells[7].Value?.ToString() ?? "";
            lblConfirmed.Text = dgvMain.CurrentRow.Cells[8].Value?.ToString();
            _cusCS = new CS()
            {
                ID = (int)dgvMain.CurrentRow.Cells[0].Value,
                Article = (string)dgvMain.CurrentRow.Cells[1].Value,
                Material = (string)dgvMain.CurrentRow.Cells[2].Value,
                Color = (string)dgvMain.CurrentRow.Cells[3].Value,
                Follow = (string)dgvMain.CurrentRow.Cells[4].Value,
                PurchaseSpecification = (string)dgvMain.CurrentRow.Cells[5].Value,
                Note = (string)dgvMain.CurrentRow.Cells[6].Value,
                SwatchLocation = (string)dgvMain.CurrentRow.Cells[7].Value,
                Confirmed = dgvMain.CurrentRow.Cells[8].Value.ToString() == "True",
            };
        }

        private bool _isUpdateLstMaterial = false;
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            _isUpdateLstMaterial = true;
            ucFilter1.ClearDataSource();
            var lstMaterial = _iniManager.GetString("Data", "Materials").Split(';').Select(x => x.Replace(" ", "")).Where(x => x.Length >= 5).ToList();
            ucFilter1.SetDataSource(lstMaterial);
            _isUpdateLstMaterial = false;
            LoadCSData();
        }

        private async void lsbMaterial_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_isUpdateLstMaterial) return;

            /*var lst = lsbMaterial.CheckedItems
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
            
            var dt = await CSDATA.Instance.Get(lst.ToArray());*/
            var a = ucFilter1.GetItemsChecked().ToArray();
            var dt = await CSDATA.Instance.Get();
            _bindingSource.DataSource = dt;
            dgvMain.DataSource = _bindingSource;

            if (dt == null || dt.Rows.Count == 0)
            {
                EnableButton(true, true, false, false);
            }
            else
            {
                EnableButton(true, true, true, true);
            }
            _action = 0;
            ApplyTexboxFilter();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCSModify frm = new frmCSModify(1);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            LoadCSData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_cusCS == null)
                return;
            frmCSModify frm = new frmCSModify(2, _cusCS);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            LoadCSData();
        }

        private async void btnDel_Click(object sender, EventArgs e)
        {
            if (_cusCS == null)
                return;
            if (MessageBox.Show($"Dữ liệu \n{_cusCS.Article}\n{_cusCS.Material}\n{_cusCS.Color}\nSẽ bị xóa vĩnh viễn. Đồng ý?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var rsp = await CSDATA.Instance.Delete(_cusCS.ID);
                MessageBox.Show(rsp.Msg);
                if (rsp.ErrCode == 0)
                    LoadCSData();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string inputText = textBox1.Text;
            string[] lines = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length == 0)
                return;
            var a = lines.Select(l => l.Split('\t'));
            var data = lines.Select(l => l.Split('\t')).Where(x => x.Length ==4).ToList();
            foreach(var item in data)
            {
                await CSDATA.Instance.Add(item[2].Trim(), item[0].Trim(), item[1].Trim(), "", "", "", item[3].Trim(),false);
            }
        }
    }
}
