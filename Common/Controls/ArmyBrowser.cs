using System.Collections.Generic;
using System.Linq;
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
            Pl = pl;
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

        private void p_unit_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Unit)))
            {
                e.Effect = e.AllowedEffect;
            }
        }

        private void p_unit_DragDrop(object sender, DragEventArgs e)
        {
            /*
            if (e.Effect == DragDropEffects.Copy)
            {
                var snd = (Panel)sender;
                var dragableUnit = (Unit)e.Data.GetData(typeof(Unit));
                dragableUnit.S = GetFromPan(snd);
                var oldUnit = GetUnitByTag(snd.Tag);
                if (oldUnit == null)
                {
                    if (String.IsNullOrEmpty(pl.Hero.Name) || pl.Hero.Leader > pl.Units.Count)
                    {
                        dragableUnit.Ad = GetNewAd();
                        pl.Units.Add(new Unit(dragableUnit));
                        snd.BackgroundImage = dragableUnit.Icon;
                    }
                }
                else
                {
                    dragableUnit.Ad = oldUnit.Ad;
                    var ind = pl.Units.IndexOf(oldUnit);
                    pl.Units.Insert(ind, new Unit(dragableUnit));
                    pl.Units.Remove(oldUnit);
                    snd.BackgroundImage = dragableUnit.Icon;
                }
            }
            if (e.Effect == DragDropEffects.Move)
            {
                var snd = (Panel)sender;
                var srcUnit = (Unit)e.Data.GetData(typeof(Unit));
                var srcPan = GetPanelByS(srcUnit);
                var oldUnit = GetUnitByTag(snd.Tag);
                if (oldUnit == null)
                {
                    srcUnit.S = GetFromPan(snd);
                    srcPan.BackgroundImage = null;
                    snd.BackgroundImage = srcUnit.Icon;
                }
                else
                {
                    oldUnit.S = srcUnit.S;
                    srcUnit.S = GetFromPan(snd);
                    srcPan.BackgroundImage = oldUnit.Icon;
                    snd.BackgroundImage = srcUnit.Icon;
                }
            }
            */
        }
    }
}
