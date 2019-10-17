using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ArmyBrowser : UserControl
    {
        public Pl Pl;
        List<Panel> pans = new List<Panel>();
        public ArmyBrowser()
        {
            InitializeComponent();
            p_0_3.Tag = "03"; pans.Add(p_0_3);
            p_0_4.Tag = "04"; pans.Add(p_0_4);
            p_0_5.Tag = "05"; pans.Add(p_0_5);
            p_0_6.Tag = "06"; pans.Add(p_0_6);
            p_1_2.Tag = "12"; pans.Add(p_1_2);
            p_1_3.Tag = "13"; pans.Add(p_1_3);
            p_1_4.Tag = "14"; pans.Add(p_1_4);
            p_1_5.Tag = "15"; pans.Add(p_1_5);
            p_1_6.Tag = "16"; pans.Add(p_1_6);
        }

        public void Set(Pl pl)
        {
            if (pl == null) return;
            this.Pl = pl;
            //p_hero.BackgroundImage = pl.Hero.Icon;
            for (int i = 0; i < pl.Units.Count; i++)
            {
                var u = pl.Units[i];
                var s = u.StartPos.Q + "" + u.StartPos.R;
                var p = pans.FirstOrDefault(x => (string)x.Tag == s);
                if (p != null)
                    p.BackgroundImage = u.Icon;
            }
        }
    }
}
