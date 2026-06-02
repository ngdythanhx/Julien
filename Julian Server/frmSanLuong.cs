using Julian.Database.DTO;
using Julian.Helper;
using Julian.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian_Server
{
    public partial class frmSanLuong : Form
    {
        frmReporter _frmReporter = null;
        List<OrderForm> _lstOrderForm => _frmReporter?.LstOrderForm;
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public frmSanLuong(frmReporter frmReporter)
        {
            InitializeComponent();
            _frmReporter = frmReporter;
            dgvMain.AutoGenerateColumns = false;
            dgvMain.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        public void SetDataSource()
        {
            var lstMaKH = _lstOrderForm.GroupBy(o => o.MaKH).Select(o => o.First().MaKH).ToList();
            filterMaKH.SetDataSource(lstMaKH);
            var nowDate = DateTime.Now;

            int daysSinceFriday = ((int)nowDate.DayOfWeek - (int)DayOfWeek.Friday + 7) % 7;

            var fromDate = nowDate.Date.AddDays(-daysSinceFriday);

            dtpFromDate.Value = fromDate;
            dtpToDate.Value = nowDate;
        }
        private async void Apply()
        {
            btnApply.Enabled = false;

            var fromDate = dtpFromDate.Value.Date;
            var toDate = dtpToDate.Value.Date;
            var checkedItems = filterMaKH.GetItemsChecked().ToList();
            var filterUnitPrice = chkFilerUnitPrice.Checked;

            var result = await Task.Run(() =>
            {
                var productionReport = _lstOrderForm.Where(o =>
                    checkedItems.Any(maKh => maKh == o.MaKH) &&
                    o.NgayDat.Date >= fromDate &&
                    o.NgayDat.Date <= toDate
                ).ToList();

                List<OrderForm> data;

                if (filterUnitPrice)
                    data = productionReport.Where(order => order.DonGia > 0).ToList();
                else
                    data = productionReport;

                var lst = new List<SanLuong>();
                foreach (var order in data)
                {
                    lst.Add(new SanLuong()
                    {
                        MaKH = order.MaKH,
                        NgayDat = order.NgayDat,
                        Brand = order.Brand,
                        MaDonKH = order.MaDonKH,
                        MaHangKH = order.MaHangKH,
                        PONhuom = order.PONhuom,
                        PONhuomMoi = order.PONhuomMoi,
                        LieuKH = order.LieuKH,
                        MauKH = order.MauKH,
                        Qty = order.SLDat,
                        DonViDo = "YD",
                        DonGia = order.DonGia,
                        NgayGiao = order.NgayXuat,
                        Season = order.Season,
                        TongTien = order.SLDat * order.DonGia,
                        LieuThayThe = order.LieuThayThe,
                        ETD = order.ETD,
                        ETDNote = order.ETDNote,
                        T1 = order.T1,
                        TrongLuong = -1,
                        TyLeBaoVeMoiTruong = "x"

                    });
                }
                var subtotalByLieuKH = data.GroupBy(o => o.LieuKH)
                    .Select(o => new SubtotalByLieuKH
                    {
                        LieuKH = o.First().LieuKH,
                        Qty = o.Sum(x => x.SLDat),
                        SoTien = o.Sum(x => x.TongTien),
                    })
                    .OrderBy(x => x.LieuKH)
                    .ToList();

                var totalQty = lst.Sum(order => order.Qty);
                var totalAmount = lst.Sum(order => order.TongTien);


                return new
                {
                    Data = data,
                    Subtotal = subtotalByLieuKH,
                    TotalQty = totalQty,
                    TotalAmount = totalAmount,
                    TotalRows = data.Count
                };
            });

            dgvMain.DataSource = new SortableBindingList<OrderForm>(result.Data);

            dgvSubtotalByLieuKH.DataSource = new SortableBindingList<SubtotalByLieuKH>(result.Subtotal);

            lblTotalRows.Text = result.TotalRows.ToString("#,##0");

            lblTotalQty.Text = result.TotalQty.ToString("#,##0.00");

            lblTotalAmount.Text = result.TotalAmount.ToString("#,##0.00");

            btnApply.Enabled = true;

        }
        private async void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void chkFilerUnitPrice_CheckedChanged(object sender, EventArgs e)
        {
            Apply();
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
        private void btnCopyData_Click(object sender, EventArgs e)
        {
            if (dgvMain.Rows.Count == 0)
            {
                toolTip1.Show("Không có dữ liệu để sao chép", btnCopyData, btnCopyData.Width, 0, 1500);
            }
            else
            {
                CopyDataGridViewToClipboard(dgvMain);
                toolTip1.Show("Đã sao chép", btnCopyData, btnCopyData.Width, 0, 1500);
            }
        }
        private class SubtotalByLieuKH
        {
            public string LieuKH { get; set; }
            public double Qty { get; set; }
            public double SoTien { get; set; }

        }

        private void btnExportExcelReport_Click(object sender, EventArgs e)
        {

        }
    }
}
