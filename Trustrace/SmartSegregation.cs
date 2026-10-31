using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Trustrace.SS;
using static Trustrace.SmartSegregation;

namespace Trustrace
{
    public class SmartSegregation
    {
        TrustraceAPI _trustraceAPI;
        public SmartSegregation(TrustraceAPI serverAPI)
        {
            _trustraceAPI = serverAPI;
        }
        public class ProductionInventory
        {
            [JsonProperty("id")]
            public string ID { get; set; }
            [JsonProperty("materialUid")]
            public string MaterialUid { get; set; }
            [JsonProperty("materialName")]
            public string MaterialName { get; set; }
            [JsonProperty("materialNumber")]
            public string MaterialID { get; set; }
            [JsonProperty("totalQuantity")]
            public InventoryQty TotalQuantity { get; set; }
            [JsonProperty("shippedQuantity")]
            public InventoryQty ShippedQuantity { get; set; }
            [JsonProperty("remainingQuantity")]
            public InventoryQty RemainingQuantity { get; set; }
            [JsonProperty("facilityReference")]
            public string FacilityReference { get; set; }
            [JsonProperty("productItemId")]
            public string ProductItemId { get; set; }


        }
        public class InventoryQty
        {
            [JsonProperty("quantity")]
            public decimal Quantity { get; set; }
            [JsonProperty("unit")]
            public string Unit { get; set; }
            [JsonProperty("unitId")]
            public string UnitId { get; set; }
        }
        public class T2Facility
        {
            [JsonProperty("address_country")]
            public string AddressCountry { get; set; }

            [JsonProperty("address_line1")]
            public string AddressLine1 { get; set; }

            [JsonProperty("allow_to_use")]
            public bool AllowToUse { get; set; }

            [JsonProperty("company_id")]
            public string CompanyId { get; set; }

            [JsonProperty("created_company_id")]
            public string CreatedCompanyId { get; set; }

            [JsonProperty("created_company_name")]
            public string CreatedCompanyName { get; set; }

            [JsonProperty("creation_source_and_status")]
            public string CreationSourceAndStatus { get; set; }

            [JsonProperty("facility_id")]
            public string FacilityId { get; set; }

            [JsonProperty("facility_link_ids")]
            public List<string> FacilityLinkIds { get; set; }

            [JsonProperty("facility_name_by_supplier")]
            public string FacilityNameBySupplier { get; set; }

            [JsonProperty("facility_uid_brand")]
            public string FacilityUidBrand { get; set; }

            [JsonProperty("facility_uid_by_brand")]
            public string FacilityUidByBrand { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("reference_id")]
            public string ReferenceId { get; set; }

            [JsonProperty("searchAfterSort")]
            public List<string> SearchAfterSort { get; set; }

            [JsonProperty("searched_reference")]
            public string SearchedReference { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("supplier_association_status")]
            public int SupplierAssociationStatus { get; set; }

            [JsonProperty("supplier_facility_id")]
            public string SupplierFacilityId { get; set; }

            [JsonProperty("supplier_id")]
            public string SupplierId { get; set; }

            [JsonProperty("supplier_name")]
            public string SupplierName { get; set; }

            [JsonProperty("used_for_production")]
            public string UsedForProduction { get; set; }
        }
        public class Material
        {
            [JsonProperty("unique_search")]
            public string UniqueSearch { get; set; }

            [JsonProperty("company_id")]
            public string CompanyId { get; set; }

            [JsonProperty("create_ts")]
            public DateTime CreateTs { get; set; }

            //[JsonProperty("custom_fields")]
            //public CustomFields CustomFields { get; set; }

            //[JsonProperty("density")]
            //public UnitAttribute Density { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("id_type")]
            public string IdType { get; set; }

            [JsonProperty("identifier_tags")]
            public List<string> IdentifierTags { get; set; }

            [JsonProperty("internal_article_name")]
            public string InternalArticleName { get; set; }

            [JsonProperty("internal_article_number")]
            public string InternalArticleNumber { get; set; }

            //[JsonProperty("material_composition")]
            //public List<MaterialComposition> MaterialComposition { get; set; }

            [JsonProperty("material_unique_code")]
            public string MaterialUniqueCode { get; set; }

            //[JsonProperty("ou_attribute")]
            //public OuAttribute OuAttribute { get; set; }

            //[JsonProperty("product_type")]
            //public ProductType ProductType { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            //[JsonProperty("sub-contractor_facilities")]
            //public List<Facility> SubContractorFacilities { get; set; }

            [JsonProperty("supplier")]
            public List<MaterialSupplier> Suppliers { get; set; }

            [JsonProperty("supplier_article_name")]
            public string SupplierArticleName { get; set; }

            [JsonProperty("supplier_article_number")]
            public string SupplierArticleNumber { get; set; }

            [JsonProperty("traceable")]
            public string Traceable { get; set; }

            [JsonProperty("update_ts")]
            public DateTime UpdateTs { get; set; }

            //[JsonProperty("width")]
            //public UnitAttribute Width { get; set; }
        }
        public class MaterialSupplier
        {
            [JsonProperty("reference_id")]
            public string ReferenceId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("id")]
            public string ID { get; set; }
            [JsonProperty("facility_id")]
            public string FacilityID { get; set; }

        }
        public class FacilityProfile
        {
            [JsonProperty("facility_uid_by_brand")]
            public string FacilityUidByBrand { get; set; }

            [JsonProperty("created_company_name")]
            public string CreatedCompanyName { get; set; }

            [JsonProperty("reference_id")]
            public string ReferenceId { get; set; }

            [JsonProperty("company_id")]
            public string CompanyId { get; set; }

            [JsonProperty("facility_name_by_supplier")]
            public string FacilityNameBySupplier { get; set; }

            [JsonProperty("address_country")]
            public string AddressCountry { get; set; }

            [JsonProperty("creation_source_and_status")]
            public string CreationSourceAndStatus { get; set; }

            [JsonProperty("used_for_production")]
            public string UsedForProduction { get; set; }

            [JsonProperty("allow_to_use")]
            public bool AllowToUse { get; set; }

            [JsonProperty("address_line1")]
            public string AddressLine1 { get; set; }

            [JsonProperty("supplier_association_status")]
            public int SupplierAssociationStatus { get; set; }

            [JsonProperty("facility_link_ids")]
            public List<string> FacilityLinkIds { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("created_company_id")]
            public string CreatedCompanyId { get; set; }

            [JsonProperty("facility_id")]
            public string FacilityId { get; set; }

            [JsonProperty("searchAfterSort")]
            public List<string> SearchAfterSort { get; set; }

            [JsonProperty("supplier_name")]
            public string SupplierName { get; set; }

            [JsonProperty("supplier_id")]
            public string SupplierId { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("supplier_facility_id")]
            public string SupplierFacilityId { get; set; }

            [JsonProperty("facility_uid_brand")]
            public string FacilityUidBrand { get; set; }

            [JsonProperty("searched_reference")]
            public string SearchedReference { get; set; }
        }
        public class CustomerProfile
        {
            [JsonProperty("facility_uid_by_brand")]
            public string FacilityUidByBrand { get; set; }

            [JsonProperty("zip")]
            public string Zip { get; set; }

            [JsonProperty("created_company_name")]
            public string CreatedCompanyName { get; set; }

            [JsonProperty("reference_id")]
            public string ReferenceId { get; set; }

            [JsonProperty("company_id")]
            public string CompanyId { get; set; }

            [JsonProperty("facility_name_by_supplier")]
            public string FacilityNameBySupplier { get; set; }

            [JsonProperty("address_country")]
            public string AddressCountry { get; set; }

            [JsonProperty("creation_source_and_status")]
            public string CreationSourceAndStatus { get; set; }

            [JsonProperty("address_state")]
            public string AddressState { get; set; }

            [JsonProperty("used_for_production")]
            public string UsedForProduction { get; set; }

            [JsonProperty("address_city")]
            public string AddressCity { get; set; }

            [JsonProperty("allow_to_use")]
            public bool AllowToUse { get; set; }

            [JsonProperty("address_line2")]
            public string AddressLine2 { get; set; }

            [JsonProperty("address_line1")]
            public string AddressLine1 { get; set; }

            [JsonProperty("supplier_association_status")]
            public int SupplierAssociationStatus { get; set; }

            [JsonProperty("facility_link_ids")]
            public List<string> FacilityLinkIds { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("created_company_id")]
            public string CreatedCompanyId { get; set; }

            [JsonProperty("facility_id")]
            public string FacilityId { get; set; }

            [JsonProperty("searchAfterSort")]
            public List<string> SearchAfterSort { get; set; }

            [JsonProperty("supplier_name")]
            public string SupplierName { get; set; }

            [JsonProperty("supplier_id")]
            public string SupplierId { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("supplier_facility_id")]
            public string SupplierFacilityId { get; set; }

            [JsonProperty("searched_reference")]
            public string SearchedReference { get; set; }
        }
        public async Task<List<Material>> GetMaterial(string mtlID)
        {
            try
            {
                string data = $"{{\"filter\":{{\"Internal Material Number\":[\"{mtlID}\"],\"Traceability Tags\":[\"CMC_SS\"]}},\"freeHand\":null,\"brandContextId\":\"{_trustraceAPI.BrandContextID}\"}}";
                //string data = $"{{\"filter\":{{\"Internal Material Number\":[\"{mtlID}\"],\"Traceability Tags\":[\"CMC_SS\"]}},\"freeHand\":null,\"brandContextId\":\"60b20f144294127f8af64c6e\"}}";
                var res = await _trustraceAPI.HttpClient.PostAsync("https://adidas.api.trustrace.com/api/smart-segregation/search/mtr_lib_cfs", new StringContent(data, Encoding.UTF8, "application/json"));
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var resResultString = await res.Content.ReadAsStringAsync();
                    var a = JObject.Parse(resResultString);
                    var lst = JsonConvert.DeserializeObject<List<Material>>(a["data"]["searchResponse"].ToString());
                    return lst.Where(x => x.Suppliers.Any(y => y.ID == _trustraceAPI.Profile.CompanyId)).ToList();
                }
                return new List<Material>();
            }
            catch
            {
                return new List<Material>();
            }
        }
        public async Task<FacilityProfile> GetFacilityProfile(string t2ID)
        {
            try
            {
                string data = $"{{\"brandContextId\":\"{_trustraceAPI.BrandContextID}\",\"filter\":{{\"Reference Id\":[\"{t2ID}\"],\"Supplier Id\":[\"{_trustraceAPI.Profile.CompanyId}\"]}},\"searchField\":\"Recording Facility\"}}";
                var res = await _trustraceAPI.HttpClient.PostAsync($"https://adidas.api.trustrace.com/api/smart-segregation/search/facility-profile-cs", new StringContent(data, Encoding.UTF8, "application/json"));
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var resResultString = await res.Content.ReadAsStringAsync();
                    var lst = JsonConvert.DeserializeObject<List<FacilityProfile>>(JObject.Parse(resResultString)["data"]["searchResponse"].ToString());
                    lst = lst.Where(x => x.AllowToUse).ToList();
                    if (lst != null && lst.Count == 1)
                        return lst[0];
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<CustomerProfile> GetCustomerProfile(string t1ID)
        {
            try
            {
                string data = $"{{\"brandContextId\":\"{_trustraceAPI.BrandContextID}\",\"filter\":{{\"Reference Id\":[\"{t1ID}\"]}},\"searchField\":\"Customer Facility\",\"mustNot\":{{\"Supplier Id\":[\"{_trustraceAPI.Profile.CompanyId}\"]}}}}";
                var res = await _trustraceAPI.HttpClient.PostAsync($"https://adidas.api.trustrace.com/api/smart-segregation/search/facility-profile-cs", new StringContent(data, Encoding.UTF8, "application/json"));
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var resResultString = await res.Content.ReadAsStringAsync();
                    var lst = JsonConvert.DeserializeObject<List<CustomerProfile>>(JObject.Parse(resResultString)["data"]["searchResponse"].ToString());
                    lst = lst.Where(x => x.AllowToUse).ToList();
                    if (lst != null && lst.Count == 1)
                        return lst[0];
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ProductionInventory> GetProductionInventory(string mtlID, string t2ID)
        {
            try
            {
                var res = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/inventory/production?materialNumber={mtlID.Trim()}&facilityReference={t2ID.Trim()}");
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var resResultString = await res.Content.ReadAsStringAsync();
                    var a = JObject.Parse(resResultString);
                    var lst = JsonConvert.DeserializeObject<List<ProductionInventory>>(a["items"].ToString());
                    lst = lst.Where(x => x.FacilityReference == t2ID).ToList();
                    if (lst != null && lst.Count > 0)
                        return lst[0];
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ProductionInventory>> GetProductionInventoryList(string mtlID, string t2ID)
        {
            try
            {
                var res = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/inventory/production?facilityReference={t2ID.Trim()}");
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    var resResultString = await res.Content.ReadAsStringAsync();
                    var a = JObject.Parse(resResultString);
                    var lst = JsonConvert.DeserializeObject<List<ProductionInventory>>(a["items"].ToString());
                    lst = lst.Where(x => x.FacilityReference == t2ID).ToList();
                    return lst;
                }
                return new List<ProductionInventory>();
            }
            catch
            {
                return new List<ProductionInventory>();
            }
        }
        public async Task<(bool success, string msg)> CheckOutboundData(string mtlID, string t2ID, decimal totalQtyUpload, string t1ID)
        {
            try
            {
                //check mtl
                var lstMaterial = await GetMaterial(mtlID);
                if (lstMaterial == null || lstMaterial.Count == 0)
                {
                    return (false, "Dữ liệu Mtl ID không hợp lệ");
                }
                //check Facility
                var facility = await GetFacilityProfile(t2ID);
                if (facility == null)
                {
                    return (false, "Dữ liệu T2 ID không hợp lệ");
                }
                //check Inventory
                var productionInventory = await GetProductionInventory(mtlID, t2ID);
                if (productionInventory == null)
                {
                    return (false, $"Không tìm thấy dữ liệu trong kho lưu trữ");
                }
                //decimal totalQtyInventory = lstInventory.Sum(x => x.RemainingQuantity.Quantity);
                if (totalQtyUpload > productionInventory.RemainingQuantity.Quantity)
                {
                    return (false, $"Số lượng Production không đủ ({productionInventory.RemainingQuantity.Quantity}<{totalQtyUpload})");
                }
                //check Customer

                var customer = await GetCustomerProfile(t1ID);
                if (customer == null)
                {
                    return (false, "Dữ liệu T1 ID không hợp lệ");
                }

                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống: {ex.Message}");
            }
        }
        public async Task<(bool success, string msg)> UploadOutbound(string mtlID, string t2ID, decimal totalQtyUpload, string t1ID)
        {
            try
            {
                //check mtl
                var lstMaterial = await GetMaterial(mtlID);
                if (lstMaterial == null || lstMaterial.Count == 0)
                {
                    return (false, "Dữ liệu Mtl ID không hợp lệ");
                }
                //check Facility
                var facility = await GetFacilityProfile(t2ID);
                if (facility == null)
                {
                    return (false, "Dữ liệu T2 ID không hợp lệ");
                }
                //check Inventory
                var productionInventory = await GetProductionInventory(mtlID, t2ID);
                if (productionInventory == null)
                {
                    return (false, $"Không tìm thấy dữ liệu trong kho lưu trữ");
                }
                //decimal totalQtyInventory = lstInventory.Sum(x => x.RemainingQuantity.Quantity);
                if (totalQtyUpload > productionInventory.RemainingQuantity.Quantity)
                {
                    return (false, $"Số lượng Production không đủ ({productionInventory.RemainingQuantity.Quantity}<{totalQtyUpload})");
                }
                //check Customer

                var customer = await GetCustomerProfile(t1ID);
                if (customer == null)
                {
                    return (false, "Dữ liệu T1 ID không hợp lệ");
                }
                //Upload
                var data = new
                {
                    sourceInventoryId = productionInventory.ID,
                    productItemId = productionInventory.ProductItemId,

                    recordingFacility = new
                    {
                        id = facility.FacilityId,
                        supplierReferenceName = facility.Name,
                        supplierReference = facility.ReferenceId,
                        searchedReference = facility.ReferenceId,
                        searchedReferenceName = facility.Name,
                        supplierId = facility.SupplierId,
                        ownedByCompanyId = facility.CompanyId,
                        brandReference = facility.ReferenceId,
                        brandReferenceName = facility.Name
                    },

                    customerFacility = new
                    {
                        id = customer.FacilityId,
                        supplierReferenceName = customer.Name,
                        supplierReference = customer.ReferenceId,
                        searchedReference = customer.ReferenceId,
                        searchedReferenceName = customer.Name,
                        supplierId = customer.SupplierId,
                        ownedByCompanyId = customer.CompanyId,
                        brandReference = customer.ReferenceId,
                        brandReferenceName = customer.Name
                    },

                    releaseQuantity = totalQtyUpload,
                    releaseUnit = productionInventory.RemainingQuantity.UnitId,
                };
                var res = await _trustraceAPI.HttpClient.PostAsync("https://adidas.api.trustrace.com/api/smart-segregation/transactions/outbound", new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
                var resString = await res.Content.ReadAsStringAsync();
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    if (resString.Contains("{\"id\":\""))
                    {
                        return (true, JObject.Parse(resString)["id"].ToString());
                    }
                }
                return (false, resString);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống: {ex.Message}");
            }
        }
        public async Task<List<Inventory>> GetInventoryList()
        {
            try
            {
                var res1 = await _trustraceAPI.HttpClient.GetAsync("https://adidas.api.trustrace.com/api/smart-segregation/inventory?from=0&limit=1&sortOrder=desc");
                var resString1 = await res1.Content.ReadAsStringAsync();
                int count = (int)JObject.Parse(resString1)["total"];
                var page = (int)Math.Ceiling((double)count / 100);
                var lstInventory = new List<Inventory>();
                for (int i = 0; i < page; i++)
                {
                    var res2 = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/inventory?from={i * 100}&limit={i * 100 + 100}&sortOrder=desc");
                    var resString2 = await res2.Content.ReadAsStringAsync();
                    var lst = JsonConvert.DeserializeObject<List<Inventory>>(JObject.Parse(resString2)["items"].ToString());
                    lstInventory.AddRange(lst);
                }
                return lstInventory.OrderBy(x => x.TcReferenceNumber).ToList();
            }
            catch
            {
                return new List<Inventory>();
            }
        }
        public async Task<SS.TcCertificateResponse> GetTCCertificate(string id, string tcName)
        {
            var a = "";
            try
            {
                string data = $"{{\"tt_document_transformed_id\":\"{id}\",\"tt_document_id\":\"\"}}";
                var res = await _trustraceAPI.HttpClient.PostAsync("https://adidas.api.trustrace.com/api/smart-segregation/tc/view", new StringContent(data, Encoding.UTF8, "application/json"));
                var resString = await res.Content.ReadAsStringAsync();
                a = resString;
                var tc = JsonConvert.DeserializeObject<TcCertificateResponse>(resString);
                if (tc == null || tc.Data == null)
                {

                }    
                return tc;
            }
            catch(Exception ex)
            {
                return null;
            }
            /*catch (JsonReaderException ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                Console.WriteLine($"Vị trí: {ex.LineNumber}, cột: {ex.LinePosition}");
                //Console.WriteLine(a);
                return null;
            }*/
        }
        public async Task<List<TcInfo>> GetTcInfoList()
        {
            try
            {
                var res1 = await _trustraceAPI.HttpClient.GetAsync("https://adidas.api.trustrace.com/api/smart-segregation/tc/list?sortBy=create_ts&sortOrder=desc&from=0&size=1");
                var resString1 = await res1.Content.ReadAsStringAsync();
                int count = (int)JObject.Parse(resString1)["total"];
                var page = (int)Math.Ceiling((double)count / 100);
                var lstTcInfo = new List<TcInfo>();
                for (int i = 0; i < page; i++)
                {
                    var res2 = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/tc/list?sortBy=create_ts&sortOrder=desc&from={i * 100}&size={i * 100 + 100}");
                    //var res2 = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/inventory?from={i * 100}&limit={i * 100 + 100}&sortOrder=desc");
                    var resString2 = await res2.Content.ReadAsStringAsync();
                    var lst = JsonConvert.DeserializeObject<List<TcInfo>>(JObject.Parse(resString2)["items"].ToString());
                    lstTcInfo.AddRange(lst);
                }
                return lstTcInfo;
            }
            catch
            {
                return new List<TcInfo>();
            }
        }
        public async Task<List<TcCertificateData>> GetTCCertificateList()
        {
            try
            {
                var res1 = await _trustraceAPI.HttpClient.GetAsync("https://adidas.api.trustrace.com/api/smart-segregation/tc/list?sortBy=create_ts&sortOrder=desc&from=0&size=1");
                var resString1 = await res1.Content.ReadAsStringAsync();
                int count = (int)JObject.Parse(resString1)["total"];
                var page = (int)Math.Ceiling((double)count / 100);
                var lstTcInfo = new ConcurrentBag<TcInfo>();
                ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount };
                var lstTask1 = new List<Task>();
                Parallel.For(0, page, parallelOptions, i =>
                {
                    lstTask1.Add(Task.Run(async () =>
                    {
                        var res2 = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/tc/list?sortBy=create_ts&sortOrder=desc&from={i * 100}&size=100");
                        //var res2 = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/inventory?from={i * 100}&limit={i * 100 + 100}&sortOrder=desc");
                        var resString2 = await res2.Content.ReadAsStringAsync();
                        var lst = JsonConvert.DeserializeObject<List<TcInfo>>(JObject.Parse(resString2)["items"].ToString());
                        foreach (var item in lst)
                        {
                            lstTcInfo.Add(item);
                        }
                    }));
                });
                await Task.WhenAll(lstTask1);
                /* for (int i = 0; i < page; i++)
                 {
                     var res2 = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/tc/list?sortBy=create_ts&sortOrder=desc&from={i * 100}&size=100");
                     //var res2 = await _trustraceAPI.HttpClient.GetAsync($"https://adidas.api.trustrace.com/api/smart-segregation/inventory?from={i * 100}&limit={i * 100 + 100}&sortOrder=desc");
                     var resString2 = await res2.Content.ReadAsStringAsync();
                     var lst = JsonConvert.DeserializeObject<List<TcInfo>>(JObject.Parse(resString2)["items"].ToString());
                     foreach (var item in lst)
                     {
                         lstTcInfo.Add(item);
                     }
                 }*/
                var lstTcCertificateData = new ConcurrentBag<TcCertificateData>();
                /*
                var lstTask2 = new List<Task>();
                foreach (var tcInfo in lstTcInfo)
                {
                    await Task.Delay(10);
                    lstTask2.Add(Task.Run(async () =>
                    {
                        var tcCertificateResponse = await GetTCCertificate(tcInfo.Id,tcInfo.TcReferenceId);
                        if (tcCertificateResponse != null)
                            lstTcCertificateData.Add(tcCertificateResponse.Data);
                    }));
                };
                await Task.WhenAll(lstTask2);*/
                foreach (var tcInfo in lstTcInfo)
                {
                    var tcCertificateResponse = await GetTCCertificate(tcInfo.Id,tcInfo.TcReferenceId);
                    if (tcCertificateResponse != null)
                        lstTcCertificateData.Add(tcCertificateResponse.Data);
                }
                return lstTcCertificateData.ToList();
            }
            catch
            {
                return new List<TcCertificateData>();
            }
        }
    }
}
