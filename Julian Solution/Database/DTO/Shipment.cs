using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Julian_SolutionDatabase.DTO
{
    public class Shipment
    {
        public int Id { get; set; }
        public string ShipCode { get; set; }
        public int Quantity { get; set; }
        public DateTime ShipDate { get; set; }
        public string InvoiceNo { get; set; }
        public string Note { get; set; }
    }
}
