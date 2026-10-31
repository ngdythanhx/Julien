using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEM.Models
{
    public class TX:Tem
    {
        public string Path { get; set; } = "";
        public async Task<(bool Success, string Msg)> Create()
        {
            return (true, "");
        }
    }
}
