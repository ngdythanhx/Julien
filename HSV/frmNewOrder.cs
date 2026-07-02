using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Julian.Database.DTO;
using Julian.Helper;
using Julian.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HSV
{
    public partial class frmNewOrder : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        List<OrderData> _lstOrderData = new List<OrderData>();
        HttpClient _httpClient = new HttpClient();
        List<OrderForm> _lstOrderForm = new List<OrderForm>();
        public frmNewOrder()
        {
            InitializeComponent();
        }

        private void frmNewOrder_Load(object sender, EventArgs e)
        {
            cbCompany.DataSource = new[] {
                new{ Key="HSV", Value= "H100" },
            };
            cbCompany.DisplayMember = "Key";
            cbCompany.ValueMember = "Value";
            var ini = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "OrderData.ini"));
            string section = "Data";
            int count = ini.GetInt(section, "Count", 0);
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    string[] data = ini.GetString(section, "_" + i, "").Split('|');
                    if (data.Length == 7 && float.TryParse(data[6], out float unitPrice))
                    {
                        var orderData = new OrderData()
                        {
                            CustomerName = data[0],
                            PO = data[1],
                            Code = data[2],
                            MtlName = data[3],
                            Color = data[4],
                            Season = data[5],
                            UnitPrice = unitPrice,
                        };
                    }
                }
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string text = txtInput.Text.Replace("\r", "");
            lsbInput.Items.AddRange(text.Split('\n'));
            txtInput.Text = "";
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            txtInput.Enabled = false;
            _lstOrderForm.Clear();
            try
            {
                var lstSearch = new List<object>();
                foreach (var item in lsbInput.Items)
                {
                    lstSearch.Add(new
                    {
                        val1 = item.ToString(),
                        val2 = ""
                    });
                }
                var ds_json = new[]
                {
                    new
                    {
                        searchType = "01",
                        frDate = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd"),
                        toDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd"),
                        lot5 = "multi",
                        lot6 = 3,
                        itemCd = "",
                        itemNm = "",
                        itemType = "",
                        supCd = "102380",
                        supNm = "CTY+TNHH+BOT+KIM+LOAI+JULIEN+V",
                        poStatus = "",
                        rQtyYn = "Y",
                        lot7 = "",
                        poType = "",
                        soLoss = "N",
                        sessionSupCd = "102380",
                        supPoNo = "",
                        supItemCd = "",
                        soCd = "",
                        soItem = "",
                        categoryName = "",
                        tx_lot5Qry1 = lstSearch
                    }
                };
                var ds_session = new[]
                {
                    new
                    {
                        sessionUserIp="0.0.0.0",
                        sessionUserId="102380",
                        sessionWhseCd="MAIN",
                        sessionLangType="EN",
                        sessionOwnerCd=cbCompany.SelectedValue.ToString(),
                    }
                };
                string postData = $"ds_json={JsonConvert.SerializeObject(ds_json)}&ds_session={JsonConvert.SerializeObject(ds_session)}";
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>(
                        "ds_json",
                        JsonConvert.SerializeObject(ds_json)
                    ),
                    new KeyValuePair<string, string>(
                        "ds_session",
                        JsonConvert.SerializeObject(ds_session)
                    )
                });
                var result = await _httpClient.PostAsync("http://scm.hsvina.com/rb7-web/asnMgt/getAsnMgtList.json", content);
                var resultData = JObject.Parse(await result.Content.ReadAsStringAsync());
                var lstResult = resultData["rtnList"].ToObject<List<NewPO>>();
                foreach (var newPO in lstResult)
                {
                    string lieuKH = _lstOrderData.FirstOrDefault(x => x.Code == newPO.ItemCode)?.MtlName;
                    OrderForm orderForm = new OrderForm()
                    {
                        MaKH = cbCompany.DisplayMember,
                        Brand = "ADS",
                        NgayDat = newPO.PoDate,
                        MaDonKH = newPO.PO,
                        MaHangKH = newPO.ItemCode,
                        LieuKH = _lstOrderData.FirstOrDefault(x => x.Code == newPO.ItemCode)?.MtlName,
                        TenMauDayDu = newPO.Remark.Substring(newPO.Remark.IndexOf("\"") + 1),
                        MauKH = newPO.Color,
                        SLDat = newPO.OrderQty,
                        DonGia = _lstOrderData.FirstOrDefault(x => x.CustomerName == cbCompany.DisplayMember && x.Season== newPO.Season && x.MtlName== lieuKH)?.UnitPrice ?? -1,
                        Category = newPO.Category,
                        Article = newPO.Article,
                        TenGiay = newPO.Model,
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
                txtInput.Enabled = true;
            }
        }
    }
}
