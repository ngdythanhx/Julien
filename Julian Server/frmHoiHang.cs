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
    public partial class frmHoiHang : Form
    {
        List<OrderForm> _lstHoiHang = new List<OrderForm>();
        frmReporter _frmReporter = null;
        List<OrderForm> _lstOrderForm => _frmReporter?.LstOrderForm;
        public frmHoiHang(frmReporter frmReporter)
        {
            InitializeComponent();
            _frmReporter = frmReporter;
            dgvMain.AutoGenerateColumns = false;
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            _lstHoiHang?.Clear();
            var fromDate = dtpFromDate.Value.Date;
            var toDate = dtpToDate.Value.Date;
            var checkedItems = filterMaKH.GetItemsChecked().ToList();

            var result = await Task.Run(() =>
            {
                _lstHoiHang = _lstOrderForm.Where(o =>
                    checkedItems.Any(maKh => maKh == o.MaKH) &&
                    o.NgayXuat?.Date >= fromDate &&
                    o.NgayXuat?.Date <= toDate &&
                    o.DonGia > 0
                ).ToList();

                var totalQty = _lstHoiHang.Sum(order => order.SLDat);
                var totalAmount = _lstHoiHang.Sum(order => order.TongTien);

                return new
                {
                    TotalQty = totalQty,
                    TotalAmount = totalAmount,
                    TotalRows = _lstHoiHang.Count
                };
            });

            dgvMain.DataSource = new SortableBindingList<OrderForm>(_lstHoiHang);
            lblTotalRows.Text = result.TotalRows.ToString("#,##0");
            lblTotalQty.Text =
                result.TotalQty.ToString("#,##0.00");

            lblTotalAmount.Text =
                result.TotalAmount.ToString("#,##0.00");

            btnApply.Enabled = true;
        }
    }
}
