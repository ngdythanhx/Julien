using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMTT
{
    public class Finder
    {
        public int Index { get; set; }
        //Input
        public int Input_Type { get; set; } = 0;
        public string Input_LotID { get; set; } = string.Empty;
        public string Input_POCustomer { get; set; } = string.Empty;
        public string Input_ItemCode { get; set; } = string.Empty;
        public string Input_MaterialID { get; set; } = string.Empty;
        public double Input_Qty { get; set; } = -1;
        //Production
        public string Production_Created =>Finish? _lstProduction.Count!=0?  string.Join(Environment.NewLine, _lstProduction.Select(x=>x.CreatedDate.ToString("yy-MM-dd"))):"null":"waitting";
        public string Production_MaterialNumber => string.Join(Environment.NewLine, _lstProduction.Select(x => x.MaterialNumber));
        public string Production_LotID => string.Join(Environment.NewLine, _lstProduction.Select(x => x.LotID));
        public string Production_Qty => string.Join(Environment.NewLine, _lstProduction.Select(x => x.Qty));
        public double Production_TotalQty => _lstProduction.Sum(x=>x.Qty);
        public string Production_Date =>string.Join(Environment.NewLine, _lstProduction.Select(x=>x.ProductionDate.ToString("yy-MM-dd")));
        //Outbound
        public string Outbound_Created => Finish ? _lstOutbound.Count != 0 ? string.Join(Environment.NewLine, _lstOutbound.Select(x => x.CreatedDate.ToString("yy-MM-dd"))) : "null":"waitting";
        public string Outbound_MaterialNumber=>  string.Join(Environment.NewLine, _lstOutbound.Select(x => x.MaterialNumber));
        public string Outbound_LotID => string.Join(Environment.NewLine, _lstOutbound.Select(x => x.LotID));
        public string Outbound_Qty => string.Join(Environment.NewLine, _lstOutbound.Select(x => x.Qty));
        public double Outbound_TotalQty => _lstOutbound.Sum(x => x.Qty);
        public string Outbound_Invoice => string.Join(Environment.NewLine, _lstOutbound.Select(x => x.InvoiceNumber));
        public string Outbound_Remarks => string.Join(Environment.NewLine, _lstOutbound.Select(x => x.Remarks));
        public string Outbound_Date => string.Join(Environment.NewLine, _lstOutbound.Select(x => x.OutBoundDate.ToString("yy-MM-dd")));
        public bool Finish { get; private set; } = false;
        public string Status { get; set; } = "...";

        List<SmttItem> _lstProduction = new List<SmttItem>();
        List<SmttItem> _lstOutbound = new List<SmttItem>();
        private Trustrace _trustrace = null;
        public Finder(Trustrace trustrace, string lotID)
        {
            _trustrace = trustrace;
            Input_Type = 1;
            Input_LotID = lotID;
        }
        public Finder(Trustrace trustrace, int type, string poCus, string itemCode, string materialID, double Qty)
        {
            _trustrace = trustrace;
            Input_Type = type;
            Input_POCustomer = poCus;
            if (type == 2)
                Input_ItemCode = itemCode;
            else if (type == 3)
                Input_MaterialID = materialID;
            Input_Qty = Qty;
        }
        private async Task Find(Trustrace.TransactionStatus transactionStatus)
        {
            _lstProduction.Clear();
            _lstOutbound.Clear();
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
                return;
            }
            var search = await _trustrace.Search(transactionStatus, searchText);
            if (search.success)
            {
                var lst = new List<SmttItem>();
                foreach (var item in search.lst)
                {
                    if (
                        Input_Type == 1 ||
                        (Input_Type == 2 && (Input_Qty == -1 || item.Qty == Input_Qty) && item.InvoiceNumber.Contains(Input_POCustomer) && item.InvoiceNumber.Contains(Input_ItemCode)) ||
                        (Input_Type == 3 && item.MaterialNumber == Input_MaterialID && (Input_Qty == -1 || item.Qty == Input_Qty))
                    )
                    {
                        if (item.Type == "PRODUCTION")
                        {
                            _lstProduction.Add(item);
                        }
                        else if (item.Type == "OUTBOUND")
                        {
                            _lstOutbound.Add(item);
                        }
                    }
                }
            }
        }
        public async Task StartFind()
        {
            var task_submitted = Find(Trustrace.TransactionStatus.SUBMITTED);
            var task_draft = Find(Trustrace.TransactionStatus.DRAFT);
            await Task.WhenAll(task_submitted, task_draft);
            Finish = true;
        }
    }
}
