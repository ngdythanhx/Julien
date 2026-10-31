using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSV
{
    public class SQ
    {
        public DateTime OrderDate { get; set; }
        public string Brand { get; set; }
        public string CusName { get; set; }
        public string POOLD { get; set; }
        public string PONEW { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "YD";
        public DateTime ETD { get; set; }
        public string Season { get; set; }
        public string Art { get; set; }
        public string Follow { get; set; }
        public string PurchaseSpecification { get; set; }
        public string PurchaserName { get; set; }
        public int Width { get; set; }
    }
}
