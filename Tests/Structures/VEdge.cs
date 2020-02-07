using System.Diagnostics;
using System.Drawing;

namespace Tests.Structures
{
    public class VEdge
    {
        public VPoint S;
        public VPoint E;
        public VPoint M;

        public VEdge(VPoint s, VPoint e)
        {
            S = s;
            E = e;
            M = (s + e) / 2;
        }

        public static bool operator ==(VEdge a, VEdge b)
        {
            if (ReferenceEquals(a, null) & (ReferenceEquals(b, null)))
                return true;
            if (ReferenceEquals(a, null) || (ReferenceEquals(b, null)))
                return false;
            return (a.S == b.S && a.E == b.E) || (a.E == b.S && a.S == b.E);
        }

        public static bool operator !=(VEdge a, VEdge b)
        {
            return !(a==b);
        }

        public override string ToString()
        {
            return "(" + S.X + ";" + S.Y + ")(" + E.X + ";" + E.Y + ")";
        }
    }
}
