using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  Julian_SolutionDatabase.DTO;
using  Julian_SolutionDatabase.DAO;
using System.Security.Cryptography;
namespace  Julian_Solution.Forms
{
    public partial class frmEmployee : Form, IFormHandler
    {
        public int ActionType { get => _actionType; set => _actionType = value; }
        private int _actionType;
        List<Employee> Employees;
        Employee _curEmployee;
        public frmEmployee()
        {
            InitializeComponent();
            LockInputs();
            this.Text = "Nhân Viên";
            dgvEmployees.AutoGenerateColumns = false;
        }
        public void LoadData()
        {
            Employees = EmployeeDAO.Instance.GetEmployees();
            dgvEmployees.DataSource = Employees;
            if (dgvEmployees.Rows.Count == 0)
                ClearUIData();
        }
        private void ClearUIData()
        {
            txtEmployeeCode.Text = "...";
            txtEmployeeName.Text = "...";
            txtBrithday.Text = "...";
        }
        public void LockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
                textBox.BorderStyle = BorderStyle.None;
                textBox.Margin = new Padding(6, 6, 1, 0);
            }
            dgvEmployees.Enabled = true;
        }
        public void UnlockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Margin = new Padding(3, 3, 1, 1);
            }
            dgvEmployees.Enabled = false;
        }
        public void ResetInputs()
        {
            txtEmployeeCode.Text = "";
            txtEmployeeName.Text = "";
            txtBrithday.Text = "";
            txtEmployeeCode.Focus();
        }
        private bool ValidateData()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    toolTip1.Show("Không thể bỏ trống!", textBox, textBox.Width, 0, 1500);
                    textBox.Focus();
                    return false;
                }
            }
            if (!int.TryParse(txtBrithday.Text, out int year) || year < 1950 || year > 2020)
            {
                toolTip1.Show("Năm sinh không hợp lệ!", txtBrithday, txtBrithday.Width, 0, 1500);
                txtBrithday.Focus();
                return false;
            }
            return true;
        }
        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow?.DataBoundItem is Employee em)
            {
                _curEmployee = em;
                LockInputs();

                txtEmployeeCode.Text = em.EmployeeCode;
                txtEmployeeName.Text = em.EmployeeName;
                txtBrithday.Text = em.Birthday.Year.ToString();
            }
            else
                _curEmployee = null;
        }
        public bool CreateData()
        {
            if (!ValidateData())
            {
                return false;
            }
            string code = txtEmployeeCode.Text;
            string name = txtEmployeeName.Text;
            int year = int.Parse(txtBrithday.Text);
            DateTime brithday = new DateTime(year, 01, 01);
            var response = EmployeeDAO.Instance.CreateEmployee(code, name, brithday);
            if ((response.ErrCode == 0))
            {
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LockInputs();
                return true;
            }
            else
            {
                MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmployeeCode.Focus();
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
            if (!ValidateData())
            {
                return false;
            }
            int id = _curEmployee.Id;
            string code = txtEmployeeCode.Text;
            string name = txtEmployeeName.Text;
            int year = int.Parse(txtBrithday.Text);
            DateTime brithday = new DateTime(year, 01, 01);
            var response = EmployeeDAO.Instance.UpdateEmployee(id, code, name, brithday);
            if ((response.ErrCode == 0))
            {
                MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                LockInputs();
                return true;
            }
            else
            {
                MessageBox.Show(response.Msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmployeeCode.Focus();
                return false;
            }
        }
        public bool DeleteData()
        {
            if (_curEmployee == null) return false;
            try
            {
                if (MessageBox.Show($"'{_curEmployee.EmployeeName}' và các dữ liệu liên quan(Nhiệm Vụ,..) sẽ bị xóa vĩnh viễn.\nĐồng ý?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var response = EmployeeDAO.Instance.DeleteEmployee(_curEmployee.Id);
                    if ((response.ErrCode == 0))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
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
    }
}
