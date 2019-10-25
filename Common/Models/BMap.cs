﻿using Common.Extensions;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Common.Models
{
    public class BMap
    {
        public int SideCellCount;
        public int ArraySize;
        public BMapCell[,] Cells;
        public float HexOutRadius, HexInRadius;
        public float HexWidth, HexHeight;
        public float WidthSpacing, HeightSpacing;
        public int Width, Height;
        public Image GridLayout;
        public List<BMapCell> CellsWithUnit = new List<BMapCell>();

        BMapCell[] _notNullCells;

        public BMap(int sideCellCount, float hexRadius)
        {
            SideCellCount = sideCellCount;
            ArraySize = sideCellCount * 2 - 1;
            HexOutRadius = hexRadius;

            // Fill all
            Cells = new BMapCell[ArraySize, ArraySize];
            for (int row = 0; row < ArraySize; row++)
                for (int col = 0; col < ArraySize; col++)
                    Cells[col, row] = new BMapCell(col, row);

            int notNullCellsCount = ArraySize * ArraySize;
            // Remove top-left corner
            for (int row = 0; row < (sideCellCount - 1); row++)
                for (int col = 0; col < (sideCellCount - row - 1); col++)
                {
                    notNullCellsCount--;
                    Cells[col, row] = null;
                }

            // Remove bottom-right corner
            for (int row = sideCellCount; row < ArraySize; row++)
                for (int col = (ArraySize - row + sideCellCount - 1); col < ArraySize; col++)
                {
                    notNullCellsCount--;
                    Cells[col, row] = null;
                }

            _notNullCells = new BMapCell[notNullCellsCount];
            notNullCellsCount = 0;
            for (int row = 0; row < ArraySize; row++)
                for (int col = 0; col < ArraySize; col++)
                    if (Cells[col, row] != null)
                    {
                        _notNullCells[notNullCellsCount] = Cells[col, row];
                        notNullCellsCount++;
                    }


            // [X] [X] [ ] [ ] [ ]
            // [X] [ ] [ ] [ ] [ ]
            // [ ] [ ] [ ] [ ] [ ]
            // [ ] [ ] [ ] [ ] [X]
            // [ ] [ ] [ ] [X] [X]

            HexWidth = (float)Math.Sqrt(3) * HexOutRadius;
            HexInRadius = HexWidth / 2f;
            HexHeight = 2f * HexOutRadius;

            WidthSpacing = HexWidth;
            HeightSpacing = HexHeight * 3f / 4f;

            // Fill hexes
            List<BMapCell> neighs = new List<BMapCell>();
            for (int row = 0; row < ArraySize; row++)
            {
                for (int col = 0; col < ArraySize; col++)
                {
                    if (Cells[col, row] != null)
                    {
                        var hex = new Hex(
                            new PointF(
                                HexWidth + (col - (ArraySize - sideCellCount - 1)) * WidthSpacing + row * HexInRadius,
                                HexOutRadius + row * HeightSpacing), HexOutRadius
                                );
                        Cells[col, row].Hex = hex;
                        Cells[col, row].ModelSize = new RectangleF(hex.Center.X - (HexWidth * 0.9f) / 2f, hex.Center.Y - (HexWidth * 0.9f) / 2f, HexWidth * 0.9f, HexWidth * 0.9f);
                        Cells[col, row].StepsSize = new RectangleF(hex.Center.X - (HexWidth * 0.4f) / 2f, hex.Center.Y - (HexWidth * 0.4f) / 2f, HexWidth * 0.4f, HexWidth * 0.4f);
                        neighs.Clear();
                        if ((col + 1).Between(-1, ArraySize) && (row + 0).Between(-1, ArraySize)) neighs.Add(Cells[col + 1, row + 0]); else neighs.Add(null);
                        if ((col + 1).Between(-1, ArraySize) && (row - 1).Between(-1, ArraySize)) neighs.Add(Cells[col + 1, row - 1]); else neighs.Add(null);
                        if ((col + 0).Between(-1, ArraySize) && (row - 1).Between(-1, ArraySize)) neighs.Add(Cells[col + 0, row - 1]); else neighs.Add(null);
                        if ((col - 1).Between(-1, ArraySize) && (row + 0).Between(-1, ArraySize)) neighs.Add(Cells[col - 1, row + 0]); else neighs.Add(null);
                        if ((col - 1).Between(-1, ArraySize) && (row + 1).Between(-1, ArraySize)) neighs.Add(Cells[col - 1, row + 1]); else neighs.Add(null);
                        if ((col + 0).Between(-1, ArraySize) && (row + 1).Between(-1, ArraySize)) neighs.Add(Cells[col + 0, row + 1]); else neighs.Add(null);
                        Cells[col, row].Neighbors = neighs.ToArray();
                    }
                }
            }
            neighs = null;

            // Prepare layout
            Width = (int)Math.Round(HexWidth * ArraySize + 1, MidpointRounding.AwayFromZero);
            Height = (int)Math.Round(HexHeight + HeightSpacing * (ArraySize - 1) + 1, MidpointRounding.AwayFromZero);

            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, b.Width, b.Height));
            for (int r = 0; r < ArraySize; r++)
                for (int c = 0; c < ArraySize; c++)
                    if (Cells[c, r] != null)
                        g.DrawPolygon(Pens.Gray, Cells[c, r].Hex.K);
            g.Flush();
            GridLayout = new Bitmap(b);
            g = null;
            b = null;

        }

        public List<BMapCell> GetCellsWithUnits()
        {
            return CellsWithUnit;
        }

        public BMapCell Cell(Axial position)
        {
            return Cells[position.Q, position.R];
        }

        public bool IsCell(Axial axial)
        {
            return IsCell(axial.Q, axial.R);
        }

        public bool IsCell(int q, int r)
        {
            for (int i = 0; i < _notNullCells.Length; i++)
                if (_notNullCells[i].Axial.Q == q && _notNullCells[i].Axial.R == r)
                    return true;
            return false;
        }

        public void CalcCellsWithUnits()
        {
            CellsWithUnit.Clear();
            for (int i = 0; i < _notNullCells.Length; i++)
                if (_notNullCells[i].Unit != null)
                    CellsWithUnit.Add(_notNullCells[i]);
        }

        public BMapCell GetCellByXY(int x, int y, bool quick = true)
        {
            if (quick)
            {
                var qr = GetQRbyXY(x, y);
                if (IsCell(qr))
                    return Cell(qr);
            }
            else
            {
                float r2 = HexInRadius * HexInRadius;
                for (int i = 0; i < _notNullCells.Length; i++)
                    if (_notNullCells[i].Hex.Center.DistanceTo(x, y) <= r2)
                        return _notNullCells[i];
            }
            return null;
        }

        public Axial GetQRbyXY(int x, int y)
        {
            var q = (int)Math.Round((Math.Sqrt(3) / 3f * (x + WidthSpacing) - 1f / 3f * (y - HexOutRadius)) / HexOutRadius);
            var r = (int)Math.Round((2f / 3f * (y - HexOutRadius)) / HexOutRadius);
            return new Axial(q, r);
        }

        public List<BMapCell> GetCellsOnLine(Cube start, Cube end, bool incStart = true, bool incEnd = true)
        {
            lock (this)
            {
                var result = new List<BMapCell>();
                var cubes = CubeUtils.GetCubesOnLine(start, end.Add(1e-6f, 2e-6f, -3e-6f), incStart, incEnd);
                for (int i = 0; i < cubes.Count; i++)
                {
                    var axial = cubes[i].ToAxial();
                    var cell = Cells[axial.Q, axial.R];
                    result.Add(cell);
                }
                cubes = null;
                return result;
            }
        }

        public int GetDirection(BMapCell c, int x, int y)
        {
            return c.Hex.Center.Angle(x, y) / 60;
        }

        public int GetDirection(BMapCell start, BMapCell target)
        {
            for (int i = 0; i < 6; i++)
                if (start.Neighbors[i] != null && start.Neighbors[i].Axial == target.Axial)
                    return i;
            return -1;
        }

        public List<BMapCell> GetPath(Unit unit, BMapCell start, BMapCell goal, List<BMapCell> moveAllowed)
        {
            if (unit.Chars.MoveType == MoveTypes.Walk)
            {
                var frontier = new Queue<BMapCell>(); //frontier = Queue()
                frontier.Enqueue(start); //frontier.put(start )
                var came_from = new Dictionary<BMapCell, BMapCell>(); //came_from = {}
                                                                    //came_from.Add(new KeyValuePair<BMapCell, BMapCell>(start, null));// came_from[start] = None
                BMapCell current;
                while (frontier.Count > 0) //while not frontier.empty():
                {
                    current = frontier.Dequeue(); //current = frontier.get()
                    if (current.Axial == goal.Axial) //if current == goal:
                        break; //break
                    for (int i = 0; i < current.Neighbors.Length; i++) //for next in graph.neighbors(current):
                    {
                        var next = current.Neighbors[i];
                        if (next == null) continue;
                        if (!moveAllowed.Any(x => x.Axial == next.Axial)) continue;
                        if (came_from.All(x => x.Key.Axial != next.Axial)) //if next not in came_from:
                        {
                            frontier.Enqueue(next); //frontier.put(next)
                            came_from.Add(next, current);//came_from[next] = current
                        }
                    }
                }
                current = goal; //current = goal 
                var res = new List<BMapCell>(); //path = []
                while (current != start) //while current != start:
                {
                    res.Add(came_from[current]); //path.append(current)
                    current = came_from[current]; //current = came_from[current]
                }
                res.Reverse();
                res.Add(goal);//path.append(start) # optional
                if (res.Count > 1)
                    res.RemoveAt(0);
                return res;
            }
            else
            {
                return new List<BMapCell>() { goal };
            }
        }

        public List<BMapCell> GetCellsInRange(Axial c, int n)
        {
            var result = new List<BMapCell>();
            var circle = c.GetInRange(n);
            for (int i = 0; i < circle.Count; i++)
                for (int j = 0; j < _notNullCells.Length; j++)
                    if (circle[i] == _notNullCells[j].Axial)
                    {
                        result.Add(_notNullCells[j]);
                        break;
                    }
            return result;
        }

        public List<BMapCell> GetCellsToMove(BMapCell start, int range, MoveTypes moveType)
        {
            if (moveType == MoveTypes.Walk)
            {
                var visited = new List<BMapCell>();// # set of hexes
                //visited.Add(start);
                var fringes = new List<List<BMapCell>>();//var fringes = [] # array of arrays of hexes
                fringes.Add(new List<BMapCell>() { start });//fringes.append([start])
                for (int k = 1; k <= range; k++)//for each 1 < k ≤ movement:
                {
                    fringes.Add(new List<BMapCell>());//fringes.append([])
                    for (int fc = 0; fc < fringes[k - 1].Count; fc++)//for each hex in fringes[k - 1]:
                    {
                        var hex = fringes[k - 1][fc];
                        for (int dir = 0; dir < 6; dir++)//for each 0 ≤ dir < 6:
                        {
                            var neighbor = hex.GetDir(dir);//var neighbor = hex_neighbor(hex, dir)
                            if (neighbor == null) continue;
                            if (!visited.Contains(neighbor) && neighbor.Unit == null)//if neighbor not in visited and not blocked:
                            {
                                visited.Add(neighbor);//add neighbor to visited
                                fringes[k].Add(neighbor);//fringes[k].append(neighbor)
                            }
                        }
                    }
                }
                return visited;
            }
            else
            {
                var res = new List<BMapCell>();
                var all = GetCellsInRange(start.Axial, range);
                for (int i = 0; i < all.Count; i++)
                {
                    if (all[i].Unit == null)
                        res.Add(all[i]);
                }
                return res;
            }
        }


        public List<BMapCell> GetCellsAvailableToAction(Unit owner, BMapCell ownerCell, Act a, int n, bool forHero = false)
        {
            var res = new List<BMapCell>();
            if (a == null || ownerCell == null || ownerCell.Unit == null)
                return res;
            if (a.BlockIfEnemyClose)
            {
                for (int i = 0; i < 6; i++)
                    if (ownerCell.Neighbors[i] != null && ownerCell.Neighbors[i].Unit != null && ownerCell.Unit.TeamId != ownerCell.Neighbors[i].Unit.TeamId)
                        return res;
            }

            var all = GetCellsInRange(ownerCell.Axial, n);
            if(a.Targetting.Allies)
            {
                res.AddRange(all.Where(cell =>
                cell.Unit != null &&
                cell.Unit.TeamId == owner.TeamId &&
                (a.Targetting.Dead ? cell.Unit.Chars.Alive == 1 : cell.Unit.Chars.Alive == 1)
                ));
            }
            if (a.Targetting.Enemies)
            {
                res.AddRange(all.Where(cell =>
                cell.Unit != null &&
                cell.Unit.TeamId != owner.TeamId &&
                (a.Targetting.Dead ? cell.Unit.Chars.Alive == 1 : cell.Unit.Chars.Alive == 1)
                ));
            }
            if (a.Targetting.MapCells)
            {
                res.AddRange(all.Where(cell =>
                cell.Unit == null
                ));
            }
            if (a.Targetting.Self)
            {
                res.Add(ownerCell);
            }
            return res;
        }


        public List<BMapCell> GetSheme(BMapCell actionStart, Act action, int dir, int revDir, List<BMapCell> availableToAction)
        {
            var res = new List<BMapCell>();
            switch (action.ShemeType)
            {
                case ShemeTypes.Sheme:
                    res.AddRange(GetSheme(actionStart, action.Sheme, dir));
                    break;
                case ShemeTypes.One:
                    break;
                case ShemeTypes.All:
                    res.AddRange(availableToAction);
                    break;
                default:
                    break;
            }
            if (action.Rad > 1)
                res.Insert(0, actionStart);
            return res;
        }

        public List<BMapCell> GetSheme(BMapCell attackPoint, int[] sheme, int dir)
        {
            var res = new List<BMapCell>();
            if (attackPoint == null) return res;
            // GetRingByRing
            for (int shemeNumber = 0; shemeNumber < sheme.Length; shemeNumber++)
            {
                List<ECube> ring = CubeUtils.GetRing(attackPoint.Cube, dir, shemeNumber + 1);
                int left = 0, right = ring.Count, curPos = 0, inSheme = 0;
                bool isLeft = true;

                while (inSheme < sheme[shemeNumber])
                {
                    var ax = ring[curPos].Axial;
                    if (IsCell(ax) && Cells[ax.Q, ax.R] != null && res.All(x => x.Axial != ax))
                        res.Add(Cells[ax.Q, ax.R]);
                    inSheme++;
                    if (isLeft)
                    {
                        isLeft = false;
                        left++;
                        curPos = left;
                    }
                    else
                    {
                        isLeft = true;
                        right--;
                        curPos = right;
                    }
                }
            }
            return res;
        }


    }// Class end.

    
}// Namesapce declaration.