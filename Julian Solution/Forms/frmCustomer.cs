using Julian_Solution.Database.DTO;
using  Julian_SolutionDatabase.DAO;
using  Julian_SolutionDatabase.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace  Julian_Solution.Forms
{
    public partial class frmCustomer : Form, IFormHandler
    {
        public int ActionType { get => _actionType; set => _actionType = value; }
        private int _actionType;
        List<Customer> Customers;
        Customer _curCustomer;
        public frmCustomer()
        {
            InitializeComponent();
            LockInputs();
            this.Text = "Khách Hàng";
            dgvCustomers.AutoGenerateColumns = false;
        }
        public void LoadData()
        {
            Customers = CustomerDAO.Instance.GetCustomers();
            dgvCustomers.DataSource = Customers;
            if (dgvCustomers.Rows.Count == 0)
                ClearUIData();
        }
        private void ClearUIData()
        {
            txtCustomerCode.Text = "...";
            txtCustomerName.Text = "...";
            txtAddress.Text = "...";
        }
        public void LockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = true;
                textBox.BorderStyle = BorderStyle.None;
                textBox.Margin = new Padding(6, 6, 1, 0);
            }
            dgvCustomers.Enabled = true;
        }
        public void UnlockInputs()
        {
            foreach (TextBox textBox in tableLayoutPanel2.Controls.OfType<TextBox>())
            {
                textBox.ReadOnly = false;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Margin = new Padding(3, 3, 1, 1);
            }
            dgvCustomers.Enabled = false;
        }
        public void ResetInputs()
        {
            txtCustomerCode.Text = "";
            txtCustomerName.Text = "";
            txtAddress.Text = "";
            txtCustomerCode.Focus();
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
            return true;
        }
        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow?.DataBoundItem is Customer cus)
            {
                _curCustomer = cus;
                LockInputs();

                txtCustomerCode.Text = cus.CustomerCode;
                txtCustomerName.Text = cus.CustomerName;
                txtAddress.Text = cus.Address;
            }
            else
                _curCustomer = null;
        }
        public bool CreateData()
        {
            if (!ValidateData())
            {
                return false;
            }
            string code = txtCustomerCode.Text;
            string name = txtCustomerName.Text;
            string address = txtAddress.Text;
            var response = CustomerDAO.Instance.CreateCustomer(code, name, address);
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
                txtCustomerCode.Focus();
                return false;
            }
        }
        public bool UpdateData()
        {
            if (_curCustomer == null)
            {
                MessageBox.Show("Không có dữ liệu Nhân Viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!ValidateData())
            {
                return false;
            }
            int id = _curCustomer.Id;
            string code = txtCustomerCode.Text;
            string name = txtCustomerName.Text;
            string address = txtAddress.Text;
            var response = CustomerDAO.Instance.UpdateCustomer(id, code, name, address);
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
                txtCustomerCode.Focus();
                return false;
            }
        }
        public bool DeleteData()
        {
            if (_curCustomer == null) return false;
            try
            {
                if (MessageBox.Show($"'{_curCustomer.CustomerName}' và các dữ liệu liên quan sẽ bị xóa vĩnh viễn.\nĐồng ý?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    var response = CustomerDAO.Instance.DeleteCustomer(_curCustomer.Id);
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

        private void frmCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
