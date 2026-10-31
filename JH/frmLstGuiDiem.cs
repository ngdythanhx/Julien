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

namespace JH
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
            var lst = new List<ListGuiDiem2>();
            foreach (var item in data)
            {
                string qtyString = item[13].Replace(",", "");
                qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                if (
                    decimal.TryParse(qtyString, out decimal qty) &&
                    int.TryParse(item[51], out int id)
                )
                {
                    /*var diem = new ListGuiDiem2()
                    {
                        ID = id,
                        Lieu = item[8],
                        Mau = item[11],
                        MauThe = item[12],
                        Article = item[24],
                        PO = item[4],
                        PONhuom = item[6],
                        Qty = qty,
                    };*/
                    var diem2 = new ListGuiDiem2()
                    {
                        ID = id,
                        Lieu = item[8],
                        MauKD = item[11],
                        MauKho = item[35],
                        NgayDatHang = DateTime.TryParse(item[1], out DateTime ngaydathang) ? ngaydathang : DateTime.MinValue,
                        Brand = item[2],
                        CusName = item[3],
                        CusPO = item[4],
                        CusItemCode = item[5],
                        PONhuom = item[6],
                        PONhuomMoi = item[7],
                        ETD = item[37],
                        ETDNote = item[38],
                        CusNote = item[39],
                        Article = item[24],
                        Width = int.TryParse(item[46], out int width) ? width : -1,
                        Weight = int.TryParse(item[47], out int weight) ? weight : -1,
                        Qty = qty,
                    };
                    lst.Add(diem2);
                }
            }
            if (lst.Count == 0 || lst.Count != data.Count)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                return;
            }
            lst = lst.OrderBy(x => x.Lieu).ThenBy(x => x.MauKD).ThenBy(x => x.Article).ThenBy(x => x.PONhuom).ToList();
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
                var lst = new List<ListGuiDiem2>();
                lst = dgvMain.DataSource as List<ListGuiDiem2>;
                string cusName = cbCus.SelectedValue.ToString();
                await Task.Run(() =>
                {
                    fs_template = new FileStream($@"Default\LISTGUIDIEM2.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreate);
                    return;
                }

                await Create(cusName, dtpCreateDate.Value, lst, wb_template);
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
        private async Task<(bool success, string msg)> Create(string cusName, DateTime deliveryDate, List<ListGuiDiem2> lst, XLWorkbook wb)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("SAN LUONG");
                    var lstDiem = lst
                        .OrderBy(g => g.Lieu)
                        .ThenBy(g => g.MauKD)
                        .ThenBy(g => g.CusPO)
                        .ToList();
                    int n = 4;
                    foreach (var item in lstDiem)
                    {
                        sheet.Row(n++).InsertRowsBelow(1);
                        var row = sheet.Range($"A{n}:O{n}").Row(1);
                        row.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        row.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        row.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        //row.Cell("A").Style.NumberFormat.Format = "#,##0";
                        //row.Style.Font.Bold = false;
                        //var item = lstDiem[i];

                        row.Cell("A").Value = item.Lieu;
                        row.Cell("B").Value = item.MauKD;
                        row.Cell("C").Value = item.MauKho;
                        row.Cell("D").Value = item.NgayDatHang_String;
                        row.Cell("E").Value = item.Brand;
                        row.Cell("F").Value = item.CusName;
                        row.Cell("G").Value = item.CusPO;
                        row.Cell("H").Value = item.CusItemCode;
                        row.Cell("I").Value = item.PONhuom;
                        row.Cell("J").Value = item.PONhuomMoi;
                        row.Cell("K").Value = item.ETD;
                        row.Cell("L").Value = item.ETDNote;
                        row.Cell("M").Value = item.CusNote;
                        row.Cell("N").Value = item.ID;
                        row.Cell("O").Value = item.Weight;
                        row.Cell("P").Value = item.Width;
                        row.Cell("T").Value = item.Qty;

                        row.Cell("Q").Value = item.PONhuom;
                        row.Cell("Q").Style.Font.Bold = false;
                        row.Cell("T").Style.NumberFormat.Format = item.Qty % 1 == 0 ? "#,##0" : "#,##0.000";
                        row.Cell("T").Style.Font.Bold = false;
                        row.Cell("T").Style.Font.FontSize = 12;
                        row.Cell("T").Style.Fill.BackgroundColor = XLColor.FromHtml("#D7E4BE");
                    }
                    sheet.Cell($"A2").Value = $"進出材料表({cusName} PRO)";
                    sheet.Cell($"A3").Value = $"日期( ngày xuất) : {deliveryDate.ToString("MM-dd")}";
                    sheet.Cell($"T{n + 1}").FormulaA1 = $"=SUBTOTAL(109,T5:T{n})";

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
            cbCus.DataSource = new string[] { "JH", "RR" };
        }

        private void dtpCreateDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtFilename_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbCus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
