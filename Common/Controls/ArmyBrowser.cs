using Common.Models;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ArmyBrowser : UserControl
    {
        public Pl Pl;
        List<Panel> pans = new List<Panel>();
        RepositoryWorker _repWkr; 
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

            _repWkr = RepositoryWorker.Instance();
            for (int i = 0; i < _repWkr.Teams.Count; i++)
                cb_team.Items.Add(_repWkr.Teams[i].Id);
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
                    p.BackgroundImage = new Bitmap(u.Icon, p.Size);
            }
            cb_team.SelectedItem = Pl.TeamId;
        }

        private void p_unit_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(BaseUnit))) { e.Effect = e.AllowedEffect; }
            if (e.Data.GetDataPresent(typeof(Unit))) { e.Effect = e.AllowedEffect; }
        }

        private void p_unit_DragDrop(object sender, DragEventArgs e)
        {
            
            if (e.Effect == DragDropEffects.Copy)
            {
                var snd = (Panel)sender;
                var dragableUnit = (BaseUnit)e.Data.GetData(typeof(BaseUnit));
                var oldUnit = GetUnitByTag(snd.Tag);
                if (oldUnit == null)
                {
                    if (Pl.Units.Count < 7)
                    {
                        var newUnit = new Unit(dragableUnit, GetFromPan(snd), Pl.TeamId);
                        newUnit.Id = GetNewGlobalId();
                        newUnit.TeamId = Pl.TeamId;
                        Pl.Units.Add(newUnit);
                        snd.BackgroundImage = new Bitmap(newUnit.Icon, snd.Size);
                    }
                }
                else
                {
                    var newUnit = new Unit(dragableUnit, GetFromPan(snd), Pl.TeamId);
                    newUnit.Id = oldUnit.Id;
                    newUnit.TeamId = Pl.TeamId;
                    var ind = Pl.Units.IndexOf(oldUnit);
                    Pl.Units.Insert(ind, newUnit);
                    Pl.Units.Remove(oldUnit);
                    snd.BackgroundImage = new Bitmap(newUnit.Icon, snd.Size);
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
                    srcUnit.StartPos = GetFromPan(snd);
                    srcPan.BackgroundImage = null;
                    snd.BackgroundImage = new Bitmap(srcUnit.Icon, snd.Size);
                }
                else
                {
                    oldUnit.StartPos = srcUnit.StartPos;
                    srcUnit.StartPos = GetFromPan(snd);
                    srcPan.BackgroundImage = new Bitmap(oldUnit.Icon, snd.Size);
                    snd.BackgroundImage = new Bitmap(srcUnit.Icon, snd.Size);
                }
            }
        }

        private void p_unit_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var snd = (Panel)sender;
                var resUnit = GetUnitByTag(snd.Tag);
                if (resUnit != null)
                    this.DoDragDrop(resUnit, DragDropEffects.Move);
            }
        }

        private Axial GetFromPan(Panel pan)
        {
            var res = new Axial();
            var rw = pan.Name.Split('_');
            int.TryParse(rw[1], out res.Q);
            int.TryParse(rw[2], out res.R);
            return res;
        }

        private Unit GetUnitByTag(object rawTag)
        {
            var tag = (string)rawTag;
            for (int i = 0; i < Pl.Units.Count; i++)
            {
                var u = Pl.Units[i];
                int q = u.StartPos.Q;
                int r = u.StartPos.R;
                var s = q + "" + r;
                if (s == tag)
                    return Pl.Units[i];
            }
            return null;
        }

        private int GetNewGlobalId()
        {
            int k = 1;
            while (Pl.Units.Any(unit=>unit.Id==k))
            {
                k++;
            }
            return k;
        }

        private Panel GetPanelByS(Unit u)
        {
            var tg = u.StartPos.Q + "" + u.StartPos.R;
            for (int i = 0; i < pans.Count; i++)
            {
                if ((string)pans[i].Tag == tg)
                    return pans[i];
            }
            return null;
        }

        private void cb_team_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_team.SelectedIndex < 0) return;
            Pl.TeamId = (int)cb_team.SelectedItem;
        }
    }
}
