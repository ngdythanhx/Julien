using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SMTT.CheckLotID;
using static SMTT.Trustrace;

namespace SMTT
{
    public class SmttItem
    {
        public string ID { get; set; }
        public string Type {  get; set; }
        public string LotID { get; set; }
        public double Qty { get; set; }
        public string Unit { get; set; }
        public string MaterialNumber { get; set; }
        //public string Facility { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerFacility { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime OutBoundDate { get; set; }
        public string Remarks { get; set; }
        public Trustrace.TransactionStatus TransactionStatus { get; set; }
    }
    public class Production
    {

    }
}
