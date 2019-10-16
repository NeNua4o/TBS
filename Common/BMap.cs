using Common.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Common
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

            // Remove top-left corner
            for (int row = 0; row < (sideCellCount - 1); row++)
                for (int col = 0; col < (sideCellCount - row - 1); col++)
                    Cells[col, row] = null;

            // Remove bottom-right corner
            for (int row = sideCellCount; row < ArraySize; row++)
                for (int col = (ArraySize - row + sideCellCount - 1); col < ArraySize; col++)
                    Cells[col, row] = null;

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
                        /*
                        Cells[col, row].UiRec = new RectangleF(hex.C.X - 30, hex.C.Y - 30, 60, 60);
                        Cells[col, row].StRec = new RectangleF(hex.C.X - 15, hex.C.Y - 15, 30, 30);
                        */
                        neighs.Clear();
                        if ((col + 1).Between(-1, ArraySize) && (row + 0).Between(-1, ArraySize) && Cells[col + 1, row + 0] != null) neighs.Add(Cells[col + 1, row + 0]); else neighs.Add(null);
                        if ((col + 1).Between(-1, ArraySize) && (row - 1).Between(-1, ArraySize) && Cells[col + 1, row - 1] != null) neighs.Add(Cells[col + 1, row - 1]); else neighs.Add(null);
                        if ((col + 0).Between(-1, ArraySize) && (row - 1).Between(-1, ArraySize) && Cells[col + 0, row - 1] != null) neighs.Add(Cells[col + 0, row - 1]); else neighs.Add(null);
                        if ((col - 1).Between(-1, ArraySize) && (row + 0).Between(-1, ArraySize) && Cells[col - 1, row + 0] != null) neighs.Add(Cells[col - 1, row + 0]); else neighs.Add(null);
                        if ((col - 1).Between(-1, ArraySize) && (row + 1).Between(-1, ArraySize) && Cells[col - 1, row + 1] != null) neighs.Add(Cells[col - 1, row + 1]); else neighs.Add(null);
                        if ((col + 0).Between(-1, ArraySize) && (row + 1).Between(-1, ArraySize) && Cells[col + 0, row + 1] != null) neighs.Add(Cells[col + 0, row + 1]); else neighs.Add(null);
                        Cells[col, row].Dirs = neighs.ToArray();

                    }
                }
            }
            neighs = null;

            // Prepare layout
            Width = (int)Math.Round(HexWidth * ArraySize + 1, MidpointRounding.AwayFromZero);
            Height = (int)Math.Round(HexHeight + HeightSpacing * (ArraySize - 1) + 1, MidpointRounding.AwayFromZero);

            Bitmap b = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(b);
            //g.FillRectangle(Brushes.White, new Rectangle(0, 0, b.Width, b.Height));
            for (int r = 0; r < ArraySize; r++)
                for (int c = 0; c < ArraySize; c++)
                    if (Cells[c, r] != null)
                        g.DrawPolygon(Pens.Gray, Cells[c, r].Hex.K);
            g.Flush();
            GridLayout = new Bitmap(b);
            g = null;
            b = null;

        }
    }

    public class BMapCell
    {
        public Axial Axial;
        public Cube Cube;
        public Hex Hex;
        public BMapCell[] Dirs = new BMapCell[6];

        public BMapCell() { }

        public BMapCell(int q, int r)
        {
            Axial = new Axial(q, r);
            Cube = new Cube(Axial);
        }
    }
}