using System.Drawing;
using System.Windows.Forms;
using Common.Repositories;
using System;
using Common.Extensions;
using Common.PropEditorsForms;
using Common.Enums;
using System.Diagnostics;

namespace Common.Controls
{
    public partial class CurrentUnit : UserControl
    {
        RepositoryWorker _repWkr;
        Unit _unit;
        bool _iconsSetted;

        public int ActionId = -1;
        public event EventHandler ActionChanged;
        

        public CurrentUnit()
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();
            actionSelectorMain.ActionChanged += ActionSelector_ActionChanged;
            actionSelectorSec.ActionChanged += ActionSelector_ActionChanged;
        }
        
        public void Set(Unit unit)
        {
            if (!_iconsSetted)
            {
                actionSelectorSkills.Init("Icons/z_skl.png");
                actionSelectorSpells.Init("Icons/z_mag.png");
                _iconsSetted = true;
            }

            _unit = unit;
            pb_icon.Image = new Bitmap(unit.Icon, pb_icon.Size);

            Act act = _repWkr.GetAction(unit.MainActId);
            actionSelectorMain.Set(act);

            SetCurrentAction(act);

            act = _repWkr.GetAction(unit.SecondActId);
            actionSelectorSec.Set(act);
        }

        private void SetCurrentAction(int id)
        {
            SetCurrentAction(_repWkr.GetAction(id));
        }

        private void SetCurrentAction(Act act)
        {
            if (act != null)
            {
                pb_curAct.Image = new Bitmap(act.Icon, pb_curAct.Size);
                ActionId = act.Id;
                ActionChanged?.Invoke(this, null);
            }
        }

        private void ActionSelector_ActionChanged(object sender, EventArgs e)
        {
            var snd = (ActionSelector)sender;
            SetCurrentAction(snd.ActionId);
        }

        private void ActionSelectorSkills_Click(object sender, EventArgs e)
        {
            using (SingleSelectorEditorForm actionSelectorForm = new SingleSelectorEditorForm(SelectorTypes.ActionsCustom, _unit.SkillsIds))
            {
                actionSelectorForm.Text = "Выберите навык";
                if (actionSelectorForm.ShowDialog() == DialogResult.OK)
                    SetCurrentAction(actionSelectorForm.SelectedId);
            }
        }

        private void actionSelectorSpells_Click(object sender, EventArgs e)
        {
            using (SingleSelectorEditorForm actionSelectorForm = new SingleSelectorEditorForm(SelectorTypes.ActionsCustom, _unit.SpellsIds))
            {
                actionSelectorForm.Text = "Выберите заклинание";
                if (actionSelectorForm.ShowDialog() == DialogResult.OK)
                    SetCurrentAction(actionSelectorForm.SelectedId);
            }
        }
    }
}
