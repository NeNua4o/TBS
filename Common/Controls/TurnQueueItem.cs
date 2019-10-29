using Common.Models;
using Common.Repositories;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class TurnQueueItem : UserControl
    {
        public TurnQueueItem(Unit unit)
        {
            InitializeComponent();
            
            pb_icon.Image = new Bitmap(unit.Icon, pb_icon.Size);
            l_init.Text = unit.Chars.Initiative + "";
            l_hp.Text = unit.Chars.HP + "/" + unit.Bu.Chars.HP;
            var repWkr = RepositoryWorker.Instance();
            var team = repWkr.GetTeam(unit.TeamId);
            if (team != null)
            {
                
            }
                
        }
    }
}
