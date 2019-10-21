using Common;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private void b_reset_Click(object sender, EventArgs e)
        {
            GenerateMap();
            PlaceAllUnits();
            InitTurnOrder();
            CalcTurns();
            DrawMap();
            turnQueue.Top = pb_field.Bottom + 5;
            currentUnit.Top = pb_field.Bottom - currentUnit.Height;
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
            var t = new List<Unit>();
            t.AddRange(_pls[0].Units);
            t.AddRange(_pls[1].Units);
            for (int i = 0; i < t.Count; i++)
            {
                t[i].Chars.Lane = rng.Next(1, (int)(t[i].Chars.Initiative * 0.1)) / 100f;
                t[i].Chars.LaneUp = t[i].Chars.Initiative / _baseLane;
            }
        }

        private void CalcTurns()
        {
            var t = new List<Unit>();
            t.AddRange(_pls[0].Units.Where(unit => unit.Chars.Alive == 1));
            t.AddRange(_pls[1].Units.Where(unit => unit.Chars.Alive == 1));

            t = t.OrderByDescending(x => x.Chars.Initiative).ThenByDescending(x => x.Chars.Lane).ToList();
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Chars.Lane >= _baseLane)
                {
                    if (_turnQueueUnits.Count < 15)
                    {
                        _turnQueueUnits.Add(t[i]);
                        t[i].Chars.Lane -= _baseLane;
                    }
                    else break;
                }
            }

            while (_turnQueueUnits.Count < 15)
            {
                t = t.OrderByDescending(x => x.Chars.Initiative).ThenByDescending(x => x.Chars.Lane).ToList();
                for (int i = 0; i < t.Count; i++)
                {
                    t[i].Chars.Lane += t[i].Chars.LaneUp;
                    if (t[i].Chars.Lane >= _baseLane)
                    {
                        if (_turnQueueUnits.Count < 15)
                        {
                            _turnQueueUnits.Add(t[i]);
                            t[i].Chars.Lane -= _baseLane;
                        }
                        else break;
                    }
                }
            }
            turnQueue.SetQueue(_turnQueueUnits);
            currentUnit.Set(_turnQueueUnits[0]);
        }
    }
}
