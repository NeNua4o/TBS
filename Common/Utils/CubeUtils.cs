using Common.Extensions;
using Common.Models;
using System;
using System.Collections.Generic;

namespace Common.Utils
{
    public static class CubeUtils
    {
        public static Cube Interpolate(Cube a, Cube b, float v)
        {
            return a + (b - a) * v;
        }

        public static List<Cube> GetCubesOnLine(Cube start, Cube end, bool includeStart = true, bool includeEnd = true)
        {
            var result = new List<Cube>();
            var distanceF = start.DistanceTo(end);
            var distance = Math.Round(distanceF, MidpointRounding.AwayFromZero);
            for (int i = 0; i <= distance; i++)
                result.Add(Interpolate(start, end, 1f / distanceF * i));
            if (!includeStart && result.Count > 0) result.RemoveAt(0);
            if (!includeEnd && result.Count > 0) result.RemoveAt(result.Count - 1);
            return result;
        }

        public static List<ECube> GetRing(Cube start, int dir, int rad)
        {
            var result = new List<ECube>();//var results = []
            var ecube = new ECube(start, true); //var cube = cube_add(center, cube_scale(cube_direction(4), radius))
            ecube.Scale(dir, rad);
            var dirsPassed = 0;
            var i = (dir + 2) % 6;
            while (dirsPassed < 6)
            {
                for (int j = 0; j < rad; j++) //for each 0 ≤ j < radius:
                {
                    if (ecube == null) break;
                    result.Add(ecube); //results.append(cube)
                    ecube = ecube.Dirs[i]; //cube = cube_neighbor(cube, i)
                    ecube.GenerateDirs();
                }
                i++;
                i = i % 6;
                dirsPassed++;
            }
            return result;
        }
    }
}
