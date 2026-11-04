using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Models
{
    public class CSData
    {
        public int ID { get; set; }
        public string CustCode { get; set; }
        public string MaterialName { get; set; }
        public int Width { get; set; }
        public string Color { get; set; }
        public string ReplacementColor { get; set; }
        public string Follow { get; set; }
        public string Note { get; set; }
        public string Article { get; set; }
        public int? Confirmed { get; set; }
        public string AdditionalOptional1 { get; set; }
        public string AdditionalOptional2 { get; set; }
        public string AdditionalOptional3 { get; set; }
        public DateTime? CreatedDatetime { get; set; }
    }
}
