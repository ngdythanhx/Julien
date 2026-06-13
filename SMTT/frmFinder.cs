using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTT
{
    public partial class frmFinder : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        //private Trustrace _trustrace;
        private Trustrace _trustrace = new Trustrace();
        private BindingList<Finder> _lstFinder = new BindingList<Finder>();
        public frmFinder()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dataGridView1, true, null);
            dataGridView1.AutoGenerateColumns = false;
            WindowState = FormWindowState.Maximized;
            this.ActiveControl = textBox1;
        }
        private void frmFinder_Load(object sender, EventArgs e)
        {
            rbType1.Checked = true;
            //col_production.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //col_outbound.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //col_production_lotid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            //col_outbound_lotid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            txtUsername.Text = _iniManager.GetString("Login", "Username", "jenny@yeechain.com");
            txtPassword.Text = _iniManager.GetString("Login", "Password", "WUN2bioxMjM0");
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            if (_lstFinder.Count == 0)
                return;
            lblProcess.Text = $"{_lstFinder.Count(c => c.Finish)}/{_lstFinder.Count.ToString("#,##0")}";
            var a = (double)_lstFinder.Count(c => c.Finish) / (double)_lstFinder.Count * 100;
            progressBar1.Value = (int)a;
            if (_lstFinder.Count(c => c.Finish) == _lstFinder.Count)
                timer1.Stop();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            if (rbType1.Checked)
            {
                col_input_lotid.Visible = true;
                col_input_pocustomer.Visible = false;
                col_input_itemid.Visible = false;
                col_input_marerialid.Visible = false;
                col_input_qty.Visible = false;
            }
            else if (rbType2.Checked)
            {
                col_input_lotid.Visible = false;
                col_input_pocustomer.Visible = true;
                col_input_itemid.Visible = true;
                col_input_marerialid.Visible = false;
                col_input_qty.Visible = true;
            }
            else if (rbType3.Checked)
            {
                col_input_lotid.Visible = false;
                col_input_pocustomer.Visible = true;
                col_input_itemid.Visible = false;
                col_input_marerialid.Visible = true;
                col_input_qty.Visible = true;
            }
        }
        private async void Start()
        {
            btnStart.Enabled = false;
            this.ActiveControl = label1;
            try
            {
                if (!_trustrace.IsLogin)
                {
                    if (!await Login())
                        return;
                }
                var text = textBox1.Text.Replace("\r", "");
                var list = text.Split('\n');

                _lstFinder.Clear();

                foreach (var line in list)
                {
                    var arr = line.Split('\t');
                    if (arr.Length == 0 || string.IsNullOrEmpty(arr[0]))
                        continue;
                    Finder finder = null;
                    if (rbType1.Checked)
                    {
                        finder = new Finder(_trustrace, arr[0]);
                    }
                    else if (rbType2.Checked)
                    {
                        if (arr.Length < 2)
                            continue;
                        double input_qty = arr.Length == 2 || !double.TryParse(arr[2], out var qty) ? -1 : qty;
                        finder = new Finder(_trustrace, 2, arr[0], arr[1], "", input_qty);

                    }
                    else if (rbType3.Checked)
                    {
                        if (arr.Length < 2)
                            continue;
                        double input_qty = arr.Length == 2 || !double.TryParse(arr[2], out var qty) ? -1 : qty;
                        finder = new Finder(_trustrace, 3, arr[0], "", arr[1], input_qty);

                    }
                    _lstFinder.Add(finder);
                }
                lblProcess.Text = $"0/{_lstFinder.Count.ToString("#,##0")}";
                dataGridView1.DataSource = _lstFinder;
                timer1.Start();

                List<Task> tasks = new List<Task>();
                foreach (var finder in _lstFinder)
                {
                    finder.Status = "waiting...";
                    tasks.Add(finder.StartFind());
                    await Task.Delay((int)updSpeed.Value);
                }
                await Task.WhenAll(tasks);
                btnStart.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống:\n" + ex.Message);
            }
            finally
            {
                btnStart.Enabled = true;
            }

        }
        private async Task<bool> Login()
        {
            var login = await _trustrace.Login(txtUsername.Text, txtPassword.Text);
            if (!login.success)
            {
                MessageBox.Show(login.msg);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {

        }
    }
}
