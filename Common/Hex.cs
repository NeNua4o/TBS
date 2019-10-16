using System;
using System.Drawing;

namespace Common
{
    public class Hex
    {
        public PointF C;
        public PointF[] K = new PointF[7];
        public PointF[] K10 = new PointF[7];
        public PointF[] K20 = new PointF[7];
        public PointF[] K30 = new PointF[7];

        public float S;

        public Hex(PointF center, float size) : this(center.X, center.Y, size) { }

        public Hex(float x, float y, float size)
        {
            S = size;
            C = new PointF(x, y);
            CalcCorners();
        }

        private void CalcCorners()
        {
            for (int i = 0; i < 7; i++) K[i] = CalcCorner(C, S, i);
            for (int i = 0; i < 7; i++) K10[i] = CalcCorner(C, (float)(S - S * 0.1), i);
            for (int i = 0; i < 7; i++) K20[i] = CalcCorner(C, (float)(S - S * 0.2), i);
            for (int i = 0; i < 7; i++) K30[i] = CalcCorner(C, (float)(S - S * 0.3), i);
        }

        public PointF CalcCorner(PointF center, float size, int corner)
        {
            var angle_deg = 60 * corner + 30;
            var angle_rad = Math.PI / 180 * angle_deg;
            return new PointF(center.X + size * (float)Math.Cos(angle_rad), center.Y + size * (float)Math.Sin(angle_rad));
        }

        public override string ToString()
        {
            return "(" + C + ") " + S;
        }
    }
}
