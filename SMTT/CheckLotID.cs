using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMTT
{

    public class CheckLotID
    {
        public int Index { get; set; }
        public int Input_Type { get; set; } = 0;
        public string Input_LotID { get; set; } = string.Empty;
        public string Input_POCustomer { get; set; } = string.Empty;
        public string Input_ItemCode { get; set; } = string.Empty;
        public string Input_MaterialID { get; set; } = string.Empty;
        public double Input_Qty { get; set; } = -1;
        public string Output_LotID { get; set; }
        public double Output_TotalProductionQty { get; set; }
        public double Output_TotalOutboundQty { get; set; }
        public string Output_OutboundInvoice { get; set; }
        public string Output_OutboundRemarks { get; set; }
        public string Output_Production { get; set; } = "...";
        public string Output_OutBound { get; set; } = "...";
        public string Production_LotID { get; set; }
        public string OutBound_LotID { get; set; }
        public bool Finish { get; private set; } = false;
        public string CheckStatus { get; set; } = "...";
        //public Dictionary<string, SmttItem> DictSmttItem { get; set; } = new Dictionary<string, SmttItem>();
        //public List<SmttItem> __lstOutbound = new List<SmttItem>();
        //public List<SmttItem> _lstSmttItem = new List<SmttItem>();
        List<string> _lstProduction = new List<string>();
        List<string> _lstOutbound = new List<string>();
        public CheckLotID(string lotID)
        {
            Input_Type = 1;
            Input_LotID = lotID;
        }
        public CheckLotID(int type, string poCus, string itemCode, string materialID, double Qty)
        {
            Input_Type = type;
            Input_POCustomer = poCus;
            if (type == 2)
                Input_ItemCode = itemCode;
            else if (type == 3)
                Input_MaterialID = materialID;
            Input_Qty = Qty;
        }
        private async Task<bool> Search(HttpClient client, Trustrace.TransactionStatus transactionStatus)
        {
            try
            {
                string searchText = "";
                if (Input_Type == 1)
                {
                    searchText = Input_LotID;
                }
                else if (Input_Type == 2 || Input_Type == 3)
                {
                    searchText = Input_POCustomer;
                }
                else
                {
                    return false;
                }
                var jsonData = new
                {
                    module = "TT_TRANSACTION",
                    filter = new Dictionary<string, object>
                    {
                        { "Brand", new[] { "60b20f144294127f8af64c6e" } },
                        { "Transaction Status", new[] { transactionStatus.ToString() } }
                    },
                    sort = new
                    {
                        sortBy = "create_ts",
                        sortOrder = "desc",
                        sortValue = "Newest First"
                    },
                    pagination = new
                    {
                        from = 0,
                        size = 10000
                    },
                    searchContext = new
                    {
                        cocType = "PRODUCT_SEGREGATION",
                        tabTTName = transactionStatus.ToString() + "_TRANSACTION"
                    },
                    freeHand = searchText
                };
                var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
                try
                {
                    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                    var res = await client.PostAsync(
                           "https://adidas.api.trustrace.com/api/material-compliance/ps/search/tt_transaction_list",
                           content,
                           cts.Token
                       );
                    var a = res.StatusCode;
                    if (res.StatusCode == HttpStatusCode.RequestTimeout)
                    {
                        return await Search(client, transactionStatus);
                    }
                    if (res.StatusCode != HttpStatusCode.OK)
                    {
                        Output_Production = $"Lỗi: Code={(int)res.StatusCode} Err={res.Content}";
                        Output_OutBound = $"Lỗi: Code={(int)res.StatusCode} Err={res.Content}";
                        return false;
                    }
                    else
                    {
                        var text = await res.Content.ReadAsStringAsync();
                        var resJson = JObject.Parse(text);
                        if ((string)resJson["status"] == "SUCCESS")
                        {
                            var lstSmttObj = resJson["data"]?["searchResponse"]?.ToObject<List<JObject>>();
                            if (lstSmttObj == null || lstSmttObj.Count == 0)
                                return false;
                            foreach (var smttObj in lstSmttObj)
                            {
                                var item = new SmttItem()
                                {
                                    ID = smttObj["id"]?.ToString(),
                                    Type = smttObj["tx_type"]?.ToString(),
                                    LotID = smttObj["lot_id"]?.ToString(),
                                    MaterialNumber = smttObj["product_number"]?.ToString(),
                                    Qty = smttObj["quantity_in_uom"]?["value"]?.ToObject<double?>() ?? -1,
                                    Unit = smttObj["quantity_in_uom"]?["unit_value"]?.ToString(),
                                    InvoiceNumber = smttObj["invoice_number"]?.ToString(),
                                    CustomerFacility = smttObj["customer_facility_name_uid"]?.ToString(),
                                    CreatedDate = smttObj["create_ts"].ToObject<DateTime>().Date,
                                    ProductionDate = smttObj["production_date"].ToObject<DateTime>().Date,
                                    OutBoundDate = smttObj["tx_date"].ToObject<DateTime>().Date,
                                    Remarks = smttObj["remarks"]?.ToString(),
                                    TransactionStatus = transactionStatus
                                };
                                if (
                                        Input_Type == 1 ||
                                        (Input_Type == 2 && (Input_Qty == -1 || item.Qty == Input_Qty) && item.InvoiceNumber.Contains(Input_POCustomer) && item.InvoiceNumber.Contains(Input_ItemCode)) ||
                                        (Input_Type == 3 && item.MaterialNumber == Input_MaterialID && (Input_Qty == -1 || item.Qty == Input_Qty))
                                    )
                                {
                                    if (item.Type == "PRODUCTION")
                                    {
                                        Output_TotalProductionQty += item.Qty;
                                        Production_LotID += $"{item.LotID} ({item.Qty})\r\n";
                                        _lstProduction.Add($"{(item.TransactionStatus == Trustrace.TransactionStatus.DRAFT ? "[draft] " : "")}created: {item.CreatedDate.ToString("yyyy/MM/dd")} {item.Qty} ProductDate: {item.ProductionDate.ToString("yyyy/MM/dd")}");
                                    }
                                    else if (item.Type == "OUTBOUND")
                                    {
                                        Output_TotalOutboundQty += item.Qty;
                                        OutBound_LotID += $"{item.LotID} ({item.Qty})\r\n";
                                        Output_OutboundInvoice += item.InvoiceNumber + "\r\n";
                                        Output_OutboundRemarks += item.Remarks + "\r\n";
                                        _lstOutbound.Add($"{(item.TransactionStatus == Trustrace.TransactionStatus.DRAFT ? "[draft] " : "")}created: {item.CreatedDate.ToString("yyyy/MM/dd")} {item.Qty} {item.Unit} OutboundDate: {item.ProductionDate.ToString("yyyy/MM/dd")}");
                                    }
                                }
                            }
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "An error occurred while sending the request." || ex.Message == "A task was canceled.")
                    {
                        await Task.Delay(3000);
                        return await Search(client, transactionStatus);
                    }
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                Output_Production = "Lỗi: " + ex.Message;
                Output_OutBound = "Lỗi: " + ex.Message;
                return false;
            }
        }
        /*
        private async Task GetTransaction(HttpClient client, TransactionStatus transactionStatus)
        {
            var jsonData = new
            {
                module = "TT_TRANSACTION",
                filter = new Dictionary<string, object>
                {
                    { "Brand", new[] { "60b20f144294127f8af64c6e" } },
                    { "Transaction Status", new[] { transactionStatus.ToString() } }
                },
                sort = new
                {
                    sortBy = "create_ts",
                    sortOrder = "desc",
                    sortValue = "Newest First"
                },
                pagination = new
                {
                    from = 0,
                    size = 50
                },
                searchContext = new
                {
                    cocType = "PRODUCT_SEGREGATION",
                    tabTTName = transactionStatus.ToString() + "_TRANSACTION"
                },
                freeHand = this.LotID
            };
            //var json = "{\"module\":\"TT_TRANSACTION\",\"filter\":{\"Brand\":[\"60b20f144294127f8af64c6e\"],\"Transaction Status\":[\""+transactionStatus.ToString()+"\"]},\"sort\":{\"sortBy\":\"create_ts\",\"sortOrder\":\"desc\",\"sortValue\":\"Newest First\"},\"pagination\":{\"from\":0,\"size\":50},\"searchContext\":{\"cocType\":\"PRODUCT_SEGREGATION\",\"tabTTName\":\"SUBMITTED_TRANSACTION\"},\"freeHand\":\"" + this.LotID + "\"}";

            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                var res = await client.PostAsync(
                       "https://adidas.api.trustrace.com/api/material-compliance/ps/search/tt_transaction_list",
                       content,
                       cts.Token
                   );
                var a = res.StatusCode;
                if (res.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    await GetTransaction(client, transactionStatus);
                    return;
                }
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    string err = $"Lỗi: Code={(int)res.StatusCode} Err={res.Content}";
                    Production = err;
                    OutBound = err;
                }
                else
                {
                    var text = await res.Content.ReadAsStringAsync();
                    var resJson = JObject.Parse(text);
                    if ((string)resJson["status"] == "SUCCESS")
                    {
                        var lstSmttObj = resJson["data"]?["searchResponse"]?.ToObject<List<JObject>>();
                        foreach (var smttObj in lstSmttObj)
                        {
                            var item = new SmttItem()
                            {
                                Type = smttObj["tx_type"].ToString(),
                                LotID = smttObj["lot_id"].ToString(),
                                ProductNumber = smttObj["product_number"].ToString(),
                                Qty = (double)smttObj["quantity_in_uom"]["value"],
                                Unit = smttObj["quantity_in_uom"]["unit_value"].ToString(),
                                InvoiceNumber = smttObj["invoice_number"]?.ToString(),
                                CustomerFacility = smttObj["customer_facility_name_uid"]?.ToString(),
                                CreatedDate = smttObj["create_ts"].ToObject<DateTime>().Date,
                                ProductionDate = smttObj["production_date"].ToObject<DateTime>().Date,
                                OutBoundDate = smttObj["tx_date"].ToObject<DateTime>().Date,
                                Status = transactionStatus
                            };
                            _lstSmttItem.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while sending the request." || ex.Message == "A task was canceled.")
                {
                    await Task.Delay(3000);
                    await GetTransaction(client, transactionStatus);
                    return;
                }
                Production = ex.Message;
                OutBound = ex.Message;
            }
        }
        private async Task GetTransaction(HttpClient client, TransactionStatus transactionStatus, string poCus, string matID)
        {
            var jsonData = new
            {
                module = "TT_TRANSACTION",
                filter = new Dictionary<string, object>
                {
                    { "Brand", new[] { "60b20f144294127f8af64c6e" } },
                    { "Transaction Status", new[] { transactionStatus.ToString() } }
                },
                sort = new
                {
                    sortBy = "create_ts",
                    sortOrder = "desc",
                    sortValue = "Newest First"
                },
                pagination = new
                {
                    from = 0,
                    size = 50
                },
                searchContext = new
                {
                    cocType = "PRODUCT_SEGREGATION",
                    tabTTName = transactionStatus.ToString() + "_TRANSACTION"
                },
                freeHand = this.LotID
            };
            //var json = "{\"module\":\"TT_TRANSACTION\",\"filter\":{\"Brand\":[\"60b20f144294127f8af64c6e\"],\"Transaction Status\":[\""+transactionStatus.ToString()+"\"]},\"sort\":{\"sortBy\":\"create_ts\",\"sortOrder\":\"desc\",\"sortValue\":\"Newest First\"},\"pagination\":{\"from\":0,\"size\":50},\"searchContext\":{\"cocType\":\"PRODUCT_SEGREGATION\",\"tabTTName\":\"SUBMITTED_TRANSACTION\"},\"freeHand\":\"" + this.LotID + "\"}";

            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                var res = await client.PostAsync(
                       "https://adidas.api.trustrace.com/api/material-compliance/ps/search/tt_transaction_list",
                       content,
                       cts.Token
                   );
                var a = res.StatusCode;
                if (res.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    await GetTransaction(client, transactionStatus, poCus, matID);
                    return;
                }
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    string err = $"Lỗi: Code={(int)res.StatusCode} Err={res.Content}";
                    Production = err;
                    OutBound = err;
                }
                else
                {
                    var text = await res.Content.ReadAsStringAsync();
                    var resJson = JObject.Parse(text);
                    if ((string)resJson["status"] == "SUCCESS")
                    {
                        var lstSmttObj = resJson["data"]?["searchResponse"]?.ToObject<List<JObject>>();
                        foreach (var smttObj in lstSmttObj)
                        {
                            var item = new SmttItem()
                            {
                                Type = smttObj["tx_type"].ToString(),
                                LotID = smttObj["lot_id"].ToString(),
                                ProductNumber = smttObj["product_number"].ToString(),
                                Qty = (double)smttObj["quantity_in_uom"]["value"],
                                Unit = smttObj["quantity_in_uom"]["unit_value"].ToString(),
                                InvoiceNumber = smttObj["invoice_number"]?.ToString(),
                                CustomerFacility = smttObj["customer_facility_name_uid"]?.ToString(),
                                CreatedDate = smttObj["create_ts"].ToObject<DateTime>().Date,
                                ProductionDate = smttObj["production_date"].ToObject<DateTime>().Date,
                                OutBoundDate = smttObj["tx_date"].ToObject<DateTime>().Date,
                                Status = transactionStatus
                            };
                            _lstSmttItem.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while sending the request." || ex.Message == "A task was canceled.")
                {
                    await Task.Delay(3000);
                    await GetTransaction(client, transactionStatus, poCus, matID);
                    return;
                }
                Production = ex.Message;
                OutBound = ex.Message;
            }
        }
        private async Task GetTransaction(HttpClient client, TransactionStatus transactionStatus, string poCus, string matID, double qty)
        {
            var jsonData = new
            {
                module = "TT_TRANSACTION",
                filter = new Dictionary<string, object>
                {
                    { "Brand", new[] { "60b20f144294127f8af64c6e" } },
                    { "Transaction Status", new[] { transactionStatus.ToString() } }
                },
                sort = new
                {
                    sortBy = "create_ts",
                    sortOrder = "desc",
                    sortValue = "Newest First"
                },
                pagination = new
                {
                    from = 0,
                    size = 50
                },
                searchContext = new
                {
                    cocType = "PRODUCT_SEGREGATION",
                    tabTTName = transactionStatus.ToString() + "_TRANSACTION"
                },
                freeHand = this.LotID
            };
            //var json = "{\"module\":\"TT_TRANSACTION\",\"filter\":{\"Brand\":[\"60b20f144294127f8af64c6e\"],\"Transaction Status\":[\""+transactionStatus.ToString()+"\"]},\"sort\":{\"sortBy\":\"create_ts\",\"sortOrder\":\"desc\",\"sortValue\":\"Newest First\"},\"pagination\":{\"from\":0,\"size\":50},\"searchContext\":{\"cocType\":\"PRODUCT_SEGREGATION\",\"tabTTName\":\"SUBMITTED_TRANSACTION\"},\"freeHand\":\"" + this.LotID + "\"}";

            var content = new StringContent(JsonConvert.SerializeObject(jsonData), Encoding.UTF8, "application/json");
            try
            {
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(15));
                var res = await client.PostAsync(
                       "https://adidas.api.trustrace.com/api/material-compliance/ps/search/tt_transaction_list",
                       content,
                       cts.Token
                   );
                var a = res.StatusCode;
                if (res.StatusCode == HttpStatusCode.RequestTimeout)
                {
                    await GetTransaction(client, transactionStatus, poCus, matID, qty);
                    return;
                }
                if (res.StatusCode != HttpStatusCode.OK)
                {
                    string err = $"Lỗi: Code={(int)res.StatusCode} Err={res.Content}";
                    Production = err;
                    OutBound = err;
                }
                else
                {
                    var text = await res.Content.ReadAsStringAsync();
                    var resJson = JObject.Parse(text);
                    if ((string)resJson["status"] == "SUCCESS")
                    {
                        var lstSmttObj = resJson["data"]?["searchResponse"]?.ToObject<List<JObject>>();
                        foreach (var smttObj in lstSmttObj)
                        {
                            var item = new SmttItem()
                            {
                                Type = smttObj["tx_type"].ToString(),
                                LotID = smttObj["lot_id"].ToString(),
                                ProductNumber = smttObj["product_number"].ToString(),
                                Qty = (double)smttObj["quantity_in_uom"]["value"],
                                Unit = smttObj["quantity_in_uom"]["unit_value"].ToString(),
                                InvoiceNumber = smttObj["invoice_number"]?.ToString(),
                                CustomerFacility = smttObj["customer_facility_name_uid"]?.ToString(),
                                CreatedDate = smttObj["create_ts"].ToObject<DateTime>().Date,
                                ProductionDate = smttObj["production_date"].ToObject<DateTime>().Date,
                                OutBoundDate = smttObj["tx_date"].ToObject<DateTime>().Date,
                                Status = transactionStatus
                            };
                            _lstSmttItem.Add(item);
                            if (transactionStatus == TransactionStatus.SUBMITTED && item.Qty == qty && item.InvoiceNumber.Contains($"{poCus}") && item.ProductNumber == matID)
                            {
                                CheckStatus = $"{(item.Status == TransactionStatus.DRAFT ? "[draft] " : "")}createdDate: {item.CreatedDate.ToString("yyyy/MM/dd")} qty: {item.Qty} {item.Unit} Inv: {item.InvoiceNumber} DeliveryDate:{item.OutBoundDate.ToString("yyyy/MM/dd")}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "An error occurred while sending the request." || ex.Message == "A task was canceled.")
                {
                    await Task.Delay(3000);
                    await GetTransaction(client, transactionStatus, poCus, matID, qty);
                    return;
                }
                Production = ex.Message;
                OutBound = ex.Message;
            }
        }
        */
        public async Task Start(HttpClient client)
        {
            Output_Production = "waiting...";
            Output_OutBound = "waiting...";
            await Search(client, Trustrace.TransactionStatus.SUBMITTED);
            await Search(client, Trustrace.TransactionStatus.DRAFT);

            Output_Production = _lstProduction.Count == 0 ? "null" : string.Join("\r\n", _lstProduction);
            Output_OutBound = _lstOutbound.Count == 0 ? "null" : string.Join("\r\n", _lstOutbound);
            Finish = true;
        }
    }
}
