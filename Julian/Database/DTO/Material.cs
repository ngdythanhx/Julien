using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Julian.Database.DTO
{
    public class Material
    {
        public int Id { get; set; }
        public string MaterialCode { get; set; }

        public string MaterialColor { get; set; }

        public string CustomerColor { get; set; }

        public string StandardColor { get; set; }

        public string Description { get; set; }

    }
}
