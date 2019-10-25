using Common.Models;
using Common.Repositories;
using Common.TBSEventArgs;
using System;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class EffectEditor : UserControl
    {
        RepositoryWorker _worker = null;

        public event EventHandler SelectedEffectChanged;

        public Effect Effect;

        public EffectEditor()
        {
            InitializeComponent();
        }

        public void SetRepWorker(RepositoryWorker worker)
        {
            this._worker = worker;
            UpdateEffectList();
        }

        public void UpdateEffectList()
        {
            lb_effects.Items.Clear();
            for (int i = 0; i < _worker.Effects.Count; i++)
                lb_effects.Items.Add(_worker.Effects[i]);
        }

        private void b_add_Click(object sender, EventArgs e)
        {
            if (_worker == null) return;
            Effect effect = _worker.CreateEffect();
            lb_effects.Items.Add(effect);
        }

        private void b_del_Click(object sender, EventArgs e)
        {
            if (_worker == null) return;
            if (lb_effects.SelectedIndex < 0) return;
            _worker.DeleteEffect((Effect)lb_effects.SelectedItem);
            lb_effects.Items.RemoveAt(lb_effects.SelectedIndex);
        }

        private void lb_units_SelectedIndexChanged(object sender, EventArgs e)
        {
            Effect = lb_effects.SelectedIndex == -1 ? null : (Effect)lb_effects.SelectedItem;
            SelectedEffectChanged?.Invoke(this, new SelectedEffectChangedEventArgs()
            {
                Effect = this.Effect,
            });
        }
    }
}
