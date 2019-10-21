using System.Drawing;
using System.Windows.Forms;
using Common.Repositories;
using System;

namespace Common.Controls
{
    public partial class CurrentUnit : UserControl
    {
        RepositoryWorker _repWkr;

        public event EventHandler ActionChanged;
        public int ActionId = -1;

        public CurrentUnit()
        {
            InitializeComponent();
            _repWkr = RepositoryWorker.GetInstance();
        }

        public void Set(Unit unit)
        {
            pb_icon.Image = new Bitmap(unit.Icon, pb_icon.Size);

            Act act = _repWkr.GetAction(unit.MainActId);
            if (act != null)
            {
                b_main.BackgroundImage = new Bitmap(act.Icon, b_main.Size);
                ActionId = unit.MainActId;
                ActionChanged?.Invoke(this, null);
            }

            act = _repWkr.GetAction(unit.SecondActId);
            if(act!=null)
                b_sec.BackgroundImage = new Bitmap(act.Icon, b_main.Size);
        }
    }
}
