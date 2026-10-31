using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
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

namespace TX
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void LoadForm(Form frm, string text)
        {
            string tabPageName = "tp" + frm.Name.Substring(3);

            if (!tcMain.TabPages.ContainsKey(tabPageName))
            {
                var tab = new TabPage(frm.Text)
                {
                    Name = tabPageName,
                    Text = text
                };
                tcMain.TabPages.Add(tab);
                Panel pnlScroll = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = SystemColors.Window,
                    AutoScroll = true
                };

                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Width = tab.Width;
                frm.Height = tab.Height;
                //frm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                frm.Dock = DockStyle.Fill;
                frm.Location = new Point(0, 0);
                frm.AutoScroll = false;
                pnlScroll.Controls.Add(frm);
                tab.Controls.Add(pnlScroll);
                frm.Show();
            }

            tcMain.SelectedTab = tcMain.TabPages[tabPageName];
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadForm(new frmINV(), "INV-PKL");
            LoadForm(new frmLstGuiDiem(), "LIST GỬI DIỄM");
            LoadForm(new frmCS(), "CS");
            LoadForm(new frmSQ(), "SQ");
            LoadForm(new frmTEM(), "Tạo tem");
            tcMain.SelectedIndex = 0;
        }
    }
}
