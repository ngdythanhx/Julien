using Julian.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML;
using System.IO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using DocumentFormat.OpenXml.Bibliography;
namespace HSV
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private void LoadForm(Form frm, string text)
        {
            string tabPageName = "tp" + frm.Name.Substring(3);

            if (!tcMain.TabPages.ContainsKey(tabPageName))
            {
                var tab = new TabPage(frm.Text)
                {
                    Name = tabPageName,
                    Text = text
                };
                tcMain.TabPages.Add(tab);
                Panel pnlScroll = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = SystemColors.Window,
                    AutoScroll = true
                };

                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Width = tab.Width;
                frm.Height = tab.Height;
                //frm.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                frm.Dock = DockStyle.Fill;
                frm.Location = new Point(0, 0);
                frm.AutoScroll = false;
                pnlScroll.Controls.Add(frm);
                tab.Controls.Add(pnlScroll);
                frm.Show();
            }

            tcMain.SelectedTab = tcMain.TabPages[tabPageName];
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadForm(new frmINV(), "INV-PKL");
            LoadForm(new frmLstGuiDiem(), "LIST GỬI DIỄM");
            LoadForm(new frmCS(), "CS");
            LoadForm(new frmSQ(), "SQ");
            LoadForm(new frmASN(), "ASN");
            tcMain.SelectedIndex = 0;
        }
        IWebDriver driver = null;
        private void button1_Click(object sender, EventArgs e)
        {


            ChromeOptions options = new ChromeOptions();

            driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl("http://scm.hsvina.com/rb7/frame/main/main.html");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var frames = driver.FindElements(By.TagName("iframe"));

            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame(0);

            var js = (IJavaScriptExecutor)driver;

            var result = js.ExecuteScript(@"
    return {
        url: location.href,
        auiGrid: typeof AUIGrid,
        element: !!document.querySelector('#grd_popDetList')
    };
");

            Console.WriteLine(
                Newtonsoft.Json.JsonConvert.SerializeObject(result)
            );
            var r = js.ExecuteScript(
                "return AUIGrid.getOrgGridData('#grd_popDetList')"
            );

            if (r == null)
            {
                Console.WriteLine("NULL");
            }
            else
            {
                Console.WriteLine("Type: " + r.GetType().FullName);
                Console.WriteLine("Value: " + r.ToString());
            }
        }
    }
}
