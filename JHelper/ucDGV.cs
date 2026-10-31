using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace JHelper
{
    [Designer(typeof(ControlDesigner))]
    public partial class ucDGV : UserControl
    {
        public ucDGV()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgvMain, true, null);
            dgvMain.AutoGenerateColumns = false;
            dgvMain.ColumnAdded += CreateTextBox;
            dgvMain.ColumnRemoved += CreateTextBox;
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataGridViewColumnCollection Columns => dgvMain.Columns;
        BindingSource _bindingSource = new BindingSource();
        private void CreateTextBox(object sender, DataGridViewColumnEventArgs e)
        {
            pnlSearch.Controls.Clear();
            Button btnX = new Button();
            btnX.Text = "X";
            btnX.Width = dgvMain.RowHeadersWidth;
            btnX.Height = 20;
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
            if (_bindingSource.DataSource == null)
                return;
            var dt = _bindingSource.DataSource as DataTable;
            List<string> conditions = new List<string>();

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
    }
}
