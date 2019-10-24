using Common;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TBS
{
    public partial class Main : Form
    {
        BMap _map;
        List<Pl> _pls = new List<Pl>();
        RepositoryWorker _repWkr = RepositoryWorker.GetInstance();

        Random rng = new Random();
        private RectangleF _srcModelSize = new RectangleF(0, 0, 60, 60);
        private List<Unit> _turnQueueUnits = new List<Unit>();
        const float _baseLane = 50;

        public Main()
        {
            InitializeComponent();
            _pls = _repWkr.Pls;
            for (int i = _pls.Count; i < 2; i++)
                _repWkr.CreatePl();
            turnControl.Init();
            turnControl.SkipClicked += TurnControl_SkipClicked;
        }

        private void TurnControl_SkipClicked(object sender, EventArgs e)
        {
            if (currentUnit.Unit == null) return;
            currentUnit.Unit.Chars.Lane -= _baseLane;
            CalcTurns();
        }

        private void b_editArmies_Click(object sender, EventArgs e)
        {
            ArmyEditor ae = new ArmyEditor(_pls);
            ae.ShowDialog();
        }

        private void DrawMap()
        {
            if (_map == null) return;
            Bitmap b = new Bitmap(_map.GridLayout); Graphics g = Graphics.FromImage(b);

            // Draw units
            var cellsWithUnits = _map.GetCellsWithUnits();
            for (int i = 0; i < cellsWithUnits.Count; i++)
            {
                var c = cellsWithUnits[i];

                g.DrawPolygon(_repWkr.GetTeam(c.Unit.TeamId).Pen, c.Hex.K1);

                g.DrawImage(c.Unit.Icon, c.ModelSize, _srcModelSize, GraphicsUnit.Pixel);
                g.DrawRectangle(Pens.Red, c.ModelSize.X, c.ModelSize.Y - 10, c.ModelSize.Width, 4);
                var wd = c.Unit.Chars.HP / (float)c.Unit.Bu.Chars.HP * c.ModelSize.Width;
                g.FillRectangle(Brushes.Red, c.ModelSize.X, c.ModelSize.Y - 10, wd, 4);
                g.DrawRectangle(Pens.Blue, c.ModelSize.X, c.ModelSize.Y - 5, c.ModelSize.Width, 4);
                wd = c.Unit.Chars.MP / (float)c.Unit.Bu.Chars.MP * c.ModelSize.Width;
                g.FillRectangle(Brushes.Blue, c.ModelSize.X, c.ModelSize.Y - 5, wd, 4);
            }

            pb_field.Image = b; g = null; b = null;
        }

        Font _fnt = new Font("Calibri Light", 10, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();
        private void DebugDraw(List<VUnit> vus, List<Unit> us)
        {
            const int vs = 15; int k = 0; var b = new Bitmap(pb_debug.Width, pb_debug.Height); var g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            if (vus != null)
                for (int i = 0; i < vus.Count; i++)
                {
                    var vu = vus[i];
                    g.DrawString(String.Format("{0} {1} {2}", vu.Unit.Name, vu.Unit.TeamId, vu.Lane), _fnt, _brush, 0, k * vs, _drawFormat);
                    g.FillRectangle(Brushes.Red, 100, k * vs, vu.Lane, 10);
                    k++;
                }

            k++;
            for (int i = 0; i < us.Count; i++)
            {
                var u = us[i];
                g.DrawString(String.Format("{0} {1} {2}", u.Name, u.TeamId, u.Chars.Lane), _fnt, _brush, 0, k * vs, _drawFormat);
                k++;
            }
            pb_debug.Image = b; g = null; b = null;
        }

        private void b_reset_Click(object sender, EventArgs e)
        {
            GenerateMap();
            PlaceAllUnits();
            InitTurnOrder();
            CalcTurns();
            DrawMap();

            turnQueue.Left = 5;
            currentUnit.Left = 5;
            pb_field.Left = currentUnit.Right + 5;
            turnControl.Left = pb_field.Right + 5;

            currentUnit.Top = pb_field.Bottom - currentUnit.Height;
            turnControl.Top = pb_field.Bottom - turnControl.Height;
            turnQueue.Top = pb_field.Bottom + 5;

            pb_debug.Left = pb_field.Right + 10;
        }

        private void GenerateMap()
        {
            _map = new BMap(4, 50);
            pb_field.Width = _map.Width;
            pb_field.Height = _map.Height;
        }

        private void PlaceAllUnits()
        {
            PlaceUnits(_pls[0]);
            PlaceUnits(_pls[1], true);
        }

        private void PlaceUnits(Pl pl, bool toRight = false)
        {
            if (pl == null) return;
            for (int i = 0; i < pl.Units.Count; i++)
            {
                var u = pl.Units[i];
                u.Bu = _repWkr.GetBaseUnit(u.BId);
                int q = toRight ? _map.ArraySize - u.StartPos.Q - 1 : u.StartPos.Q, r = toRight ? _map.ArraySize - u.StartPos.R - 1 : u.StartPos.R;
                _map.Cells[q, r].Unit = u;
                u.CurPos = new Axial(q, r);
            }
        }

        private void InitTurnOrder()
        {
            _turnQueueUnits.Clear();
            currentUnit.Set(null);
            var t = new List<Unit>();
            t.AddRange(_pls[0].Units);
            t.AddRange(_pls[1].Units);
            for (int i = 0; i < t.Count; i++)
                t[i].Chars.Lane = rng.Next(1, (int)(t[i].Chars.Initiative * 0.1)) / 100f;
        }

        public class VUnit
        {
            public Unit Unit;
            public float Lane;
            public int Initiative;
            public float LaneUp;
            public VUnit(Unit unit)
            {
                Unit = unit;
                Initiative = unit.Chars.Initiative;
                Lane = unit.Chars.Lane;
                LaneUp = unit.Chars.LaneUp;
            }
        }

        private void CalcTurns()
        {
            var units = new List<Unit>();
            units.AddRange(_pls[0].Units.Where(unit => unit.Chars.Alive == 1));
            units.AddRange(_pls[1].Units.Where(unit => unit.Chars.Alive == 1));
            
            for (int i = 0; i < units.Count; i++)
            {
                // TODO Учитывать эффекты.
                units[i].Chars.LaneUp = units[i].Chars.Initiative / _baseLane;
            }

            _turnQueueUnits.Clear();

            // Заполним полосу хода для первого/первых юнитов.
            while (_turnQueueUnits.Count == 0)
            {
                units = units.OrderByDescending(x => x.Chars.Lane).ThenByDescending(x => x.Chars.Initiative).ToList();
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].Chars.Lane >= _baseLane)
                        _turnQueueUnits.Add(units[i]);
                    units[i].Chars.Lane += units[i].Chars.LaneUp;
                }
            }
            
            currentUnit.Set(_turnQueueUnits[0]);
            _turnQueueUnits.Clear();

            var vunits = new List<VUnit>();
            for (int i = 0; i < units.Count; i++)
                vunits.Add(new VUnit(units[i]));

            // При помощи виртуальных юнитов предсказываем порядок ходов.
            while (_turnQueueUnits.Count < 15)
            {
                vunits = vunits.OrderByDescending(x => x.Lane).ThenByDescending(x => x.Initiative).ToList();
                for (int i = 0; i < vunits.Count; i++)
                {
                    var vunit = vunits[i];
                    if (vunit.Lane >= _baseLane)
                    {
                        if (_turnQueueUnits.Count < 15)
                        {
                            _turnQueueUnits.Add(vunit.Unit);
                            vunit.Lane -= _baseLane;
                        }
                    }
                    vunit.Lane += vunit.LaneUp;
                }
            }

            turnQueue.SetQueue(_turnQueueUnits);

            units = null;
            vunits = null;
        }
    }
}
