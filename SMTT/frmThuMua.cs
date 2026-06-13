using ClosedXML.Excel;
using System;
using System.Collections.Concurrent;
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
    public partial class frmThuMua : Form
    {
        IniManager _iniManager = new IniManager(Path.Combine(Directory.GetCurrentDirectory(), "config.ini"));
        List<string> _lstThuMua = new List<string>();
        string _folderThuMua = "";
        public frmThuMua()
        {
            InitializeComponent();
        }

        private void frmThuMua_Load(object sender, EventArgs e)
        {
            _folderThuMua = _iniManager.GetString("ThuMuaData", "FolderThuMua", @"\\VP_JULIEN\thu mua 採購");
            _folderThuMua = Encoding.UTF8.GetString(Encoding.Default.GetBytes(_folderThuMua));
            int count = _iniManager.GetInt("ThuMuaData", "Count", 0);
            for (int i = 0; i < count; i++)
            {
                string file = _iniManager.GetString("ThuMuaData", "_" + i);
                file = Encoding.UTF8.GetString(Encoding.Default.GetBytes(file));
                if (!string.IsNullOrEmpty(file) && !_lstThuMua.Contains(file))
                {
                    _lstThuMua.Add(file);
                    checkedListBox1.Items.Add(file);
                }
            }
        }

        private async void btnLoadThuMuaData_Click(object sender, EventArgs e)
        {
            btnLoadThuMuaData.Enabled = false;
            checkedListBox1.Enabled = false;
            try
            {
                if (!Directory.Exists(_folderThuMua))
                {
                    MessageBox.Show("Đường dẫn không tồn tại:\n" + _folderThuMua);
                    return;
                }
                foreach (string file in _lstThuMua)
                {
                    if (!File.Exists(Path.Combine(_folderThuMua, file)))
                    {
                        MessageBox.Show("File không tồn tại:\n" + _folderThuMua);
                        return;
                    }
                }
                await Task.Yield();
                var lstTask1 = new List<Task<bool>>();

                ConcurrentBag<XLWorkbook> lstWorkbook = new();
                foreach (string file in _lstThuMua)
                {
                    lstTask1.Add(Task.Run(() =>
                    {
                        try
                        {
                            using var fs = new FileStream(Path.Combine(_folderThuMua, file), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            lstWorkbook.Add(new XLWorkbook(fs));
                            return true;
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }
                    }));
                }
                var lstResult = await Task.WhenAll(lstTask1);
                var lstLoadErr = new List<string>();
                for (int i = 0; i < lstResult.Length; i++)
                {
                    if (lstResult[i] == false)
                    {
                        lstLoadErr.Add(_lstThuMua[i]);
                    }
                }
                if (lstLoadErr.Count > 0)
                {
                    MessageBox.Show("Lỗi load file:\n" + string.Join("\n", lstLoadErr));
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi hệ thống");
                return;
            }
            finally
            {
                btnLoadThuMuaData.Enabled = true;
                checkedListBox1.Enabled = true;
            }
        }
    }
}
