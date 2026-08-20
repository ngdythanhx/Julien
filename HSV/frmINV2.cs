using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class frmINV2 : Form
    {
        public frmINV2()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string inputText = txtInput.Text;
            string[] lines = inputText.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length == 0)
                return;
            var data = lines.Select(l => l.Split('\t')).Where(x => x.Length >= 52).ToList();
            var inputData = inputText.Split('\n').Select(x => x.Split('\t')).Where(x => x.Length >= 19).ToArray();
            if (inputData.Length == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                return;
            }
            var lstINV = new List<INV>();
            foreach (var item in inputData)
            {
                string qtyString = item[13].Replace(",", "");
                qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;

                string unitPriceUSDString = item[17].Replace(",", "");
                unitPriceUSDString = unitPriceUSDString.LastOrDefault() == '.' ? unitPriceUSDString.Substring(0, unitPriceUSDString.Length - 1) : unitPriceUSDString;
                string unitPriceVNDString = item[17].Replace(",", "");
                unitPriceVNDString = unitPriceVNDString.LastOrDefault() == '.' ? unitPriceVNDString.Substring(0, unitPriceVNDString.Length - 1) : unitPriceVNDString;
                if (
                    decimal.TryParse(qtyString, out decimal qty) &&
                    decimal.TryParse(unitPriceUSDString, out decimal unitPriceUSD) &&
                    int.TryParse(unitPriceVNDString, out int unitPriceVND)
                )
                {
                    var inv = new INV()
                    {
                        ID = int.TryParse(item[0], out int id) ? id : 0,
                        PO = item[01],
                        BuyMonth = item[2],
                        Article = item[3],
                        MaterialCode = item[4],
                        DyeLot = item[5],
                        HSCode = item[6],
                        SaleNo = item[7],
                        Description = item[8],
                        Color = item[9],
                        Pantone = item[10],
                        Thickness = item[11],
                        Qty = qty,
                        Unit = item[13],
                        UnitPriceVND = unitPriceVND,
                        //AmountVND = item[14],
                        DeliveryDate = item[16],
                        TemCode = item[17],
                        UnitPriceUSD = unitPriceUSD,
                    };
                    lstINV.Add(inv);
                }
            }
            if (lstINV.Count == 0 || lstINV.Count != data.Count)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput);
                return;
            }
            dgvMain.DataSource = new SortableBindingList<INV>(lstINV);
            lblRows.Text = lstINV.Count.ToString("#,##0");
            lblTotalQty.Text = lstINV.Sum(x => x.Qty).ToString("#,##0.000");
            lblTotalAmount.Text = lstINV.Sum(x => x.AmountVND).ToString("#,##0.000");
            lblGTGT.Text = (lstINV.Sum(x => x.AmountVND) * 0.08m).ToString("#,##0.000");
            lblGrandTotal.Text = (lstINV.Sum(x => x.AmountVND) * 1.08m).ToString("#,##0.000");
        }
    }
}
