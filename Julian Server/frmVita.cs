using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Julian;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Julian_Server
{
    public partial class frmVita : Form
    {
        frmReporter _frmReporter = null;
        List<OrderForm> _lstOrderForm => _frmReporter?.LstOrderForm;
        List<Vita> _lstVita = null;
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        public frmVita(frmReporter frmReporter)
        {
            InitializeComponent();
            _frmReporter = frmReporter;
            string folder = _iniManager.GetString("Vita", "Folder", "\\VP_JULIEN\\Kinh doanh 業務\\13.Báo cáo Report 業務報告\\Vita Báo cáo\\2026");
            folder = Encoding.UTF8.GetString(Encoding.Default.GetBytes(folder));
            if (Directory.Exists(folder))
            {
                var files = Directory.GetFiles(folder, "*.xlsx").Where(f => (File.GetAttributes(f) & FileAttributes.Hidden) == 0).OrderByDescending(f => File.GetLastWriteTime(f)).ToArray();
                if (files.Length > 0)
                    cbVitaList.DataSource = files;
            }
            foreach (var col in dgvSubTotal.Columns.Cast<DataGridViewColumn>())
            {
                if (col.Name == "col_checked")
                    col.ReadOnly = false;
                else
                    col.ReadOnly = true;
            }
            dgvSubTotal.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvSubTotal.IsCurrentCellDirty)
                {
                    dgvSubTotal.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            dgvSubTotal.CellValueChanged += (obj, e) =>
            {
                if (e.RowIndex < 0)
                    return;

                if (dgvSubTotal.Columns[e.ColumnIndex].Name == "col_checked")
                {
                    var source = dgvSubTotal.DataSource as BindingList<SubtotalData>;
                    var lstCheked = source.ToList().Where(s => s.Checked).ToList();
                    var lstVita = _lstVita.Where(v => lstCheked.Any(c => c.MaKH == v.MaKH && c.NgayXuat == v.NgayXuat)).OrderBy(v=>v.MaKH).ThenBy(v=>v.NgayXuat).ToList();
                    dgvMain.DataSource = lstVita;
                    var totalQty = lstVita.Sum(order => order.Qty1);
                    var totalAmount = lstVita.Sum(order => order.TongTien);
                    var rowCount = lstVita.Count;
                    lblTotalQty.Text = totalQty.ToString("#,##0.00");
                    lblTotalAmount.Text = totalAmount.ToString("#,##0.00");
                    lblTotalRows.Text = rowCount.ToString("#,##0");
                }
            };
        }
        private void UpdateSubtotalLabelPosition(DataGridViewColumn col)
        {
            if (col.DataPropertyName == "TotalQty")
            {
                Rectangle rect = dgvSubTotal.GetCellDisplayRectangle(col.Index, -1, true);
                //lblSubtotalQty.Width = rect.Width - 3;
               // lblSubtotalQty.Location = new System.Drawing.Point(rect.X + 3, 2);
            }
        }
        public void SetDataSource()
        {
            var lstMaKH = _lstOrderForm.GroupBy(o => o.MaKH).Select(o => o.First().MaKH).ToList();
            filterMaKH.SetDataSource(lstMaKH);
            var minDate = _lstOrderForm.Min(o => o.NgayDat);
            var maxDate = _lstOrderForm.Max(o => o.NgayDat);
            var nowDate = DateTime.Now;
            var fromDate = nowDate.Day >= 26 ? new DateTime(nowDate.Year, nowDate.Month, 26) : new DateTime(nowDate.AddMonths(-1).Year, nowDate.AddMonths(-1).Month, 26);
            dtpFromDate.Value = fromDate;
            dtpToDate.Value = nowDate;
        }
        private async void btnApply_Click(object sender, EventArgs e)
        {
            btnApply.Enabled = false;
            _lstVita?.Clear();
            var fromDate = dtpFromDate.Value.Date;
            var toDate = dtpToDate.Value.Date;
            var checkedItems = filterMaKH.GetItemsChecked().ToList();

            var result = await Task.Run(() =>
            {
                var vitaReport = _lstOrderForm.Where(o =>
                    checkedItems.Any(maKh => maKh == o.MaKH) &&
                    o.NgayXuat?.Date >= fromDate &&
                    o.NgayXuat?.Date <= toDate &&
                    o.DonGia > 0
                ).ToList();
                _lstVita = vitaReport
                    .Select(o => new Vita
                    {
                        MaKH = o.MaKH,
                        NgayXuat = o.NgayXuat,
                        Season = o.Season,
                        MaDonKH = o.MaDonKH,
                        LieuKH = o.LieuKH,
                        LieuThayThe = "",
                        PONhuom = o.PONhuom,
                        MauSac = o.MauKH,
                        Qty1 = o.SLDat,
                        Qty2 = o.SLDat,
                        DonVi = "YD",
                        DonGia = o.DonGia,
                        TongTien = o.TongTien,
                        Invoice = o.InvoiceHoaDon,
                        MaHangKH = o.MaHangKH,
                        T1 = o.T1
                    }).Reverse().ToList();
                //}).OrderBy(o => o.NgayXuat).ToList();

                var totalQty = _lstVita.Sum(order => order.Qty1);
                var totalAmount = _lstVita.Sum(order => order.TongTien);
                var subtotal = _lstVita.GroupBy(o => new { o.MaKH, o.NgayXuat }).Select(o => new SubtotalData
                {
                    Checked = true,
                    MaKH = o.First().MaKH,
                    NgayXuat = o.First().NgayXuat,
                    TotalQty = o.Sum(x => x.Qty1)
                }).OrderBy(s => s.MaKH).ThenBy(s => s.NgayXuat).ToList();

                return new
                {
                    SubtotalData = subtotal,
                    TotalQty = totalQty,
                    TotalAmount = totalAmount,
                    TotalRows = _lstVita.Count
                };
            });

            dgvMain.DataSource = new SortableBindingList<Vita>(_lstVita);
            dgvSubTotal.DataSource = new SortableBindingList<SubtotalData>(result.SubtotalData);
            lblTotalRows.Text = result.TotalRows.ToString("#,##0");

            lblTotalQty.Text =
                result.TotalQty.ToString("#,##0.00");

            lblTotalAmount.Text =
                result.TotalAmount.ToString("#,##0.00");

            btnApply.Enabled = true;
        }
        private class SubtotalData
        {
            public bool Checked { get; set; }
            public string MaKH { get; set; }
            public DateTime? NgayXuat { get; set; }
            public double TotalQty { get; set; }
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
        private string XoaPivotLoi(string path)
        {
            string tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".xlsx");
            File.Copy(path, tempFile, true);
            using (var doc = SpreadsheetDocument.Open(tempFile, true))
            {
                var wbPart = doc.WorkbookPart;

                foreach (var ws in wbPart.WorksheetParts)
                {
                    foreach (var pivot in ws.PivotTableParts.ToList())
                    {
                        ws.DeletePart(pivot);
                    }
                }

                foreach (var cache in wbPart.PivotTableCacheDefinitionParts.ToList())
                {
                    wbPart.DeletePart(cache);
                }

                doc.Save();
            }
            return tempFile;
        }
        private async void btnLoadVita_Click(object sender, EventArgs e)
        {
            btnLoadVita.Enabled = false;
            try
            {
                string filePath = cbVitaList.SelectedValue?.ToString();
                if (!File.Exists(filePath))
                    return;
                string tempPath = XoaPivotLoi(filePath);
                var lstVita = await Task.Run(() =>
                {
                    var lst = new List<Vita>();
                    try
                    {
                        using (FileStream fs = new FileStream(tempPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (XLWorkbook workbook = new XLWorkbook(fs))
                            {
                                IXLWorksheet sheet = null;
                                foreach (var s in workbook.Worksheets.ToList())
                                {
                                    if (s.Name.ToUpper().Contains("VITA"))
                                    {
                                        sheet = s;
                                        break;
                                    }
                                }
                                if (sheet != null)
                                {
                                    var rows = sheet.RangeUsed().RowsUsed().Skip(3).ToList();
                                    var rowHeader = rows[0];
                                    var vitaConfig = new VitaConfig();
                                    foreach (var cell in rowHeader.Cells())
                                    {
                                        if (cell.GetString().Contains("KHACH HANG"))
                                            vitaConfig.MaKH = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Ngày xuất hàng"))
                                            vitaConfig.NgayXuat = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("SEASON"))
                                            vitaConfig.Season = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Đơn đặt hàng của KH"))
                                            vitaConfig.MaDonKH = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Sản phẩm"))
                                            vitaConfig.LieuKH = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("lieu thay the"))
                                            vitaConfig.LieuThayThe = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Tên đơn hàng"))
                                            vitaConfig.PONhuom = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Màu sắc"))
                                            vitaConfig.MauSac = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Màu sắc"))
                                            vitaConfig.MauSac = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Số lượng đặt"))
                                            vitaConfig.Qty1 = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Số lượng xuất"))
                                            vitaConfig.Qty2 = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Đơn vị"))
                                            vitaConfig.DonVi = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Đơn giá"))
                                            vitaConfig.DonGia = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("Tổng tiền"))
                                            vitaConfig.TongTien = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("INVOICE NO"))
                                            vitaConfig.Invoice = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("CODE") && cell.GetString().Contains("MATERIAL"))
                                            vitaConfig.MaHangKH = cell.Address.ColumnLetter;
                                        else if (cell.GetString().Contains("CODE"))
                                            vitaConfig.T1 = cell.Address.ColumnLetter;

                                    }
                                    foreach (var row in rows.Skip(1))
                                    {
                                        string lieuKh = row.Cell(vitaConfig.LieuKH).GetString();
                                        lieuKh = lieuKh.Replace(" ", "").Replace("\"", "").Replace("'", "");
                                        var vita = new Vita()
                                        {
                                            MaKH = row.Cell(vitaConfig.MaKH).GetString(),
                                            NgayXuat = row.Cell(vitaConfig.NgayXuat).TryGetValue(out DateTime ngayxuathang) ? ngayxuathang : DateTime.MinValue,
                                            Season = string.IsNullOrEmpty(vitaConfig.Season) ? "" : row.Cell(vitaConfig.Season).GetString(),
                                            MaDonKH = row.Cell(vitaConfig.MaDonKH).GetString(),
                                            LieuKH = lieuKh == "YHM1093EPM558" ? "YHM1093EPM5" : lieuKh,
                                            LieuThayThe = row.Cell(vitaConfig.LieuThayThe).GetString(),
                                            PONhuom = row.Cell(vitaConfig.PONhuom).GetString(),
                                            MauSac = row.Cell(vitaConfig.MauSac).GetString(),
                                            Qty1 = row.Cell(vitaConfig.Qty1).TryGetValue(out float qty1) ? qty1 : 0,
                                            Qty2 = row.Cell(vitaConfig.Qty2).TryGetValue(out float qty2) ? qty2 : 0,
                                            DonVi = row.Cell(vitaConfig.LieuThayThe).GetString(),
                                            DonGia = row.Cell(vitaConfig.DonGia).TryGetValue(out float dongia) ? dongia : 0,
                                            TongTien = row.Cell(vitaConfig.TongTien).TryGetValue(out float tongtien) ? tongtien : -1,
                                            Invoice = row.Cell(vitaConfig.Invoice).GetString(),
                                            MaHangKH = row.Cell(vitaConfig.MaHangKH).GetString(),
                                            T1 = row.Cell(vitaConfig.LieuThayThe).GetString(),
                                        };
                                        lst.Add(vita);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                        if (ex.InnerException != null)
                            Console.WriteLine(ex.InnerException.ToString());
                    }
                    return lst;
                });
                frmVitaViewer frmVitaViewer = new frmVitaViewer(lstVita);
                frmVitaViewer.Text = "Vita: " + Path.GetFileName(filePath);
                frmVitaViewer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống");
            }
            finally
            {
                btnLoadVita.Enabled = true;
            }
        }
        private class VitaConfig
        {
            public string MaKH { get; set; }
            public string NgayXuat { get; set; }
            public string Season { get; set; }
            public string MaDonKH { get; set; }
            public string LieuKH { get; set; }
            public string LieuThayThe { get; set; }
            public string PONhuom { get; set; }
            public string MauSac { get; set; }
            public string Qty1 { get; set; }
            public string Qty2 { get; set; }
            public string DonVi { get; set; }
            public string DonGia { get; set; }
            public string TongTien { get; set; }
            public string Invoice { get; set; }
            public string MaHangKH { get; set; }
            public string T1 { get; set; }
        }
    }
}
