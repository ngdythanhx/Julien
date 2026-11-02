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

namespace HSV
{
    public partial class frmINVUSD : Form
    {
        public frmINVUSD()
        {
            InitializeComponent();
        }

        private async void txtInput2_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtInput.Text;
            if (string.IsNullOrEmpty(inputText))
            {
                dgvMain.DataSource = null;
                return;
            }
            var excelData = JHelper.ConvertData.ConvertExcelDataToRows(inputText);
            var inputData = excelData.Data;
            if (excelData.ColumnCount < 24)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                dgvMain.DataSource = null;
                return;
            }
            var lst = new List<INVPKLUSD>();
            await Task.Run(() =>
            {
                foreach (var item in inputData)
                {
                    string qtyString = item[19].Replace(",", "");
                    qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                    var pgh = new INVPKLUSD()
                    {
                        //ID = int.TryParse(item[13], out int id) ? id : -1,
                        Lieu = item[0],
                        MauKD = item[1],
                       // MauKho = item[2],
                        //NgayDatHang = DateTime.TryParse(item[3], out DateTime ngaydathang) ? ngaydathang : DateTime.MinValue,
                        //Brand = item[4],
                       // CusName = item[5],
                        CusPO = item[6],
                        CusItemCode = item[7],
                        //PONhuom = item[8],
                        //PONhuomMoi = item[9],
                        //ETD = item[10],
                        //ETDNote = item[11],
                        Article = item[12],
                        Width = int.TryParse(item[15], out int width) ? width : 0,
                        UnitPriceUSD = decimal.TryParse(item[14], out decimal unitPrice) ? unitPrice : 0,
                        //Weight = int.TryParse(item[47], out int weight) ? weight : -1,
                        Qty = int.TryParse(item[19], out int q) ? q : 0,
                        RollString = item[23].Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\"", "")
                    };
                    lst.Add(pgh);
                }
            });
            if (lst.Count == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                dgvMain.DataSource = null;
                return;
            }
            lst = lst.GroupBy(x =>new { x.Lieu, x.CusItemCode, x.ArticleShort }).Select(x =>
            {
                var a = x.First();
                a.Qty = x.Sum(y => y.Qty);
                return a;
            }).ToList();
            dgvMain.DataSource = new SortableBindingList<INVPKLUSD>(lst);
            lblRows.Text = lst.Count.ToString("#,##0");
            lblTotalQty.Text = lst.Sum(x => x.Qty).ToString("#,##0.000");
            lblTotalRollQty.Text = lst.Sum(x => x.RollQty).ToString("#,##0.000");
        }

        private void lblTotalQty_Click(object sender, EventArgs e)
        {

        }
    }
}
