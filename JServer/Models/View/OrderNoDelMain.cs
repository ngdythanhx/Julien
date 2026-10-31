using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JServer.Models.View
{
    public class OrderNoDelMain
    {
        [JsonProperty("客戶代碼")]
        public string CustomerCode { get; set; }

        [JsonProperty("客戶簡稱")]
        public string CustomerShortName { get; set; }

        [JsonProperty("下單日期")]
        public string OrderDate { get; set; }

        [JsonProperty("訂單號碼")]
        public string OrderNumber { get; set; }

        [JsonProperty("品牌")]
        public string Brand { get; set; }

        [JsonProperty("客戶單號")]
        public string CustomerOrderNumber { get; set; }

        [JsonProperty("客戶(採購)單號")]
        public string CustomerPurchaseOrderNumber { get; set; }

        [JsonProperty("品名")]
        public string ProductName { get; set; }

        [JsonProperty("公司品名")]
        public string InternalProductName { get; set; }

        [JsonProperty("顏色")]
        public string Color { get; set; }

        [JsonProperty("稽核單號")]
        public string AuditNumber { get; set; }

        [JsonProperty("染整單號")]
        public string DyeingFinishingOrderNumber { get; set; }

        [JsonProperty("未交量(YD)")]
        public decimal UndeliveredQuantityYD { get; set; }

        [JsonProperty("客戶訂量")]
        public decimal CustomerOrderQuantity { get; set; }

        [JsonProperty("客戶訂量單位")]
        public string CustomerOrderUnit { get; set; }

        [JsonProperty("含客戶LOSS訂量")]
        public decimal CustomerOrderQuantityIncludingLoss { get; set; }

        [JsonProperty("VOID")]
        public string Void { get; set; }

        [JsonProperty("CREATEDATE")]
        public DateTime CreateDate { get; set; }

        [JsonProperty("ORDERDATE")]
        public DateTime OrderDateTime { get; set; }

        [JsonProperty("SERIES")]
        public string Series { get; set; }

        [JsonProperty("css_style")]
        public string CssStyle { get; set; }
    }
}
