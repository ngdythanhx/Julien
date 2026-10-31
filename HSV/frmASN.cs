using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V154.WebAuthn;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HSV
{
    public partial class frmASN : Form
    {
        IWebDriver driver = null;
        public frmASN()
        {
            InitializeComponent();

        }

        private void btnOpenSCM_Click(object sender, EventArgs e)
        {
            ChromeOptions options = new ChromeOptions();
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://scm.hsvina.com/rb7/");
        }

        private void frmASN_Load(object sender, EventArgs e)
        {
            dgvMain.AutoGenerateColumns = false;
            checkBox1.Checked = false;
            dgvMain.Columns["col_Article"].Visible = false;
        }
        public class DeliveryDetail
        {
            public object apiyn { get; set; }
            public string article { get; set; }
            public string articleShort => !string.IsNullOrEmpty(article) && article.Length >= 6 ? article.Substring(article.Length - 6, 6) : "";
            public string asnNo { get; set; }
            public decimal asnQty { get; set; }
            public string buyMonth { get; set; }
            public string factory { get; set; }
            public string grpKey { get; set; }
            public string itemCd { get; set; }
            public string itemNm { get; set; }
            public string itemType { get; set; }
            public decimal loadQty { get; set; }
            public decimal orderQty { get; set; }
            public string owner { get; set; }
            public string ownerNm { get; set; }
            public string pantone { get; set; }
            public string poType { get; set; }
            public string qtyUnit { get; set; }
            public decimal reMainQty { get; set; }
            public int serialkey { get; set; }
            public string supplier { get; set; }
            public string supplierNm { get; set; }
            public object udf2ItemType { get; set; }
        }
        public class Tem
        {
            public object ox { get; set; }
            public string grade { get; set; }
            public string grpKey { get; set; }
            public string itemCd { get; set; }
            public int keyVal { get; set; }
            public decimal qty { get; set; }
            public object refkey { get; set; }
            public string serialkey { get; set; }
        }
        private void btnGetTemCount_Click(object sender, EventArgs e)
        {
            btnGetTemCount.Enabled = false;
            dgvMain.DataSource = null;
            try
            {
                if (driver == null)
                    return;
                driver.SwitchTo().DefaultContent();

                driver.SwitchTo().Frame(0);
                var js = (IJavaScriptExecutor)driver;
                var l1 = js.ExecuteScript("return JSON.stringify(AUIGrid.getOrgGridData('#grd_popList'))").ToString();
                var l2 = js.ExecuteScript("return JSON.stringify(AUIGrid.getOrgGridData('#grd_popDetList'))").ToString();
                if (l1 == null || l2 == null)
                {
                    MessageBox.Show("Dữ liệu chưa sẵn sàng!");
                    return;
                }
                List<DeliveryDetail> lst1 = JsonConvert.DeserializeObject<List<DeliveryDetail>>(l1);
                List<Tem> lst2 = JsonConvert.DeserializeObject<List<Tem>>(l2);
                var dt = new DataTable();
                dt.Columns.Add("MaterialCode", typeof(string));
                dt.Columns.Add("TemCount", typeof(decimal));
                if (checkBox1.Checked)
                {
                    dgvMain.Columns["col_Article"].Visible = true;
                    dt.Columns.Add("Article", typeof(string));
                    var lstG = lst1.GroupBy(x => new { x.itemCd, x.articleShort }).OrderBy(x => x.Key.itemCd).ThenBy(x => x.Key.articleShort).ToList();
                    foreach (var item in lstG)
                    {
                        var materialCode = item.Key.itemCd;
                        var article = item.Key.articleShort;
                        var temCount = item.Sum(y => lst2.Count(z => z.grpKey == y.grpKey));
                        dt.Rows.Add(materialCode, temCount, article);
                    }

                    /*var lstG = lst1.GroupBy(x => new { x.itemCd, x.articleShort }).Select(x =>
                    {
                        var a = x.First();
                        return new
                        {
                            MaterialCode = x.Key.itemCd,
                            Article = x.Key.articleShort,
                            TemCount = x.Sum(y=>lst2.Count(z=>z.grpKey==y.grpKey))
                        };
                    });*/
                }
                else
                {
                    dgvMain.Columns["col_Article"].Visible = false;
                    var lstG = lst2.GroupBy(x => x.itemCd).OrderBy(x => x.Key).ToList();
                    foreach (var item in lstG)
                    {
                        var materialCode = item.Key;
                        var temCount = item.Count();
                        dt.Rows.Add(materialCode, temCount);
                    }
                }
                dgvMain.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống!");
            }
            finally
            {
                btnGetTemCount.Enabled = true;
            }
        }
    }
}
