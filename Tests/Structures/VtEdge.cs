using System.Diagnostics;
using System.Drawing;
using Voronoy.Structures;

namespace Tests.Structures
{
    public class VtEdge
    {
        public VtPoint S;
        public VtPoint E;
        public VtPoint M;

        public VtEdge(VtPoint s, VtPoint e)
        {
            S = s;
            E = e;
            M = (s + e) / 2;
        }

        public VtEdge(VPoint start, VPoint end)
        {
            S = new VtPoint(start);
            E = new VtPoint(end);
            M = (S + E) / 2;
        }

        public static bool operator ==(VtEdge a, VtEdge b)
        {
            if (ReferenceEquals(a, null) & (ReferenceEquals(b, null)))
                return true;
            if (ReferenceEquals(a, null) || (ReferenceEquals(b, null)))
                return false;
            return (a.S == b.S && a.E == b.E) || (a.E == b.S && a.S == b.E);
        }

        public static bool operator !=(VtEdge a, VtEdge b)
        {
            return !(a==b);
        }

        public override string ToString()
        {
            return "(" + S.X + ";" + S.Y + ")(" + E.X + ";" + E.Y + ")";
        }
    }
}
