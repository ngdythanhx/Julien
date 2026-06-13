using Julian.Database.DTO;
using Julian.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Julian_Server
{
    public partial class frmVitaViewer : Form
    {
        List<Vita> _lstVita = null;
        public frmVitaViewer(List<Vita> lstVita)
        {
            InitializeComponent();
            _lstVita = lstVita;
            dgvSubTotalByNgayXuat.AutoGenerateColumns = false;
            WindowState = FormWindowState.Maximized;
        }
        private class SubtotalByNgayXuat
        {
            public bool Checked { get; set; } = true;
            public string MaKH { get; set; }
            public DateTime? NgayXuat { get; set; }
            public double TotalQty { get; set; }
            public double TotalAmount { get; set; }
        }
        private class SubtotalByLieuKH
        {
            public string LieuKH { get; set; }
            public double TotalQty { get; set; }
            public double TotalAmount { get; set; }
        }
        private void frmVitaViewer_Load(object sender, EventArgs e)
        {
            if (_lstVita == null)
                return;
            var totalQty = _lstVita.Sum(order => order.Qty1);
            var totalAmount = _lstVita.Sum(order => order.TongTien);
            var lstMaKH = _lstVita.GroupBy(o => o.MaKH).Select(o => o.First().MaKH).ToList();
            filterMaKH.ItemCheckedChange += () =>
            {
                var lstChecked = filterMaKH.GetItemsChecked();
                var lstNewVita = _lstVita.Where(order => lstChecked.Contains(order.MaKH)).ToList();
                var subtotalByNgayXuat = lstNewVita.GroupBy(o => o.NgayXuat).Select(o => new SubtotalByNgayXuat
                {
                    MaKH = o.First().MaKH,
                    NgayXuat = o.First().NgayXuat,
                    TotalQty = o.Sum(x => x.Qty1),
                    TotalAmount = o.Sum(x => x.TongTien),
                }).OrderBy(s => s.NgayXuat).ToList();
                var subtotalByLieuKH = lstNewVita.GroupBy(o => o.NgayXuat).Select(o =>
                {
                    string lieukh = o.First().LieuKH.Replace(" ", "").Replace("\"", "");
                    return new SubtotalByLieuKH
                    {
                        LieuKH = lieukh == "YH-M1093EPM558" ? "YH-M1093EPM5" : lieukh,
                        TotalQty = o.Sum(x => x.Qty1),
                        TotalAmount = o.Sum(x => x.TongTien),
                    };
                }).OrderBy(s => s.LieuKH).ToList();
                dgvMain.DataSource = new SortableBindingList<Vita>(lstNewVita);
                dgvSubTotalByNgayXuat.DataSource = new SortableBindingList<SubtotalByNgayXuat>(subtotalByNgayXuat);
                dgvSubtotalByLieuKH.DataSource = new SortableBindingList<SubtotalByLieuKH>(subtotalByLieuKH);
                lblRowCount.Text = lstNewVita.Count.ToString("#,##0");
                lblTotalQty.Text = lstNewVita.Sum(v => v.Qty1).ToString("#,##0.00");
                lblTotalAmount.Text = lstNewVita.Sum(v => v.TongTien).ToString("#,##0.00");
            };
            filterMaKH.SetDataSource(lstMaKH);
            foreach (var col in dgvSubTotalByNgayXuat.Columns.Cast<DataGridViewColumn>())
            {
                if (col.Name == "col_checked")
                    col.ReadOnly = false;
                else
                    col.ReadOnly = true;
            }
            dgvSubTotalByNgayXuat.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvSubTotalByNgayXuat.IsCurrentCellDirty)
                {
                    dgvSubTotalByNgayXuat.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            dgvSubTotalByNgayXuat.CellValueChanged += (obj, e) =>
            {
                if (e.RowIndex < 0)
                    return;

                if (dgvSubTotalByNgayXuat.Columns[e.ColumnIndex].Name == "col_checked")
                {
                    var source = dgvSubTotalByNgayXuat.DataSource as BindingList<SubtotalByNgayXuat>;
                    var lstCheked = source.ToList().Where(s => s.Checked).ToList();
                    var lstVita = _lstVita.Where(v => lstCheked.Any(c => c.MaKH == v.MaKH && c.NgayXuat == v.NgayXuat)).OrderBy(v => v.MaKH).ThenBy(v => v.NgayXuat).ToList();
                    dgvMain.DataSource = new SortableBindingList<Vita>(lstVita);
                    var totalQty = lstVita.Sum(order => order.Qty1);
                    var totalAmount = lstVita.Sum(order => order.TongTien);
                    var rowCount = lstVita.Count;
                    lblTotalQty.Text = totalQty.ToString("#,##0.00");
                    lblTotalAmount.Text = totalAmount.ToString("#,##0.00");
                    dgvSubTotalByNgayXuat.Text = rowCount.ToString("#,##0");
                }
            };
        }
        private void CopyDataGridViewToClipboard(DataGridView dgv)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    string value = row.Cells[i].Value?.ToString() ?? "";

                    if (value.Contains("\n"))
                        value = "\"" + value + "\"";

                    sb.Append(value);

                    if (i < dgv.Columns.Count - 1)
                        sb.Append("\t");
                }

                sb.Append("\r\n");
            }

            Clipboard.SetText(sb.ToString());
        }
        private void btnCopySMTT_Click(object sender, EventArgs e)
        {
            if (dgvMain.Rows.Count == 0)
            {
                toolTip1.Show("Không có dữ liệu để sao chép", btnCopySMTT, btnCopySMTT.Width, 0, 1500);
            }
            else
            {
                var lst = dgvMain.DataSource as BindingList<Vita>;
                if (lst != null)
                {
                    var lstRow = new List<string>();
                    foreach (Vita vita in lst.ToList())
                    {
                        string dyingPo = vita.PONhuom.Contains("\n") ? $"\"{vita.PONhuom}\"" : vita.PONhuom;
                        string lieu = vita.LieuKH.Contains("\n") ? $"\"{vita.LieuKH}\"" : vita.LieuKH;
                        string t2Id = "ZZXYEC-VN01";
                        string t1Name = vita.MaKH.Contains("\n") ? $"\"{vita.MaKH}\"" : vita.MaKH;
                        if (t1Name.Contains("APH") || t1Name.Contains("DC"))
                        {
                            t1Name = vita.T1.Contains("\n") ? $"\"{vita.T1}\"" : vita.T1;
                        }
                        string deliveryDate = vita.NgayXuat.ToString().Contains("\n") ? $"\"{vita.NgayXuat.ToString()}\"" : vita.NgayXuat?.ToString("yyyy-MM-dd");
                        string season = vita.Season.Contains("\n") ? $"\"{vita.Season}\"" : vita.Season;
                        string cusPo = vita.MaDonKH.Contains("\n") ? $"\"{vita.MaDonKH}\"" : vita.MaDonKH;
                        string codeKH = vita.MaHangKH.Contains("\n") ? $"\"{vita.MaHangKH}\"" : vita.MaHangKH;
                        float qty = vita.Qty1;
                        string invoice = vita.Invoice.Contains("\n") ? $"\"{vita.Invoice}\"" : vita.Invoice;
                        string color = vita.MauSac.Contains("\n") ? $"\"{vita.MauSac}\"" : vita.MauSac;
                        string row = $"{dyingPo}\t{lieu}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{t2Id}\t{t1Name}\t{""}\t{deliveryDate}\t{season}\t{cusPo}\t{codeKH}\t{qty}\t{"Yards"}\t{invoice}\t{""}\t{""}\t{""}\t{""}\t{""}\t{""}\t{color}";
                        lstRow.Add(row);
                    }
                    var result = string.Join("\r\n", lstRow);
                    Clipboard.SetText(result);
                    toolTip1.Show("Đã sao chép", btnCopySMTT, btnCopySMTT.Width, 0, 1500);
                }
                else
                {
                    toolTip1.Show("Lỗi dữ liệu!", btnCopySMTT, btnCopySMTT.Width, 0, 1500);
                }

            }

        }
        private void CopySelectedCells()
        {
            var cells = dgvMain.SelectedCells
                .Cast<DataGridViewCell>()
                .OrderBy(c => c.RowIndex)
                .ThenBy(c => c.ColumnIndex)
                .ToList();

            if (!cells.Any())
                return;

            var rows = cells.GroupBy(c => c.RowIndex);
            StringBuilder sb = new StringBuilder();

            foreach (var row in rows)
            {
                var values = row
                    .OrderBy(c => c.ColumnIndex)
                    .Select(c =>
                    {
                        string text = c.Value?.ToString() ?? "";

                        // Escape dấu "
                        text = text.Replace("\"", "\"\"");

                        // Nếu có xuống dòng hoặc tab thì bọc bằng "
                        if (text.Contains('\n') || text.Contains('\r') || text.Contains('\t'))
                            text = $"\"{text}\"";

                        return text;
                    });

                sb.AppendLine(string.Join("\t", values));
            }

            Clipboard.SetText(sb.ToString());
        }
        private void dgvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedCells();
                e.SuppressKeyPress = true;
            }
        }
    }
}
