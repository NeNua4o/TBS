using Common.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            var obj = (int[])value;
            for (int i = 0; i < obj.Length; i++)
                Ids.Add(obj[i]);

            flp_items.Controls.Clear();
            ItemMultiSelector item;
            for (int i = 0; i < _repWkr.Effects.Count; i++)
            {
                item = new ItemMultiSelector(_repWkr.Effects[i].Id, _repWkr.Effects[i].Icon, _repWkr.Effects[i].Name);
                item.Click += Item_Click;
                flp_items.Controls.Add(item);
            }
            item = null;
        }
    }
}
