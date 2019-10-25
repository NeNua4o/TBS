using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class PointFExtensions
    {
        public static float DistanceTo(this PointF a, int x, int y)
        {
            return (x - a.X) * (x - a.X) + (y - a.Y) * (y - a.Y);
        }

        public static int Angle(this PointF a, int x, int y)
        {
            var xd = x - a.X;
            var yd = y - a.Y;
            var angle = Math.Atan(yd / xd) * 180 / Math.PI;
            angle = (xd > 0) ? angle - 30 : angle - 210;
            if (angle > 0) angle -= 360;
            return (int)(angle * -1);
        }
    }
}
