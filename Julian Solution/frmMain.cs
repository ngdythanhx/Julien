using DocumentFormat.OpenXml.Wordprocessing;
using  Julian_SolutionDatabase.DTO;
using  Julian_Solution.Forms;
using  Julian_SolutionUtils;
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

namespace Julian_Solution
{
    public partial class frmMain : Form
    {
        Dictionary<string, IFormHandler> FormHanlers = new Dictionary<string, IFormHandler>();
        private const int _defaultHeight = 545;
        public frmMain()
        {
            InitializeComponent();
            this.Size = new Size(1200, 800);
            //this.WindowState = FormWindowState.Maximized;
            FormHanlers["Employee"] = new Forms.frmEmployee();
            FormHanlers["EmployeeTask"] = new Forms.frmEmployeeTask();
            FormHanlers["Customer"] = new Forms.frmCustomer();
            FormHanlers["Material"] = new Forms.frmMaterial();
        }
        private void LoadForm(Form frm)
        {
            string tabPageName = "tp" + frm.Name.Substring(3);

            if (!tcMain.TabPages.ContainsKey(tabPageName))
            {
                var tab = new TabPage(frm.Text)
                {
                    Name = tabPageName,
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
            IniManager iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
            Config.ConnectionString = iniManager.GetString("Default", "ConnectionString");


            tsEmployeeTask.PerformClick();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            LoadForm(frm);
        }




        private void btnEmployee_Click(object sender, EventArgs e)
        {
            frmEmployee frm = new frmEmployee();
            LoadForm(frm);
        }


        private void btnMaterial_Click(object sender, EventArgs e)
        {
            frmMaterial frm = new frmMaterial();
            LoadForm(frm);
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            frmAddOrder frmAddOrder = new frmAddOrder();
            frmAddOrder.StartPosition = FormStartPosition.CenterParent;
            frmAddOrder.ShowDialog();
        }

        private void EnabledActionsButton(bool type1, bool type2)
        {
            tsCreateNew.Enabled = type1;
            tsEdit.Enabled = type1;
            tsDelete.Enabled = type1;
            tsSave.Enabled = type2;
            tsCancel.Enabled = type2;
        }
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            if (frm.ActionType == 0)
            {
                EnabledActionsButton(true, false);
            }
            else
            {
                EnabledActionsButton(false, true);
            }
        }
        private void tsEmployee_Click(object sender, EventArgs e)
        {
            var frm = (Forms.frmEmployee)FormHanlers["Employee"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }

        private void tsEmployeeTask_Click(object sender, EventArgs e)
        {
            var frm = (Forms.frmEmployeeTask)FormHanlers["EmployeeTask"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }
        private void tsCustomer_Click(object sender, EventArgs e)
        {
            var frm = (Forms.frmCustomer)FormHanlers["Customer"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }
        private void tsMaterial_Click(object sender, EventArgs e)
        {
            var frm = (Forms.frmMaterial)FormHanlers["Material"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }

        private void tsOrder_Click(object sender, EventArgs e)
        {

        }
        private void tsReload_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.LoadData();
            frm.LockInputs();
            frm.ActionType = 0;
        }
        private void tsCreateNew_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(false, true);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.UnlockInputs();
            frm.ResetInputs();
            frm.ActionType = 1;
        }

        private void tsEdit_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(false, true);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.UnlockInputs();
            frm.ActionType = 2;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.DeleteData();
        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            if (frm.ActionType == 1)
            {
                if (frm.CreateData())
                {
                    frm.ActionType = 0;
                    EnabledActionsButton(true, false);
                }
                else
                {

                }
            }
            else if (frm.ActionType == 2)
            {
                if (frm.UpdateData())
                {
                    frm.ActionType = 0;
                    EnabledActionsButton(true, false);
                }
                else
                {

                }
            }
        }
        private void tsCancel_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(true, false);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.LockInputs();
            frm.LoadData();
            frm.ActionType = 0;
        }


        private void tsGroupByPO_Click(object sender, EventArgs e)
        {
            frmGroupByPO frm = new frmGroupByPO();
            frm.ShowDialog();
        }

        private void tsAddOrder_Click(object sender, EventArgs e)
        {

        }
        private void tsCompare_Click(object sender, EventArgs e)
        {
            frmCompare frm = new frmCompare();
            frm.ShowDialog();
        }


    }
}
