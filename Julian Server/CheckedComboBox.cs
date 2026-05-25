using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

public class CheckedComboBox : UserControl
{
    private TextBox txtDisplay;
    private Button btnDrop;
    private Form dropDownForm;
    private CheckedListBox clb;
    private TextBox txtSearch;
    private CheckBox chkAll;

    private List<object> _data = new List<object>();

    public string DisplayMember { get; set; }
    public string ValueMember { get; set; }

    public event EventHandler SelectedChanged;

    public CheckedComboBox()
    {
        Height = 26;

        txtDisplay = new TextBox
        {
            ReadOnly = true,
            Dock = DockStyle.Fill
        };

        btnDrop = new Button
        {
            Text = "▼",
            Dock = DockStyle.Right,
            Width = 25
        };

        btnDrop.Click += BtnDrop_Click;

        Controls.Add(txtDisplay);
        Controls.Add(btnDrop);

        InitDropdown();
    }

    private void InitDropdown()
    {
        dropDownForm = new Form
        {
            FormBorderStyle = FormBorderStyle.None,
            StartPosition = FormStartPosition.Manual,
            ShowInTaskbar = false,
            Size = new Size(250, 300)
        };

        dropDownForm.Deactivate += (s, e) => dropDownForm.Hide();

        txtSearch = new TextBox
        {
            Dock = DockStyle.Top,
            //PlaceholderText = "Search..."
        };

        chkAll = new CheckBox
        {
            Text = "Select All",
            Dock = DockStyle.Top
        };

        clb = new CheckedListBox
        {
            Dock = DockStyle.Fill,
            CheckOnClick = true
        };

        txtSearch.TextChanged += TxtSearch_TextChanged;
        chkAll.CheckedChanged += ChkAll_CheckedChanged;
        clb.ItemCheck += Clb_ItemCheck;

        dropDownForm.Controls.Add(clb);
        dropDownForm.Controls.Add(chkAll);
        dropDownForm.Controls.Add(txtSearch);
    }

    // ================= UI =================

    private void BtnDrop_Click(object sender, EventArgs e)
    {
        var location = Parent.PointToScreen(Location);
        dropDownForm.Location = new Point(location.X, location.Y + Height);

        dropDownForm.Show();
        dropDownForm.BringToFront();
    }

    // ================= Search =================

    private void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        string keyword = txtSearch.Text.ToLower();

        clb.Items.Clear();

        foreach (var item in _data)
        {
            string text = GetDisplay(item);

            if (text.ToLower().Contains(keyword))
            {
                clb.Items.Add(item, IsChecked(item));
            }
        }
    }

    // ================= Select All =================

    private void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < clb.Items.Count; i++)
        {
            clb.SetItemChecked(i, chkAll.Checked);
        }
    }

    // ================= Item Check =================

    private void Clb_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        BeginInvoke((MethodInvoker)(() =>
        {
            UpdateText();
            SelectedChanged?.Invoke(this, EventArgs.Empty);
        }));
    }

    // ================= Helpers =================

    private string GetDisplay(object item)
    {
        if (DisplayMember == null) return item.ToString();

        return item.GetType().GetProperty(DisplayMember)?.GetValue(item)?.ToString();
    }

    private object GetValue(object item)
    {
        if (ValueMember == null) return item;

        return item.GetType().GetProperty(ValueMember)?.GetValue(item);
    }

    private bool IsChecked(object item)
    {
        return clb.CheckedItems.Cast<object>().Any(x => GetValue(x).Equals(GetValue(item)));
    }

    private void UpdateText()
    {
        int count = clb.CheckedItems.Count;

        if (count == 0)
            txtDisplay.Text = "";
        else if (count <= 3)
            txtDisplay.Text = string.Join(", ", clb.CheckedItems.Cast<object>().Select(GetDisplay));
        else
            txtDisplay.Text = $"{count} items selected";
    }

    // ================= Public API =================

    public void SetData<T>(List<T> data)
    {
        _data = data.Cast<object>().ToList();
        ReloadList();
    }

    private void ReloadList()
    {
        clb.Items.Clear();

        foreach (var item in _data)
        {
            clb.Items.Add(item);
        }
    }

    public List<object> GetSelectedValues()
    {
        return clb.CheckedItems.Cast<object>().Select(GetValue).ToList();
    }

    public List<object> GetSelectedItems()
    {
        return clb.CheckedItems.Cast<object>().ToList();
    }

    public void Clear()
    {
        clb.Items.Clear();
        _data.Clear();
        txtDisplay.Clear();
    }
}