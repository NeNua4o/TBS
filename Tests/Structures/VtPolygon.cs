using System.Collections.Generic;

namespace Tests.Structures
{
    public class VtPolygon
    {
        public List<VtEdge> Edges;
        public VtPoint C;
        public VtPoint Ac;

        public VtPolygon()
        {
            Edges = new List<VtEdge>();
        }
    }
}
