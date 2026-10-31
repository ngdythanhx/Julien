using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JServer.Models
{
    public class Material
    {
        //public static string Query => @"select PRODNO Code, PRODNAME Name, GSM, upwidth from ledwaybasic.product where isnull(series,'')<>'' and isnull(PRODNAME,'')<>''";
        public string Code { get; set; }
        public string Name { get; set; }
        public int GSM { get; set; }
        public int upwidth { get; set; }
    }
}
