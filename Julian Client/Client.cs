using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Julian_Client
{
    public class Client
    {
        static HttpClient _httpClient = new HttpClient();
        static readonly byte[] _secretKey = Encoding.UTF8.GetBytes("ngdythanh@123456");
        static readonly byte[] _iv = Encoding.UTF8.GetBytes("beta111111111111");
        static string IPServer = "127.0.0.1:8080";
        static Client()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(10);
        }
        public static string DecryptAES(string base64, byte[] key, byte[] iv)
        {
            byte[] cipherBytes = Convert.FromBase64String(base64);

            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor();
            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        public static Task<string[]> GetBotDataFromServer()
        {
            return Task.Run(() =>
            {
                try
                {
                    string base64 = _httpClient.GetStringAsync($"http://{IPServer}/GetBotData").Result;

                    string json = DecryptAES(base64, _secretKey, _iv);
                    string[] bots = JsonConvert.DeserializeObject<string[]>(json);
                    return bots;
                }
                catch
                {
                    return null;
                }
            });
        }
        public static Task<string[]> GetAdminDataFromServer()
        {
            return Task.Run(() =>
            {
                try
                {
                    string base64 = _httpClient.GetStringAsync($"http://{IPServer}/GetAdminData").Result;

                    string json = DecryptAES(base64, _secretKey, _iv);
                    string[] bots = JsonConvert.DeserializeObject<string[]>(json);
                    return bots;
                }
                catch
                {
                    return null;
                }
            });
        }
        public static async Task<bool> UpdateClientData(ClientData clientData)
        {
            try
            {
                if (clientData == null) return false;
                var content = new StringContent(JsonConvert.SerializeObject(clientData), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"http://{IPServer}/UpdateClientData", content);
                string resultBase64 = response.Content.ReadAsStringAsync().Result;

                string jsonText = DecryptAES(resultBase64, _secretKey, _iv);
                dynamic json = JObject.Parse(jsonText);
                bool result = json.Result;
                return result;
            }
            catch
            {
                return false;
            }
        }
        public static Task<AccountBetDetail[]> GetAccountBetDetails(string nickname)
        {
            return Task.Run(() =>
            {
                try
                {
                    var data = new
                    {
                        Nickname = nickname
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _httpClient.PostAsync($"http://{IPServer}/AccountBetDetails", content).Result;
                    string resultBase64 = response.Content.ReadAsStringAsync().Result;

                    string jsonText = DecryptAES(resultBase64, _secretKey, _iv);
                    var accountBetDetails = JsonConvert.DeserializeObject<AccountBetDetail[]>(jsonText);
                    return accountBetDetails;
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}
