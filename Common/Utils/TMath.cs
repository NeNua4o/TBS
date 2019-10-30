using System;

namespace Common.Utils
{
    public static class TMath
    {
        public static int Round(double value)
        {
            return (int)Math.Round(value, MidpointRounding.AwayFromZero);
        }
    }
}
