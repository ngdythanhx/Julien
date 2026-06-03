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
        }
    }
}
