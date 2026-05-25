using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterHSV
{
    using System;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class UbiClient
    {
        const char RS = (char)30;
        const char CS = (char)31;
        const char EOF = (char)27;
        const char VER_EOF = (char)29;

        private readonly ClientWebSocket _ws = new ClientWebSocket();

        public async Task ConnectAsync()
        {
            await _ws.ConnectAsync(
                new Uri("ws://127.0.0.1:6886"),
                CancellationToken.None);

            Console.WriteLine("Connected");
        }

        public async Task CheckVersion()
        {
            string version = "2, 502, 1703, 1601";

            string msg =
                "reqtype" + CS +
                "checkversion#" + version +
                RS +
                VER_EOF +
                EOF;

            await Send(msg);
        }

        public async Task Retrieve(
            string file,
            string args)
        {
            string rootUrl = "http://scm.hsvina.com/ubi";
            string fileUrl = rootUrl + "/ubireport/";
            string dataUrl = rootUrl + "/UbiData";
            string jrfDir = rootUrl + "/ubireport/work/";

            string props =
                $"fileurl{CS}{fileUrl}{RS}" +
                $"servletrooturl{CS}{rootUrl}{RS}" +
                $"servleturl2{CS}{dataUrl}{RS}" +
                $"jrffiledir{CS}{jrfDir}{RS}" +
                $"jrffilename{CS}{file}{RS}" +
                $"datasource{CS}jdbc/RB7TEST{RS}" +
                $"scale{CS}100{RS}" +
                $"resource{CS}auto{RS}" +
                $"arg{CS}{args}{RS}";

            string msg =
                $"reqtype{CS}retrieve{RS}" +
                props +
                EOF;

            await Send(msg);
        }

        private async Task Send(string msg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(msg);

            await _ws.SendAsync(
                new ArraySegment<byte>(bytes),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);

            Console.WriteLine("Sent:");
            Console.WriteLine(msg);
        }

        public async Task ReceiveLoop()
        {
            var buffer = new byte[8192];
            var a = new ArraySegment<byte>(buffer);
            while (_ws.State == WebSocketState.Open)
            {
                var result = await _ws.ReceiveAsync(
                    a,
                    CancellationToken.None);

                string msg = Encoding.UTF8.GetString(
                    buffer,
                    0,
                    result.Count);

                Console.WriteLine("Received:");
                Console.WriteLine(msg);

                if (msg.Contains("CHECKVERSION#true"))
                {
                    Console.WriteLine("Version OK");
                }

                if (msg.Contains("RETRIEVEEND"))
                {
                    Console.WriteLine("Report loaded");
                }
            }
        }
    }
}
