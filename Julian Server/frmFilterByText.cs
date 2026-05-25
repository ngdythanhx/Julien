using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian_Server
{
    public partial class frmFilterByText : Form
    {
        public List<FilterByText> LstFilterByText => _lstFilterByText;
        private List<FilterByText> _lstFilterByText = new List<FilterByText>();
        public frmFilterByText()
        {
            InitializeComponent();
            comboBox1.DataSource = new[] 
            {
                "Mã Đơn KH\t Mã Hàng KH",
                "Mã Đơn KH\t Mã Hàng KH\t Sl Đặt",
                "Mã Đơn KH\t Mã Hàng KH\t Sl Đặt\t Ngày giao",
                "Mã Đơn KH\t Mã Hàng KH\t Sl Đặt\t Ngày giao\t Màu sắc",
            };
        }

        private void btnTrans_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            var lines = text.Split(Environment.NewLine.ToCharArray()).Where(l => !string.IsNullOrEmpty(l)).ToArray();
            if (lines.Length == 0) return;
            var data = lines.Select(l => l.Split('\t'));
            int maxItem = data.Max(d => d.Length);
            data = data.Where(d => d.Length == maxItem && d.Length >= 2).ToArray();
            foreach (var d in data)
            {
                var filter = new FilterByText()
                {
                    MaDonKH = d[0],
                    MaHangKH = d[1],
                    SlDat = d.Length > 2 && int.TryParse(d[2], out int slDat) ? slDat : -1,
                    NgayGiao = d.Length > 3 && DateTime.TryParse(d[3], out DateTime ngayGiao) && ngayGiao.Year >= 2025 ? ngayGiao : DateTime.MinValue,
                };
                if (!string.IsNullOrEmpty(filter.MaDonKH) && !string.IsNullOrEmpty(filter.MaHangKH))
                {
                    var item = new ListViewItem(d[0]);
                    item.SubItems.Add(filter.MaHangKH);
                    item.SubItems.Add(filter.SlDat == -1 ? "" : filter.SlDat.ToString());
                    item.SubItems.Add(filter.NgayGiao == DateTime.MinValue ? "" : filter.NgayGiao.ToString("yy-MM-dd"));
                    item.Checked = true;
                    listView1.Items.Add(item);
                }
            }
        }

        private void btnUsing_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                {
                    var filter = new FilterByText()
                    {
                        MaDonKH = item.SubItems[0].Text,
                        MaHangKH = item.SubItems[1].Text,
                        SlDat = int.TryParse(item.SubItems[2].Text, out int slDat) ? slDat : -1,
                        NgayGiao = DateTime.TryParse(item.SubItems[1].Text, out DateTime ngayGiao) && ngayGiao.Year >= 2025 ? ngayGiao : DateTime.MinValue,
                    };
                    _lstFilterByText.Add(filter);
                }
            }
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            listView1.Items.Clear();
        }

        private void frmFilterByText_Load(object sender, EventArgs e)
        {

        }
    }
}
