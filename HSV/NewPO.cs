using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSV
{
    public class NewPO
    {
        [JsonProperty("poNo")]
        public string PO { get; set; }

        [JsonProperty("soNo")]
        public string SalesNo { get; set; }

        [JsonProperty("buyMonth")]
        public string BuyMonth { get; set; }

        [JsonProperty("article")]
        public string Article { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("itemCd")]
        public string ItemCode { get; set; }

        [JsonProperty("itemNm")]
        public string ItemName { get; set; }

        [JsonProperty("poDate")]
        public DateTime PoDate { get; set; }

        [JsonProperty("arriveExpDate")]
        public DateTime ETA { get; set; }

        [JsonProperty("orderQty")]
        public float OrderQty { get; set; }

        [JsonProperty("unitPrice")]
        public string UnitPriceVND { get; set; }

        [JsonProperty("amt")]
        public string Amount { get; set; }

        [JsonProperty("pantone")]
        public string Color { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("gd")]
        public string Category { get; set; }

        [JsonIgnore]
        public string Season { get; set; }
    }
}
