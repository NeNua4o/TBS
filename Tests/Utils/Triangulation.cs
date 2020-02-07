using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Structures;

namespace Tests.Utils
{
    public class Triangulation
    {
        public List<VPoint> Points;
        public List<VTriangle> Triangles;
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;

        public Triangulation(List<VPoint> points)
        {
            Points = new List<VPoint>();
            Points.AddRange(points);
        }

        internal List<VTriangle> CalcDelanua(float border = 10)
        {
            // Bound box
            MinX = Points.Min(point => point.X);
            MinY = Points.Min(point => point.Y);
            MaxX = Points.Max(point => point.X);
            MaxY = Points.Max(point => point.Y);

            Triangles = new List<VTriangle>();

            var triangle = new VTriangle(
                new VPoint(MinX - border, MinY - border),
                new VPoint(MinX - border, MaxY + border),
                new VPoint(MaxX + border, MinY - border)
                );
            Triangles.Add(triangle);
            triangle = new VTriangle(
                new VPoint(MinX - border, MaxY + border),
                new VPoint(MaxX + border, MinY - border),
                new VPoint(MaxX + border, MaxY + border)
                );
            Triangles.Add(triangle);

            for (int i = 0; i < Points.Count; i++)
            {
                var pt = Points[i];
                DelanuaStep(ref Triangles, pt);
            }
            return Triangles;
        }

        private void DelanuaStep(ref List<VTriangle> triangles, VPoint newPoint)
        {
            var badTriangles = new List<VTriangle>();
            var edges = new List<VEdge>();
            FindInvalidTriangles(triangles, newPoint, out badTriangles, out edges);
            edges = RemoveDuplicateEdges(edges);
            triangles = RemoveBadTriangles(triangles, badTriangles);
            FillHole(ref triangles, edges, newPoint);
        }

        internal void FindInvalidTriangles(List<VTriangle> triangles, VPoint newPoint, out List<VTriangle> badTriangles, out List<VEdge> allEdges)
        {
            badTriangles = new List<VTriangle>();
            allEdges = new List<VEdge>();
            for (int  triCounter= 0; triCounter < triangles.Count; triCounter++)
            {
                if (EMath.Len(triangles[triCounter].CirC, newPoint) < triangles[triCounter].CirR)
                {
                    badTriangles.Add(triangles[triCounter]);
                    allEdges.Add(triangles[triCounter].AB);
                    allEdges.Add(triangles[triCounter].BC);
                    allEdges.Add(triangles[triCounter].AC);
                }
            }
        }

        internal List<VEdge> RemoveDuplicateEdges(List<VEdge> edges)
        {
            var polygonHole = new List<VEdge>();
            for (int edgesCounter = 0; edgesCounter < edges.Count; edgesCounter++)
            {
                var edge = edges[edgesCounter];
                VEdge edgeToRemove = null;
                for (int polyCounter = 0; polyCounter < polygonHole.Count; polyCounter++)
                {
                    if (polygonHole[polyCounter] == edge)
                    {
                        edgeToRemove = polygonHole[polyCounter];
                        break;
                    }
                }
                if (edgeToRemove == null) polygonHole.Add(edge);
                else polygonHole.Remove(edgeToRemove);
            }
            return polygonHole;
        }

        internal List<VTriangle> RemoveBadTriangles(List<VTriangle> triangles, List<VTriangle> badTriangles)
        {
            var result = new List<VTriangle>();
            for (int i = 0; i < triangles.Count; i++)
            {
                bool bad = false;
                for (int j = 0; j < badTriangles.Count; j++)
                {
                    if (triangles[i] == badTriangles[j])
                        bad = true;
                }
                if (bad)
                    continue;
                result.Add(triangles[i]);
            }
            return result;
        }

        private void FillHole(ref List<VTriangle> triangles, List<VEdge> polygonHole, VPoint newPoint)
        {
            for (int polyCounter = 0; polyCounter < polygonHole.Count; polyCounter++)
                triangles.Add(new VTriangle(polygonHole[polyCounter].S, polygonHole[polyCounter].E, newPoint));
        }

        
    }
}
