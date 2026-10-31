using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SHC
{
    public partial class frmSQ : Form
    {
        public frmSQ()
        {
            InitializeComponent();
        }
        private async void txtInput_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtInput.Text;
            string[] lines = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length == 0)
                return;
            var a = lines.Select(l => l.Split('\t'));
            var data = lines.Select(l => l.Split('\t')).Where(x => x.Length >= 52).ToList();
            if (data.Count == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                txtInput.Text = "";
                return;
            }
            var lst = new List<SQ>();
            await Task.Run(() =>
            {
                foreach (var item in data)
                {
                    string qtyString = item[13].Replace(",", "");
                    qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                    if (
                        DateTime.TryParse(item[1].Trim(), out DateTime createDate) &&
                        DateTime.TryParse(item[37].Trim(), out DateTime etd) &&
                        decimal.TryParse(qtyString, out decimal qty) &&
                        int.TryParse(item[51], out int id) &&
                        int.TryParse(item[48], out int width)
                    )
                    {
                        var sq = new SQ()
                        {
                            OrderDate = createDate,
                            Brand = item[2],
                            CusName = item[3],
                            POOLD = string.IsNullOrEmpty(item[6]) ? "Mua mới" : item[6],
                            PONEW = item[15],
                            Material = item[8],
                            Color = !string.IsNullOrEmpty(item[12]) ? item[12] : item[11],
                            Qty = qty,
                            Unit = "YD",
                            ETD = etd.AddDays(-1),
                            Season = item[21],
                            Art = item[24],
                            Follow = item[27],
                            PurchaseSpecification = item[28],
                            PurchaserName = item[29],
                            Width = width,
                        };
                        lst.Add(sq);
                    }
                }
            });
            if (lst.Count == 0 || lst.Count != data.Count)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                return;
            }
            lst = lst.GroupBy(x => new { x.POOLD, x.Material, x.Color, x.Follow }).OrderBy(x => x.Key.Material).ThenBy(x => x.Key.Color).Select(x =>
                new SQ()
                {
                    OrderDate = x.Max(a => a.OrderDate),
                    Brand = x.First().Brand,
                    CusName = x.First().CusName,
                    POOLD = x.First().POOLD.Trim('"'),
                    PONEW = x.First().PONEW,
                    Material = x.First().Material,
                    Color = x.First().Color,
                    Qty = x.Sum(a => a.Qty),
                    Unit = "YD",
                    ETD = x.Min(a => a.ETD),
                    Season = x.First().Season,
                    Art = x.First().Art,
                    Follow = x.First().Follow.Trim('"'),
                    PurchaseSpecification = x.First().PurchaseSpecification.Trim('"'),
                    PurchaserName = x.First().PurchaserName,
                    Width = x.First().Width,
                }
            ).ToList();
            dgvMain.DataSource = lst;
            lblTotalRows.Text = lst.Count.ToString("#,##0");
            lblTotalQty.Text = lst.Sum(x => x.Qty).ToString("#,##0.000");
        }
        private bool CheckReadWriteFile(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return true;
                }
            }
            catch (IOException)
            {

                return false;
            }
        }
        private async void btnCreate_Click(object sender, EventArgs e)
        {
            txtInput.Enabled = false;
            txtFilename.Enabled = false;
            btnCreate.Enabled = false;

            FileStream fs_template = null;
            XLWorkbook wb_template = null;

            FileStream fs_Main = null;
            XLWorkbook wb_Main = null;
            try
            {
                string output = txtFilename.Text;
                string filePath = $@"OUTPUT\SQ\{txtFilename.Text}.xlsx";
                if (string.IsNullOrEmpty(output) || output.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                {
                    toolTip1.Show("Tên file không hợp lệ!", txtFilename);
                    return;
                }
                if (File.Exists(filePath) && !CheckReadWriteFile(filePath))
                {
                    toolTip1.Show("Có file đã tồn tại đang mở!", txtFilename);
                    return;
                }
                var lst = new List<SQ>();
                lst = dgvMain.DataSource as List<SQ>;
                //string cusName = cbCus.SelectedValue.ToString();
                await Task.Run(() =>
                {
                    fs_template = new FileStream($@"Default\SQ.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreate);
                    return;
                }

                await Create(lst, wb_template);
                await Task.Run(() => wb_template.SaveAs(filePath));
                if (File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start("explorer.exe", @"/select, " + filePath);
                }
                else
                {
                    MessageBox.Show("Lỗi không xác định!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                txtInput.Enabled = true;
                txtFilename.Enabled = true;
                btnCreate.Enabled = true;

                wb_template?.Dispose();
                fs_template?.Dispose();

                wb_Main?.Dispose();
                fs_Main?.Dispose();
            }
        }
        private async Task<(bool success, string msg)> Create(List<SQ> lst, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("1095");
                    int n = 3;
                    foreach (var item in lst)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        var row = sheet.Range($"A{n}:AW{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        row.Cell("B").Value = item.OrderDate.Date;
                        row.Cell("C").Value = item.Brand;
                        row.Cell("D").Value = item.CusName;
                        row.Cell("G").Value = item.POOLD.Trim('"');
                        row.Cell("H").Value = item.PONEW;
                        row.Cell("I").Value = item.Material;
                        row.Cell("L").Value = item.Color;
                        row.Cell("N").Value = item.Qty;
                        row.Cell("O").Value = item.Unit;
                        row.Cell("T").Value = item.ETD.Date;
                        row.Cell("V").Value = item.Season;
                        row.Cell("Y").Value = item.Art;
                        row.Cell("AB").Value = item.Follow.Trim('"');
                        row.Cell("AC").Value = item.PurchaseSpecification.Trim('"');
                        row.Cell("AD").Value = item.PurchaserName;
                        row.Cell("AW").Value = item.Width;

                    }

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
    }
}
