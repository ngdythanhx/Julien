using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JServer.Models
{
    public class User
    {
        [JsonProperty("員工代碼")]
        public string Code { get; set; }
        [JsonProperty("姓名")]
        public string FullName { get; set; }
        public string VOID { get; set; }
        public string SERIES { get; set; }
    }
}
