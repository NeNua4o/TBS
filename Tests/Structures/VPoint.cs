using System;
using System.Diagnostics;

namespace Tests.Structures
{
    public class VPoint
    {
        public float X;
        public float Y;

        public VPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static VPoint operator +(VPoint a, VPoint b)
        {
            return new VPoint(a.X + b.X, a.Y + b.Y);
        }

        public static VPoint operator -(VPoint a, VPoint b)
        {
            return new VPoint(a.X - b.X, a.Y - b.Y);
        }

        public static VPoint operator *(VPoint a, float n)
        {
            return new VPoint(a.X + n, a.Y + n);
        }

        public static VPoint operator /(VPoint a, float n)
        {
            return new VPoint(a.X / n, a.Y / n);
        }

        
        public static bool operator ==(VPoint a, VPoint b)
        {
            if (ReferenceEquals(a, null) & (ReferenceEquals(b, null)))
                return true;
            if (ReferenceEquals(a, null) || (ReferenceEquals(b, null)))
                return false;
            var dx = Math.Abs(a.X - b.X);
            var dy = Math.Abs(a.Y - b.Y);
            var res = dx < float.Epsilon && dy < float.Epsilon;
            return res;
        }

        public static bool operator !=(VPoint a, VPoint b)
        {
            return !(a==b);
        }

        public override string ToString()
        {
            return "(" + X + ";" + Y + ")";
        }
    }
}
