using JHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP
{
    public class Config
    {
        private static IniManager _iniManager = new IniManager(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini"));
        public static string ConnectionString_BasicDB => _iniManager.GetString("Local", "ConnectionString_BasicDB");
    }
}
