using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Spire.Doc;

namespace ThuDamBao_LHG
{
    public partial class Form1 : Form
    {
        private ToolTip _toolTip = new ToolTip();
        private List<string> _lstMatName;
        private List<string> _lstOrderNumber;
        private List<string> _lstArt;
        private List<string> _lstQty;
        private List<string> _lstTesting;
        private List<string> _lstResult;
        private List<string> _lstDRDefectCodes;

        public Form1()
        {
            InitializeComponent();
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            var a = txtInput.Text;
            List<LAB> list = new List<LAB>();

            string text = a;

            var rawLines = text
                .Split(new[] { "\r\n", "\n" },
                StringSplitOptions.RemoveEmptyEntries);

            List<string> lines = new List<string>();

            StringBuilder current = new StringBuilder();

            foreach (var line in rawLines)
            {
                if (Regex.IsMatch(line, @"^\d+\w*\t"))
                {
                    if (current.Length > 0)
                    {
                        lines.Add(current.ToString());
                        current.Clear();
                    }
                }

                current.Append(line);
            }

            if (current.Length > 0)
                lines.Add(current.ToString());

            foreach (var line in lines.Skip(1))
            {
                var cols = line.Split('\t');

                if (cols.Length < 13)
                    continue;

                LAB item = new LAB();

                item.LabNo = cols[0];
                item.Supplier = cols[1];
                item.MatName = cols[2];
                item.MaterialSpecification = cols[3];
                item.Qty = cols[4];
                item.ShipmentNumber = cols[5];
                item.MaterialID = cols[6];
                item.Art = cols[7];
                item.PartGroup = cols[8];

                // lấy 4 cột cuối
                item.RejectReason = cols[cols.Length - 4];
                item.StandardCP = cols[cols.Length - 3];
                item.TestResult = cols[cols.Length - 2];
                item.DRDefectCodes = cols[cols.Length - 1];

                list.Add(item);
            }
            _lstMatName = list.Select(x => x.MatName).ToList();
            txtMaterial.Text = string.Join("; ", _lstMatName);

            _lstOrderNumber = list.Select(x =>
            {
                var lst = x.ShipmentNumber.Split(' ');
                return string.Join(",", lst.Where(y => !string.IsNullOrWhiteSpace(y)).ToList());

            }).ToList();
            //_lstOrderNumber = list.Select(x => x.ShipmentNumber.Replace("  ", ",")).ToList();
            txtOrderNumber.Text = string.Join("; ", _lstOrderNumber);

            _lstArt = list.Select(x =>
            {
                var lst = x.Art.Split(';');
                return string.Join(",", lst.Where(y => !string.IsNullOrWhiteSpace(y)).ToList());

            }).ToList();
            txtArt.Text = string.Join("; ", _lstArt);

            _lstQty = list.Select(x => x.Qty + "Y").ToList();
            txtQty.Text = string.Join("; ", _lstQty);

            _lstTesting = list.Select(x => x.RejectReason).ToList();
            txtTesting.Text = string.Join("; ", _lstTesting);

            _lstResult = list.Select(x => x.TestResult).ToList();
            txtResult.Text = string.Join("; ", _lstResult);

            _lstDRDefectCodes = list.Select(x => x.DRDefectCodes.Replace("\r", "").Replace("\n", "")).ToList();
            txtDRDefectCode.Text = string.Join("; ", _lstDRDefectCodes);
        }

        private void txt_DoubleClick(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            string text = txt.Text;
            txt.SelectAll();
            if (string.IsNullOrWhiteSpace(text))
                return;

            Clipboard.SetText(text);
            _toolTip.Show("Đã copy", txt, txt.Width, 0, 1500);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;
            Document doc = new Document();
            try
            {
                using (var fs = new FileStream("template.docx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    doc.LoadFromStream(fs, FileFormat.Docx);
                    doc.Replace("{MatName}", string.Join("; ", _lstMatName), true, true);
                    doc.Replace("{OrderNumber}", string.Join("; ", _lstOrderNumber), true, true);
                    doc.Replace("{ART}", string.Join("; ", _lstArt), true, true);
                    doc.Replace("{Qty}", string.Join("; ", _lstQty), true, true);
                    doc.Replace("{Testing}", string.Join("; ", _lstTesting), true, true);
                    doc.Replace("{Result}", string.Join("; ", _lstResult), true, true);
                    doc.Replace("{DRDefectCode}", string.Join("; ", _lstDRDefectCodes), true, true);
                    doc.Replace("{Date}", string.Join("; ", DateTime.Now.ToString("yyyy-MM-dd")), true, true);

                    //string path = Path.Combine(Directory.GetCurrentDirectory(), txtFileName.Text.Trim()+ ".docx");
                    string path = Path.Combine(@"\\VP_JULIEN\Kinh doanh 業務\20.報價-保證書 BÁO GIÁ-THƯ ĐẢM BẢO\保證書(THƯ ĐẢM BẢO)\LHG", txtFileName.Text.Trim() + ".docx");
                    if (File.Exists(path))
                    {
                        if (MessageBox.Show("File đã tồn tại, bạn có muốn ghi đè", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    doc.SaveToFile(path);
                    doc.SaveToFile(path.Replace(".docx",".pdf"),FileFormat.PDF);
                    if (File.Exists(path))
                    {
                        MessageBox.Show("Tạo thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra!");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message);
            }
            finally
            {
                doc.Dispose();
                btnCreate.Enabled = true;
            }
        }
    }
}
