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
    public partial class frmDebitNote : Form
    {
        frmReporter _frmReporter = null;
        public frmDebitNote(frmReporter frmReporter)
        {
            InitializeComponent();
            _frmReporter = frmReporter;
            var lst = _frmReporter.LstOrderForm.GroupBy(o => o.MaKH).Select(o => o.First().MaKH).ToList();
            filterMaKH.SetDataSource(lst);
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = false;

            var fromDate = dtpFromDate.Value.Date;
            var toDate = dtpToDate.Value.Date;
            var checkedItems = filterMaKH.GetItemsChecked().ToList();
            var result = await Task.Run(() =>
            {
                var lstFiltered = _frmReporter.LstOrderForm.Where(o =>
                    checkedItems.Contains(o.MaKH) &&
                    o.NgayDat?.Date >= fromDate &&
                    o.NgayDat?.Date <= toDate
                ).ToList();

                var newData = lstFiltered.Select(o => new ReportItem
                {
                    DeliveryDate = o.NgayXuat,
                    Invoice = o.InvoicePGH,
                    PO = o.MaDonKH,
                    MaterialCode = o.MaHangKH,
                    MaterialName = o.LieuKH,
                    Color = o.MauKH,
                    Width = o.Kho,
                    Qty = o.SLDat,
                    UnitPrice = o.DonGia,
                    Amount = o.TongTien,
                    Discount = o.TongTien * 0.87f,
                    BillNumber = o.BillNumber,
                    ShippingMethod = o.ShippingMethod,
                })
                .OrderBy(x => x.PO)
                .ToList();
                return newData;
            });

            dgvMain.DataSource =                new SortableBindingList<ReportItem>(result);
            btnApply.Enabled = true;
        }
        public class ReportItem
        {
            public DateTime? DeliveryDate { get; set; }
            public string Invoice { get; set; }
            public string PO { get; set; }
            public string MaterialCode { get; set; }
            public string MaterialName { get; set; }
            public string Color { get; set; }
            public int Width { get; set; }
            public float Qty { get; set; }
            public float UnitPrice { get; set; }
            public double Amount { get; set; }
            public double Discount { get; set; }
            public string BillNumber { get; set; }
            public string ShippingMethod { get; set; }
        }
    }
}
