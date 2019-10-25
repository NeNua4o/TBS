using System;
using System.Windows.Forms;
using Common.TBSEventArgs;
using Common.Repositories;
using Common.Models;

namespace Common.Controls
{
    public partial class ActionEditor : UserControl
    {
        RepositoryWorker _worker = null;

        public event EventHandler SelectedActionChanged;

        public Act Action;

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
            if (lb_actions.SelectedIndex < 0) return;
            _worker.DeleteAction((Act)lb_actions.SelectedItem);
            lb_actions.Items.RemoveAt(lb_actions.SelectedIndex);
        }

        private void lb_actions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Action = lb_actions.SelectedIndex == -1 ? null : (Act)lb_actions.SelectedItem;
            SelectedActionChanged?.Invoke(this, new SelectedActionChangedEventArgs()
            {
                Action = this.Action,
            });
        }
    }
}
