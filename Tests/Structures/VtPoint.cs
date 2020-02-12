using System;
using System.Diagnostics;
using Voronoy.Structures;

namespace Tests.Structures
{
    public class VtPoint
    {
        public float X;
        public float Y;

        public VtPoint(VPoint s) : this((float)s.X, (float)s.Y)
        {
        }

        public VtPoint(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static VtPoint operator +(VtPoint a, VtPoint b)
        {
            return new VtPoint(a.X + b.X, a.Y + b.Y);
        }

        public static VtPoint operator -(VtPoint a, VtPoint b)
        {
            return new VtPoint(a.X - b.X, a.Y - b.Y);
        }

        public static VtPoint operator *(VtPoint a, float n)
        {
            return new VtPoint(a.X + n, a.Y + n);
        }

        public static VtPoint operator /(VtPoint a, float n)
        {
            return new VtPoint(a.X / n, a.Y / n);
        }

        
        public static bool operator ==(VtPoint a, VtPoint b)
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

        public static bool operator !=(VtPoint a, VtPoint b)
        {
            return !(a==b);
        }

        public override string ToString()
        {
            return "(" + X + ";" + Y + ")";
        }
    }
}
