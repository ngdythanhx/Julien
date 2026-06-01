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
            dgvSubTotal.AutoGenerateColumns = false;
        }
        private class SubtotalData
        {
            public string MaKH { get; set; }
            public DateTime NgayXuat { get; set; }
            public double Total { get; set; }
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
                var lstNewOrderForm = _lstVita.Where(order => lstChecked.Contains(order.MaKH)).ToList();
                var subtotal = lstNewOrderForm.GroupBy(o =>o.NgayXuat).Select(o => new SubtotalData
                {
                    NgayXuat = o.First().NgayXuat,
                    Total = o.Sum(x => x.Qty1)
                }).OrderBy(s => s.NgayXuat).ToList();
                dgvMain.DataSource = lstNewOrderForm;
                dgvSubTotal.DataSource = subtotal;
            };
            filterMaKH.SetDataSource(lstMaKH);
        }
    }
}
