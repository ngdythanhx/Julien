using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Trustrace
{
    public class TrustraceAPI
    {
        private HttpClient _httpClient = null;
        public HttpClient HttpClient => _httpClient;
        private CookieContainer _cookies = null;
        private HttpClientHandler _httpClientHandler = null;
        private string _urlLogin = string.Empty;
        private bool _isLogin;
        public bool IsLogin => _isLogin;
        private SmartSegregation _ss;
        public SmartSegregation SmartSegregation => _ss;
        public UserProfile Profile { get; set; } = new UserProfile();
        public string BrandContextID { get; set; } = "";
        public TrustraceAPI()
        {
            _ss = new SmartSegregation(this);
            _cookies = new CookieContainer();
            //_cookies.Add(new Cookie("TT-XSRF-TOKEN", "1", "/", ".trustrace.com"));
            _httpClientHandler = new HttpClientHandler()
            {
                UseCookies = true,
                CookieContainer = _cookies,
                MaxConnectionsPerServer = 500
            };
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.DefaultRequestHeaders.Host = "adidas.api.trustrace.com";
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://adidas.trustrace.com");
            _httpClient.DefaultRequestHeaders.Add("Referer", "https://adidas.trustrace.com/");
            //_httpClient.DefaultRequestHeaders.Add("X-TT-XSRF-TOKEN", "1");
            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/155.0.0.0 Safari/537.36");
            StartAutoUpdateCookie();
        }
        private void StartAutoUpdateCookie()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (_isLogin)
                    {
                        var res = await _httpClient.GetAsync($"https://adidas.api.trustrace.com/user/refreshtoken");
                        if (res.IsSuccessStatusCode)
                        {
                            string resString = await res.Content.ReadAsStringAsync();
                            //_httpClient.DefaultRequestHeaders.Remove("X-Token");
                            //_httpClient.DefaultRequestHeaders.Add("X-Token", "Bearer " + JObject.Parse(resString)["accessToken"]?.ToString());
                        }
                        await Task.Delay(TimeSpan.FromSeconds(60));
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
                    var res2String = await res2.Content.ReadAsStringAsync();
                    if(res2.IsSuccessStatusCode)
                    {
                        var c = _cookies.GetCookies(new Uri("https://trustrace.com"));
                        var tk = _cookies.GetCookies(new Uri("https://trustrace.com"))["TT-XSRF-TOKEN"].Value;
                        var tttoken = c["tt_token"].Value;
                        var art = _cookies.GetCookies(new Uri("https://trustrace.com"))["expiresAt"]?.Value;
                        _httpClient.DefaultRequestHeaders.Add("X-Tt-Xsrf-Token", tk);
                        _httpClient.DefaultRequestHeaders.Remove("X-Token");
                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(tttoken);
                        var payload = token.Payload;
                        Profile.CompanyId = payload["company_id"].ToString();
                        var context = JObject.Parse(payload["company-context"].ToString());
                        BrandContextID = context["id"]?.ToString();
                    }    
                    if (!res2.IsSuccessStatusCode)
                        return (false, res2String);
                    _isLogin = true;
                   // await UpdateUserProfile();
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
        public class UserProfile
        {
            [JsonProperty("id")]
            public string ID { get; set; }
            [JsonProperty("companyId")]
            public string CompanyId { get; set; }
            [JsonProperty("companyName")]
            public string CompanyName { get; set; }
        }
        private async Task UpdateUserProfile()
        {
            try
            {
                var res = await _httpClient.GetAsync("https://adidas.api.trustrace.com/api/secured/user/");
                var resString = await res.Content.ReadAsStringAsync();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var resultString = await res.Content.ReadAsStringAsync();
                    Profile = JsonConvert.DeserializeObject<UserProfile>(resultString);
                    BrandContextID = JObject.Parse(resultString)["companyContext"]["id"].ToString();
                }
            }
            catch
            {
                Profile = new UserProfile();
                BrandContextID = "";
            }
        }
        public enum TransactionStatus
        {
            SUBMITTED,
            DRAFT,
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

            //MessageBox.Show(response.StatusCode.ToString());

            Console.WriteLine(result);
        }
        public async Task<string> DEL(List<string> lst)
        {
            try
            {
                var b = new List<string>() { lst.FirstOrDefault() };
                var jsonData = new
                {
                    deleteMessage = (string)null,
                    deleteTransactions = lst
                    //deleteTransactions = new List<string>() { lst.FirstOrDefault() }
                };
                var a = JsonConvert.SerializeObject(jsonData);
                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
                try
                {
                    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                    var res = await _httpClient.PostAsync(
                           $"https://adidas.api.trustrace.com/api/material-compliance/ps/delete-multiple-sub-transactions",
                           content,
                           cts.Token
                       );
                    var text = await res.Content.ReadAsStringAsync();
                    return text;
                }
                catch
                {
                    return "false";
                }
            }
            catch
            {
                return "false";
            }
        }
    }
}
