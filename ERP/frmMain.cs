using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JServer.Forms;
namespace ERP
{
    public partial class frmMain : Form
    {
        public frmMain()
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
        private void tsCS_Click(object sender, EventArgs e)
        {

        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.ShowDialog();
            if (frmLogin.DialogResult == DialogResult.OK)
            {
                var x = frmLogin.ServerAPI;
                string u = x.UserInfo.FullName;
            }
        }
    }
}
