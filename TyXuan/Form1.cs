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

namespace TyXuan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private class Art
        {
            public string Article { get; set; }
            public decimal Qty { get; set; }
            public string Result => $"{Article} ({Qty})";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"(AL\d{4}-?[A-Z0-9]+)\s*\(([\d.]+)&([A-Z0-9]+)\)";
            string inputText = textBox1.Text;
            inputText = inputText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            var matches = Regex.Matches(inputText, pattern);
            var lst = new List<Art>();
            foreach (Match m in matches)
            {
                string maAL = m.Groups[1].Value;
                string soLuong = m.Groups[2].Value;
                string maHang = m.Groups[3].Value;

                if (decimal.TryParse(soLuong, out decimal qty))
                {
                    var art = new Art
                    {
                        Article = maHang,
                        Qty = qty
                    };
                    lst.Add(art);
                }
            }
            var lstG = lst.GroupBy(x => x.Article).Select(x =>
            {
                var a = x.First();
                a.Qty = x.Sum(y => y.Qty);
                return a;
            }).ToList();
            textBox2.Text = string.Join("\n", lstG.Select(x => x.Result).ToArray());
            lblTotalQty.Text = lstG.Sum(x => x.Qty).ToString("#,##0.000");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"(AL\d{4}-?[A-Z0-9]+)\s*\(([\d.]+)&([A-Z0-9]+)\)";
            string inputText = textBox1.Text;
            inputText = inputText.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
            var matches = Regex.Matches(inputText, pattern);
            var lst = new List<Art>();
            foreach (Match m in matches)
            {
                string maAL = m.Groups[1].Value;
                string soLuong = m.Groups[2].Value;
                string maHang = m.Groups[3].Value;

                if (decimal.TryParse(soLuong, out decimal qty))
                {
                    var art = new Art
                    {
                        Article = maHang,
                        Qty = qty
                    };
                    lst.Add(art);
                }
            }
            var lstG = lst.GroupBy(x => x.Article).Select(x =>
            {
                var a = x.First();
                a.Qty = x.Sum(y => y.Qty);
                return a;
            }).ToList();
            textBox2.Text = string.Join("\n", lstG.Select(x => x.Result).ToArray());
            lblTotalQty.Text = lstG.Sum(x => x.Qty).ToString("#,##0.000");
        }
    }
}
