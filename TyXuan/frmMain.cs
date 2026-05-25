using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TyXuan
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //var dt = extractor.Extract(@"C:\Users\Admin\Desktop\TyXuan\20260402248 JULIEN.pdf");
            var extractor = new Extractor();

            string pdfPath = @"C:\Users\Admin\Desktop\TyXuan\20260402248 JULIEN.pdf";

            DataTable dt = extractor.Run(pdfPath);

            // 🔥 In ra console
            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(
                    $"{row["Item"]} | {row["MatNo"]} | {row["Name"]} | {row["Qty"]}");
            }

            Console.WriteLine("DONE");

        }
    }
}
