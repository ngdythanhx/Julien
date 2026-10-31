using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JServer.Models
{
    public class QueryResponse<T>
    {
        [JsonProperty("recordset")]
        public List<T> Recordset { get; set; }

        [JsonProperty("rowsAffected")]
        public List<int> RowsAffected { get; set; }
    }
}
