using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Julian;
using Julian.Enums;
using Julian.Forms;
using Julian.Utils;
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

namespace Julian_Server
{
    public partial class frmMain : Form
    {
        Dictionary<string, IFormHandler> FormHanlers = new Dictionary<string, IFormHandler>();
        private const int _defaultHeight = 545;
        public frmMain()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1200, 800);
            //this.WindowState = FormWindowState.Maximized;
            FormHanlers["Employee"] = new frmEmployee();
            FormHanlers["EmployeeTask"] = new frmEmployeeTask();
            FormHanlers["Customer"] = new frmCustomer();
            FormHanlers["Material"] = new frmMaterial();
        }
        private void LoadForm(Form frm)
        {
            string tabPageName = "tp" + frm.Name.Substring(3);

            if (!tcMain.TabPages.ContainsKey(tabPageName))
            {
                var tab = new TabPage(frm.Text)
                {
                    Name = tabPageName,
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
            IniManager iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
            Config.Instance.ConnectionString = iniManager.GetString("Default", "ConnectionString");
            tsDatabase.Enabled = false;

            //tsEmployeeTask.PerformClick();
        }


        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            frmAddOrder frmAddOrder = new frmAddOrder();
            frmAddOrder.StartPosition = FormStartPosition.CenterParent;
            frmAddOrder.ShowDialog();
        }

        private void EnabledActionsButton(bool type1, bool type2)
        {
            tsCreateNew.Enabled = type1;
            tsEdit.Enabled = type1;
            tsDelete.Enabled = type1;
            tsSave.Enabled = type2;
            tsCancel.Enabled = type2;
        }
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            if (frm.ActionType == 0)
            {
                EnabledActionsButton(true, false);
            }
            else
            {
                EnabledActionsButton(false, true);
            }
        }
        private void tsEmployee_Click(object sender, EventArgs e)
        {
            var frm = (frmEmployee)FormHanlers["Employee"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }

        private void tsEmployeeTask_Click(object sender, EventArgs e)
        {
            var frm = (frmEmployeeTask)FormHanlers["EmployeeTask"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }
        private void tsCustomer_Click(object sender, EventArgs e)
        {
            var frm = (frmCustomer)FormHanlers["Customer"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }
        private void tsMaterial_Click(object sender, EventArgs e)
        {
            var frm = (frmMaterial)FormHanlers["Material"];
            LoadForm(frm);
            if (frm.ActionType == 0)
            {
                frm.LoadData();
            }
        }

        private void tsOrder_Click(object sender, EventArgs e)
        {

        }
        private void tsReload_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.LoadData();
            frm.LockInputs();
            frm.ActionType = 0;
        }
        private void tsCreateNew_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(false, true);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.UnlockInputs();
            frm.ResetInputs();
            frm.ActionType = 1;
        }

        private void tsEdit_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(false, true);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.UnlockInputs();
            frm.ActionType = 2;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.DeleteData();
        }
        private async void tsSave_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            if (frm.ActionType == 1)
            {
                if (await frm.CreateData())
                {
                    frm.ActionType = 0;
                    EnabledActionsButton(true, false);
                }
                else
                {

                }
            }
            else if (frm.ActionType == 2)
            {
                if (await frm.UpdateData())
                {
                    frm.ActionType = 0;
                    EnabledActionsButton(true, false);
                }
                else
                {

                }
            }
        }
        private void tsCancel_Click(object sender, EventArgs e)
        {
            var tabPage = tcMain.SelectedTab;
            if (tabPage == null) return;
            EnabledActionsButton(true, false);
            var frm = FormHanlers[tabPage.Name.Substring(2)];
            frm.LockInputs();
            frm.LoadData();
            frm.ActionType = 0;
        }


        private void tsGroupByPO_Click(object sender, EventArgs e)
        {
            frmGroupByPO frm = new frmGroupByPO();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void tsAddOrder_Click(object sender, EventArgs e)
        {
            frmAddOrder frm = new frmAddOrder();
            frm.ShowDialog();
            frm?.Dispose();
        }
        private void tsCompare_Click(object sender, EventArgs e)
        {
            frmCompare frm = new frmCompare();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void tsHoiHang_Click(object sender, EventArgs e)
        {
            Julian.Forms.frmHoiHang2 frm = new Julian.Forms.frmHoiHang2();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void tsBaoCaoSanLuong_Click(object sender, EventArgs e)
        {
            frmBaoCaoSanLuong frm = new frmBaoCaoSanLuong();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void tsThemVaiMoc_Click(object sender, EventArgs e)
        {
            frmThemVaiMoc frm = new frmThemVaiMoc();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void tsBaoCaoTienDo_Click(object sender, EventArgs e)
        {
            frmBaoCaoTienDo frm = new frmBaoCaoTienDo();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void tsSHC_Click(object sender, EventArgs e)
        {
            frmThoaBeSHC frm = new frmThoaBeSHC();
            frm.ShowDialog();
            frm?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*using (PdfReader reader = new PdfReader(@"C:\Users\Admin\Desktop\Cong viec\KhachOrder.pdf"))
            using (PdfDocument pdf = new PdfDocument(reader))
            {
                List<string> lstText = new List<string>();
                for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                {
                    string text = PdfTextExtractor.GetTextFromPage(pdf.GetPage(i));
                    lstText.Add(text);
                    Console.WriteLine(text);
                }
                var result = string.Join("\r\n", lstText);
            }*/
            /*using (var pdf = new PdfDocument(new PdfReader(@"C:\Users\Admin\Desktop\Cong viec\KhachOrder.pdf")))
            {
                for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                {
                    Console.WriteLine($"===== PAGE {i} =====");

                    var strategy = new TabExtractionStrategy(); // class bạn đã viết
                    var parser = new PdfCanvasProcessor(strategy);

                    parser.ProcessPageContent(pdf.GetPage(i));

                    string pageText = strategy.GetResult();

                    Console.WriteLine(pageText);
                }
            }*/
            test();
        }
        private void test()
        {

            List<string> lines = new List<string>();

            using (var pdf = new PdfDocument(new PdfReader(@"C:\Users\Admin\Desktop\Cong viec\KhachOrder.pdf")))
            {
                for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
                {
                    var text = PdfTextExtractor.GetTextFromPage(pdf.GetPage(i));
                    lines.AddRange(text.Split('\n'));
                }
            }
            var rows = new List<string>();
            StringBuilder current = new StringBuilder();

            foreach (var line in lines)
            {
                var clean = line.Trim();
                if (string.IsNullOrEmpty(clean)) continue;

                current.Append(" " + clean);

                // dòng có Unit + Qty => kết thúc row
                if (System.Text.RegularExpressions.Regex.IsMatch(clean, @"\b[A-Z]{2,5}\s+\d+(\.\d+)?\b"))
                {
                    rows.Add(current.ToString().Trim());
                    current.Clear();
                }
            }
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Item");
            dt.Columns.Add("MatNo");
            dt.Columns.Add("MLM");
            dt.Columns.Add("Name");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Date");
            dt.Columns.Add("Remark");
            foreach (var row in rows)
            {
                var match = System.Text.RegularExpressions.Regex.Match(row,
                    @"^(?<item>\d+)\s+(?<matno>\S+)\s+(?<mlm>\S+)\s+(?<name>.+?)\s+(?<unit>[A-Z]+)\s+(?<qty>\d+(\.\d+)?)\s+(?<date>\d{2}/\d{2})\s*(?<remark>.*)");

                if (match.Success)
                {
                    dt.Rows.Add(
                        match.Groups["item"].Value,
                        match.Groups["matno"].Value,
                        match.Groups["mlm"].Value,
                        match.Groups["name"].Value,
                        match.Groups["unit"].Value,
                        match.Groups["qty"].Value,
                        match.Groups["date"].Value,
                        match.Groups["remark"].Value
                    );
                }
            }

        }
    }
}
