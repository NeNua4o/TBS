using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ItemMultiSelector : UserControl
    {
        public event EventHandler ItemMultiSelectorCBChanged;

        public int Id;
        public bool Selected;

        private bool _initializing;

        public ItemMultiSelector(int id, Image icon, string name, bool selected)
        {
            _initializing = true;
            InitializeComponent();
            Id = id;
            pb_icon.Image = icon;
            cb_selected.Text = name;
            cb_selected.Checked = selected;
            Selected = selected;
            _initializing = false;
        }

        private void cb_selected_CheckedChanged(object sender, EventArgs e)
        {
            if (_initializing) return;
            Selected = cb_selected.Checked;
            ItemMultiSelectorCBChanged?.Invoke(this, null);
        }
    }
}
