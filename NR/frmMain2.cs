using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NR
{
    public partial class frmMain2 : Form
    {
        public frmMain2()
        {
            InitializeComponent();
        }
        private void LoadForm(Form frm, string text)
        {
            string tabPageName = "tp" + frm.Name;

            if (!tcMain.TabPages.ContainsKey(tabPageName))
            {
                var tab = new TabPage(text)
                {
                    Name = tabPageName,
                };
                tcMain.TabPages.Add(tab);
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Width = tab.Width - 20;
                frm.Height = tab.Height - 20;
                //frm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                frm.Dock = DockStyle.Fill;
                frm.Location = new Point(0, 0);
                frm.AutoScroll = false;
                tab.Controls.Add(frm);
                frm.Show();
            }

            tcMain.SelectedTab = tcMain.TabPages[tabPageName];
        }
        private void frmMain2_Load(object sender, EventArgs e)
        {

        }

        private void tsNR_LstDiem_Click(object sender, EventArgs e)
        {
            LoadForm(new Forms.PhieuGiaoHang_NR(), "NR Phiếu giao hàng");
        }
    }
}
