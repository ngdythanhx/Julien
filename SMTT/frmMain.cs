using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SMTT
{
    public partial class frmMain : Form
    {
        HttpClient _httpClient = null;
        BindingList<CheckLotID> LstCheckLotID = new BindingList<CheckLotID>();
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        private Trustrace _trustrace = new Trustrace();
        public frmMain()
        {
            InitializeComponent();
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dataGridView1, true, null);
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dataGridView2, true, null);
            dataGridView1.AutoGenerateColumns = false;
            WindowState = FormWindowState.Maximized;
            advancedDataGridView1.FilterStringChanged += (s, ee) =>
            {
                _bindingSource.Filter = advancedDataGridView1.FilterString;
                advancedDataGridView1.Refresh();
                lblTotalRows.Text = advancedDataGridView1.Rows.Count.ToString();
                float totalQty = 0;

                foreach (DataRowView rowView in _bindingSource)
                {
                    if (float.TryParse(
                        rowView["SLChenhLech"]?.ToString(),
                        out float qty))
                    {
                        totalQty += qty;
                    }
                }

                lblTotalRemainQty.Text = totalQty.ToString("N2");
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private async Task<HttpClient> CreateHttpClient()
        {
            var cookies = new CookieContainer();

            string domain = ".trustrace.com";

            // Cookie bắt buộc
            //cookies.Add(new Cookie("tt_token", "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJqZW5ueUB5ZWVjaGFpbi5jb20iLCJjb21wYW55LWNvbnRleHQiOnsiaWQiOiI2MGIyMGYxNDQyOTQxMjdmOGFmNjRjNmUiLCJuYW1lIjoiYWRpZGFzIGJyYW5kIn0sInRpZXItY29udGV4dCI6eyJrZXkiOiJ0cl90aWVyMiIsInZhbHVlIjoiVGllciAyIn0sImV4cCI6MTc3NDg1NzI3NSwiaWF0IjoxNzc0ODU1NDc1fQ.QwyR58ncXmKM5wLUzdt_hGya5fCztWr9OWdhDyxR6Ojtzf7RdCE7lfRgnlW6twkoL2jDtYpIXkvAWgMCE47XUw", "/", domain));
            cookies.Add(new Cookie("TT-XSRF-TOKEN", "1", "/", domain));
            //cookies.Add(new Cookie("refreshtoken", "69be60e76472-78f4216b9a17_491b9b3b119d_a6d6-401c-69be60e76472491b9b3b119e", "/", domain));

            var handler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = cookies,
            };

            var client = new HttpClient(handler);
            //client.Timeout = TimeSpan.FromSeconds(3);
            // Headers giống browser
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 ...");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add("Origin", "https://adidas.trustrace.com");
            client.DefaultRequestHeaders.Add("Referer", "https://adidas.trustrace.com/");
            client.DefaultRequestHeaders.Add("X-TT-XSRF-TOKEN", "1");
            if (await Login(client))
            {
                return client;
            }
            client.Dispose();
            return null;
        }
        private async Task<bool> Login(HttpClient client)
        {
            var jsonData = new
            {
                username = txtUsername.Text,
                password = txtPassword.Text,
            };
            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
            var res = await client.PostAsync("https://adidas.api.trustrace.com/api/auth/login", content);
            var resStatus = res.StatusCode;
            if (resStatus == HttpStatusCode.OK)
            {
                var resResultString = await res.Content.ReadAsStringAsync();
                var preAuthToken = JObject.Parse(resResultString)["preAuthToken"]?.ToString();
                client.DefaultRequestHeaders.Add("X-Token", preAuthToken);
                var res2 = await client.PostAsync("https://adidas.api.trustrace.com/api/auth/get-access-token", new StringContent("{}", Encoding.UTF8, "application/json"));
                _iniManager.WriteString("Login", "Username", txtUsername.Text);
                _iniManager.WriteString("Login", "Password", txtPassword.Text);
                return true;
            }
            toolTip1.Show("Tài khoản hoặc mật khẩu không đúng!", txtPassword, txtPassword.Location, 1500);
            return false;
        }
        private async Task<bool> Login()
        {
            var login =await _trustrace.Login(txtUsername.Text,txtPassword.Text);
            if(!login.success)
            {
                MessageBox.Show(login.msg);
                return false;
            }
            else
            {
                return true;
            }
        }
        private async void Start()
        {

            btnStart.Enabled = false;
            var text = textBox1.Text.Replace("\r", "");
            var list = text.Split('\n');
            //int id = 0;
            LstCheckLotID.Clear();

            foreach (var line in list)
            {
                var arr = line.Split('\t');
                if (arr.Length == 0 || string.IsNullOrEmpty(arr[0]))
                    continue;
                CheckLotID check = null;
                if (rbType1.Checked)
                {
                    check = new CheckLotID(arr[0]);
                }
                else if (rbType2.Checked)
                {
                    if (arr.Length < 2)
                        continue;
                    double input_qty = arr.Length == 2 || !double.TryParse(arr[2], out var qty) ? -1 : qty;
                    check = new CheckLotID(2, arr[0], arr[1], "", input_qty);

                }
                else if (rbType3.Checked)
                {
                    if (arr.Length < 2)
                        continue;
                    double input_qty = arr.Length == 2 || !double.TryParse(arr[2], out var qty) ? -1 : qty;
                    check = new CheckLotID(3, arr[0], "", arr[1], input_qty);

                }
                LstCheckLotID.Add(check);
            }
            lblProcess.Text = $"0/{LstCheckLotID.Count.ToString("#,##0")}";
            dataGridView1.DataSource = LstCheckLotID;

            _httpClient = await CreateHttpClient();
            if (_httpClient == null)
            {
                MessageBox.Show("Lỗi đăng nhập SMTT!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            timer1.Start();
            List<Task> tasks = new List<Task>();
            foreach (var checkLot in LstCheckLotID)
            {
                checkLot.Output_Production = "waiting...";
                checkLot.Output_OutBound = "waiting...";
                tasks.Add(checkLot.Start(_httpClient));
                await Task.Delay((int)updSpeed.Value);
            }
            await Task.WhenAll(tasks);
            btnStart.Enabled = true;

        }
        private async void Run()
        {
            if (!await Login())
                return;

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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
            if (LstCheckLotID.Count == 0)
                return;
            lblProcess.Text = $"{LstCheckLotID.Count(c => c.Finish)}/{LstCheckLotID.Count.ToString("#,##0")}";
            var a = (double)LstCheckLotID.Count(c => c.Finish) / (double)LstCheckLotID.Count * 100;
            progressBar1.Value = (int)a;
            if (LstCheckLotID.Count(c => c.Finish) == LstCheckLotID.Count)
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            rbType1.Checked = true;
            col_production.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            col_outbound.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            col_production_lotid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            col_outbound_lotid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            txtUsername.Text = _iniManager.GetString("Login", "Username", "jenny@yeechain.com");
            txtPassword.Text = _iniManager.GetString("Login", "Password", "WUN2bioxMjM0");
            Config.Instance.ConnectionString = _iniManager.GetString("Database", "ConnectionString", "Data Source=DESKTOP-TLNO6C7;Initial Catalog=Julian;User ID=client;Password=dythanh94@;Connect Timeout=1;");
            //Outbound
            Config.Instance.SMTTFolderPath = Encoding.UTF8.GetString(Encoding.Default.GetBytes(_iniManager.GetString("SMTT", "FolderPath", @"\\Vp_julien\smtt 2.0\3. SMTT\2026\ALL")));
            var files = Directory.GetFiles(Config.Instance.SMTTFolderPath, "*.xls*");
            foreach (var file in files)
            {
                var attr = File.GetAttributes(file);
                if ((attr & FileAttributes.Hidden) == FileAttributes.Hidden)
                    continue;
                var item = new ListViewItem();
                item.Checked = true;
                item.SubItems.Add(Path.GetFileName(file));
                lsvSMTTFiles.Items.Add(item);
            }
        }
        private void CopyDataGridViewToClipboard(DataGridView dgv)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    string value = row.Cells[i].Value?.ToString() ?? "";

                    // nếu có xuống dòng thì đặt trong ""
                    if (value.Contains("\n"))
                        value = "\"" + value + "\"";

                    sb.Append(value);

                    if (i < dgv.Columns.Count - 1)
                        sb.Append("\t");
                }

                sb.Append("\r\n");
            }

            Clipboard.SetText(sb.ToString());
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                toolTip1.Show("Không có dữ liệu để sao chép", btnCopy, btnCopy.Width, 0, 1500);
            }
            else
            {
                CopyDataGridViewToClipboard(dataGridView1);
                toolTip1.Show("Đã sao chép", btnCopy, btnCopy.Width, 0, 1500);
            }
        }
        private DataTable GetDataTable(string inputText)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PO", typeof(string));
            dt.Columns.Add("Qty", typeof(float));
            string[] data = inputText.Replace("\r", "").Replace(" ", "").Split('\n');
            foreach (string s in data)
            {
                string[] x = s.Split('\t');
                string po = x[0];
                if (x.Length >= 2 && float.TryParse(x[1], out float qty))
                {
                    dt.Rows.Add(po, qty);
                }
                else
                {
                    return null;
                }
            }
            return dt;
        }
        private List<TinhSLConLai> GetTinhSLConLaiList(string inputText)
        {
            string[] data = inputText.Replace("\r", "").Replace(" ", "").Split('\n');
            var lst = new List<TinhSLConLai>();
            foreach (string s in data)
            {
                if (string.IsNullOrEmpty(s))
                    continue;
                string[] x = s.Split('\t');
                string po = x[0];
                if (x.Length >= 2 && float.TryParse(x[x.Length - 1], out float qty))
                {
                    lst.Add(new TinhSLConLai() { PO = po, Qty = qty });
                }
                else
                {
                    return null;
                }
            }
            return lst.Count == 0 ? null : lst;
        }
        private BindingSource _bindingSource = new BindingSource();
        private void Start_TinhSLConLai()
        {
            var lstXuatKho = GetTinhSLConLaiList(txtXuatKho.Text);
            if (lstXuatKho == null)
            {
                toolTip1.Show("Dữ liệu XuatKho không hợp lệ!", txtXuatKho, txtXuatKho.Width, 0, 1500);
                return;
            }
            lblTotalXuatKho.Text = lstXuatKho.Sum(x => x.Qty).ToString("#,##0.00");

            var lstSMTT = GetTinhSLConLaiList(txtSMTTData.Text);
            if (lstSMTT == null)
            {
                toolTip1.Show("Dữ liệu XuatKho không hợp lệ!", txtSMTTData, txtSMTTData.Width, 0, 1500);
                return;
            }
            lblTotalSMTT.Text = lstSMTT.Sum(x => x.Qty).ToString("#,##0.00");
            var dt = new DataTable();
            dt.Columns.Add("PO", typeof(string));
            dt.Columns.Add("SLXuatKho", typeof(float));
            dt.Columns.Add("SLSMTT", typeof(float));
            dt.Columns.Add("SLChenhLech", typeof(float));
            var lstOut = lstXuatKho.GroupBy(x => x.PO).Select(x => new TinhSLConLai() { PO = x.First().PO, Qty = x.Sum(y => y.Qty) }).ToList();
            foreach (var item in lstOut)
            {
                float totalXuatKho = lstXuatKho.Where(x => x.PO == item.PO).Sum(x => x.Qty);
                float totalSMTT = lstSMTT.Where(x => x.PO.Contains(item.PO)).Sum(x => x.Qty);
                item.Qty = totalXuatKho - totalSMTT;
                dt.Rows.Add(item.PO, totalXuatKho, totalSMTT, totalXuatKho - totalSMTT);
            }
            _bindingSource.DataSource = dt;
            advancedDataGridView1.DataSource = _bindingSource;
            lblTotalRows.Text = advancedDataGridView1.RowCount.ToString();
            lblTotalRemainQty.Text = lstOut.Sum(x => x.Qty).ToString("#,##0.00");
        }
        private void txtXuatKho_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSMTTData_TextChanged(object sender, EventArgs e)
        {

        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnStart_TinhSlConLai_Click(object sender, EventArgs e)
        {
            Start_TinhSLConLai();
        }

        private void txtSMTTData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                BeginInvoke(new Action(() =>
                {
                    string[] lines = txtSMTTData.Text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    List<string> tempCodes = new List<string>();
                    List<string> result = new List<string>();

                    foreach (string line in lines)
                    {
                        string trimmed = line.Trim();

                        Match m = Regex.Match(trimmed, @"^(.*?)(\d+(\.\d+)?)$");

                        if (m.Success)
                        {
                            string code = m.Groups[1].Value.Trim();
                            string qty = m.Groups[2].Value.Trim();

                            tempCodes.Add(code);

                            foreach (string c in tempCodes)
                            {
                                result.Add($"{c.Replace("\"", "")}\t{qty}");
                            }

                            tempCodes.Clear();
                        }
                        else
                        {
                            tempCodes.Add(trimmed);
                        }
                    }

                    txtSMTTData.Text = string.Join(Environment.NewLine, result);
                }));
            }
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            Start();
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Start();
                e.Handled = true;
            }
        }

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Start();
                e.SuppressKeyPress = true;
            }
        }
        private void UploadOutBound()
        {

        }

        private async void btnRemoveTrans_Click(object sender, EventArgs e)
        {

        }
        public async Task<DataTable> LoadTonKho()
        {
            try
            {
                var dbHelper = new DBHelper(Config.Instance.ConnectionString);

                return await dbHelper.GetDataTableSPAsync("SP_TonKho_GetAll");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private async void btnLoadTonKho_Click(object sender, EventArgs e)
        {
            var dt = await LoadTonKho();
            dataGridView2.DataSource = dt;
        }
    }
    public class TinhSLConLai
    {
        public string PO { get; set; }
        public float Qty { get; set; }
    }
}
