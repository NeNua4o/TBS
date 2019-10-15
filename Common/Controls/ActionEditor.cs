using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TBS;
using Common.TBSEventArgs;

namespace Common.Controls
{
    public partial class ActionEditor : UserControl
    {
        RepositoryWorker _worker = null;

        public event EventHandler SelectedActionChanged;

        public ActionEditor()
        {
            InitializeComponent();
        }

        public void SetRepWorker(RepositoryWorker worker)
        {
            this._worker = worker;
            UpdateActionList();
        }

        public void UpdateActionList()
        {
            lb_actions.Items.Clear();
            for (int i = 0; i < _worker.Actions.Count; i++)
                lb_actions.Items.Add(_worker.Actions[i]);
        }

        private void b_add_Click(object sender, EventArgs e)
        {
            if (_worker == null) return;
            Act action = _worker.CreateAction();
            lb_actions.Items.Add(action);
        }

        private void b_del_Click(object sender, EventArgs e)
        {
            if (_worker == null) return;
            var snd = (ListBox)sender;
            if (snd.SelectedIndex < 0) return;
            var ind = snd.SelectedIndex;
            _worker.DeleteAction((Act)snd.SelectedItem);
            snd.Items.RemoveAt(ind);
        }

        private void lb_actions_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedActionChanged?.Invoke(this, new SelectedActionChangedEventArgs()
            {
                Action = lb_actions.SelectedIndex == -1 ? null : (Act)lb_actions.SelectedItem,
            });
        }
    }
}
