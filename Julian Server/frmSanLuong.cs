using ClosedXML.Excel;
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
            //var filterUnitPrice = chkFilerUnitPrice.Checked;

            var result = await Task.Run(() =>
            {
                var productionReport = _lstOrderForm.Where(o =>
                    checkedItems.Any(maKh => maKh == o.MaKH) &&
                    o.NgayDat.Date >= fromDate &&
                    o.NgayDat.Date <= toDate
                ).ToList();

                List<OrderForm> data = productionReport.Where(order => order.DonGia > 0).ToList();

                /*if (filterUnitPrice)
                    data = productionReport.Where(order => order.DonGia > 0).ToList();
                else
                    data = productionReport;*/

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
                    Data = lst,
                    Subtotal = subtotalByLieuKH,
                    TotalQty = totalQty,
                    TotalAmount = totalAmount,
                    TotalRows = data.Count
                };
            });

            dgvMain.DataSource = new SortableBindingList<SanLuong>(result.Data);

            dgvSubtotalByLieuKH.DataSource = new SortableBindingList<SubtotalByLieuKH>(result.Subtotal);

            lblTotalRows.Text = result.TotalRows.ToString("#,##0");

            lblTotalQty.Text = result.TotalQty.ToString("#,##0.00");

            lblTotalAmount.Text = result.TotalAmount.ToString("#,##0.00");

            btnApply.Enabled = true;

        }
        private void btnApply_Click(object sender, EventArgs e)
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

        private async void btnExportExcelReport_Click(object sender, EventArgs e)
        {
            btnExportExcelReport.Enabled = false;
            try
            {
                var source = dgvMain.DataSource as BindingList<SanLuong>;
                var lst = source.ToList();
                await Task.Run(() =>
                {
                    if (lst == null || lst.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xuất!");
                        return;
                    }
                    string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Template", "ProductionReport.xlsx");
                    if (!File.Exists(templatePath))
                    {
                        MessageBox.Show("Không tìm thấy file:\n" + templatePath);
                        return;
                    }
                    //string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xlsx");
                    //File.Copy(templatePath, tempFile, true);
                    using var fs = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using var workbook = new XLWorkbook(fs);
                    var sheets = workbook.Worksheets.ToList();
                    var sheet = sheets.FirstOrDefault(s => s.Name == "Main");
                    if (sheet == null)
                    {
                        MessageBox.Show("Không tìm thấy sheet 'Main'");
                        return;
                    }
                    var rangRows = sheet.Range("A4:V" + (3 + lst.Count)).Rows().ToList();
                    for (int i = 0; i < rangRows.Count; i++)
                    {
                        var row = rangRows[i];
                        row.Cell("A").Value = lst[i].NgayDat.Date;
                        row.Cell("A").Style.DateFormat.Format = "yyyy-MM-dd";
                        row.Cell("B").Value = lst[i].Brand;
                        row.Cell("C").Value = lst[i].MaKH;
                        row.Cell("D").Value = lst[i].MaDonKH;
                        row.Cell("E").Value = lst[i].MaHangKH;
                        row.Cell("F").Value = lst[i].PONhuom;
                        row.Cell("G").Value = lst[i].PONhuomMoi;
                        row.Cell("H").Value = lst[i].LieuKH;
                        row.Cell("I").Value = lst[i].MauKH;
                        row.Cell("J").Value = lst[i].Qty;
                        row.Cell("K").Value = lst[i].DonViDo;
                        row.Cell("L").Value = lst[i].DonGia;
                        row.Cell("M").Value = lst[i].NgayGiao== DateTime.MinValue?"":lst[i].NgayGiao.Date;
                        row.Cell("M").Style.DateFormat.Format = "yyyy-MM-dd";
                        row.Cell("N").Value = lst[i].Season;
                        row.Cell("O").Value = Math.Round(lst[i].TongTien, 3); ;
                        row.Cell("P").Value = lst[i].LieuThayThe;
                        row.Cell("Q").Value = lst[i].ETD;
                        row.Cell("R").Value = lst[i].ETDNote;
                        row.Cell("S").Value = lst[i].T1;
                        //row.Cell("T").Value = lst[i].TrongLuong;
                        row.Cell("T").FormulaA1 = $"Index(tbMtl[GSM],Match(H{i + 4},tbMtl[Code],0))";
                        //row.Cell("U").Value = lst[i].TyLeBaoVeMoiTruong;
                        row.Cell("U").FormulaA1 = $"Index(tbMtl[REC],Match(H{i + 4},tbMtl[Code],0))";

                        row.Cell("V").Value = lst[i].LieuKH.ToUpper().Contains("EPM5") || lst[i].LieuThayThe.ToUpper().Contains("EPM5") ? "YES" : "NO";
                    }
                    if (!Directory.Exists(@"Output\ProductionReport"))
                    {
                        Directory.CreateDirectory(@"Output\ProductionReport");
                    }
                    string fileName = "ProductionReport_" + DateTime.Now.ToString("dd-MM-yyyy_") + DateTime.Now.Ticks + ".xlsx";
                    string filePath = @"Output\ProductionReport\" + fileName;
                    workbook.SaveAs(filePath);
                    if (File.Exists(filePath))
                    {
                        System.Diagnostics.Process.Start("explorer.exe", @"/select, " + filePath);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi không xác định!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                btnExportExcelReport.Enabled = true;
            }
        }
    }
}
