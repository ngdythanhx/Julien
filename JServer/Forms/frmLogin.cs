using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JHelper;
namespace JServer.Forms
{
    public partial class frmLogin : Form
    {
        ServerAPI _serverAPI = new ServerAPI();
        public ServerAPI ServerAPI => _serverAPI;
        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            try
            {
                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    toolTip1.Show("Vui lòng nhập tài khoản", txtUsername, txtUsername.Width - 10, txtUsername.Height, 1500);
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    toolTip1.Show("Vui lòng nhập mật khẩu", txtPassword, txtPassword.Width - 10, txtPassword.Height, 1500);
                    return;
                }
                var login = await _serverAPI.Login(txtUsername.Text.Trim().ToLower(), txtPassword.Text.Trim().ToLower());
                if (!login.Success)
                {
                    toolTip1.Show(login.Msg, txtUsername, txtUsername.Width - 10, txtUsername.Height, 1500);
                    return;
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUsername;
        }
    }
}
