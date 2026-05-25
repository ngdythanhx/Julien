using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Julian
{
    public sealed class Config
    {
        private static readonly Config _instance = new Config();
        private string _conn;
        public static string ConnectionString { get => _instance._conn; set { _instance._conn = value; } }
    }
}
