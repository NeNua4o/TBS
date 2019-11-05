using BS;
using Common;
using Common.Extensions;
using Common.Models;
using Common.Repositories;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using Common.Enums;

namespace TBS
{
    public partial class Main : Form
    {
        BMap _map;
        List<Pl> _pls = new List<Pl>();
        RepositoryWorker _repWkr = RepositoryWorker.Instance();
        BattleUtils _battle = BattleUtils.Instance();

        RandomUtils _rng = RandomUtils.Instance();
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

        LogForm _lf = new LogForm();
        bool _lfopened = false;
        private void b_log_Click(object sender, EventArgs e)
        {
            _lf.AddS(_log);
            _lf.Show();
            _lfopened = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_lfopened)
                _lf.Close();
            base.OnClosing(e);
        }

        private void CurrentUnit_ActionChanged(object sender, EventArgs e)
        {
            ReSetCurrentParams();
            DrawMap();
        }

        private void ReSetCurrentParams()
        {
            _currentUnit = currentUnit.Unit;
            if (_currentUnit == null) return;
            _currentCell = _map.Cell(_currentUnit.CurPos);
            _aForMove = _map.GetAFormMove(_currentCell, _currentUnit.CharCursI(CharType.MoveRange), (MoveTypes)_currentUnit.CharCursI(CharType.MoveType));
            if (currentUnit.ActionId == -1) return;
            _selectedAction = _repWkr.GetAction(currentUnit.ActionId);
            _aForAction = _map.GetAForAction(_currentUnit, _currentCell, _selectedAction);
            _aForApply = _map.GetAForApply(_currentUnit, _currentCell, _selectedAction.AppliesOn);
            _actionRange = _map.GetCellsInRange(_currentCell.Axial, _selectedAction.Range);
            DrawDebug();
        }

        private void TurnControl_WaitClicked(object sender, EventArgs e)
        {
            if (currentUnit.Unit == null) return;
            currentUnit.Unit.Chars.SummCurrent(CharType.Lane, -_baseLane / 2);
            CalcTurns();
        }

        private void TurnControl_SkipClicked(object sender, EventArgs e)
        {
            if (currentUnit.Unit == null) return;
            currentUnit.Unit.Chars.SummCurrent(CharType.Lane, -_baseLane / 2);
            CalcTurns();
        }

        private void b_editArmies_Click(object sender, EventArgs e) { ArmyEditor armyEditor = new ArmyEditor(_pls); armyEditor.ShowDialog(); }

        Brush _targetingBrush = new SolidBrush(Color.FromArgb(60, Color.Red));
        Brush _movePtBrush = new SolidBrush(Color.FromArgb(60, Color.Orange));
        Brush _shemeBrush = new SolidBrush(Color.FromArgb(60, Color.Blue));
        Brush _linePathBrush = new SolidBrush(Color.FromArgb(60, Color.Green));
        private void DrawMap()
        {
            if (_map == null) return;
            Bitmap b = new Bitmap(_map.GridLayout); Graphics g = Graphics.FromImage(b);


            // Available for move + movement path
            for (int i = 0; i < _aForMove.Count; i++)
                g.DrawImage(_stepsD, _aForMove[i].StepsSize, _step_srec, GraphicsUnit.Pixel);
            for (int i = 0; i < _movementPath.Count; i++)
                g.DrawImage(_steps, _movementPath[i].StepsSize, _step_srec, GraphicsUnit.Pixel);

            // Available for action
            for (int i = 0; i < _aForAction.Count; i++)
                g.FillPolygon(_targetingBrush, _aForAction[i].Hex.K10);

            // Available for apply
            for (int i = 0; i < _aForApply.Count; i++)
                g.FillPolygon(_targetingBrush, _aForApply[i].Hex.K40);

            // ActionRange
            for (int i = 0; i < _actionRange.Count; i++)
                g.DrawPolygon(Pens.Green, _actionRange[i].Hex.K);

            // LinePath
            if (_actionLinePath.Count > 0)
            {
                for (int i = 0; i < _actionLinePath.Count; i++)
                {
                    g.FillPolygon(_linePathBrush, _actionLinePath[i].Hex.K30);
                }
                if (_currentCell != null && _hoveredCell != null)
                    g.DrawLine(Pens.Blue, _currentCell.Hex.Center, _hoveredCell.Hex.Center);
            }

            // ActionStartPoint
            if (_actionStartPoint != null)
                g.DrawPolygon(Pens.Blue, _actionStartPoint.Hex.K20);

            // MoveDestination
            if (_moveDestination != null)
                g.FillPolygon(_movePtBrush, _moveDestination.Hex.K20);

            // ActionSheme
            for (int i = 0; i < _actionSheme.Count; i++)
            {
                g.FillPolygon(_shemeBrush, _actionSheme[i].Hex.K20);
            }

            // Draw units
            var cellsWithUnits = _map.GetCellsWithUnits();
            for (int i = 0; i < cellsWithUnits.Count; i++)
            {
                var c = cellsWithUnits[i];

                g.DrawPolygon(_repWkr.GetTeam(c.Unit.TeamId).Pen, c.Hex.K1);

                g.DrawImage(c.Unit.Icon, c.ModelSize, _srcModelSize, GraphicsUnit.Pixel);
                g.DrawRectangle(Pens.Red, c.ModelSize.X, c.ModelSize.Y - 10, c.ModelSize.Width, 4);

                var curHP = c.Unit.CharCursF(CharType.Hp);
                var basHp = c.Unit.CharBaseF(CharType.Hp);

                var width = curHP / basHp * c.ModelSize.Width;
                g.FillRectangle(Brushes.Red, c.ModelSize.X, c.ModelSize.Y - 10, width, 4);

                var mp = c.Unit.CharBaseI(CharType.Mp);
                if (mp != 0)
                {
                    g.DrawRectangle(Pens.Blue, c.ModelSize.X, c.ModelSize.Y - 5, c.ModelSize.Width, 4);
                    width = c.Unit.CharCursF(CharType.Mp) / mp * c.ModelSize.Width;
                    g.FillRectangle(Brushes.Blue, c.ModelSize.X, c.ModelSize.Y - 5, width, 4);
                }
            }

            pb_field.Image = b; g = null; b = null;
        }

        Font _fnt = new Font("Calibri Light", 10, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();
        private void DrawDebug()
        {
            const int vs = 15; int k = 0; var b = new Bitmap(pb_debug.Width, pb_debug.Height); var g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            // Common.
            g.DrawString(String.Format("Текущая: {0}", _currentCell == null ? "" : _currentCell.Axial + ""), _fnt, _brush, 0, k * vs, _drawFormat); k++;
            g.DrawString(String.Format("Цель: {0}", _hoveredCell == null ? "" : _hoveredCell.Axial + ""), _fnt, _brush, 0, k * vs, _drawFormat); k++;
            g.DrawString(String.Format("Расстояние: {0}", _distance), _fnt, _brush, 0, k * vs, _drawFormat); k++;
            k++;
            if (_selectedAction != null)
            {
                g.DrawString(String.Format("Дистанция атаки: {0}", _selectedAction.Range), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Блокируется: {0}", _selectedAction.BlockIfEnemyClose), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Откат всего: {0}", _selectedAction.CoolTimeMax), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Откат текущий: {0}", _selectedAction.CoolTime), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Из: {0}", _goesFrom), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("В: {0}", _goesTo), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Распространение: {0}", _selectedAction.ShemeType), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Отсчёт от цели: {0}", _selectedAction.CalcFromTarget), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Включать точку отсчёта: {0}", _selectedAction.IncludeStartPoint), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                k++;
                g.DrawString(String.Format("Доступно для прицеливания: {0}", _aForAction.Count), _fnt, _brush, 0, k * vs, _drawFormat); k++;
                g.DrawString(String.Format("Доступно для урона/эффекта: {0}", _aForApply.Count), _fnt, _brush, 0, k * vs, _drawFormat); k++;
            }

            

            pb_debug.Image = b; g = null; b = null;
        }

        private void b_reset_Click(object sender, EventArgs e)
        {
            _log.Clear();
            _lf.Reset();
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
                int q = toRight ? _map.ArraySize - u.StartPos.Q - 1 : u.StartPos.Q, r = toRight ? _map.ArraySize - u.StartPos.R - 1 : u.StartPos.R;
                _map.Cells[q, r].Unit = u;
                u.CurPos = new Axial(q, r);
                u.ResetCurrent();
            }
        }

        private void InitTurnOrder()
        {
            currentUnit.Set(null);
            var units = new List<Unit>(); units.AddRange(_pls[0].Units); units.AddRange(_pls[1].Units);
            for (int i = 0; i < units.Count; i++)
                units[i].RepMods(CharType.Lane, _rng.Next(1, (int)(units[i].CharCursF(CharType.Initiative) / 4f)) / 100f);
            units = null;
        }

        private void CalcTurns()
        {
            var units = new List<Unit>();
            units.AddRange(_pls[0].Units.Where(unit => unit.CharCursI(CharType.Alive) == 1));
            units.AddRange(_pls[1].Units.Where(unit => unit.CharCursI(CharType.Alive) == 1));

            if (units.Count == 0)
                return;

            for (int i = 0; i < units.Count; i++)
            {
                // TODO Учитывать эффекты.
                units[i].RepMods(CharType.LaneUp, units[i].CharCursF(CharType.Initiative) / _baseLane);
            }

            var turnQueueUnits = new List<Unit>();
            // Заполним полосу хода для первого/первых юнитов.
            while (turnQueueUnits.Count == 0)
            {
                units = units.OrderByDescending(x => x.CharCursF(CharType.Lane)).ThenByDescending(x => x.CharCursF(CharType.Initiative)).ToList();
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].CharCursF(CharType.Lane) >= _baseLane)
                        turnQueueUnits.Add(units[i]);
                    units[i].AddMods(CharType.Lane, units[i].CharCursF(CharType.LaneUp));
                }
            }
            currentUnit.Set(turnQueueUnits[0]);
            var log = _battle.ApplyPassives(turnQueueUnits[0]);
            _lf.AddS(log);
            _log.AddRange(log);

            if (WinCondition())
                return;

            ReSetCurrentParams();

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

        List<BMapCell> _aForMove = new List<BMapCell>();
        List<BMapCell> _aForAction = new List<BMapCell>();
        List<BMapCell> _aForApply = new List<BMapCell>();
        List<BMapCell> _actionRange = new List<BMapCell>();
        List<BMapCell> _movementPath = new List<BMapCell>();
        List<BMapCell> _actionLinePath = new List<BMapCell>();
        List<BMapCell> _actionSheme = new List<BMapCell>();
        List<Unit> _targetsOnLine = new List<Unit>();
        List<string> _log = new List<string>();

        BMapCell _actionStartPoint;
        BMapCell _moveDestination;
        int _goesFrom, _distance, _goesTo;

        

        private void pb_field_MouseMove(object sender, MouseEventArgs e)
        {
            // Clears
            _actionLinePath.Clear();
            _movementPath.Clear();
            _actionSheme.Clear();
            _actionStartPoint = null;
            _moveDestination = null;
            _targetsOnLine.Clear();

            if (_map == null) goto end;
            _hoveredCell = _map.GetCellByXY(e.X, e.Y);
            if (_hoveredCell == null) goto end;
            _distance = _hoveredCell.Cube.DistanceToI(_currentCell.Cube);

            var actionCell = _aForAction.WitchIs(_hoveredCell);
            if (actionCell != null) // Навели на клетку прицеливания.
            {
                
                if (_selectedAction.Range > 1) // Дистанционное дествие.
                {
                    switch (_distance)
                    {
                        case 0: // На себя.
                            _goesFrom = _map.GetDirection(_hoveredCell, e.X, e.Y);
                            break;
                        case 1: // Соседняя клетка.
                            _goesFrom = _map.GetDirection(_hoveredCell, _currentCell);
                            break;
                        default: // Дистанционная клетка.
                            _actionLinePath = _map.GetCellsOnLine(_currentCell.Cube, actionCell.Cube, false, false);
                            _goesFrom = _map.GetDirection(_hoveredCell, _actionLinePath[_actionLinePath.Count - 1]);
                            break;
                    }
                    if (_goesFrom == -1) { _goesTo = -1; goto end; }
                    _goesTo = (_goesFrom + 3) % 6;
                    // Точка отсчёта действия.
                    _actionStartPoint = _selectedAction.CalcFromTarget ? _hoveredCell : _hoveredCell.GetDir(_goesFrom);
                }
                else // Ближнее действие.
                {
                    _goesFrom = _map.GetDirection(_hoveredCell, e.X, e.Y);
                    _goesTo = (_goesFrom + 3) % 6;
                    _actionStartPoint = _selectedAction.CalcFromTarget ? _hoveredCell : _hoveredCell.GetDir(_goesFrom);
                    switch (_distance)
                    {
                        case 0: // На себя.
                            break;
                        case 1: // Соседняя клетка.
                        default: // Дистанционная клетка.
                            _moveDestination = _hoveredCell.GetDir(_goesFrom);
                            if (_moveDestination == null)// Некуда идти.
                            {
                                _actionStartPoint = null;
                                goto end;
                            }
                            if (_aForMove.WitchIs(_moveDestination) == null)// Нельзя переместиться.
                            {
                                if(_moveDestination.Axial != _currentCell.Axial)// Точка атаки не совпадает с позицией атакующего
                                {
                                    _actionStartPoint = null;
                                    _moveDestination = null;
                                    goto end;
                                }
                            }
                            _movementPath.AddRange(_map.GetPath(_currentUnit, _currentCell, _moveDestination, _aForMove));
                            break;
                    }
                }
                
                _actionSheme = _map.GetSheme(_actionStartPoint, _selectedAction, _goesTo, _aForAction);
            }
            else
            {
                // Навели на что-то другое.
                _goesFrom = -1;
                _goesTo = -1;
                var movementCell = _aForMove.WitchIs(_hoveredCell);
                if (movementCell != null)
                {
                    _moveDestination = _hoveredCell;
                    _movementPath.AddRange(_map.GetPath(_currentUnit, _currentCell, _moveDestination, _aForMove));
                }
            }
            end:
            DrawMap();
            DrawDebug();
        }

        private void pb_field_Click(object sender, EventArgs e)
        {
            var a = (MouseEventArgs)e;
            if (a.Button == MouseButtons.Left)
            {
                bool doMove = false, doAtack = false;
                if (_moveDestination != null) // Если есть куда идти - идём.
                {
                    _currentCell.Unit = null;
                    var u = _currentUnit;
                    u.CurPos.Q = _moveDestination.Axial.Q;
                    u.CurPos.R = _moveDestination.Axial.R;
                    _moveDestination.Unit = u;
                    _map.CalcCellsWithUnits();
                    doMove = true;
                }

                if (_selectedAction != null && _actionSheme.Count > 0) // Выбрано действие и есть по кому ударить.
                {
                    var directAttacked = _actionSheme.Intersection(_aForApply);
                    for (int i = 0; i < directAttacked.Count; i++) // Для каждого юнита.
                    {
                        var unit = directAttacked[i].Unit;
                        for (int j = 0; j < _selectedAction.Effects.Length; j++) // Применим эффект.
                        {
                            var log = _battle.ApplyEffect(_currentUnit, unit, _selectedAction.Effects[j]);
                            _lf.AddS(log);
                            _log.AddRange(log);
                        }
                    }
                    doAtack = true;
                }

                if (doMove || doAtack) // Двигались или применяли действие. 
                {
                    // Снимем полоску хода.
                    currentUnit.Unit.AddMods(CharType.Lane, -_baseLane);
                    if (WinCondition())
                        return;
                    // Пересчитаем ходы.
                    CalcTurns();
                }
            }
        }// click.

        private bool WinCondition()
        {
            var pl1 = _pls[0].Units.Any(unit => unit.CharCursI(CharType.Alive) == 1);
            var pl2 = _pls[1].Units.Any(unit => unit.CharCursI(CharType.Alive) == 1);
            if (pl1 && pl2)
                return false;
            else
            {
                if (!pl1) MessageBox.Show("Победил игрок справа");
                if (!pl2) MessageBox.Show("Победил игрок слева");
                return true;
            }
        }
    }// class end.

    
}// namespace.

