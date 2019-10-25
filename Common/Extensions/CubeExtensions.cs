using Common.Models;
using System;

namespace Common.Extensions
{
    public static class CubeExtensions
    {
        public static float DistanceTo(this Cube a, Cube b)
        {
            return Math.Max(Math.Max(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y)), Math.Abs(a.Z - b.Z));
        }

        public static Axial ToAxial(this Cube a)
        {
            return new Axial((int)Math.Round(a.X, MidpointRounding.AwayFromZero), (int)Math.Round(a.Z, MidpointRounding.AwayFromZero));
        }

        public static Cube Add(this Cube a, float x, float y, float z)
        {
            return new Cube(a.X + x, a.Y + y, a.Z + z);
        }
    }
}
