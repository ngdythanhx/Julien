using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace JCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class TinhSLConLai
        {
            public string PO { get; set; }
            public decimal Qty { get; set; }
        }
        private List<TinhSLConLai> GetTinhSLConLaiList(string inputText)
        {
            var rows = JHelper.ConvertData.ConvertExcelDataToRows(inputText);
            var lines = rows.Data;
            var lst = new List<TinhSLConLai>();
            if (rows.ColumnCount >= 2)
            {
                foreach (var line in lines)
                {
                    string qtyString = line.Last().Replace(",", "");
                    qtyString = qtyString.LastOrDefault() == '.' ? qtyString.Substring(0, qtyString.Length - 1) : qtyString;
                    if (string.IsNullOrEmpty(line[0]) || !decimal.TryParse(qtyString, out decimal qty))
                    {
                        continue;
                    }
                    else
                    {
                        var s = line[0].Split('\n').Where(x => !string.IsNullOrEmpty(x)).ToList();
                        if (s.Count > 1)
                        {
                            foreach (var a in s)
                            {
                                if (string.IsNullOrEmpty(a))
                                    continue;
                                lst.Add(new TinhSLConLai() { PO = a.Replace(" ", "").Replace("\"", ""), Qty = qty });
                            }
                        }
                        else
                        {
                            lst.Add(new TinhSLConLai() { PO = s[0].Replace(" ","").Replace("\"",""), Qty = qty });
                        }
                    }
                }
            }
            return lst;
        }
        private async void btnStart_TinhSlConLai_Click(object sender, EventArgs e)
        {
            btnStart_TinhSlConLai.Enabled = false;
            var lstXuatKho = new List<TinhSLConLai>();
            await Task.Run(() => lstXuatKho = GetTinhSLConLaiList(txtXuatKho.Text));
            if (lstXuatKho.Count == 0)
            {
                toolTip1.Show("Dữ liệu XuatKho không hợp lệ!", txtXuatKho, txtXuatKho.Width, 0, 1500);
                return;
            }
            lblTotalXuatKho.Text = lstXuatKho.Sum(x => x.Qty).ToString("#,##0.00");

            var lstSMTT = new List<TinhSLConLai>();
            await Task.Run(() => lstSMTT = GetTinhSLConLaiList(txtSMTTData.Text));
            if (lstSMTT.Count == 0)
            {
                toolTip1.Show("Dữ liệu XuatKho không hợp lệ!", txtSMTTData, txtSMTTData.Width, 0, 1500);
                return;
            }
            lblTotalSMTT.Text = lstSMTT.Sum(x => x.Qty).ToString("#,##0.00");
            var dt = new DataTable();
            dt.Columns.Add("PO", typeof(string));
            dt.Columns.Add("SLXuatKho", typeof(float));
            dt.Columns.Add("SLSMTT", typeof(float));
            dt.Columns.Add("SLChenhLech", typeof(float));
            var lstOut = new List<TinhSLConLai>();
            await Task.Run(() =>
            {
                lstOut = lstXuatKho.GroupBy(x => x.PO).Select(x => new TinhSLConLai() { PO = x.First().PO, Qty = x.Sum(y => y.Qty) }).ToList();
                foreach (var item in lstOut)
                {
                    decimal totalXuatKho = lstXuatKho.Where(x => x.PO == item.PO).Sum(x => x.Qty);
                    decimal totalSMTT = lstSMTT.Where(x => x.PO.Contains(item.PO)).Sum(x => x.Qty);
                    item.Qty = totalXuatKho - totalSMTT;
                    dt.Rows.Add(item.PO, totalXuatKho, totalSMTT, totalXuatKho - totalSMTT);
                }
            });
            dgvMain.DataSource = dt;
            lblTotalRows.Text = dgvMain.RowCount.ToString();
            lblTotalRemainQty.Text = lstOut.Sum(x => x.Qty).ToString("#,##0.000");
            btnStart_TinhSlConLai.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"(AL\d{4}-?[A-Z0-9]+)\s*\(([\d.]+)&([A-Z0-9]+)\)";
            string inputText = txtXuatKho.Text;
            inputText = inputText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            var matches = Regex.Matches(inputText, pattern);

            foreach (Match m in matches)
            {
                string maAL = m.Groups[1].Value;
                string soLuong = m.Groups[2].Value;
                string maHang = m.Groups[3].Value;

                Console.WriteLine($"{maHang}");
            }
            foreach (Match m in matches)
            {
                string maAL = m.Groups[1].Value;
                string soLuong = m.Groups[2].Value;
                string maHang = m.Groups[3].Value;

                Console.WriteLine($"{soLuong}");
            }
        }
    }
}
