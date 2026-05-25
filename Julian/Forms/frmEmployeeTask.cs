using  Julian.Database.DAO;
using  Julian.Database.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  Julian.Forms
{
    public partial class frmEmployeeTask : Form, IFormHandler
    {
        public int ActionType { get => _actionType; set => _actionType = value; }
        private int _actionType;
        public List<Employee> Employees;
        public List<EmployeeTask> EmployeeTasks;
        private Employee _curEmployee;
        private EmployeeTask _curEmployeeTask;

        public frmEmployeeTask()
        {
            InitializeComponent();
            LockInputs();
            this.Text = "Nhiệm Vụ";
            dtpCreateDatetime.ValueChanged += (sender, e) => { lblCreateDatetime.Text = dtpCreateDatetime.Value.ToString("dd/MM/yyyy HH:mm"); };
            dtpComplateDatetime.ValueChanged += (sender, e) => { lblComplateDatetime.Text = dtpComplateDatetime.Value.ToString("dd/MM/yyyy HH:mm"); };
            dgvEmployee.AutoGenerateColumns = false;
            dgvTask.AutoGenerateColumns = false;

        }
        public void LoadData()
        {
            Employees = EmployeeDAO.Instance.GetEmployees();
            dgvEmployee.DataSource = Employees;
            if (dgvEmployee.Rows.Count == 0)
                ClearUIData();
        }
        private void ClearUIData()
        {
            // lblEmployeeCode.Text = "...";
            //lblEmployeeName.Text = "...";
            txtTaskName.Text = "...";
            lblCreateDatetime.Text = "...";
            lblComplateDatetime.Text = "...";
            rb1.Checked = false;
            rb2.Checked = false;
            txtDescription.Text = "";
        }
        public void LockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
                textBox.BorderStyle = BorderStyle.None;
                textBox.Margin = new Padding(6, 6, 1, 0);
            }
            rb1.AutoCheck = false;
            rb2.AutoCheck = false;
            dtpCreateDatetime.Visible = false;
            dtpComplateDatetime.Visible = false;
            dgvTask.Enabled = true;
            dgvEmployee.Enabled = true;
        }
        public void UnlockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel1.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Margin = new Padding(3, 3, 1, 1);
            }
            rb1.AutoCheck = true;
            rb2.AutoCheck = true;
            dtpCreateDatetime.Visible = true;
            dtpComplateDatetime.Visible = true;
            dgvTask.Enabled = false;
            dgvEmployee.Enabled = false;
        }
        public void ResetInputs()
        {
            dtpCreateDatetime.Value = DateTime.Now;
            dtpComplateDatetime.Value = DateTime.Now;

            rb1.AutoCheck = true;
            rb2.AutoCheck = true;

            txtTaskName.Text = "";
            txtDescription.Text = "";
            txtTaskName.Focus();
        }
        private bool ValidateData()
        {

            string taskName = txtTaskName.Text;
            if (string.IsNullOrEmpty(taskName))
            {
                toolTip1.Show("Không thể bỏ trống!", txtTaskName, txtTaskName.Width, 0, 1500);
                txtTaskName.Focus();
                return false;
            }
            string description = txtDescription.Text;
            if (string.IsNullOrEmpty(description))
            {
                toolTip1.Show("Không thể bỏ trống!", txtDescription, txtDescription.Width, 0, 1500);
                txtDescription.Focus();
                return false;
            }
            return true;
        }
        private void UpdateUIEmployeeTask()
        {
            if (_curEmployeeTask == null) return;
            if (_actionType == 0)
                LockInputs();
            //lblEmployeeCode.Text = _curEmployeeTask.EmployeeCode;
            //lblEmployeeName.Text = _curEmployeeTask.EmployeeName;
            txtTaskName.Text = _curEmployeeTask.TaskName;
            lblCreateDatetime.Text = _curEmployeeTask.CreateDatetime.ToString("dd/MM/yyyy HH:mm");
            dtpCreateDatetime.Value = _curEmployeeTask.CreateDatetime;
            lblComplateDatetime.Text = _curEmployeeTask.CompleteDatetime.ToString("dd/MM/yyyy HH:mm");
            dtpComplateDatetime.Value = _curEmployeeTask.CompleteDatetime;
            rb1.Checked = !_curEmployeeTask.CompleteState;
            rb2.Checked = _curEmployeeTask.CompleteState;
            txtDescription.Text = _curEmployeeTask.Description;
        }
        private void LoadEmployeTasks()
        {
            if (dgvEmployee.CurrentRow != null)
            {
                var em = dgvEmployee.CurrentRow.DataBoundItem as Employee;
                _curEmployee = em;
                lblEmployeeCode.Text = em.EmployeeCode;
                lblEmployeeName.Text = em.EmployeeName;
                var lstTask = EmployeeTaskDAO.Instance.GetEmployeeTasks(em.Id);
                dgvTask.DataSource = lstTask;
                if (lstTask == null || lstTask.Count == 0)
                {
                    rb1.Checked = false;
                    rb2.Checked = false;
                    ClearUIData();
                    LockInputs();
                }
            }
            else
            {
                _curEmployee = null;
                ClearUIData();
            }
        }
        private void dgvEmployee_SelectionChanged(object sender, EventArgs e)
        {
            LoadEmployeTasks();
        }
        private void dgvTask_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTask.CurrentRow == null || dgvTask.CurrentRow.Index < 0)
                return;
            if (dgvTask.CurrentRow != null)
            {
                var employeeTask = dgvTask.CurrentRow.DataBoundItem as EmployeeTask;
                _curEmployeeTask = employeeTask;
                UpdateUIEmployeeTask();
                this.ActiveControl = label1;
            }
            else
            {
                _curEmployeeTask = null;
            }
        }
        public bool CreateData()
        {
            if (!ValidateData())
            {
                return false;
            }
            int employeeId = _curEmployee.Id;
            string taskName = txtTaskName.Text;
            DateTime createDatetime = dtpCreateDatetime.Value;
            DateTime completeDatetime = dtpComplateDatetime.Value;
            string description = txtDescription.Text;
            var response = EmployeeTaskDAO.Instance.CreateEmployeeTask(employeeId, taskName, createDatetime, completeDatetime, description);
            if ((response.ErrCode == 0))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeTasks();
                LockInputs();
                return true;
            }
            else
            {
                MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTaskName.Focus();
                return false;
            }
        }
        public bool UpdateData()
        {
            if (_curEmployee == null)
            {
                MessageBox.Show("Không có dữ liệu Nhân Viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (_curEmployeeTask == null)
            {
                MessageBox.Show("Không có dữ liệu Nhiệm Vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateData())
            {
                return false;
            }
            int taskId = _curEmployeeTask.Id;
            int employeeId = _curEmployee.Id;
            string taskName = txtTaskName.Text;
            DateTime createDatetime = dtpCreateDatetime.Value;
            DateTime completeDatetime = dtpComplateDatetime.Value;
            string description = txtDescription.Text;
            var response = EmployeeTaskDAO.Instance.UpdateEmployeeTask(taskId, employeeId, taskName, createDatetime, completeDatetime, rb2.Checked, description);
            if ((response.ErrCode == 0))
            {
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeTasks();
                LockInputs();
                return true;
            }
            else
            {
                MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTaskName.Focus();
                return false;
            }
        }
        public bool DeleteData()
        {
            if (_curEmployee == null) return false;
            try
            {
                if (MessageBox.Show($"Nhiệm vụ '{_curEmployeeTask.TaskName}' sẽ bị xóa vĩnh viễn.\nĐồng ý?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var response = EmployeeTaskDAO.Instance.DeleteEmployeeTask(_curEmployeeTask.Id);
                    if ((response.ErrCode == 0))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployeTasks();
                        LockInputs();
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private async Task AutoUpdateData()
        {
            while (true)
            {
                await Task.Delay(1000);
                var lst = EmployeeTaskDAO.Instance.GetEmployeeTasks();
                if (EmployeeTasks == null)
                {
                    EmployeeTasks = lst;
                    continue;
                }
                if (lst != null)
                {
                    foreach (var emTask in lst)
                    {
                        var taskSrc = EmployeeTasks.FirstOrDefault(em => em.Id == emTask.Id);
                        if (taskSrc != null)
                        {
                            if (emTask.Id == 6)
                            {

                            }
                            if (taskSrc.CompleteState != emTask.CompleteState)
                            {
                                if (emTask.CompleteState)
                                {
                                    notifyIcon1.ShowBalloonTip(3000, emTask.EmployeeName, "Có nhiệm vụ đã hoàn thành cần xử lý", ToolTipIcon.Info);
                                    await Task.Delay(3000);
                                }
                                taskSrc.CompleteState = emTask.CompleteState;
                            }

                        }
                    }
                }
            }
        }

        private void frmEmployeeTask_Load(object sender, EventArgs e)
        {
            _ = AutoUpdateData();
        }

        private void frmEmployeeTask_Shown(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 200;
        }
    }
}
