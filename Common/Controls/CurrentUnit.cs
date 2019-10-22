using System.Drawing;
using System.Windows.Forms;
using Common.Repositories;
using System;

namespace Common.Controls
{
    public partial class CurrentUnit : UserControl
    {
        RepositoryWorker _repWkr;
        
        public int ActionId = -1;

        public CurrentUnit()
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();
            actionMultiSelectorSkills.Init("Icons/z_skl.png");
            actionMultiSelectorSpells.Init("Icons/z_mag.png");
        }

        public void Set(Unit unit)
        {
            pb_icon.Image = new Bitmap(unit.Icon, pb_icon.Size);
            Act act = _repWkr.GetAction(unit.MainActId);
            actionSelectorMain.Set(act);
            act = _repWkr.GetAction(unit.SecondActId);
            actionSelectorSec.Set(act);
        }
    }
}
