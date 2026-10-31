using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTTV2
{
    public class SmttItem
    {
        public string[] PODyingList {  get; set; }
        public string MaterialName {  get; set; }
        public string MaterialNumber {  get; set; }
        public decimal QtyInKG {  get; set; }
        public decimal QtyInYard {  get; set; }
        public string T2ID {  get; set; }
        public string T1Name {  get; set; }
        public string T1Code {  get; set; }
        public DateTime DeliveryDate {  get; set; }
        public string POCus {  get; set; }
        public string Code {  get; set; }
        public string Invoice {  get; set; }
    }
}
