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
            var repWkr = RepositoryWorker.GetInstance();
            var team = repWkr.GetTeam(unit.TeamId);
            if (team != null)
            {
                var b = new Bitmap(Width, Height);
                var g = Graphics.FromImage(b);
                g.DrawRectangle(new Pen(team.Color), 0, 0, b.Width - 1, b.Height - 1);
                BackgroundImage = b;
                g = null;
                b = null;
            }
                
        }
    }
}
