using ClosedXML.Excel;
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
using static TEM.Helper;

namespace TEM.Forms
{
    public partial class SGC : Form
    {
        public SGC()
        {
            InitializeComponent();
        }
        private class Tem
        {
            public string Item { get; set; }
            public string PO { get; set; }
            public string CODE { get; set; }
            public string Lieu { get; set; }
            public string Mau { get; set; }
            public int Kho { get; set; }
            public string Weight { get; set; }
            public int Qty => int.TryParse(Qty_String, out int qty) ? qty : -1;
            public string Unit { get; set; }
            public string Qty_String { get; set; }
            public int RollQty { get; set; } = 1;
            public int RollIndex { get; set; }
            public string Check
            {
                get
                {
                    if (!int.TryParse(Qty_String, out int qty))
                    {
                        return "Số lượng không hợp lệ";
                    }
                    else if (qty > 100)
                    {
                        return "Số lượng trêm tem lớn hơn 100";
                    }
                    else
                    {
                        return "OK";
                    }
                }
            }

        }
        private async void txtInput_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtInput.Text;
            if (string.IsNullOrEmpty(inputText))
            {
                dgvMain.DataSource = null;
                return;
            }
            //var inputData = inputText.Split('\n').Select(x => x.Split('\t')).Where(x => x.Length >= 19).ToArray();
            var excelData = JHelper.ConvertData.ConvertExcelDataToRows(inputText);
            var inputData = excelData.Data;
            // var inputData = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None).Select(x => x.Split('\t')).Where(x => x.Length >= 24).ToArray();
            if (excelData.ColumnCount < 11)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                dgvMain.DataSource = null;
                return;
            }
            var lst = new List<Tem>();
            await Task.Run(() =>
            {
                for (int i = 0; i < inputData.Count; i++)
                {
                    var item = inputData[i];
                    string qtyString = item[6].Replace(",", "");
                    qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                    var tem = new Tem()
                    {
                        Item = item[0],
                        PO = item[1],
                        CODE = item[2],
                        Lieu = item[3],
                        Mau = item[4],
                        Kho = int.TryParse(item[5], out int w) ? w : -1,
                        Weight = item[6],
                        Qty_String = item[7],
                        Unit = item[8],
                        RollQty = int.TryParse(item[9], out int rq) ? rq : 1,
                        RollIndex = i + 1
                    };
                    if (!string.IsNullOrEmpty(tem.PO) && !string.IsNullOrEmpty(tem.CODE))
                        lst.Add(tem);
                }
            });
            if (lst.Count == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                dgvMain.DataSource = null;
                return;
            }
            dgvMain.DataSource = new SortableBindingList<Tem>(lst);
            lblRows.Text = lst.Count.ToString("");
            lblTotalRoll.Text = lst.Sum(x => x.RollQty).ToString();
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

        private void dgvMain_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.DataSource != null)
            {
                dgvMain.Refresh();
                var lst = dgvMain.DataSource as SortableBindingList<Tem>;
                lblRows.Text = lst.Count.ToString("");
                lblTotalRoll.Text = lst.Sum(x => x.RollQty).ToString();
                lblTotalQty.Text = lst.Sum(x => x.Qty).ToString("#,##0.000");
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
                string filePath = $@"OUTPUT\INV\{txtFilename.Text}.xlsx";
                if (string.IsNullOrWhiteSpace(output) ||
                    output.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    toolTip1.Show("Tên file không hợp lệ!", txtFilename);
                    return;
                }
                if (File.Exists(filePath) && !CheckReadWriteFile(filePath))
                {
                    toolTip1.Show("Có file đã tồn tại đang mở!", txtFilename);
                    return;
                }
                await Task.Run(() =>
                {
                    fs_template = new FileStream($@"Default\TEM_SHC.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    wb_template = new XLWorkbook(fs_template);
                });
                if (wb_template == null)
                {
                    toolTip1.Show("Lỗi load template!", btnCreate);
                    return;
                }

                var lstTem = (dgvMain.DataSource as BindingList<Tem>).ToList();
                string productionDate = txtProductionDate.Text;
                await CreateTEMLST(lstTem, wb_template, productionDate);
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
        private async Task<(bool success, string msg)> CreateTEMLST(List<Tem> lstTem, XLWorkbook wb, string productionDate)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var sheet = wb.Worksheet("Sheet1");
                    string beginCol = "A";
                    string endCol = "F";
                    int temHeight = 21;
                    var rangeTem = wb.Worksheet("TEM").Range($"{beginCol}1:{endCol}{temHeight}");
                    int index = 0;

                    foreach (var tem in lstTem)
                    {

                        CreateTem(
                            rangeTem,
                            sheet.Range($"{beginCol}{index * temHeight + 1}:{endCol}{index * temHeight + temHeight}"),
                            tem,
                            productionDate,
                            dtpCreateDate.Value
                        );
                        index++;
                    }
                    sheet.PageSetup.PrintAreas.Clear();
                    sheet.PageSetup.PrintAreas.Add($"{beginCol}1:{endCol}{index * temHeight}");

                    for (int i = 1; i < index; i++)
                    {
                        sheet.PageSetup.AddHorizontalPageBreak(i * temHeight);
                    }

                    return (true, "");
                }
                catch (Exception ex)
                {
                    return (false, "Lỗi hệ thống: " + ex.Message);
                }
            });
        }
        private void CreateTem(IXLRange rangeTem, IXLRange rangeNewTem, Tem tem, string productionDate, DateTime deliveryDate)
        {
            //if (rangeTem.RangeAddress.ToString() != rangeNewTem.RangeAddress.ToString())
            //{
            rangeTem.CopyTo(rangeNewTem);

            for (int i = 0; i < rangeTem.RowCount(); i++)
            {
                rangeNewTem.Worksheet.Row(rangeNewTem.FirstRow().RowNumber() + i).Height =
                    rangeTem.Worksheet.Row(rangeTem.FirstRow().RowNumber() + i).Height;
            }
            //}
            rangeNewTem.Cell("C2").Value = tem.Lieu;
            rangeNewTem.Cell("C3").Value = tem.Mau;
            rangeNewTem.Cell("C4").Value = tem.Kho;
            rangeNewTem.Cell("C5").Value = tem.Qty;
            rangeNewTem.Cell("C6").Value = tem.PO;
            rangeNewTem.Cell("C7").Value = tem.CODE;
            rangeNewTem.Cell("C8").Value = tem.RollIndex;
            rangeNewTem.Cell("C9").Value = productionDate;
            rangeNewTem.Cell("C13").Value = deliveryDate.ToString("yyyy.MM.dd");
        }
    }
}
