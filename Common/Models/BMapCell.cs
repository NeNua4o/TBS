using System.Drawing;

namespace Common.Models
{
    public class BMapCell
    {
        public Axial Axial;
        public Cube Cube;
        public Hex Hex;
        public BMapCell[] Neighbors = new BMapCell[6];
        public Unit Unit;
        public RectangleF ModelSize;
        public RectangleF StepsSize;

        public BMapCell() { }

        public BMapCell(int q, int r)
        {
            Axial = new Axial(q, r);
            Cube = new Cube(Axial);
        }

        public BMapCell GetDir(int direction)
        {
            return Neighbors[direction];
        }
    }
}
