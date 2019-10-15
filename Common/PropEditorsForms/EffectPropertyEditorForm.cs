using Common.Controls;
using System.Windows.Forms;

namespace Common.PropEditorsForms
{
    public partial class EffectPropertyEditorForm : Form
    {
        public int SelectedId = -1;

        RepositoryWorker _repWkr;
        public EffectPropertyEditorForm()
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();

            flp_items.Controls.Clear();
            ItemSelector item;
            for (int i = 0; i < _repWkr.Effects.Count; i++)
            {
                item = new ItemSelector(_repWkr.Effects[i].Id, _repWkr.Effects[i].Icon, _repWkr.Effects[i].Name);
                item.Click += Item_Click;
                flp_items.Controls.Add(item);
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
