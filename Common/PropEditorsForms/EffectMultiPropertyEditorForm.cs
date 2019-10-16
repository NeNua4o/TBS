using Common.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;

namespace Common.PropEditorsForms
{
    public partial class EffectMultiPropertyEditorForm : Form
    {
        public List<int> Ids = new List<int>();

        RepositoryWorker _repWkr;

        public EffectMultiPropertyEditorForm(object value)
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();

            if (value != null)
            {
                var obj = (int[])value;
                for (int i = 0; i < obj.Length; i++)
                    Ids.Add(obj[i]);
            }

            flp_items.Controls.Clear();
            ItemMultiSelector item;
            for (int i = 0; i < _repWkr.Effects.Count; i++)
            {
                item = new ItemMultiSelector(_repWkr.Effects[i].Id, _repWkr.Effects[i].Icon, _repWkr.Effects[i].Name, Ids.Any(x=>x== _repWkr.Effects[i].Id));
                item.ItemMultiSelectorCBChanged += Item_ItemMultiSelectorCBChanged;
                flp_items.Controls.Add(item);
            }
            item = null;
        }

        private void Item_ItemMultiSelectorCBChanged(object sender, EventArgs e)
        {
            var snd = (ItemMultiSelector)sender;
            if (snd.Selected) Ids.Add(snd.Id);
            else Ids.Remove(snd.Id);
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
