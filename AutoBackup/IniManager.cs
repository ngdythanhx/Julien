using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoBackup
{
    public class IniManager
    {
        private readonly string _path;

        public IniManager(string path) => _path = path;

        public string GetString(string section, string key, string defaultValue = "")
        {
            var sb = new StringBuilder(260);
            WinApi.GetPrivateProfileStringW(section, key, defaultValue, sb, sb.Capacity, _path);
            return sb.ToString();
        }

        public int GetInt(string section, string key, int defaultValue = 0)
        {
            var str = GetString(section, key, defaultValue.ToString());
            return int.TryParse(str, out var result) ? result : defaultValue;
        }

        public bool GetBool(string section, string key, bool defaultValue = false)
        {
            var str = GetString(section, key, defaultValue.ToString());
            return bool.TryParse(str, out var result) ? result : defaultValue;
        }

        public void WriteString(string section, string key, string value) => WinApi.WritePrivateProfileStringW(section, key, value, _path);

        public void WriteInt(string section, string key, int value) => WriteString(section, key, value.ToString());
        public void WriteBool(string section, string key, bool value) => WriteString(section, key, value.ToString());
        public void WriteObj(string section, object obj)
        {
            if (obj == null) return;
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var propValue = property.GetValue(obj)?.ToString() ?? "";
                WinApi.WritePrivateProfileStringW(section, property.Name, propValue, _path);
            }
        }

        public void DeleteKey(string section, string key) => WinApi.WritePrivateProfileStringW(section, key, null, _path);

        public void DeleteSection(string section) => WinApi.WritePrivateProfileStringW(section, null, null, _path);
    }
}
