using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Julian
{
    public class Config
    {
        private static readonly Lazy<Config> _instance = new Lazy<Config>(() => new Config());
        public static Config Instance => _instance.Value;
        private Config()
        {

        }
        public string ConnectionString { get; set; }
        public string SMTTFolderPath { get; set; }
    }
}
