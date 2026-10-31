using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustrace.SS
{
    public class Inventory
    {
        [JsonProperty("availableQuantity")]
        public Quantity AvailableQuantity { get; set; }
        public string AvailableQuantityString => AvailableQuantity == null ? "0.00" : $"{AvailableQuantity.QuantityValue} {AvailableQuantity.Unit}";
        [JsonProperty("certificationStandards")]
        public List<string> CertificationStandards { get; set; }
        public string CertificationStandardsString => CertificationStandards == null ? "" : string.Join("; ", CertificationStandards);

        [JsonProperty("createTs")]
        public DateTime CreateTs { get; set; }

        [JsonProperty("facilityId")]
        public string FacilityId { get; set; }

        [JsonProperty("facilityName")]
        public string FacilityName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("inboundTxId")]
        public string InboundTxId { get; set; }

        [JsonProperty("materialComposition")]
        public List<MaterialComposition> MaterialCompositionList { get; set; } = new List<MaterialComposition>();

        public string ProductComposition => MaterialCompositionList == null || MaterialCompositionList.Count == 0 ? "" : $"{MaterialCompositionList.First()?.MaterialName} {MaterialCompositionList.First()?.Value}%";

        [JsonProperty("productCategory")]
        public string ProductCategory { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tcReferenceNumber")]
        public string TcReferenceNumber { get; set; }

        [JsonProperty("totalQuantity")]
        public Quantity TotalQuantity { get; set; }
        public string TotalQuantityString => TotalQuantity == null ? "0.00" : $"{TotalQuantity.QuantityValue} {TotalQuantity.Unit}";
        [JsonProperty("updateTs")]
        public DateTime UpdateTs { get; set; }

        [JsonProperty("usedQuantity")]
        public Quantity UsedQuantity { get; set; }
        public string UsedQuantityString => UsedQuantity == null ? "0.00" : $"{UsedQuantity.QuantityValue} {UsedQuantity.Unit}";
    }

    public class Quantity
    {
        [JsonProperty("quantity")]
        public decimal QuantityValue { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class MaterialComposition
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("materialName")]
        public string MaterialName { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("countryOfOrigin")]
        public List<object> CountryOfOrigin { get; set; }
    }
}
