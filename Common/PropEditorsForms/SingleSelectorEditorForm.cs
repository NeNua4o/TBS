using Common.Controls;
using Common.Enums;
using Common.Repositories;
using System;
using System.Windows.Forms;

namespace Common.PropEditorsForms
{
    public partial class SingleSelectorEditorForm : Form
    {
        public int SelectedId = -1;

        RepositoryWorker _repWkr;
        public SingleSelectorEditorForm(SelectorTypes selectorType)
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();
            ItemSelector item;
            flp_items.Controls.Clear();
            switch (selectorType)
            {
                case SelectorTypes.Actions:
                    for (int i = 0; i < _repWkr.Actions.Count; i++)
                    {
                        item = new ItemSelector(_repWkr.Actions[i].Id, _repWkr.Actions[i].Icon, _repWkr.Actions[i].Name);
                        item.Click += Item_Click;
                        flp_items.Controls.Add(item);
                    }
                    break;
                case SelectorTypes.Effects:
                    for (int i = 0; i < _repWkr.Effects.Count; i++)
                    {
                        item = new ItemSelector(_repWkr.Effects[i].Id, _repWkr.Effects[i].Icon, _repWkr.Effects[i].Name);
                        item.Click += Item_Click;
                        flp_items.Controls.Add(item);
                    }
                    break;
                default:
                    break;
            }
            item = null;
        }

        public SingleSelectorEditorForm(SelectorTypes selectorType, int[] ids)
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();
            ItemSelector item;
            flp_items.Controls.Clear();
            switch (selectorType)
            {
                case SelectorTypes.ActionsCustom:
                    for (int i = 0; i < ids.Length; i++)
                    {
                        var act = _repWkr.GetAction(ids[i]);
                        item = new ItemSelector(act.Id, act.Icon, act.Name);
                        item.Click += Item_Click;
                        flp_items.Controls.Add(item);
                    }
                    break;
                default:
                    break;
            }
            item = null;
        }

        private void Item_Click(object sender, System.EventArgs e)
        {
            var snd = (ItemSelector)sender;
            SelectedId = snd.Id;
            DialogResult = DialogResult.OK;
        }
    }
}
