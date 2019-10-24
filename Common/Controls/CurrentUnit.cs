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
        public Unit Unit;
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
            Unit = unit;
            if (unit == null)
            {
                SetCurrentAction(null);
                return;
            }

            actionSelectorSkills.Init("Icons/z_skl.png");
            actionSelectorSpells.Init("Icons/z_mag.png");
            _iconsSetted = true;

            pb_icon.Image = new Bitmap(unit.Icon, pb_icon.Size);

            Act act = _repWkr.GetAction(unit.MainActId);
            actionSelectorMain.Set(act);

            SetCurrentAction(act);

            act = _repWkr.GetAction(unit.SecondActId);
            actionSelectorSec.Set(act);

            if (unit.SkillsIds == null || unit.SkillsIds.Length == 0)
                actionSelectorSkills.Set(null);
            if (unit.SpellsIds == null || unit.SpellsIds.Length == 0)
                actionSelectorSpells.Set(null);
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
            else
            {
                ActionId = -1;
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
            using (SingleSelectorEditorForm actionSelectorForm = new SingleSelectorEditorForm(SelectorTypes.ActionsCustom, Unit.SkillsIds))
            {
                actionSelectorForm.Text = "Выберите навык";
                if (actionSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    actionSelectorSkills.Set(_repWkr.GetAction(actionSelectorForm.SelectedId));
                    SetCurrentAction(actionSelectorForm.SelectedId);
                }
            }
        }

        private void actionSelectorSpells_Click(object sender, EventArgs e)
        {
            using (SingleSelectorEditorForm actionSelectorForm = new SingleSelectorEditorForm(SelectorTypes.ActionsCustom, Unit.SpellsIds))
            {
                actionSelectorForm.Text = "Выберите заклинание";
                if (actionSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    actionSelectorSpells.Set(_repWkr.GetAction(actionSelectorForm.SelectedId));
                    SetCurrentAction(actionSelectorForm.SelectedId);
                }
            }
        }
    }
}
