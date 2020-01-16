using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VoronoiLib.Structures
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

        public void OrderEdges()
        {
            if (Cell.Count == 0)
                return;
            double eps = 0.00001;
            var newEdges = new List<VEdge>();
            var ce = Cell[0];
            newEdges.Add(ce);
            List<int> skip = new List<int>();
            while (skip.Count < Cell.Count)
            {
                bool foundAny = false;
                for (int i = 1; i < Cell.Count; i++)
                {
                    if (skip.Contains(i))
                        continue;
                    VEdge ne = Cell[i];
                    if (Math.Abs(ce.End.X - ne.Start.X) < eps && Math.Abs(ce.End.Y - ne.Start.Y) < eps)
                    {
                        newEdges.Add(ne);
                        ce = ne;
                        skip.Add(i);
                        foundAny = true;
                    }
                    else
                    {
                        if (Math.Abs(ce.End.X - ne.End.X) < eps && Math.Abs(ce.End.Y - ne.End.Y) < eps)
                        {
                            var t = ne.End;
                            ne.End = ne.Start;
                            ne.Start = t;
                            newEdges.Add(ne);
                            ce = ne;
                            skip.Add(i);
                            foundAny = true;
                        }
                    }
                }
                if (!foundAny)
                    break;
            }

            if (Cell.Count != newEdges.Count)
            {
                while (skip.Count < Cell.Count)
                {
                    bool foundAny = false;
                    for (int i = 1; i < Cell.Count; i++)
                    {
                        if (skip.Contains(i))
                            continue;
                        VEdge ne = Cell[i];
                        if (Math.Abs(ce.Start.X - ne.End.X) < eps && Math.Abs(ce.Start.Y - ne.End.Y) < eps)
                        {
                            newEdges.Add(ne);
                            ce = ne;
                            skip.Add(i);
                            foundAny = true;
                        }
                        else
                        {
                            if (Math.Abs(ce.Start.X - ne.Start.X) < eps && Math.Abs(ce.Start.Y - ne.Start.Y) < eps)
                            {
                                var t = ne.End;
                                ne.End = ne.Start;
                                ne.Start = t;
                                newEdges.Add(ne);
                                ce = ne;
                                skip.Add(i);
                                foundAny = true;
                            }
                        }
                    }
                    if (!foundAny)
                        break;
                }
            }
            if (Cell.Count != newEdges.Count)
                Debug.WriteLine("nope");
            Cell = newEdges;
        }
    }
}
