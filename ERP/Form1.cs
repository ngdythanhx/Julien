using JServer;
using JServer.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var api = new ServerAPI();
            var a = await api.Login("b019", "b019");
            var b = await api.GetUserInfo();
            //textBox2.Text= await api.ExQuery(textBox1.Text);
            textBox2.Text=checkBox1.Checked? await api.GetCode(textBox1.Text): await api.ExQuery(textBox1.Text);
            button1.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
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
