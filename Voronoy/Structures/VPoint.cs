using Common.Extensions;

namespace Voronoy.Structures
{
    public class VPoint
    {
        public double X { get; }
        public double Y { get; }

        internal VPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public bool Eq(VPoint point)
        {
            return X.Eq(point.X) && Y.Eq(point.Y);
        }
    }
}
