using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trustrace.SS
{
    public class TcInfo
    {
        [JsonProperty("certId")]
        public string CertId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tcReferenceId")]
        public string TcReferenceId { get; set; }
    }
    public class TcCertificateResponse
    {
        [JsonProperty("data")]
        public TcCertificateData Data { get; set; }

    }

    public class TcCertificateData
    {
        /*[JsonProperty("certId")]
        public string CertId { get; set; }

        [JsonProperty("certType")]
        public string CertType { get; set; }

        [JsonProperty("ttDocumentId")]
        public string TtDocumentId { get; set; }

        //[JsonProperty("creationDate")]
        //public DateTime CreationDate { get; set; }

        //[JsonProperty("companyId")]
       /// public string CompanyId { get; set; }

        //[JsonProperty("chainOfCustody")]
        //public string ChainOfCustody { get; set; }

        //[JsonProperty("tierId")]
        //public string TierId { get; set; }

        //[JsonProperty("supportingDocumentsCollectedDuringTCUpload")]
        //public SupportingDocuments SupportingDocuments { get; set; }

        //[JsonProperty("extractedData")]
        //public object ExtractedData { get; set; }
        */
        [JsonProperty("changedData")]
        public ChangedData ChangedData { get; set; }
    }

    public class SupportingDocuments
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("uploadedFiles")]
        public List<string> UploadedFiles { get; set; }

       // [JsonProperty("createTs")]
        //public DateTime CreateTs { get; set; }
    }

    /*public class ChangedData
    {
        [JsonProperty("txType")]
        public string TxType { get; set; }

        [JsonProperty("productEntity")]
        public string ProductEntity { get; set; }

        [JsonProperty("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("countryOfDispatch")]
        public string CountryOfDispatch { get; set; }

        [JsonProperty("bodyIssuingCertificate")]
        public string BodyIssuingCertificate { get; set; }

        [JsonProperty("licenseCode")]
        public string LicenseCode { get; set; }

        [JsonProperty("netWeight")]
        public string NetWeight { get; set; }

        [JsonProperty("grossWeight")]
        public string GrossWeight { get; set; }

        [JsonProperty("commercialWeight")]
        public string CommercialWeight { get; set; }

        [JsonProperty("inspectionBody")]
        public string InspectionBody { get; set; }

        [JsonProperty("lastProcessorOfProducts")]
        public string LastProcessorOfProducts { get; set; }

        [JsonProperty("placeAndDateOfIssue")]
        public string PlaceAndDateOfIssue { get; set; }

        [JsonProperty("productAndShipmentInformation")]
        public string ProductAndShipmentInformation { get; set; }

        [JsonProperty("isTrader")]
        public bool IsTrader { get; set; }

        [JsonProperty("certTypeIds")]
        public List<string> CertTypeIds { get; set; }

        [JsonProperty("buyerOfProduct")]
        public CertificateFacility BuyerOfProduct { get; set; }

        [JsonProperty("sellerOfProduct")]
        public CertificateFacility SellerOfProduct { get; set; }

        [JsonProperty("consigneeOfProduct")]
        public List<CertificateFacility> ConsigneeOfProduct { get; set; }

        [JsonProperty("productInfo")]
        public List<ProductInfo> ProductInfo { get; set; }

        [JsonProperty("validationResponse")]
        public ValidationResponse ValidationResponse { get; set; }
    }
    */
    public class ChangedData
    {       

        [JsonProperty("productInfo")]
        public List<ProductInfo> ProductInfo { get; set; }
    }
    public class CertificateFacility
    {
        [JsonProperty("facilityName")]
        public string FacilityName { get; set; }

        [JsonProperty("facilityId")]
        public string FacilityId { get; set; }

        [JsonProperty("facilityReferenceId")]
        public string FacilityReferenceId { get; set; }

        [JsonProperty("isMatched")]
        public bool IsMatched { get; set; }

        [JsonProperty("shipmentUids")]
        public List<string> ShipmentUids { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("errorMsg")]
        public string ErrorMsg { get; set; }
    }

    public class ProductInfo
    {
        [JsonProperty("productNo")]
        public string ProductNo { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("productCategory")]
        public string ProductCategory { get; set; }

        [JsonProperty("netWeight")]
        public string NetWeight { get; set; }

        [JsonProperty("grossWeight")]
        public string GrossWeight { get; set; }

        [JsonProperty("certTypeIds")]
        public List<string> CertTypeIds { get; set; }

        [JsonProperty("tradeName")]
        public string TradeName { get; set; }

        [JsonProperty("lotNumbers")]
        public string LotNumbers { get; set; }

        [JsonProperty("labelGrade")]
        public string LabelGrade { get; set; }

        [JsonProperty("packedIn")]
        public string PackedIn { get; set; }

        [JsonProperty("materialComposition")]
        public List<ProductMaterialComposition> MaterialComposition { get; set; }

        //[JsonProperty("supplierUom")]
        //public string SupplierUom { get; set; }

        [JsonProperty("productItem")]
        public object ProductItem { get; set; }

        [JsonProperty("customerFacilityId")]
        public string CustomerFacilityId { get; set; }
    }

    public class ProductMaterialComposition
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }

        [JsonProperty("materialName")]
        public string MaterialName { get; set; }
    }

    public class ValidationResponse
    {
        [JsonProperty("validationStatus")]
        public string ValidationStatus { get; set; }

        [JsonProperty("buyerValidation")]
        public ValidationDetail BuyerValidation { get; set; }

        [JsonProperty("sellerValidation")]
        public ValidationDetail SellerValidation { get; set; }

        [JsonProperty("consigneeValidation")]
        public ValidationDetail ConsigneeValidation { get; set; }

        [JsonProperty("duplicateCertificateValidation")]
        public object DuplicateCertificateValidation { get; set; }

        [JsonProperty("materialCompositionValidation")]
        public MaterialCompositionValidation MaterialCompositionValidation { get; set; }

        [JsonProperty("uomValidation")]
        public object UomValidation { get; set; }

        [JsonProperty("quantityValidation")]
        public MaterialCompositionValidation QuantityValidation { get; set; }
    }

    public class ValidationDetail
    {
        [JsonProperty("validationUid")]
        public string ValidationUid { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("failureAction")]
        public string FailureAction { get; set; }

        [JsonProperty("validationStatus")]
        public bool? ValidationStatus { get; set; }

        [JsonProperty("isEnabled")]
        public bool? IsEnabled { get; set; }
    }

    public class MaterialCompositionValidation
    {
        [JsonProperty("validationUid")]
        public string ValidationUid { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("failureAction")]
        public string FailureAction { get; set; }

        [JsonProperty("productInfoList")]
        public List<object> ProductInfoList { get; set; }
    }
}
