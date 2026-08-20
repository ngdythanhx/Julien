using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Julian.Helper;
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

namespace HSV
{
    public partial class frmLstGuiDiem : Form
    {
        public frmLstGuiDiem()
        {
            InitializeComponent();
        }
        private void txtInput_TextChanged(object sender, EventArgs e)
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
                return;
            }
            var lst = new List<ListGuiDiem>();
            foreach (var item in data)
            {
                string qtyString = item[13].Replace(",", "");
                qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                if (
                    decimal.TryParse(qtyString, out decimal qty) &&
                    int.TryParse(item[51], out int id)
                )
                {
                    var diem = new ListGuiDiem()
                    {
                        ID = id,
                        Lieu = item[8],
                        Mau = item[11],
                        MauThe = item[12],
                        Article = item[24],
                        PO = item[4],
                        PONhuom = item[6],
                        Qty = qty,
                    };
                    lst.Add(diem);
                }
            }
            if (lst.Count == 0 || lst.Count != data.Count)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                return;
            }
            lst = lst.OrderBy(x => x.Lieu).ThenBy(x => x.Mau).ThenBy(x => x.Article).ThenBy(x => x.PONhuom).ToList();
            dgvMain.DataSource = lst;

            /*
            dgvMain.DataSource = new SortableBindingList<INV>(lst);
            lblRows.Text = lst.Count.ToString("#,##0");
            lblTotalQty.Text = lst.Sum(x => x.Qty).ToString("#,##0.000");
            lblTotalAmount.Text = lst.Sum(x => x.AmountVND).ToString("#,##0.000");
            lblGTGT.Text = (lst.Sum(x => x.AmountVND) * 0.08m).ToString("#,##0.000");
            lblGrandTotal.Text = (lst.Sum(x => x.AmountVND) * 1.08m).ToString("#,##0.000");
            */
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
            cbCus.Enabled = false;
            txtFilename.Enabled = false;
            btnCreate.Enabled = false;

            FileStream fs_template = null;
            XLWorkbook wb_template = null;

            FileStream fs_Main = null;
            XLWorkbook wb_Main = null;
            try
            {
                string output = txtFilename.Text;
                string filePath = $@"OUTPUT\ListGuiDiem\{txtFilename.Text}.xlsx";
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
                var lst = new List<ListGuiDiem>();
                lst = dgvMain.DataSource as List<ListGuiDiem>;
                string cusName = cbCus.SelectedValue.ToString();
                await Task.Run(() =>
                {
                    fs_template = new FileStream($@"Default\LISTGUIDIEM.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreate);
                    return;
                }

                await Create(cusName, lst, wb_template);
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
                cbCus.Enabled = true;
                txtFilename.Enabled = true;
                btnCreate.Enabled = true;

                wb_template?.Dispose();
                fs_template?.Dispose();

                wb_Main?.Dispose();
                fs_Main?.Dispose();
            }
        }
        private async Task<(bool success, string msg)> Create(string cusName, List<ListGuiDiem> lst, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("SAN LUONG");
                    var lstG = lst
                        .GroupBy(x => new { x.Lieu, x.Mau, x.Article, x.PO })
                        .OrderBy(g => g.Key.Lieu)
                        .ThenBy(g => g.Key.Mau)
                        .ThenBy(g => g.Key.Article)
                        .ThenBy(g => g.Key.PO)
                        .Select(g => g.ToList())
                        .ToList();
                    int n = 4;
                    foreach (var lstDiem in lstG)
                    {
                        for (int i = 0; i < lstDiem.Count; i++)
                        {
                            sheet.Row(n++).InsertRowsBelow(1);
                            var row = sheet.Range($"A{n}:O{n}").Row(1);
                            row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                            row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            //row.Cell("A").Style.NumberFormat.Format = "#,##0";
                            //row.Style.Font.Bold = false;
                            var item = lstDiem[i];

                            row.Cell("A").Value = item.ID;
                            row.Cell("B").Value = item.Lieu;
                            row.Cell("C").Value = item.Mau;
                            row.Cell("D").Value = item.MauThe;
                            row.Cell("E").Value = item.Article;
                            row.Cell("F").Value = item.PO;
                            row.Cell("G").Value = item.PONhuom;
                            row.Cell("G").Style.Font.Bold = false;
                            row.Cell("J").Value = item.Qty;
                            row.Cell("J").Style.NumberFormat.Format = item.Qty % 1 == 0 ? "#,##0" : "#,##0.000";
                            row.Cell("J").Style.Font.Bold = false;
                            row.Cell("J").Style.Font.FontSize = 14;
                            row.Cell("J").Style.Fill.BackgroundColor = XLColor.FromHtml("#D7E4BE");
                            if (i == lstDiem.Count - 1)
                            {
                                sheet.Range($"N{n - lstDiem.Count + 1}:N{n}").Merge();
                                sheet.Cell($"N{n - lstDiem.Count + 1}").Value = lstDiem.Sum(x => x.Qty);
                                sheet.Range($"N{n - lstDiem.Count + 1}:N{n}").Style.NumberFormat.Format = lstDiem.Sum(x => x.Qty) % 1 == 0 ? "#,##0" : "#,##0.000";
                                sheet.Range($"N{n - lstDiem.Count + 1}:N{n}").Style.Font.Bold = false;
                            }
                        }
                    }
                    sheet.Cell($"J{n + 1}").FormulaA1 = $"=SUBTOTAL(109,J5:J{n})";
                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }

        private void frmLstGuiDiem_Load(object sender, EventArgs e)
        {
            cbCus.DataSource = new string[] { "HSV", "HWK" };
        }
    }
}
