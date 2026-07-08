using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhoHang
{
    public partial class frmKhoHang : Form
    {
        static DataTable _tbTonKho = new DataTable("TonKho");
        static DataTable _tbXuatKho = new DataTable("XuatKho");
        BindingSource _bindingSource_TonKho = new BindingSource()
        {
            DataSource = _tbTonKho
        };
        BindingSource _bindingSource_XuatKho = new BindingSource()
        {
            DataSource = _tbXuatKho
        };
        public frmKhoHang()
        {
            InitializeComponent();
            //TonKho
            _tbTonKho.Columns.Add("NgayNhap", typeof(DateTime));
            _tbTonKho.Columns.Add("KhachHang", typeof(string));
            _tbTonKho.Columns.Add("Lieu", typeof(string));
            _tbTonKho.Columns.Add("MauSac", typeof(string));
            _tbTonKho.Columns.Add("MoTaMau", typeof(string));
            _tbTonKho.Columns.Add("PO", typeof(string));
            _tbTonKho.Columns.Add("CK", typeof(string));
            _tbTonKho.Columns.Add("STTGia", typeof(string));
            _tbTonKho.Columns.Add("SLBanDau", typeof(float));
            _tbTonKho.Columns.Add("SLDauThang", typeof(float));
            _tbTonKho.Columns.Add("SLCuoiThang", typeof(float));
            _tbTonKho.Columns.Add("DuLieuXuat", typeof(string));
            //XuatKho
            _tbXuatKho.Columns.Add("NgayXuat", typeof(DateTime));
            _tbXuatKho.Columns.Add("KhachHang", typeof(string));
            _tbXuatKho.Columns.Add("Lieu", typeof(string));
            _tbXuatKho.Columns.Add("Mau", typeof(string));
            _tbXuatKho.Columns.Add("PO", typeof(string));
            _tbXuatKho.Columns.Add("CK", typeof(string));
            _tbXuatKho.Columns.Add("SLXuat", typeof(float));
        }
        private void SetFilter(DataGridView dgv)
        {
            this.WindowState = FormWindowState.Maximized;
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dgv, true, null);
            dgv.AutoGenerateColumns = false;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnWidthChanged += (obj, e) =>
            {
                UpdateFilterPosition(dgv.Name.Contains("TonKho") ? pnlFilter_TonKho : pnlFilter_XuatKho);
            };
            dgv.Scroll += (obj, e) =>
            {
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                {
                    UpdateFilterPosition(dgv.Name.Contains("TonKho") ? pnlFilter_TonKho : pnlFilter_XuatKho);
                }
            };
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.DataPropertyName = col.HeaderText;
                Rectangle rect = dgv.GetCellDisplayRectangle(col.Index, -1, true);
                var textbox = new TextBox();
                textbox.Tag = col.DataPropertyName;
                textbox.Width = rect.Width - 2;
                textbox.Location = new Point(rect.X + 3, 0);
                textbox.TextChanged += textBox_TextChanged;
                if (dgv.Name.Contains("TonKho"))
                {
                    textbox.Name = "txtTonKho_" + col.DataPropertyName;
                    pnlFilter_TonKho.Controls.Add(textbox);
                }
                else
                {
                    textbox.Name = "txtXuatKho_" + col.DataPropertyName;
                    pnlFilter_XuatKho.Controls.Add(textbox);
                }
            }
        }
        private void UpdateFilterPosition(Panel pnl)
        {
            var lstTextBox = pnl.Controls.OfType<TextBox>().ToList();
            for (int i = 0; i < lstTextBox.Count; i++)
            {
                TextBox textBox = lstTextBox[i];
                var col = dgvTonKho.Columns[i];
                Rectangle rect = dgvTonKho.GetCellDisplayRectangle(col.Index, -1, true);
                textBox.SetBounds(rect.X + 3, 0, rect.Width - 2, pnl.Height);
                textBox.Visible = rect.Width > 0;
            }
        }
        private void frmKhoHang_Load(object sender, EventArgs e)
        {
            SetFilter(dgvTonKho);
            SetFilter(dgvXuatKho);
        }
        private async void frmKhoHang_Shown(object sender, EventArgs e)
        {
            string path = @"\\VP_JULIEN\Kho 倉庫\07月-2026 ton kho (产品).xlsx";
            XLWorkbook xLWorkbook = null;
            await Task.Run(async () =>
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                xLWorkbook = new XLWorkbook(fs);
                await Task.WhenAll(UpdateTonKho(xLWorkbook), UpdateXuatKho(xLWorkbook));
            });
            if (xLWorkbook != null)
                dgvTonKho.DataSource = _bindingSource_TonKho;
        }
        private static string EscapeLikeValue(string value)
        {
            return value
                .Replace("'", "''")
                .Replace("[", "[[]")
                .Replace("*", "[*]")
                .Replace("%", "[%]");
        }
        private void SetTextBoxChanged(BindingSource bindingSource)
        {
            if (bindingSource.DataSource is not DataTable dt)
                return;

            List<string> conditions = new();

            foreach (Control ctrl in pnlFilter_TonKho.Controls)
            {
                if (string.IsNullOrWhiteSpace(ctrl.Text))
                    continue;

                string columnName = ctrl.Tag.ToString();
                string value = EscapeLikeValue(ctrl.Text.Trim());

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
            var textBox = (TextBox)sender;
            var bindingSource = textBox.Name.Contains("TonKho") ? _bindingSource_TonKho : _bindingSource_XuatKho;
            var pnlFilter = textBox.Name.Contains("TonKho") ? pnlFilter_TonKho : pnlFilter_XuatKho;
            if (bindingSource.DataSource is not DataTable dt)
                return;

            List<string> conditions = new();
            var a = pnlFilter_XuatKho.Controls.OfType<TextBox>();
            foreach (Control ctrl in pnlFilter.Controls)
            {
                if (string.IsNullOrWhiteSpace(ctrl.Text))
                    continue;

                string columnName = ctrl.Tag.ToString();
                string value = EscapeLikeValue(ctrl.Text.Trim());

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
        private async Task UpdateXuatKho(XLWorkbook workbook)
        {
            await Task.Run(() =>
            {
                _tbXuatKho.Clear();
                _tbXuatKho.BeginLoadData();
                try
                {
                    var sheet = workbook.Worksheet("出货（xuat）");

                    var rows = sheet.RangeUsed().RowsUsed().Skip(3);
                    foreach (var row in rows)
                    {
                        var cellA = row.Cell("A");
                        var cellJ = row.Cell("J");

                        if (!cellA.TryGetValue(out DateTime ngayxuat))
                            continue;

                        if (!cellJ.TryGetValue(out float slxuat))
                            continue;

                        string khachhang = row.Cell("B").GetString().Replace(" ", "");
                        string lieu = row.Cell("C").GetString().Replace(" ", "").Replace("-", "").Replace("\"", "");
                        string mau = row.Cell("D").GetString().Replace(" ", "");
                        string po = row.Cell("F").GetString().Replace(" ", "");
                        string ck = row.Cell("G").GetString().Replace(" ", "");

                        if (string.IsNullOrEmpty(khachhang) ||
                            string.IsNullOrEmpty(lieu) ||
                            string.IsNullOrEmpty(mau) ||
                            string.IsNullOrEmpty(po))
                        {
                            continue;
                        }
                        _tbXuatKho.Rows.Add(
                            ngayxuat.Date,
                            khachhang,
                            lieu,
                            mau,
                            po,
                            ck,
                            slxuat
                        );
                    }
                    _tbXuatKho.EndLoadData();

                    if (_tbXuatKho.Rows.Count > 0)
                    {
                        //_bindingSource_XuatKho.DataSource = _tbXuatKho;
                    }
                }
                finally
                {
                    _tbXuatKho.EndLoadData();
                }
            });
        }
        private async Task UpdateTonKho(XLWorkbook workbook)
        {
            await Task.Run(() =>
            {
                _tbTonKho.Clear();
                _tbTonKho.BeginLoadData();

                try
                {
                    var sheet = workbook.Worksheet("tồn kho");
                    var rows = sheet.RangeUsed().RowsUsed().Skip(3);
                    foreach (var row in rows)
                    {
                        var cellA = row.Cell("A");
                        var cellJ = row.Cell("J");

                        if (!cellA.TryGetValue(out DateTime ngaynhap))
                            continue;

                        string khachhang = row.Cell("B").GetString().Replace(" ", "");
                        string lieu = row.Cell("C").GetString().Replace(" ", "").Replace("-", "").Replace("\"", "");
                        string mau = row.Cell("D").GetString().Replace(" ", "");
                        string motamau = row.Cell("E").GetString().Replace(" ", "");
                        string po = row.Cell("F").GetString().Replace(" ", "");
                        string ck = row.Cell("G").GetString().Replace(" ", "");
                        string gia = row.Cell("I").GetString().Replace(" ", "");
                        float slbandau = row.Cell("J").TryGetValue<float>(out float _slbandau) ? _slbandau : 0;
                        float sldauthang = row.Cell("K").TryGetValue<float>(out float _sldauthang) ? _slbandau : 0;
                        float slcuoithang = row.Cell("N").TryGetValue<float>(out float _slcuoithang) ? _slbandau : 0;
                        string dulieuxuat = row.Cell("L").GetString().Replace(" ", "");
                        if (string.IsNullOrEmpty(khachhang) ||
                            string.IsNullOrEmpty(lieu) ||
                            string.IsNullOrEmpty(mau) ||
                            string.IsNullOrEmpty(po))
                        {
                            continue;
                        }
                        _tbTonKho.Rows.Add(
                            ngaynhap.Date,
                            khachhang,
                            lieu,
                            mau,
                            motamau,
                            po,
                            ck,
                            gia,
                            slbandau,
                            sldauthang,
                            slcuoithang,
                            dulieuxuat
                        );
                    }

                    _tbTonKho.EndLoadData();

                    if (_tbTonKho.Rows.Count > 0)
                    {
                        //BulkInsert(_tbTonKho, "TonKho");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    _tbTonKho.EndLoadData();
                }
            });
        }

    }
}
