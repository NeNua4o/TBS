namespace Common.Extensions
{
    public static class IntExtensions
    {
        public static bool Between(this int value, int minBorder, int maxBorder, bool inclusive = false)
        {
            return inclusive ? (value >= minBorder && value <= maxBorder) : (value > minBorder && value < maxBorder);
        }

        public static bool Between(this int value, float minBorder, float maxBorder, bool inclusive = false)
        {
            return inclusive ? (value >= minBorder && value <= maxBorder) : (value > minBorder && value < maxBorder);
        }
    }
}
