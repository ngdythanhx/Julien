using JHelper;
using JServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JServer
{
    public class ServerAPI : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly CookieContainer _cookies;
        private string _username = "";
        public User UserInfo { get; private set; } = new User();
        public HttpClient HttpClient => _httpClient;

        public ServerAPI()
        {
            string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
            var ini = new IniManager(iniPath);

            Config.Host = ini.GetString("Server", "Host", "r411.ledway.com.tw");
            Config.LoginPort = ini.GetString("Server", "LoginPort", "7081");
            Config.LoginEndpoint = ini.GetString("Server", "LoginEndpoint", "api4006/oauth/access_token");
            Config.QueryPort = ini.GetString("Server", "QueryPort", "7081");
            Config.QueryEndpoint = ini.GetString("Server", "QueryEndpoint", "api3046/v1/query");

            _cookies = new CookieContainer();
            _httpClientHandler = new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = _cookies,
                MaxConnectionsPerServer = 500
            };
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
        }

        private string LoginUrl => $"https://{Config.Host}:{Config.LoginPort}/{Config.LoginEndpoint}";
        private string QueryUrl => $"https://{Config.Host}:{Config.QueryPort}/{Config.QueryEndpoint}";

        public async Task<(bool Success, string Msg)> Login(string username, string password)
        {
            try
            {
                _username = "";
                _httpClient.DefaultRequestHeaders.Authorization = null;
                UserInfo = new User();
                var inputData = new Dictionary<string, string>
                {
                    ["username"] = username,
                    ["password"] = password,
                    ["grant_type"] = "password",
                    ["client_id"] = "vueup",
                    ["client_secret"] = "vueup",
                    ["lang"] = "Vietnam",
                    ["deviceId"] = "webup-1792392995876-1qitspx34s"
                };

                using (var res = await _httpClient.PostAsync(LoginUrl, new FormUrlEncodedContent(inputData)))
                {
                    var resString = await res.Content.ReadAsStringAsync();

                    if (res.StatusCode != HttpStatusCode.OK)
                        return (false, resString);
                    if (resString.Contains("invalid_grant"))
                        return (false, "Tài khoản hoặc mật khẩu không đúng"); 
                    var token = JObject.Parse(resString)["access_token"]?.ToString();
                    if (string.IsNullOrEmpty(token))
                        return (false, "Server không trả về access_token.");
                    var user = await GetUserInfo();
                    UserInfo = user ?? new User();
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    _username = username;
                    return (true, "Đăng nhập thành công!");
                }
            }
            catch (Exception ex)
            {
                return (false, "Lỗi hệ thống: " + ex.Message);
            }
        }

        private async Task<(bool Ok, string Body)> PostQuery(string inputCmd)
        {
            string cmd = Regex.Replace(inputCmd, @"\s+", " ").Trim();
            var json = JsonConvert.SerializeObject(new { cmd, buffer_to_json = false });

            using (var content = new StringContent(json, Encoding.UTF8, "application/json"))
            using (var res = await _httpClient.PostAsync(QueryUrl, content))
            {
                var body = await res.Content.ReadAsStringAsync();
                return (res.IsSuccessStatusCode, body);
            }
        }

        public async Task<QueryResponse<T>> ExQuery<T>(string inputCmd)
        {
            try
            {
                var (ok, body) = await PostQuery(inputCmd);
                return ok ? JsonConvert.DeserializeObject<QueryResponse<T>>(body) : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> ExQuery(string inputCmd)
        {
            try
            {
                var (ok, body) = await PostQuery(inputCmd);
                if (!ok) return body;

                return JObject.Parse(body)["recordset"]?.ToString()
                       ?? throw new Exception("API trả về dữ liệu rỗng.");
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        public async Task<string> GetCode(string objectName)
        {
            try
            {
                // Chỉ cho phép tên đối tượng hợp lệ (schema.name) để tránh SQL injection
                if (string.IsNullOrWhiteSpace(objectName) ||
                    !Regex.IsMatch(objectName, @"^[\w\.\[\]]+$"))
                    return "Lỗi hệ thống: Tên đối tượng không hợp lệ.";

                var resString = await ExQuery(
                    $"SELECT OBJECT_DEFINITION(OBJECT_ID('{objectName}')) AS ViewCode");
                var arr = JArray.Parse(resString);
                return arr.Count > 0 ? arr[0]?["ViewCode"]?.ToString() : null;
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        public async Task<User> GetUserInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(_username)) return null;

                // Escape dấu nháy đơn và ký tự đặc biệt của LIKE
                string safe = _username.Replace("'", "''")
                                       .Replace("[", "[[]")
                                       .Replace("%", "[%]")
                                       .Replace("_", "[_]");

                var u = await ExQuery<User>(
                    $"SELECT * FROM ledway.viewEmployeeMain WHERE (員工代碼 LIKE '%{safe}%')");
                return u?.Recordset?.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _httpClientHandler?.Dispose();
        }
    }
}
