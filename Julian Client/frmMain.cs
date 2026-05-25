using DocumentFormat.OpenXml.Drawing.Charts;
using Julian;
using Julian.Database.DAO;
using Julian.Database.DTO;
using Julian.Forms;
using Julian.Utils;
using Julian_Server;
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

namespace Julian_Client
{
    public partial class frmMain : Form
    {
        Dictionary<string, IFormHandler> FormHanlers = new Dictionary<string, IFormHandler>();
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        Employee _employee = null;
        public frmMain()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1200, 800);
            //this.WindowState = FormWindowState.Maximized;
            FormHanlers["DYeClaim"] = new frmDyeClaim();
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

        }
        private async void frmMain_Shown(object sender, EventArgs e)
        {
            Config.ConnectionString = _iniManager.GetString("Default", "ConnectionString");
            if (string.IsNullOrEmpty(Config.ConnectionString))
            {
                MessageBox.Show("ConnectionString không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Enabled = false;
            string emCode = _iniManager.GetString("Client", "EmployeeCode");
            lblEmployeeCode.Text = emCode;
            await Task.Run(() => _employee = EmployeeDAO.Instance.GetEmployee(emCode));
            if (_employee == null)
            {
                MessageBox.Show($"Không thể tải dữ liệu Nhân viên '{emCode}'", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void EnabledActionsButton(bool type1, bool type2)
        {
            btnAdd.Enabled = type1;
            btnEdit.Enabled = type1;
            btnDelete.Enabled = type1;
            btnSave.Enabled = type2;
            btnCancel.Enabled = type2;
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

        private void tsOrder_Click(object sender, EventArgs e)
        {

        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.LoadData();
            frm.LockInputs();
            frm.ActionType = 0;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(false, true);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.UnlockInputs();
            frm.ResetInputs();
            frm.ActionType = 1;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(false, true);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.UnlockInputs();
            frm.ActionType = 2;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.DeleteData();
        }
        private void btnSave_Click(object sender, EventArgs e)
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
        private void btnCancel_Click(object sender, EventArgs e)
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
            frm?.Dispose();
        }

        private void tsAddOrder_Click(object sender, EventArgs e)
        {
            frmAddOrder frm = new frmAddOrder();
            frm.ShowDialog();
            frm?.Dispose();
        }
        private void tsCompare_Click(object sender, EventArgs e)
        {
            frmCompare frm = new frmCompare();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void tsHoiHang_Click(object sender, EventArgs e)
        {
            frmHoiHang frm = new frmHoiHang();
            frm.ShowDialog();
            frm?.Dispose();
        }
    }
}
