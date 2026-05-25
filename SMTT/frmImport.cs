using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMTT
{
    public partial class frmImport : Form
    {
        private List<Import> _lstImport = new List<Import>();
        public frmImport()
        {
            InitializeComponent();
        }

        private void frmImport_Shown(object sender, EventArgs e)
        {
            var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Import"), "*.ini");
            foreach (var file in files)
            {
                var ini = new IniManager(file);
                var import_source = new Import_Source
                (
                                        ini.GetString("Source", "DelieryInvoice"),
                    ini.GetString("Source", "Path"),
                    ini.GetString("Source", "Sheet"),
                    ini.GetInt("Source", "StartIndex"),
                    ini.GetString("Source", "PONhuom"),
                    ini.GetString("Source", "MaterialCode"),
                    ini.GetString("Source", "DeliveryDate"),
                    ini.GetString("Source", "POCustomer"),
                    ini.GetString("Source", "ItemCode"),
                    ini.GetString("Source", "Qty"),
                    ini.GetString("Source", "DelieryInvoice"),
                    ini.GetString("Source", "DelieryInvoice")
                );
                var import_detination = new Import_Detination
                (
                    ini.GetString("Source", "Path"),
                    ini.GetString("Source", "Sheet"),
                    ini.GetString("Source", "TableName")
                );
                _lstImport.Add(new Import(import_source,import_detination,new Dictionary<string, Material>(),new Dictionary<string, Customer>()));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

        }
    }
}
