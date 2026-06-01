using Julian.Database.DAO;
using Julian.Database.DTO;
using Julian.Utils;
using Julian;
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
using Julian.Forms;

namespace Julian_Client
{
    public partial class frmClient : Form
    {
        private string _employeeCode = string.Empty;
        Employee _employee;
        BindingList<EmployeeTask> _employeeTasks = new BindingList<EmployeeTask>();
        private int _actionType;
        public int ActionType { get => _actionType; set => _actionType = value; }
        public frmClient()
        {
            InitializeComponent();
            dgvEmployeeTasks.AutoGenerateColumns = false;
            LockInputs();
            EnabledActionsButton(true, false);
            tsTools.Enabled = false;
        }
        private void ShowNotifyTask(int type)
        {
            if (type == 1)
            {
                notifyIcon1.ShowBalloonTip(3000, "Nhiệm vụ mới", "Bạn có nhiệm vụ mới cần xử lý", ToolTipIcon.Info);
            }
            else if (type == 2)
            {
                notifyIcon1.ShowBalloonTip(3000, "Cập nhật nhiệm vụ", "Có nhiệm vụ đã thay đổi nội dung cần xử lý", ToolTipIcon.Info);
            }
        }
        private void GetEmployeeTask()
        {
            if (_employee != null)
            {
                tsEmployeeCode.Text = $"Mã NV: {_employee.EmployeeCode}";
                tsEmployeeName.Text = $"Tên NV: {_employee.EmployeeName}";
                var lst = EmployeeTaskDAO.Instance.GetEmployeeTasks(_employee.Id);
            }
        }
        private void LoadData()
        {
            if (_employee != null)
            {
                tsEmployeeCode.Text = $"Mã NV: {_employee.EmployeeCode}";
                tsEmployeeName.Text = $"Tên NV: {_employee.EmployeeName}";
                var lst = EmployeeTaskDAO.Instance.GetEmployeeTasks(_employee.Id);
                _employeeTasks = new BindingList<EmployeeTask>(lst);
                dgvEmployeeTasks.DataSource = _employeeTasks;
            }
        }
        public void LockInputs()
        {
            rb1.AutoCheck = false;
            rb2.AutoCheck = false;
            dgvEmployeeTasks.Enabled = true;
        }
        public void UnlockInputs()
        {
            rb1.AutoCheck = true;
            rb2.AutoCheck = true;
            dgvEmployeeTasks.Enabled = true;
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
        }
        private void frmClient_Load(object sender, EventArgs e)
        {
            IniManager iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
            Config.Instance.ConnectionString = iniManager.GetString("Default", "ConnectionString");
            _employeeCode = iniManager.GetString("Client", "EmployeeCode");
            _employee = EmployeeDAO.Instance.GetEmployee(_employeeCode);
            LoadData();
            _ = AutoUpdateData();
            if (_employee != null && _employee.Id != 0)
            {
                tsTools.Enabled = true;
                //LoadForm(new Julian.Forms.frmGroupByPO());
                //LoadForm(new Julian.Forms.frmCompare());
            }

            tcMain.SelectedTab = tcMain.TabPages[0];
        }

        private void dgvEmployeeTasks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEmployeeTasks.Columns[e.ColumnIndex].DataPropertyName == "CompleteState" && e.Value != null)
            {
                if (e.Value is bool b)
                {
                    e.Value = b ? "Đã xong" : "Chưa xong";
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvEmployeeTasks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployeeTasks.CurrentRow.DataBoundItem is EmployeeTask employeeTask)
            {
                lblTaskName.Text = employeeTask.TaskName;
                lblCreatedDatetime.Text = employeeTask.CreateDatetime.ToString("dd/MM/yyyy HH:mm");
                lblCompletedDatetime.Text = employeeTask.CompleteDatetime.ToString("dd/MM/yyyy HH:mm");
                rb1.Checked = !employeeTask.CompleteState;
                rb2.Checked = employeeTask.CompleteState;
                lblDescription.Text = employeeTask.Description;
            }
        }
        private void EnabledActionsButton(bool type1, bool type2)
        {
            tsEdit.Enabled = type1;
            tsSave.Enabled = type2;
            tsCancel.Enabled = type2;
        }
        private void tsReload_Click(object sender, EventArgs e)
        {
            if (tcMain.SelectedIndex == 0)
            {
                LoadData();
                LockInputs();
            }
        }

        private void tsEdit_Click(object sender, EventArgs e)
        {
            EnabledActionsButton(false, true);
            UnlockInputs();
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            if (dgvEmployeeTasks.CurrentRow.DataBoundItem is EmployeeTask employeeTask)
            {
                DateTime completedDatetime = employeeTask.CompleteDatetime;
                if (rb1.Checked)
                {
                    completedDatetime = employeeTask.CreateDatetime;
                }
                else if (rb2.Checked)
                {
                    completedDatetime = DateTime.Now;
                }
                var response = EmployeeTaskDAO.Instance.UpdateComplateState(employeeTask.Id, rb2.Checked, completedDatetime);
                if (response.ErrCode == 0)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tsCancel.PerformClick();
                }
                else
                {
                    MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void tsCancel_Click(object sender, EventArgs e)
        {
            EnabledActionsButton(true, false);
            LoadData();
            LockInputs();
        }
        private async Task AutoUpdateData()
        {
            while (true)
            {
                await Task.Delay(1000);
                if (_employee == null)
                    continue;
                var lst = EmployeeTaskDAO.Instance.GetEmployeeTasks(_employee.Id);
                if (lst != null)
                {
                    int countNew = 0;
                    int countUpdate = 0;
                    var lstUpdate = new List<EmployeeTask>();
                    foreach (var emTask in lst)
                    {
                        var taskSrc = _employeeTasks.FirstOrDefault(em => em.Id == emTask.Id);
                        if (taskSrc == null)
                        {
                            _employeeTasks.Add(emTask);
                            countNew++;
                        }
                        else
                        {
                            if (taskSrc.TaskName != emTask.TaskName || taskSrc.Description != emTask.Description)
                            {
                                taskSrc.TaskName = emTask.TaskName;
                                taskSrc.Description = emTask.Description;
                                countUpdate++;
                            }
                        }
                    }
                    if (countNew > 0)
                    {
                        ShowNotifyTask(1);
                    }
                    else if (countUpdate > 0)
                    {
                        ShowNotifyTask(2);
                    }
                }
            }
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
