using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Structures;

namespace Tests.Utils
{
    public class Triangulation
    {
        public List<VtPoint> Points;
        public List<VtTriangle> Triangles;
        public List<VtPolygon> Polygons;
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;

        public Triangulation(List<VtPoint> points)
        {
            Points = new List<VtPoint>();
            Points.AddRange(points);
        }


        internal List<VtPolygon> CalcVoronoy()
        {
            var result = new List<VtPolygon>();
            for (int i = 0; i < Points.Count; i++)
            //for (int i = 0; i < 1; i++)
            {
                var pt = Points[i];
                var poly = new VtPolygon();
                List<VtTriangle> trs = GetTriangles(Triangles, pt);
                VtTriangle fTr = trs[0];
                VtTriangle sTr = GetNeighbor(fTr, trs);
                poly.Edges.Add(new VtEdge(fTr.CirC, sTr.CirC));
                for (int j = 2; j < trs.Count+1; j++)
                {
                    VtTriangle tTr = GetNeighbor(fTr, sTr, trs);
                    poly.Edges.Add(new VtEdge(sTr.CirC, tTr.CirC));
                    fTr = sTr;
                    sTr = tTr;
                }
                //poly.Edges.Add(new VEdge(poly.Edges.Last().E, poly.Edges.Last().S));
                result.Add(poly);
            }
            return result;
        }

        public static VtTriangle GetNeighbor(VtTriangle fTri, List<VtTriangle> tries)
        {
            for (int i = 0; i < tries.Count; i++)
            {
                var tri = tries[i];
                // same - skip
                if (fTri == tri)
                    continue;
                if (fTri.HaveSameEdge(tri))
                    return tri;
            }
            return null;
        }

        public static VtTriangle GetNeighbor(VtTriangle fTri, VtTriangle sTri, List<VtTriangle> tries)
        {
            for (int i = 0; i < tries.Count; i++)
            {
                var tri = tries[i];
                // same - skip
                if (fTri == tri || sTri == tri)
                    continue;
                if (sTri.HaveSameEdge(tri))
                    return tri;
            }
            return null;
        }

        public static List<VtTriangle> GetTriangles(List<VtTriangle> triangles, VtPoint pt)
        {
            var result = new List<VtTriangle>();
            for (int i = 0; i < triangles.Count; i++)
            {
                var tr = triangles[i];
                if (tr.A == pt || tr.B == pt || tr.C == pt)
                    result.Add(tr);
            }
            return result;
        }

        internal List<VtTriangle> CalcDelanua(float border = 10)
        {
            // Bound box
            MinX = Points.Min(point => point.X);
            MinY = Points.Min(point => point.Y);
            MaxX = Points.Max(point => point.X);
            MaxY = Points.Max(point => point.Y);

            Triangles = new List<VtTriangle>();

            var triangle = new VtTriangle(
                new VtPoint(MinX - border, MinY - border),
                new VtPoint(MinX - border, MaxY + border),
                new VtPoint(MaxX + border, MinY - border)
                );
            Triangles.Add(triangle);
            triangle = new VtTriangle(
                new VtPoint(MinX - border, MaxY + border),
                new VtPoint(MaxX + border, MinY - border),
                new VtPoint(MaxX + border, MaxY + border)
                );
            Triangles.Add(triangle);

            for (int i = 0; i < Points.Count; i++)
            {
                var pt = Points[i];
                DelanuaStep(ref Triangles, pt);
            }
            return Triangles;
        }

        public static void DelanuaStep(ref List<VtTriangle> triangles, VtPoint newPoint)
        {
            var badTriangles = new List<VtTriangle>();
            var edges = new List<VtEdge>();
            FindInvalidTriangles(triangles, newPoint, out badTriangles, out edges);
            edges = RemoveDuplicateEdges(edges);
            triangles = RemoveBadTriangles(triangles, badTriangles);
            FillHole(ref triangles, edges, newPoint);
        }

        internal static void FindInvalidTriangles(List<VtTriangle> triangles, VtPoint newPoint, out List<VtTriangle> badTriangles, out List<VtEdge> allEdges)
        {
            badTriangles = new List<VtTriangle>();
            allEdges = new List<VtEdge>();
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

        internal static List<VtEdge> RemoveDuplicateEdges(List<VtEdge> edges)
        {
            var polygonHole = new List<VtEdge>();
            for (int edgesCounter = 0; edgesCounter < edges.Count; edgesCounter++)
            {
                var edge = edges[edgesCounter];
                VtEdge edgeToRemove = null;
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

        internal static List<VtTriangle> RemoveBadTriangles(List<VtTriangle> triangles, List<VtTriangle> badTriangles)
        {
            var result = new List<VtTriangle>();
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

        private static void FillHole(ref List<VtTriangle> triangles, List<VtEdge> polygonHole, VtPoint newPoint)
        {
            for (int polyCounter = 0; polyCounter < polygonHole.Count; polyCounter++)
                triangles.Add(new VtTriangle(polygonHole[polyCounter].S, polygonHole[polyCounter].E, newPoint));
        }

        
    }
}
