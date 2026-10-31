using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JServer
{
    public class Config
    {
        public static string Host{  get; set; }
        public static string LoginPort {  get; set; }
        public static string LoginEndpoint {  get; set; }
        public static string QueryPort { get; set; }
        public static string QueryEndpoint { get; set; }
    }
}
