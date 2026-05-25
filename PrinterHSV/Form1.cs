using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;

namespace PrinterHSV
{
    public partial class Form1 : Form
    {
        WebView2 webView = new WebView2();
        public Form1()
        {
            InitializeComponent();
            textBox1.Focus();
        }
        private async Task InitBrowser()
        {
            var options = new CoreWebView2EnvironmentOptions(
                "--disable-web-security " +
                "--allow-running-insecure-content " +
                "--disable-features=BlockInsecurePrivateNetworkRequests"
            );

            var env = await CoreWebView2Environment.CreateAsync(
                null,
                null,
                options);
            await webView.EnsureCoreWebView2Async(env);

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            btnRun.Enabled = false;
            try
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string ubiwsPath = Path.Combine(appData, "UbiDecision", "UbiViewerWS");
                try
                {
                    while (Process.GetProcessesByName("UbiViewerWS") == null || Process.GetProcessesByName("UbiViewerWS").Length == 0)
                    {
                        var psi = new ProcessStartInfo()
                        {
                            FileName = ubiwsPath + @"\UbiViewerWS.exe",
                            WorkingDirectory = ubiwsPath,
                            UseShellExecute = true
                        };

                        var p = Process.Start(psi);
                        if (p == null)
                        {

                            break;
                        }
                        else
                        {
                            p.WaitForInputIdle();
                        }
                        //MessageBox.Show(p == null ? "null" : p.Id.ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
                var httpClient = new HttpClient();

                var response = await httpClient.GetStringAsync("https://script.google.com/macros/s/AKfycby-YEnra2DZh7NmbhCzAaoU7K3jP0IANsbzfUikTOw79VhV6hWkQHCDSKregC4zA_tI/exec");
                if (response != "ok")
                {
                    MessageBox.Show(response);
                    return;
                }
                //url
                var text = textBox1.Text.Trim();
                if (!text.StartsWith("http://") &&
                    !text.StartsWith("https://"))
                {
                    text = "https://" + text;
                }
                Uri uri = null;
                if (!Uri.TryCreate(text, UriKind.Absolute, out uri))
                {
                    MessageBox.Show("URL không hợp lệ");
                    return;
                }
                if (webView.Source.OriginalString == "about:blank" || webView.Source.OriginalString != uri.OriginalString)
                {

                    webView.Source = uri;
                }
                else
                {
                    webView.Reload();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n"+ex.Message);
            }
            finally
            {
                textBox1.Enabled = true;
                btnRun.Enabled = true;
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await InitBrowser();
            this.ActiveControl = textBox1;
        }

        private async Task button1_Click_1(object sender, EventArgs e)
        {
            webView.CoreWebView2.OpenDevToolsWindow();
        }
        private bool IsPortOpen(string host, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    var result = client.BeginConnect(host, port, null, null);

                    bool success = result.AsyncWaitHandle.WaitOne(1000);

                    return success;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task RunUbi()
        {
            try
            {
                string appData =
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string ubiPath = Path.Combine(
                    appData,
                    "UbiDecision",
                    "UbiViewerWS",
                    "UbiViewer.exe");

                string ubiWSPath = Path.Combine(
                    appData,
                    "UbiDecision",
                    "UbiViewerWS",
                    "UbiViewerWS.exe");

                if (!File.Exists(ubiPath))
                {
                    MessageBox.Show("Không tìm thấy UbiViewer.exe");
                    return;
                }

                if (!File.Exists(ubiWSPath))
                {
                    MessageBox.Show("Không tìm thấy UbiViewerWS.exe");
                    return;
                }

                // chạy websocket trước
                if (Process.GetProcessesByName("UbiViewerWS").Length == 0)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = ubiWSPath,
                        UseShellExecute = true
                    });

                    await Task.Delay(3000);
                }

                // chạy viewer
                if (Process.GetProcessesByName("UbiViewer").Length == 0)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = ubiPath,
                        UseShellExecute = true
                    });

                    await Task.Delay(3000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void button1_Click_2(object sender, EventArgs e)
        {
            /* var client = new UbiClient();

             await client.ConnectAsync();

             await client.CheckVersion();

             await client.Retrieve(
                 "Box.jrf",
                 "sql#OMS_G#whse#R200#asnNo#R20260505677#multikey#7e614ac9-24c0-4a58-91c0-fcd18daadd1d"
             );

             await client.ReceiveLoop();*/
            webView.CoreWebView2.OpenDevToolsWindow();
        }
    }
}
