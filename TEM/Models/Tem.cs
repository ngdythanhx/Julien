using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEM.Models
{
    public interface Tem
    {
        Task<(bool Success, string Msg)> Create();
    }
}
