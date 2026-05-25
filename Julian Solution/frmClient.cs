using DocumentFormat.OpenXml.Wordprocessing;
using Julian_Solution;
using  Julian_SolutionDatabase.DAO;
using  Julian_SolutionDatabase.DTO;
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

namespace Julian
{
    public partial class frmClient : Form
    {
        private string _employeeCode = string.Empty;
        Employee _employee;
        BindingList<EmployeeTask> _employeeTasks = new BindingList<EmployeeTask>();
        public frmClient()
        {
            InitializeComponent();
            dgvEmployeeTasks.AutoGenerateColumns = false;
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
        private void frmClient_Load(object sender, EventArgs e)
        {
            IniManager iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
            Config.ConnectionString = iniManager.GetString("Default", "ConnectionString");    
            _employeeCode = iniManager.GetString("Client", "EmployeeCode");
            _employee = EmployeeDAO.Instance.GetEmployee(_employeeCode);
            if (_employee != null)
            {
                tsEmployeeCode.Text = $"Mã NV: {_employee.EmployeeCode}";
                tsEmployeeName.Text = $"Tên NV: {_employee.EmployeeName}";
                var lst = EmployeeTaskDAO.Instance.GetEmployeeTasks(_employee.Id);
                _employeeTasks = new BindingList<EmployeeTask>(lst);
                dgvEmployeeTasks.DataSource = _employeeTasks;
            }
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
    }
}
