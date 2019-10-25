using Common;
using Common.Extensions;
using Common.Models;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
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
        RectangleF _srcModelSize = new RectangleF(0, 0, 60, 60);
        RectangleF _step_srec = new RectangleF(0, 0, 40, 40);
        Bitmap _steps, _stepsD;
        const float _baseLane = 50;
        Act _selectedAction;

        public Main()
        {
            InitializeComponent();
            _pls = _repWkr.Pls;
            for (int i = _pls.Count; i < 2; i++)
                _repWkr.CreatePl();
            currentUnit.ActionChanged += CurrentUnit_ActionChanged;
            turnControl.Init();
            turnControl.SkipClicked += TurnControl_SkipClicked;
            turnControl.WaitClicked += TurnControl_WaitClicked;

            _steps = new Bitmap("icons/step.png");
            _stepsD = new Bitmap("icons/z_step.png");
        }

        private void CurrentUnit_ActionChanged(object sender, EventArgs e)
        {
            _selectedAction = _repWkr.GetAction(currentUnit.ActionId);
        }

        private void TurnControl_WaitClicked(object sender, EventArgs e)
        {
            if (currentUnit.Unit == null) return;
            currentUnit.Unit.Chars.Lane -= (_baseLane / 2);
            CalcTurns();
        }

        private void TurnControl_SkipClicked(object sender, EventArgs e)
        {
            if (currentUnit.Unit == null) return;
            currentUnit.Unit.Chars.Lane -= _baseLane;
            CalcTurns();
        }

        private void b_editArmies_Click(object sender, EventArgs e) { ArmyEditor armyEditor = new ArmyEditor(_pls); armyEditor.ShowDialog(); }

        private void DrawMap()
        {
            if (_map == null) return;
            Bitmap b = new Bitmap(_map.GridLayout); Graphics g = Graphics.FromImage(b);


            // Available for move + path
            for (int i = 0; i < _cellsAvailableToMove.Count; i++)
                g.DrawImage(_stepsD, _cellsAvailableToMove[i].StepsSize, _step_srec, GraphicsUnit.Pixel);
            for (int i = 0; i < _pathCells.Count; i++)
                g.DrawImage(_steps, _pathCells[i].StepsSize, _step_srec, GraphicsUnit.Pixel);

            // Available for action
            for (int i = 0; i < _cellsAvailableToAction.Count; i++)
                g.DrawPolygon(Pens.Violet, _cellsAvailableToAction[i].Hex.K10);

            // LinePath
            if (_actionLinePath.Count > 0)
            {
                for (int i = 0; i < _actionLinePath.Count; i++)
                {
                    g.DrawPolygon(Pens.Blue, _actionLinePath[i].Hex.K30);
                }
                if (_currentCell != null && _hoveredCell != null)
                    g.DrawLine(Pens.Blue, _currentCell.Hex.Center, _hoveredCell.Hex.Center);
            }

            // ActionStartPt
            if (_actionStartPoint != null)
                g.DrawPolygon(Pens.Aqua, _actionStartPoint.Hex.K20);

            // MoveDestination
            if (_moveDestination != null)
                g.DrawPolygon(Pens.Orange, _moveDestination.Hex.K20);

            // ActionSheme
            for (int i = 0; i < _actionSheme.Count; i++)
            {
                g.DrawPolygon(Pens.DarkBlue, _actionSheme[i].Hex.K30);
            }



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

        private void GenerateMap() { _map = new BMap(4, 50); pb_field.Width = _map.Width; pb_field.Height = _map.Height; }

        private void PlaceAllUnits() { PlaceUnits(_pls[0]); PlaceUnits(_pls[1], true); _map.CalcCellsWithUnits(); }
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
            currentUnit.Set(null);
            var units = new List<Unit>(); units.AddRange(_pls[0].Units); units.AddRange(_pls[1].Units);
            for (int i = 0; i < units.Count; i++) units[i].Chars.Lane = rng.Next(1, (int)(units[i].Chars.Initiative * 0.1)) / 100f;
            units = null;
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

            var turnQueueUnits = new List<Unit>();
            // Заполним полосу хода для первого/первых юнитов.
            while (turnQueueUnits.Count == 0)
            {
                units = units.OrderByDescending(x => x.Chars.Lane).ThenByDescending(x => x.Chars.Initiative).ToList();
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].Chars.Lane >= _baseLane)
                        turnQueueUnits.Add(units[i]);
                    units[i].Chars.Lane += units[i].Chars.LaneUp;
                }
            }
            currentUnit.Set(turnQueueUnits[0]);
            _currentUnit = turnQueueUnits[0];
            _currentCell = _map.Cell(_currentUnit.CurPos);
            _cellsAvailableToMove = _map.GetCellsToMove(_currentCell, _currentUnit.Chars.MoveRad, _currentUnit.Chars.MoveType);
            _cellsAvailableToAction = _map.GetCellsAvailableToAction(_currentUnit, _currentCell, _selectedAction, _selectedAction.Rad);

            turnQueueUnits.Clear();
            // Создадим виртуальных юнитов.
            var vunits = new List<VUnit>(); for (int i = 0; i < units.Count; i++) vunits.Add(new VUnit(units[i]));
            // При помощи виртуальных юнитов предсказываем порядок ходов.
            while (turnQueueUnits.Count < 15)
            {
                vunits = vunits.OrderByDescending(x => x.Lane).ThenByDescending(x => x.Initiative).ToList();
                for (int i = 0; i < vunits.Count; i++)
                {
                    var vunit = vunits[i];
                    if (vunit.Lane >= _baseLane)
                    {
                        if (turnQueueUnits.Count < 15)
                        {
                            turnQueueUnits.Add(vunit.Unit);
                            vunit.Lane -= _baseLane;
                        }
                    }
                    vunit.Lane += vunit.LaneUp;
                }
            }
            turnQueue.SetQueue(turnQueueUnits);

            turnQueueUnits = null; units = null; vunits = null;
        }


        BMapCell _hoveredCell, _currentCell;
        Unit _currentUnit;

        List<BMapCell> _cellsAvailableToMove = new List<BMapCell>();
        List<BMapCell> _cellsAvailableToAction = new List<BMapCell>();
        List<BMapCell> _pathCells = new List<BMapCell>();
        List<BMapCell> _actionLinePath = new List<BMapCell>();
        List<BMapCell> _actionSheme = new List<BMapCell>();
        List<Unit> _targetsOnLine = new List<Unit>();

        BMapCell _actionStartPoint;
        BMapCell _moveDestination;
        int _direction;
        


        private void pb_field_MouseMove(object sender, MouseEventArgs e)
        {
            // Clears
            _actionLinePath.Clear();
            _pathCells.Clear();
            _actionSheme.Clear();
            _actionStartPoint = null;
            _moveDestination = null;
            _targetsOnLine.Clear();

            if (_map == null) goto end;
            _hoveredCell = _map.GetCellByXY(e.X, e.Y);
            if (_hoveredCell == null) goto end;

            var actionCell = _cellsAvailableToAction.WitchIs(_hoveredCell);
            if (actionCell != null)
            {
                if (_selectedAction.Rad > 1)
                {
                    // dist attack
                    _actionLinePath = _map.GetCellsOnLine(_currentCell.Cube, actionCell.Cube, false, false);
                    if (_actionLinePath.Count == 0)
                    {
                        if (_hoveredCell.Axial == _currentCell.Axial) _direction = _map.GetDirection(_hoveredCell, e.X, e.Y);
                        else _direction = _map.GetDirection(_hoveredCell, _currentCell);
                    }
                    else _direction = _map.GetDirection(_hoveredCell, _actionLinePath[_actionLinePath.Count - 1]);
                    if (_direction == -1) goto end;
                    _actionStartPoint = _hoveredCell;
                }
                else
                {
                    // close attack
                    _direction = _map.GetDirection(_hoveredCell, e.X, e.Y);
                    _actionStartPoint = _hoveredCell.GetDir(_direction);
                    if (_actionStartPoint == null)
                    {
                        _direction = -1;
                        _actionStartPoint = null;
                        goto end;
                    }
                    else
                    {
                        if (_cellsAvailableToMove.WitchIs(_actionStartPoint) != null)
                        {
                            _moveDestination = _actionStartPoint;
                            _pathCells.AddRange(_map.GetPath((Unit)_currentUnit, _currentCell, _moveDestination, _cellsAvailableToMove));
                        }
                        else
                        {
                            if (_actionStartPoint.Axial != _currentCell.Axial)
                            {
                                _direction = -1;
                                _actionStartPoint = null;
                                goto end;
                            }
                        }
                    }
                }
                _actionSheme = _map.GetSheme(_actionStartPoint, _selectedAction, (_direction + 3) % 6, _direction, _cellsAvailableToAction);
            }
            else // Action miss
            {
                var movementCell = _cellsAvailableToMove.WitchIs(_hoveredCell);
                if (movementCell != null)
                {
                    _moveDestination = _hoveredCell;
                    _pathCells.AddRange(_map.GetPath((Unit)_currentUnit, _currentCell, _moveDestination, _cellsAvailableToMove));
                }
            }
            end:
            DrawMap();
        }

        private void pb_field_Click(object sender, EventArgs e)
        {
            var a = (MouseEventArgs)e;
            if (a.Button == MouseButtons.Left)
            {
                bool doMove = false, doAtack = false;
                if (_moveDestination != null) // If move
                {
                    _currentCell.Unit = null;
                    var u = _currentUnit;
                    u.CurPos.Q = _moveDestination.Axial.Q;
                    u.CurPos.R = _moveDestination.Axial.R;
                    _moveDestination.Unit = u;
                    _map.CalcCellsWithUnits();
                    doMove = true;
                }

                if (_selectedAction != null && _actionSheme.Count > 0) // If attack
                {
                    doAtack = true;
                }
                if (doMove || doAtack)
                {
                    currentUnit.Unit.Chars.Lane -= _baseLane;
                    CalcTurns();
                }
            }
        }

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
}

