using Common.Models;

namespace Common.Extensions
{
    public static class AxialExtensions
    {
        public static Cube ToCube(this Axial a)
        {
            return new Cube(a.Q, -a.Q - a.R, a.R);
        }
    }
}
