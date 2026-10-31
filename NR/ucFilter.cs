using DocumentFormat.OpenXml.Office2010.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Julian_Server
{
    public partial class ucFilter : UserControl
    {
        List<string> filters = new List<string>();
        Dictionary<string, bool> _dictSource = new Dictionary<string, bool>();
        public ucFilter()
        {
            InitializeComponent();
            var a = _dictSource.Keys.ToArray();
            var b = a.Where(x => x == "");
        }
        public void SetDataSource(List<string> list)
        {
            if (list != null && list.Count > 0)
            {
                _dictSource.Clear();
                lsbData.DataSource = list;
                for (int i = 0; i < list.Count; i++)
                {
                    lsbData.SetItemChecked(i, true);
                    _dictSource[list[i]] = true;
                }
                chkAll.Checked = true;
            }
        }
        public void SetDataSource(List<string> list, bool check)
        {
            if (list != null && list.Count > 0)
            {
                _dictSource.Clear();
                lsbData.DataSource = list;
                for (int i = 0; i < list.Count; i++)
                {
                    lsbData.SetItemChecked(i, check);
                    _dictSource[list[i]] = check;
                }
                chkAll.Checked = check;
            }
        }
        public void ClearDataSource()
        {

            _dictSource.Clear();
            lsbData.DataSource = null;
            chkAll.Checked = false;
        }
        public bool CheckedChangeItem(string itemText)
        {
            if (_dictSource.ContainsKey(itemText))
            {
                _dictSource[itemText] = !_dictSource[itemText];
                return true;
            }
            return false;
        }
        public bool CheckedItem(string itemText, bool itemChecked)
        {
            if (_dictSource.ContainsKey(itemText))
            {
                _dictSource[itemText] = itemChecked;
                return true;
            }
            return false;
        }
        public List<string> GetItemsChecked()
        {
            return _dictSource.Where(x => x.Value == true).Select(x => x.Key).ToList();
        }

        /*private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            var item = _dictSource.Keys.First(x => x.Contains(txtFilter.Text));
            if (item==null || string.IsNullOrEmpty(item))
            {
                return;
            }
            int index = lsbData.Items.IndexOf(item);
            if (index >= 0)
            {   
                lsbData.SelectedIndex = index;
                lsbData.TopIndex = index;
            }
            /*if (String.IsNullOrWhiteSpace(txtFilter.Text))
            {
                lsbData.DataSource = _dictSource.Keys.ToList();
                foreach (string text in _dictSource.Keys.ToList())
                {
                    lsbData.SetItemChecked(lsbData.Items.IndexOf(text), _dictSource[text]);
                }
            }
            else
            {
                var filtered = _dictSource.Keys
                    .Where(x => x.ToUpper().Contains(txtFilter.Text.ToUpper()))
                    .ToList();
                lsbData.DataSource = filtered;
                foreach (string text in filtered)
                {
                    lsbData.SetItemChecked(lsbData.Items.IndexOf(text), _dictSource[text]);
                }
            }
        }*/
        private bool _lockItemCheck = false;
        private bool _lockCheckAll = false;
        private void lsbData_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_lockItemCheck) return;

            var lsb = (CheckedListBox)sender;
            _dictSource[lsb.Items[e.Index].ToString()] = (e.NewValue == CheckState.Checked);

            this.BeginInvoke(new Action(() =>
            {
                bool allChecked = true;

                for (int i = 0; i < lsb.Items.Count; i++)
                {
                    if (!lsb.GetItemChecked(i))
                    {
                        allChecked = false;
                        break;
                    }
                }

                _lockCheckAll = true;
                chkAll.Checked = allChecked;
                _lockCheckAll = false;
            }));
            ItemCheckedChange?.Invoke();
        }
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_lockCheckAll)
                return;
            _lockItemCheck = true;

            for (int i = 0; i < lsbData.Items.Count; i++)
            {
                _dictSource[lsbData.Items[i].ToString()] = chkAll.Checked;
                lsbData.SetItemChecked(i, chkAll.Checked);
            }
            _lockItemCheck = false;
            ItemCheckedChange?.Invoke();
        }
        public event Action ItemCheckedChange;

        private void txtFilter_Leave(object sender, EventArgs e)
        {

        }
    }
}
