using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Structures;
using Voronoy.Structures;

namespace Tests.Utils
{
    class PointWorker
    {
        private static PointWorker _instance;
        private Random _rng;

        private PointWorker()
        {
            _rng = new Random();
        }

        public static PointWorker GetInstance()
        {
            if (_instance == null)
                _instance = new PointWorker();
            return _instance;
        }

        public List<VtPoint> GetUniqDots(int xmin, int ymin, int xmax, int ymax, int ptCount)
        {
            // Generate dots.
            var res = new List<VtPoint>();
            int k = 0;
            while (k < ptCount)
            {
                // Make dot with uniq pos.
                var newx = _rng.Next(xmin, xmax + 1);
                var newy = _rng.Next(ymin, ymax + 1);
                while (AnyHas(res, newx, newy))
                {
                    newx = _rng.Next(xmin, xmax + 1);
                    newy = _rng.Next(ymin, ymax + 1);
                }
                res.Add(new VtPoint(newx, newy));
                k++;
            }
            return res;
        }

        public List<FortuneSite> DotsToSites(List<VtPoint> dots)
        {
            var result = new List<FortuneSite>();
            for (int i = 0; i < dots.Count; i++)
            {
                var d = dots[i];
                result.Add(new FortuneSite(d.X, d.Y));
            }
            return result;
        }

        private bool AnyHas(List<VtPoint> points, int x, int y)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] == null) continue;
                if (points[i].X == x && points[i].Y == y)
                    return true;
            }
            return false;
        }

        class TempPoly
        {
            public List<VPoint> Points = new List<VPoint>();
            public FortuneSite NewC;
        }

        internal List<FortuneSite> Relax(LinkedList<VEdge> lEdges, float minX, float minY, float maxX, float maxY)
        {
            var edges = lEdges.ToList();
            Dictionary<FortuneSite, TempPoly> lefts = new Dictionary<FortuneSite, TempPoly>();
            Dictionary<FortuneSite, TempPoly> rights = new Dictionary<FortuneSite, TempPoly>();
            TempPoly epoly;
            for (int ec = 0; ec < edges.Count; ec++)
            {
                var edge = edges[ec];
                if (lefts.ContainsKey(edge.Left))
                    epoly = lefts[edge.Left];
                else
                {
                    epoly = new TempPoly();
                    lefts.Add(edge.Left, epoly);
                }
                epoly.Points.Add(edge.Start);
                epoly.Points.Add(edge.End);

                if (rights.ContainsKey(edge.Right))
                    epoly = rights[edge.Right];
                else
                {
                    epoly = new TempPoly();
                    rights.Add(edge.Right, epoly);
                }
                epoly.Points.Add(edge.Start);
                epoly.Points.Add(edge.End);
            }
            foreach (var eright in rights)
            {
                if (lefts.ContainsKey(eright.Key))
                    lefts[eright.Key].Points.AddRange(eright.Value.Points);
                else
                    lefts.Add(eright.Key, eright.Value);
            }

            double xs = double.NaN, ys = double.NaN;
            foreach (var eleft in lefts)
            {
                var left = eleft.Value;
                double newX = 0, newY = 0;
                int xc = 0, yc = 0;
                for (int i = 0; i < left.Points.Count; i++)
                {
                    var pt = left.Points[i];
                    newX += pt.X;
                    newY += pt.Y;
                    if (xc < 2)
                        if (Math.Abs(pt.X - minX) < float.Epsilon || Math.Abs(pt.X - maxX) < float.Epsilon)
                        {
                            xs = pt.X;
                            xc++;
                        }
                    if (yc < 2)
                        if (Math.Abs(pt.Y - minY) < float.Epsilon || Math.Abs(pt.Y - maxY) < float.Epsilon)
                        {
                            ys = pt.Y;
                            yc++;
                        }
                }
                newX /= left.Points.Count;
                newY /= left.Points.Count;
                if (xc == 1 && yc == 1)
                {
                    newX = (newX + xs) / 2;
                    newY = (newY + ys) / 2;
                }
                left.NewC = new FortuneSite(newX, newY);
                continue;
            }
            var result = new List<FortuneSite>();
            foreach (var left in lefts)
            {
                result.Add(left.Value.NewC);
            }
            return result;
        }

        internal List<VtPoint> SitesToDots(List<FortuneSite> sites)
        {
            var result = new List<VtPoint>();
            for (int i = 0; i < sites.Count; i++)
            {
                var d = sites[i];
                result.Add(new VtPoint((float)d.X, (float)d.Y));
            }
            return result;
        }
    }
}
