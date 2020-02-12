using System;

namespace Common.Extensions
{
    public static class DoubleExtensions
    {
        public static bool Eq(this double a, double b)
        {
            return Math.Abs(a - b) < double.Epsilon;
        }
    }
}
