
using Julian.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyXuan.LocalDatabase.DTO;

namespace DAO
{
    public class DAO
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "Database", "TX.ini"));
        private static readonly Lazy<DAO> _instance = new Lazy<DAO>(() => new DAO());

        public static DAO Instance => _instance.Value;
        public List<Material> GetMaterials()
        {
            string section = "Material";
            int count = _iniManager.GetInt(section, "Count", 0);
            var lst = new List<Material>();
            for (int i = 0; i < count; i++)
            {
                string[] data = _iniManager.GetString(section, "_" + i).Split('|');
                if (data.Length == 4)
                {
                    var mtl = new Material()
                    {
                        Code = data[0],
                        Name = data[1],
                        Color = data[2],
                        ReplacementColor = data[3],
                    };
                    lst.Add(mtl);
                }

            }
            return lst;
        }
        public List<Follow> GetFollows()
        {
            string section = "Follow";
            int count = _iniManager.GetInt(section, "Count", 0);
            var lst = new List<Follow>();
            for (int i = 0; i < count; i++)
            {
                string[] data = _iniManager.GetString(section, "_" + i).Split('|');
                if (data.Length == 3)
                {
                    var fl = new Follow()
                    {
                        MaterialCode = data[0],
                        PO = data[1],
                        Note = data[2],
                    };
                    lst.Add(fl);
                }
            }
            return lst;
        }
        public List<Price> GetPrices()
        {
            string section = "Price";
            int count = _iniManager.GetInt(section, "Count", 0);
            var lst = new List<Price>();
            for (int i = 0; i < count; i++)
            {
                string[] data = _iniManager.GetString(section, "_" + i).Split('|');
                if (data.Length == 3 && float.TryParse(data[2],out float unitPrice))
                {
                    var fl = new Price()
                    {
                        MaterialCode = data[0],
                        Season = data[1],
                        UnitPrice = unitPrice,
                    };
                    lst.Add(fl);
                }
            }
            return lst;
        }
    }
}
