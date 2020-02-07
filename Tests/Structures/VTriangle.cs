using System;
using Tests.Utils;

namespace Tests.Structures
{
    public class VTriangle
    {
        public VPoint A;
        public VPoint B;
        public VPoint C;

        public VEdge AB;
        public VEdge AC;
        public VEdge BC;

        public VPoint CirC;
        public float CirR;

        public VTriangle() { }

        public VTriangle(VPoint pointA, VPoint pointB, VPoint pointC)
        {
            A = pointA;
            B = pointB;
            C = pointC;
            AB = new VEdge(A, B);
            AC = new VEdge(A, C);
            BC = new VEdge(B, C);
            CalcCircumCenter();
        }

        void LineFromPoints(VPoint p, VPoint q, out float a, out float b, out float c)
        {
            a = q.Y - p.Y;
            b = p.X - q.X;
            c = a * (p.X) + b * (p.Y);
        }

        void PerpendicularBisectorFromLine(VPoint p, VPoint q, ref float a, ref float b, ref float c)
        {
            VPoint mid_point = new VPoint((p.X + q.X) / 2f, (p.Y + q.Y) / 2f);
            c = -b * (mid_point.X) + a * (mid_point.Y);// c = -bx + ay 
            float temp = a;
            a = -b;
            b = temp;
        }

        VPoint lineLineIntersection(float a1, float b1, float c1, float a2, float b2, float c2)
        {
            float determinant = a1 * b2 - a2 * b1;
            if (determinant == 0)
                return new VPoint(float.NaN, float.NaN);
            else
            {
                float x = (b2 * c1 - b1 * c2) / determinant;
                float y = (a1 * c2 - a2 * c1) / determinant;
                return new VPoint(x, y);
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

        public override string ToString()
        {
            return "(" + A.X + ";" + A.Y + ")(" + B.X + ";" + B.Y + ")(" + C.X + ";" + C.Y + ")";
        }
    }
}
