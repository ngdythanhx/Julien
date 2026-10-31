using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trustrace;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace SMTTV2
{
    public partial class frmUpload : Form
    {
        TrustraceAPI _trustraceAPI;
        public frmUpload()
        {
            InitializeComponent();
        }
        private async Task<bool> LoginTrustrace()
        {
            if (_trustraceAPI == null)
                _trustraceAPI = new TrustraceAPI();
            if (!_trustraceAPI.IsLogin)
            {
                var login = await _trustraceAPI.Login(txtUsername.Text, txtPassword.Text);
                if (!login.success)
                {
                    MessageBox.Show(login.msg);
                    return false;
                }
            }
            return true;
        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            btnLogin.Enabled = false;
            try
            {
                if (await LoginTrustrace())
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = true;
                    groupBox3.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                btnLogin.Enabled = false;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!await LoginTrustrace())
                return;
            var a = await _trustraceAPI.SmartSegregation.UploadOutbound("40053917", "ZZXYEC-VN01", 0.6m, "753002");
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            var inputString = txtInput.Text;
            if (inputString.Length == 0)
            {
                return;
            }
            string[] lines = inputString.Split(new[] { "\r\n" }, StringSplitOptions.None);
            if (lines.Length == 0)
                return;
            var a = lines.Select(l => l.Split('\t'));
            var data = lines.Select(l => l.Split('\t')).Where(x => x.Length >= 32).ToList();
            if (data.Count == 0)
            {
                toolTip1.Show("Dữ liệu đầu vào không đúng!", txtInput, txtInput.Width, 0, 1500);
                return;
            }
            var lstSMTT = new List<SmttItem>();
            foreach (var item in data)
            {
                if (int.TryParse(item[3], out int mtlID) &&
                    decimal.TryParse(item[4], out decimal qtyKG) &&
                    decimal.TryParse(item[5], out decimal qtyYard) &&
                    DateTime.TryParseExact(item[20], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var deliveryDate))
                {
                    var SmttItem = new SmttItem()
                    {
                        PODyingList = item[0].Trim('"').Split('\n').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Replace(" ", "")).ToArray(),
                        MaterialName = item[1].Trim(),
                        MaterialNumber = mtlID.ToString(),
                        QtyInKG = qtyKG,
                        QtyInYard = qtyYard,
                        T2ID = item[8].Trim(),
                        T1Name = item[18].Trim(),
                        T1Code = item[19].Trim(),
                        DeliveryDate = deliveryDate,
                        POCus = item[22].Trim(),
                        Code = item[23].Trim(),
                        Invoice = item[26].Trim(),
                    };
                    lstSMTT.Add(SmttItem);
                }
            }
            var lstOutboundItem = lstSMTT.GroupBy(x => new { x.MaterialNumber, x.T2ID, x.T1Code }).Select(x => new OutboundUploadItem()
            {
                MaterialNumber = x.First().MaterialNumber,
                TotalQtyInKG = x.Sum(i => i.QtyInKG),
                TotalQtyInYard = x.Sum(i => i.QtyInYard),
                T2ID = x.First().T2ID,
                T1Name = x.First().T1Name,
                T1Code = x.First().T1Code
            }).ToList();
            dataGridView1.DataSource = lstOutboundItem;
            lblTotalQtyInYard.Text = lstOutboundItem.Sum(x => x.TotalQtyInYard).ToString("#,##0.000");
            lblTotalQtyInKg.Text = lstOutboundItem.Sum(x => x.TotalQtyInKG).ToString("#,##0.000");
        }

        private void frmUpload_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dataGridView1, true, null);
        }
        private void WriteLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => WriteLog(message)));
                return;
            }
            //txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            txtLog.AppendText($"{message}{Environment.NewLine}");
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtInput.Enabled = false;
            try
            {
                var data = dataGridView1.DataSource as List<OutboundUploadItem>;
                if (data != null)
                {
                    foreach (var ob in data)
                    {
                        ob.Status = "";
                    }
                    int n = 0;
                    foreach (var ob in data)
                    {
                        ob.Status = "Đang kiểm tra dữ liệu..";
                        WriteLog($"{ob.MaterialNumber} {ob.T1Name} {ob.T1Code} Đang kiểm tra dữ liệu..");
                        var check = await _trustraceAPI.SmartSegregation.CheckOutboundData(ob.MaterialNumber, ob.T2ID, ob.TotalQtyInKG, ob.T1Code);
                        if (check.success)
                        {
                            WriteLog("-> OK");
                            ob.Status = "Đã sẵn sàng";
                            n++;
                        }
                        else
                        {
                            ob.Status = check.msg;
                            break;
                        }
                    }
                    if (n == data.Count)
                    {
                        foreach (var ob in data)
                        {
                            ob.Status = "Đang upload dữ liệu..";
                            WriteLog($"{ob.MaterialNumber} {ob.T1Name} {ob.T1Code} Đang upload dữ liệu..");
                            var upload = await _trustraceAPI.SmartSegregation.UploadOutbound(ob.MaterialNumber, ob.T2ID, ob.TotalQtyInKG, ob.T1Code);
                            if (upload.success)
                            {
                                WriteLog("-> OK");
                                //ob.Status = "Upload thành công";
                                ob.Status = upload.msg;
                            }
                            else
                            {
                                ob.Status = upload.msg;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog($"Lỗi hệ thống: " + ex.Message);
            }
            finally
            {
                btnStart.Enabled = true;
                txtInput.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            /*if (_lstFinder.Count == 0)
                return;
            lblProcess.Text = $"{_lstFinder.Count(c => c.Finish)}/{_lstFinder.Count.ToString("#,##0")}";
            var a = (double)_lstFinder.Count(c => c.Finish) / (double)_lstFinder.Count * 100;
            progressBar1.Value = (int)a;
            if (_lstFinder.Count(c => c.Finish) == _lstFinder.Count)
                timer1.Stop();
            */
        }

    }
}
