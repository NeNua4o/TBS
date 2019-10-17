﻿using Common.TBSEventArgs;
using System;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class UnitEditor : UserControl
    {
        RepositoryWorker _worker = null;

        public event EventHandler SelectedUnitChanged;

        public BaseUnit BaseUnit;

        public UnitEditor()
        {
            InitializeComponent();
        }

        public void SetRepWorker(RepositoryWorker worker)
        {
            this._worker = worker;
            UpdateUnitList();
        }

        public void UpdateUnitList()
        {
            lb_units.Items.Clear();
            for (int i = 0; i < _worker.BaseUnits.Count; i++)
                lb_units.Items.Add(_worker.BaseUnits[i]);
        }

        private void b_add_Click(object sender, EventArgs e)
        {
            if (_worker == null) return;
            BaseUnit unit = _worker.CreateUnit();
            lb_units.Items.Add(unit);
        }

        private void b_del_Click(object sender, EventArgs e)
        {
            if (_worker == null) return;
            var snd = (ListBox)sender;
            if (snd.SelectedIndex < 0) return;
            var ind = snd.SelectedIndex;
            _worker.DeleteUnit((BaseUnit)snd.SelectedItem);
            snd.Items.RemoveAt(ind);
        }

        private void lb_units_SelectedIndexChanged(object sender, EventArgs e)
        {
            BaseUnit = lb_units.SelectedIndex == -1 ? null : (BaseUnit)lb_units.SelectedItem;
            SelectedUnitChanged?.Invoke(this, new SelectedUnitChangedEventArgs()
            {
                BaseUnit = this.BaseUnit,
            });
        }
    }
}