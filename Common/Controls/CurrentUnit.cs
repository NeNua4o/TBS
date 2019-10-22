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
        
        public int ActionId = -1;
        public event EventHandler ActionChanged;

        public CurrentUnit()
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();
            actionSelectorSkills.Init("Icons/z_skl.png");
            actionSelectorSpells.Init("Icons/z_mag.png");

            actionSelectorMain.ActionChanged += ActionSelector_ActionChanged;
            actionSelectorSec.ActionChanged += ActionSelector_ActionChanged;

            actionSelectorSkills.Click += ActionSelectorSkills_Click;
        }

        private void ActionSelectorSkills_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("click");
            using (SingleSelectorEditorForm actionSelectorForm = new SingleSelectorEditorForm(SelectorTypes.ActionsCustom, _unit.SkillsIds))
            {
                actionSelectorForm.Text = "Выберите навык";
                if (actionSelectorForm.ShowDialog() == DialogResult.OK)
                {
                    ActionId = actionSelectorForm.SelectedId;
                    ActionChanged?.Invoke(this, null);
                }
            }
        }

        private void ActionSelector_ActionChanged(object sender, EventArgs e)
        {
            var snd = (ActionSelector)sender;
            Act act = _repWkr.GetAction(snd.ActionId);
            if (act != null)
            {
                pb_curAct.Image = new Bitmap(act.Icon, pb_curAct.Size);
                ActionId = act.Id;
                ActionChanged?.Invoke(this, null);
            }
        }

        Unit _unit;
        public void Set(Unit unit)
        {
            _unit = unit;
            pb_icon.Image = new Bitmap(unit.Icon, pb_icon.Size);
            Act act = _repWkr.GetAction(unit.MainActId);
            actionSelectorMain.Set(act);
            if (act != null)
            {
                pb_curAct.Image = new Bitmap(act.Icon, pb_curAct.Size);
                ActionId = act.Id;
                ActionChanged?.Invoke(this, null);
            }
            act = _repWkr.GetAction(unit.SecondActId);
            actionSelectorSec.Set(act);
        }

        
    }
}
