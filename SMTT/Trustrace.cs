using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SMTT.CheckLotID;

namespace SMTT
{
    public class Trustrace
    {
        private HttpClient _httpClient = null;
        private CookieContainer _cookies = null;
        private HttpClientHandler _httpClientHandler = null;
        private string _urlLogin = string.Empty;
        private bool _isLogin;
        public bool IsLogin => _isLogin;
        public Trustrace()
        {
            _cookies = new CookieContainer();
            _cookies.Add(new Cookie("TT-XSRF-TOKEN", "1", "/", ".trustrace.com"));
            _httpClientHandler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = _cookies,
                MaxConnectionsPerServer = 500
            };
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://adidas.trustrace.com");
            _httpClient.DefaultRequestHeaders.Add("Referer", "https://adidas.trustrace.com/");
            _httpClient.DefaultRequestHeaders.Add("X-TT-XSRF-TOKEN", "1");
            StartAutoUpdateCookie();
        }
        private void StartAutoUpdateCookie()
        {
            Task.Run(async() =>
            {
                while (true)
                {
                    if (_isLogin)
                    {
                        await _httpClient.GetAsync($"https://adidas.api.trustrace.com/user/refreshtoken");
                        await Task.Delay(TimeSpan.FromMinutes(1));
                    }
                }
            });
        }
        public async Task<(bool success, string msg)> Login(string username, string password)
        {
            _isLogin = false;
            try
            {
                var jsonData = new
                {
                    username = username,
                    password = password,
                };
                var jsonDatax = JsonConvert.SerializeObject(jsonData);
                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
                var res = await _httpClient.PostAsync("https://adidas.api.trustrace.com/api/auth/login", content);
                var resStatus = res.StatusCode;
                if (resStatus == HttpStatusCode.OK)
                {
                    var resResultString = await res.Content.ReadAsStringAsync();
                    var preAuthToken = JObject.Parse(resResultString)["preAuthToken"]?.ToString();
                    _httpClient.DefaultRequestHeaders.Add("X-Token", preAuthToken);
                    var res2 = await _httpClient.PostAsync($"https://adidas.api.trustrace.com/api/auth/get-access-token", new StringContent("{}", Encoding.UTF8, "application/json"));
                    _isLogin = true;
                    return (true, "");
                }
                _isLogin = false;
                return (false, "Lỗi đăng nhập!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi hệ thống:\n" + ex.Message);
            }
        }
        public enum TransactionStatus
        {
            SUBMITTED,
            DRAFT,
        }
        public async Task<(bool success, string msg, List<SmttItem> lst)> Search(TransactionStatus transactionStatus, string searchText)
        {
            try
            {
                var jsonData = new
                {
                    module = "TT_TRANSACTION",
                    filter = new Dictionary<string, object>
                    {
                        { "Brand", new[] { "60b20f144294127f8af64c6e" } },
                        { "Transaction Status", new[] { transactionStatus.ToString() } }
                    },
                    sort = new
                    {
                        sortBy = "create_ts",
                        sortOrder = "desc",
                        sortValue = "Newest First"
                    },
                    pagination = new
                    {
                        from = 0,
                        size = 10000
                    },
                    searchContext = new
                    {
                        cocType = "PRODUCT_SEGREGATION",
                        tabTTName = transactionStatus.ToString() + "_TRANSACTION"
                    },
                    freeHand = searchText
                };
                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
                try
                {
                    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                    var res = await _httpClient.PostAsync(
                           $"https://adidas.api.trustrace.com/api/material-compliance/ps/search/tt_transaction_list",
                           content,
                           cts.Token
                       );
                    var a = res.StatusCode;
                    if (res.StatusCode == HttpStatusCode.RequestTimeout)
                    {
                        return await Search(transactionStatus, searchText);
                    }
                    if (res.StatusCode != HttpStatusCode.OK)
                    {
                        return (false, "Lỗi request", null);
                    }
                    else
                    {
                        var text = await res.Content.ReadAsStringAsync();
                        var resJson = JObject.Parse(text);
                        if ((string)resJson["status"] == "SUCCESS")
                        {
                            var lstSmttObj = resJson["data"]?["searchResponse"]?.ToObject<List<JObject>>();
                            if (lstSmttObj == null || lstSmttObj.Count == 0)
                                return (false, "Lỗi jsonData", null);
                            var lst = new List<SmttItem>();
                            foreach (var smttObj in lstSmttObj)
                            {
                                var item = new SmttItem()
                                {
                                    ID = smttObj["id"]?.ToString(),
                                    Type = smttObj["tx_type"]?.ToString(),
                                    LotID = smttObj["lot_id"]?.ToString(),
                                    MaterialNumber = smttObj["product_number"]?.ToString(),
                                    Qty = smttObj["quantity_in_uom"]?["value"]?.ToObject<double?>() ?? -1,
                                    Unit = smttObj["quantity_in_uom"]?["unit_value"]?.ToString(),
                                    InvoiceNumber = smttObj["invoice_number"]?.ToString(),
                                    CustomerFacility = smttObj["customer_facility_name_uid"]?.ToString(),
                                    CreatedDate = smttObj["create_ts"].ToObject<DateTime>().Date,
                                    ProductionDate = smttObj["production_date"].ToObject<DateTime>().Date,
                                    OutBoundDate = smttObj["tx_date"].ToObject<DateTime>().Date,
                                    Remarks = smttObj["remarks"]?.ToString(),
                                    TransactionStatus = Enum.TryParse<TransactionStatus>(smttObj["tx_type"]?.ToString(), out var status) ? status : default,
                                };
                                lst.Add(item);
                            }
                            return (true, "", lst);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "An error occurred while sending the request." || ex.Message == "A task was canceled.")
                    {
                        await Task.Delay(3000);
                        return await Search(transactionStatus, searchText);
                    }
                    return (false, "Lỗi hệ thống: " + ex.Message, null);
                }
                return (false, "Lỗi không xác định!", null);
            }
            catch (Exception ex)
            {
                return (false, "Lỗi hệ thống: " + ex.Message, null);
            }
        }
        private async void UploadOutBound(HttpClient httpClient, string filePath)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://adidas.api.trustrace.com/api/material-compliance/ps/import");

            var form = new MultipartFormDataContent();

            if (!File.Exists(filePath))
                return;
            var fileBytes = File.ReadAllBytes(filePath);

            var fileContent = new ByteArrayContent(fileBytes);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            form.Add(fileContent, "file", Path.GetFileName(filePath));

            // JSON
            string json = @"
            {
              ""brandCompanyId"": ""60b20f144294127f8af64c6e"",
              ""tier"": ""tr_tier2"",
              ""masterDataLinkingStatusList"": [
                {
                  ""txType"": ""INBOUND"",
                  ""masterDataLinkingStatus"": ""WITHOUT_MASTER_DATA""
                },
                {
                  ""txType"": ""OUTBOUND"",
                  ""masterDataLinkingStatus"": ""WITH_MASTER_DATA""
                },
                {
                  ""txType"": ""PRODUCTION"",
                  ""masterDataLinkingStatus"": ""SUPPLIER_CHOICE""
                }
              ],
              ""chainOfCustody"": ""PRODUCT_SEGREGATION""
            }";

            form.Add(new StringContent(json, Encoding.UTF8, "application/json"), "inputPayload");

            request.Content = form;

            var response = await httpClient.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            MessageBox.Show(response.StatusCode.ToString());

            Console.WriteLine(result);
        }
    }
}
