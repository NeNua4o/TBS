using Common;
using Common.TBSEventArgs;
using System;
using System.Windows.Forms;

namespace UnitEditor
{
    public partial class UnitEditoMn : Form
    {
        RepositoryWorker _repWkr;
        public UnitEditoMn()
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();
            _repWkr.LoadAll();
            propertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;
            unitEditor.SelectedUnitChanged += UnitEditor_SelectedUnitChanged;
            unitEditor.SetRepWorker(_repWkr);
            actionEditor.SelectedActionChanged += ActionEditor_SelectedActionChanged;
            actionEditor.SetRepWorker(_repWkr);
            effectEditor.SelectedEffectChanged += EffectEditor_SelectedEffectChanged;
            effectEditor.SetRepWorker(_repWkr);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var snd = (PropertyGrid)s;
            snd.Refresh();
            unitEditor.UpdateUnitList();
            actionEditor.UpdateActionList();
            effectEditor.UpdateEffectList();
        }

        private void UnitEditor_SelectedUnitChanged(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = ((SelectedUnitChangedEventArgs)e).BaseUnit;
        }

        private void ActionEditor_SelectedActionChanged(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = ((SelectedActionChangedEventArgs)e).Action;
        }

        private void EffectEditor_SelectedEffectChanged(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = ((SelectedEffectChangedEventArgs)e).Effect;
        }

        private void b_save_Click(object sender, EventArgs e)
        {
            _repWkr.SaveAll();
            MessageBox.Show("Готово", "Сохранение");
        }
    }
}
