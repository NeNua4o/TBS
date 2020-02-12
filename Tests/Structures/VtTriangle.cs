using System;
using Tests.Utils;

namespace Tests.Structures
{
    public class VtTriangle
    {
        public VtPoint A;
        public VtPoint B;
        public VtPoint C;

        public VtEdge AB;
        public VtEdge AC;
        public VtEdge BC;

        public VtPoint CirC;
        public float CirR;

        public VtTriangle() { }

        public VtTriangle(VtPoint pointA, VtPoint pointB, VtPoint pointC)
        {
            A = pointA;
            B = pointB;
            C = pointC;
            AB = new VtEdge(A, B);
            AC = new VtEdge(A, C);
            BC = new VtEdge(B, C);
            CalcCircumCenter();
        }

        void LineFromPoints(VtPoint p, VtPoint q, out float a, out float b, out float c)
        {
            a = q.Y - p.Y;
            b = p.X - q.X;
            c = a * (p.X) + b * (p.Y);
        }

        void PerpendicularBisectorFromLine(VtPoint p, VtPoint q, ref float a, ref float b, ref float c)
        {
            VtPoint mid_point = new VtPoint((p.X + q.X) / 2f, (p.Y + q.Y) / 2f);
            c = -b * (mid_point.X) + a * (mid_point.Y);// c = -bx + ay 
            float temp = a;
            a = -b;
            b = temp;
        }

        VtPoint lineLineIntersection(float a1, float b1, float c1, float a2, float b2, float c2)
        {
            float determinant = a1 * b2 - a2 * b1;
            if (determinant == 0)
                return new VtPoint(float.NaN, float.NaN);
            else
            {
                float x = (b2 * c1 - b1 * c2) / determinant;
                float y = (a1 * c2 - a2 * c1) / determinant;
                return new VtPoint(x, y);
            }
        }

        public void CalcCircumCenter()
        {
            float a, b, c;
            LineFromPoints(A, B, out a, out b, out c);
            float e, f, g;
            LineFromPoints(B, C, out e, out f, out g);

            PerpendicularBisectorFromLine(A, B, ref a, ref b, ref c);
            PerpendicularBisectorFromLine(B, C, ref e, ref f, ref g);

            CirC = lineLineIntersection(a, b, c, e, f, g);
            CirR = EMath.Len(A, CirC);
        }

        public bool HaveSameEdge(VtTriangle triangle)
        {
            var count = 0;
            if (A == triangle.A || A == triangle.B || A == triangle.C) count++;
            if (B == triangle.A || B == triangle.B || B == triangle.C) count++;
            if (C == triangle.A || C == triangle.B || C == triangle.C) count++;
            return count >= 2;
        }

        public static bool operator ==(VtTriangle a, VtTriangle b)
        {
            var count = 0;
            if (a.A == b.A || a.A == b.B || a.A == b.C) count++;
            if (a.B == b.A || a.B == b.B || a.B == b.C) count++;
            if (a.C == b.A || a.C == b.B || a.C == b.C) count++;
            return count == 3;
        }

        public static bool operator !=(VtTriangle a, VtTriangle b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return "(" + A.X + ";" + A.Y + ")(" + B.X + ";" + B.Y + ")(" + C.X + ";" + C.Y + ")";
        }
    }
}
