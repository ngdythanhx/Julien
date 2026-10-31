using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NR
{
    public class INV
    {
        public int ID { get; set; }
        public string PO { get; set; }
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Width { get; set; }
        public int Weight { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "YD";
        public decimal UnitPrice { get; set; }
        public decimal Amount => Qty * UnitPrice;
        public DateTime DeliveryDate { get; set; }
    }
}
