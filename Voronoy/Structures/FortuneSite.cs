using Common.Extensions;
using System;
using System.Collections.Generic;

namespace Voronoy.Structures
{
    public class FortuneSite
    {
        public double X { get; }
        public double Y { get; }

        public List<VEdge> Cell { get; private set; }

        public List<FortuneSite> Neighbors { get; private set; }

        public FortuneSite(double x, double y)
        {
            X = x;
            Y = y;
            Cell = new List<VEdge>();
            Neighbors = new List<FortuneSite>();
        }

        public bool Eq(FortuneSite site)
        {
            return X.Eq(site.X) && Y.Eq(site.Y);
        }
    }
}
