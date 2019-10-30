using Common.Models;
using Common.Utils;
using System;

namespace Common.Extensions
{
    public static class CubeExtensions
    {
        public static float DistanceToF(this Cube a, Cube b)
        {
            return Math.Max(Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y)), Math.Abs(a.Z - b.Z));
        }

        public static int DistanceToI(this Cube a, Cube b)
        {
            return TMath.Round(Math.Max(Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y)), Math.Abs(a.Z - b.Z)));
        }

        public static Axial ToAxial(this Cube a)
        {
            return new Axial(TMath.Round(a.X), TMath.Round(a.Z));
        }

        public static Cube Add(this Cube a, float x, float y, float z)
        {
            return new Cube(a.X + x, a.Y + y, a.Z + z);
        }
    }
}
