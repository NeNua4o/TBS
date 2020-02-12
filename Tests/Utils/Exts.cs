using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tests.Structures;

namespace Tests.Utils
{
    public static class Exts
    {
        public static List<VtEdge> MissedEdges(this List<VtEdge> edges, float minX, float minY, float maxX, float maxY)
        {
            List<VtEdge> result = new List<VtEdge>();
            var points = new List<VtPoint>();
            for (int i = 0; i < edges.Count; i++)
            {
                var edge = edges[i];

                var ex = points.FirstOrDefault(point => point == edge.S);
                if (ex != null) points.Remove(ex);
                else points.Add(edge.S);

                ex = points.FirstOrDefault(point => point == edge.E);
                if (ex != null) points.Remove(ex);
                else points.Add(edge.E);
            }

            if (points.Count != 2) return result;

            var pt1 = points[0];
            var pt2 = points[1];

            if (Math.Abs(pt1.X - pt2.X) < float.Epsilon || Math.Abs(pt1.Y - pt2.Y) < float.Epsilon)
            {
                result.Add(new VtEdge(pt1, pt2));
                return result;
            }

            float x = 0, y = 0;
            if (Math.Abs(pt1.X - minX) < float.Epsilon || Math.Abs(pt1.X - maxX) < float.Epsilon) x = pt1.X;
            else
                if (Math.Abs(pt2.X - minX) < float.Epsilon || Math.Abs(pt2.X - maxX) < float.Epsilon) x = pt2.X;
            if (Math.Abs(pt1.Y - minY) < float.Epsilon || Math.Abs(pt1.Y - maxY) < float.Epsilon) y = pt1.Y;
            else
                if (Math.Abs(pt2.Y - minY) < float.Epsilon || Math.Abs(pt2.Y - maxY) < float.Epsilon) y = pt2.Y;

            var pt = new VtPoint(x, y);
            result.Add(new VtEdge(pt1, pt));
            result.Add(new VtEdge(pt, pt2));
            return result;
        }

        public static List<VtPoint> Relax(this List<VtPolygon> polys)
        {
            var result = new List<VtPoint>();

            for (int pc = 0; pc < polys.Count(); pc++)
            {
                var poly = polys[pc];
                var points = new List<VtPoint>();
                for (int ec = 0; ec < poly.Edges.Count; ec++)
                {
                    var edge = poly.Edges[ec];
                    if (points.All(point => point != edge.S)) points.Add(edge.S);
                    if (points.All(point => point != edge.E)) points.Add(edge.E);
                }

                var x = points.Sum(point => point.X) / points.Count;
                var y = points.Sum(point => point.Y) / points.Count;
                result.Add(new VtPoint(x, y));
            }

            return result;
        }

        
    }
}
