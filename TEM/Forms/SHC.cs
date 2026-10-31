using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TEM.Helper;

namespace TEM.Forms
{
    public partial class SHC : Form
    {
        public SHC()
        {
            InitializeComponent();
        }
        private class Tem
        {
            public int STT { get; set; }
            public string PO { get; set; }
            public string CODE { get; set; }
            public string Lieu { get; set; }
            public string Mau { get; set; }
            public int Kho { get; set; }
            public decimal Qty { get; set; }
            public string Unit { get; set; }
            public int RollQty
            {
                get
                {
                    try
                    {
                        if (string.IsNullOrEmpty(RollString))
                            return 0;
                        var x = RollString.Replace(" ", "");
                        var l = x.Split('+');
                        int n = 0;
                        foreach (var item in l)
                        {
                            if (item.Contains("*"))
                            {
                                n += int.Parse(item.Substring(0, item.IndexOf('*')));
                            }
                            else
                                n++;
                        }
                        return n;
                    }
                    catch
                    {
                        return -1
                            ;
                    }
                }
            }
            public string RollString { get; set; }
            public string Lot { get; set; }
            public string Check
            {
                get
                {
                    string chars = "0123456789+*";
                    if (RollString.Any(x => !chars.Contains(x)))
                        return "Số cây chứa ký tự không đúng";
                    if (string.IsNullOrEmpty(RollString))
                        return "Chưa nhập số cây";
                    if (!"0123456789".Contains(RollString.First()))
                        return "Ký tự đầu trong Số cây không đúng";
                    if (!"0123456789".Contains(RollString.Last()))
                        return "Ký tự cuối trong Số cây không đúng";
                    var arr = RollString.Split('+').Select(x => x.Split('*')).ToArray();
                    if (arr.Any(x => x.Any(y => !decimal.TryParse(y, out _))))
                        return "Số cây không hợp lệ";
                    if (arr.Any(x => x.Any(y => int.Parse(y)>100)))
                        return "Có cây lớn hơn 100";
                    var e = Helper.RollStringToQty(RollString);
                    if (e.Result <= 0)
                        return e.Msg;
                    if (e.Result > Math.Ceiling(Qty))
                        return "Số lượng trên Tem lớn hơn số lượng giao";
                    if (e.Result < Math.Ceiling(Qty))
                        return "Số lượng trên Tem nhỏ hơn số lượng giao";
                    return "OK";
                }
            }
        }

        private void SHC_Load(object sender, EventArgs e)
        {
            dgvMain.AutoGenerateColumns = false;
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
                        STT = i + 1,
                        PO = item[1],
                        CODE = item[2],
                        Lieu = item[3],
                        Mau = item[4],
                        Kho = int.TryParse(item[5], out int w) ? w : -1,
                        Qty = decimal.TryParse(qtyString, out decimal q) ? q : -1,
                        Unit = item[7],
                        RollString = item[9].Replace("\r", "").Replace("\n", "").Replace(" ", ""),
                        Lot = item[10]
                    };
                    lst.Add(tem);
                }
                var lstG = lst.GroupBy(x => new { x.CODE, x.Mau }).Select(x =>
                {
                    var t = x.First();
                    t.Qty = x.Sum(y => y.Qty);
                    return t;
                }).ToList();


                lst = lstG;
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
                        var l1 = tem.RollString.Split('+');

                        foreach (var i1 in l1)
                        {
                            int n = i1.Contains("*")
                                ? int.Parse(i1.Substring(0, i1.IndexOf('*')))
                                : 1;

                            decimal qty = i1.Contains("*")
                                ? decimal.Parse(i1.Substring(i1.IndexOf('*') + 1))
                                : decimal.Parse(i1);

                            for (int i = 0; i < n; i++)
                            {
                                CreateTem(
                                    rangeTem,
                                    sheet.Range($"{beginCol}{index * temHeight + 1}:{endCol}{index * temHeight + temHeight}"),
                                    tem,
                                    index+1,
                                    productionDate,
                                    dtpCreateDate.Value
                                );
                                index++;
                            }
                        }
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
        private void CreateTem(IXLRange rangeTem, IXLRange rangeNewTem, Tem tem, int index, string productionDate, DateTime deliveryDate)
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
            rangeNewTem.Cell("C8").Value = index;
            rangeNewTem.Cell("C9").Value = productionDate;
            rangeNewTem.Cell("C13").Value = deliveryDate.ToString("yyyy.MM.dd");
        }
    }
}
