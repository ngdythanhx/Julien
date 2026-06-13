using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Julian.Database.DTO;
using Julian.Utils;
using System;
using System.Collections.Concurrent;
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

namespace Julian.Forms
{
    public partial class frmHoiHang2 : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        XLWorkbook workbook1 = null;
        XLWorkbook workbook2 = null;
        public frmHoiHang2()
        {
            InitializeComponent();
        }
        private class XNYVConfig()
        {
            public static int StartIndex
            {
                get => _startIndex;
                set => _startIndex = value < 1 ? 1 : value;
            }
            public static int _startIndex { get; set; }
            public static string NgayNhap { get; set; }
            public static string PONhuom { get; set; }
            public static string PS { get; set; }
            public static string Lieu { get; set; }
            public static string Mau { get; set; }
            public static string KetQua { get; set; }
            public static string ChiTiet { get; set; }
            public static string ETD { get; set; }
            public static string GhiChuGiaoHang { get; set; }
        }
        private void LoadXNYVConfig()
        {
            XNYVConfig.StartIndex = _iniManager.GetInt("XacNhanYVai", "StartIndex", 1);
            XNYVConfig.NgayNhap = _iniManager.GetString("XacNhanYVai", "NgayNhap");
            XNYVConfig.PONhuom = _iniManager.GetString("XacNhanYVai", "PONhuom");
            XNYVConfig.PS = _iniManager.GetString("XacNhanYVai", "PS");
            XNYVConfig.Lieu = _iniManager.GetString("XacNhanYVai", "Lieu");
            XNYVConfig.Mau = _iniManager.GetString("XacNhanYVai", "Mau");
            XNYVConfig.KetQua = _iniManager.GetString("XacNhanYVai", "KetQua");
            XNYVConfig.ChiTiet = _iniManager.GetString("XacNhanYVai", "ChiTiet");
            XNYVConfig.ETD = _iniManager.GetString("XacNhanYVai", "ETD");
            XNYVConfig.GhiChuGiaoHang = _iniManager.GetString("XacNhanYVai", "GhiChuGiaoHang");
        }
        private void frmHoiHang2_Load(object sender, EventArgs e)
        {
            LoadXNYVConfig();
            txtPoNhuom1.Text = _iniManager.GetString("HoiHang", "PONhuom", "A");
        }
        private async void btnSelectedFile1_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label1;
                gb1.Enabled = false;
                var dl = new OpenFileDialog();
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!");
                        return;
                    }
                    if (selectedPath != lblFileName1.Text)
                    {
                        await Task.Run(() =>
                        {
                            workbook1?.Dispose();
                        });
                        lblFileName1.Text = selectedPath;
                        List<IXLWorksheet> lstSheet = null;
                        await Task.Run(() =>
                        {
                            using var fs = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            workbook1 = new XLWorkbook(fs);
                            lstSheet = workbook1.Worksheets.ToList();
                        });
                        lblFileName1.Text = fileName;
                        cbSheets1.DataSource = lstSheet;
                        cbSheets1.DisplayMember = "Name";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!");
            }
            finally
            {
                gb1.Enabled = true;
            }
        }

        private async void btnSelectedFile2_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = label2;
                gb2.Enabled = false;
                var dl = new OpenFileDialog();
                var file = Encoding.UTF8.GetString(Encoding.Default.GetBytes(_iniManager.GetString("XacNhanYVai", "Path")));
                if (File.Exists(file))
                {
                    dl.InitialDirectory = Path.GetDirectoryName(file);
                    dl.FileName = Path.GetFileName(file);
                }
                dl.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (dl.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dl.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedPath);
                    string extension = System.IO.Path.GetExtension(selectedPath);
                    if (extension != ".xlsx")
                    {
                        MessageBox.Show("File không hợp lệ!");
                        return;
                    }
                    if (selectedPath != lblFileName2.Text)
                    {
                        await Task.Run(() =>
                        {
                            workbook2?.Dispose();
                        });
                        lblFileName2.Text = selectedPath;
                        List<IXLWorksheet> lstSheet = null;
                        await Task.Run(() =>
                        {
                            using var fs = new FileStream(selectedPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            workbook2 = new XLWorkbook(fs);
                            lstSheet = workbook2.Worksheets.ToList();
                        });
                        lblFileName2.Text = fileName;
                        cbSheets2.DataSource = lstSheet;
                        cbSheets2.DisplayMember = "Name";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!");
            }
            finally
            {
                gb2.Enabled = true;
            }
        }
        private string GetStringCellFromRangeRow(IXLRangeRow rangeRow, string columnName)
        {
            var cell = rangeRow.Cell(columnName);
            string cellValue = string.Empty;
            if (cell.IsMerged())
            {
                var mergedRange = cell.MergedRange();
                var topLeft = mergedRange.FirstCell();
                cellValue = topLeft.GetString();
            }
            else
            {
                cellValue = cell.GetString();
            }
            return cellValue;
        }
        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            try
            {
                dgvData.DataSource = null;

                await Task.Yield(); // trả UI ngay lập tức

                _iniManager.WriteString(
                    "HoiHang",
                    "PONhuom",
                    txtPoNhuom1.Text.Trim().ToUpper());

                var sheet1 = cbSheets1.SelectedValue as IXLWorksheet;
                var sheet2 = cbSheets2.SelectedValue as IXLWorksheet;

                if (sheet1 == null || sheet2 == null)
                {
                    MessageBox.Show("Lỗi load Sheet!");
                    return;
                }

                string poColumn = txtPoNhuom1.Text.Trim().ToUpper();

                var result = await Task.Run(() =>
                {
                    // Đọc dữ liệu Excel trong background
                    var rangeRows2 = sheet2.RangeUsed()
                                           .RowsUsed()
                                           .Skip(XNYVConfig.StartIndex - 1)
                                           .ToList();

                    var dict = new ConcurrentDictionary<string, List<XacNhanYVai>>();
                    foreach (var row in rangeRows2)
                    {
                        string po = GetStringCellFromRangeRow(row, XNYVConfig.PONhuom)
                            .Replace("\r", "")
                            .Replace("\n", "");

                        if (string.IsNullOrWhiteSpace(po))
                            continue;

                        var cellNgayNhap = row.Cell(XNYVConfig.NgayNhap);

                        object ngaynhap =
                            cellNgayNhap.DataType == XLDataType.DateTime
                            ? cellNgayNhap.GetDateTime().Date
                            : cellNgayNhap.GetString();

                        var cellETD = row.Cell(XNYVConfig.ETD);

                        object etd =
                            cellETD.DataType == XLDataType.DateTime
                            ? cellETD.GetDateTime().Date
                            : cellETD.GetString();
                        var item = new XacNhanYVai
                        {
                            PONhuom = po,
                            NgayNhap = ngaynhap,
                            KetQua = GetStringCellFromRangeRow(row, XNYVConfig.KetQua),
                            ChiTiet = GetStringCellFromRangeRow(row, XNYVConfig.ChiTiet),
                            ETD = etd,
                            GhiChuGiaoHang = GetStringCellFromRangeRow(row, XNYVConfig.GhiChuGiaoHang)
                        };

                        dict.GetOrAdd(po, _ => new List<XacNhanYVai>())
                            .Add(item);
                    }
                    ;
                    /*
                    Parallel.ForEach(rangeRows2, row =>
                    {
                        string po = GetStringCellFromRangeRow(row, XNYVConfig.PONhuom)
                            .Replace("\r", "")
                            .Replace("\n", "");

                        if (string.IsNullOrWhiteSpace(po))
                            return;

                        var cellETD = row.Cell(XNYVConfig.ETD);

                        object etd =
                            cellETD.DataType == XLDataType.DateTime
                            ? cellETD.GetDateTime().Date
                            : cellETD.GetString();

                        var item = new XacNhanYVai
                        {
                            PONhuom = po,
                            KetQua = GetStringCellFromRangeRow(row, XNYVConfig.KetQua),
                            ChiTiet = GetStringCellFromRangeRow(row, XNYVConfig.ChiTiet),
                            ETD = etd,
                            GhiChuGiaoHang = GetStringCellFromRangeRow(row, XNYVConfig.GhiChuGiaoHang)
                        };

                        dict.GetOrAdd(po, _ => new ConcurrentBag<XacNhanYVai>())
                            .Add(item);
                    });
                    */

                    var rangeRows1 = sheet1.RangeUsed()
                                           .RowsUsed()
                                           .ToList();

                    var dt = new DataTable();

                    dt.Columns.Add("PONhuom");
                    dt.Columns.Add("KetQua");
                    dt.Columns.Add("ChiTiet");
                    dt.Columns.Add("ETD");
                    dt.Columns.Add("GhiChuGiaoHang");

                    int found = 0;

                    foreach (var row in rangeRows1)
                    {
                        var po = row.Cell(poColumn)
                            ?.GetString()
                            .Replace("\r", "")
                            .Replace("\n", "")
                            .Replace(" ", "");

                        if (!string.IsNullOrWhiteSpace(po)
                            && dict.TryGetValue(po, out var lst))
                        {
                            found++;

                            dt.Rows.Add(
                                po,
                                string.Join("\n", lst.Select(x => $"{(x.NgayNhap is DateTime ngaynhap ? ngaynhap.ToString("yyyy/MM/dd") : x.NgayNhap.ToString())}-{x.KetQua ?? "..."}-{x.ChiTiet ?? "..."}-{(x.ETD is DateTime d ? d.ToString("dd-MMM-yyyy") : x.ETD?.ToString())}-{x.GhiChuGiaoHang ?? "..."}")),
                                string.Join("\n", lst.Select(x => x.ChiTiet)),
                                string.Join("\n", lst.Select(x => x.ETD is DateTime d ? d.ToString("dd-MMM-yyyy") : x.ETD?.ToString())),
                                string.Join("\n", lst.Select(x => x.GhiChuGiaoHang))
                            );
                        }
                        else
                        {
                            dt.Rows.Add(po, "", "", "", "");
                        }
                    }

                    return (dt, found);
                });
                dgvData.DataSource = result.dt;

                lblTotalRow.Text = result.dt.Rows.Count.ToString();
                lblTotalFound.Text = result.found.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Lỗi hệ thống",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnStart.Enabled = true;
            }
        }

        private void btnXNYVConfig_Click(object sender, EventArgs e)
        {
            frmXNYVConfig frm = new frmXNYVConfig();
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadXNYVConfig();
            }
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

                    // nếu có xuống dòng thì đặt trong ""
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
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count == 0)
            {
                toolTip1.Show("Không có dữ liệu để sao chép", btnCopy, btnCopy.Width, 0, 1500);
            }
            else
            {
                CopyDataGridViewToClipboard(dgvData);
                toolTip1.Show("Đã sao chép", btnCopy, btnCopy.Width, 0, 1500);
            }
        }

        private void dgvData_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
