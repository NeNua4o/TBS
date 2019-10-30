using Common.Extensions;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var distanceF = start.DistanceToF(end);
            var distance = TMath.Round(distanceF);
            for (int i = 0; i <= distance; i++)
                result.Add(Interpolate(start, end, 1f / distanceF * i));
            if (!includeStart && result.Count > 0) result.RemoveAt(0);
            if (!includeEnd && result.Count > 0) result.RemoveAt(result.Count - 1);
            return result;
        }

        public static List<Cube> GetSheme(Cube startPoint, int[] sheme, int goesTo, bool includeStartPoint)
        {
            var res = new List<Cube>();
            if (startPoint == null) return res;
            if (sheme == null) return res;
            // Кольцо за кольцом.
            for (int shemeNumber = 0; shemeNumber < sheme.Length; shemeNumber++)
            {
                List<VCube> ring = GetRing(startPoint, goesTo, shemeNumber + 1);
                int left = 0, right = ring.Count, curPos = 0, inSheme = 0;
                bool isLeft = true; // Начинаем обход слева.

                while (inSheme < sheme[shemeNumber]) // Пока не дойдём до конца схемы.
                {
                    for (int resultCounter = 0; resultCounter < res.Count; resultCounter++)
                        if (res[resultCounter] == ring[curPos])
                            goto noAdd; // Если уже есть такие координаты, то перескочим этап добавления.
                    res.Add(ring[curPos]);
                    noAdd:
                    inSheme++;
                    if (isLeft) // Меняем направление.
                    {
                        isLeft = false;
                        left++;
                        curPos = left;
                    }
                    else
                    {
                        isLeft = true;
                        right--;
                        curPos = right;
                    }
                }
            }
            if (includeStartPoint) // Если нужно добавить точку отсчёта в схему то добавим.
                res.Add(startPoint);
            return res;
        }

        public static List<VCube> GetRing(Cube startPoint, int direction, int radius)
        {
            var result = new List<VCube>();
            var vCube = new VCube(startPoint, true);
            vCube.Scale(direction, radius);
            var directionPasses = 0;
            var currentDirection = (direction + 2) % 6;
            while (directionPasses < 6)
            {
                for (int i = 0; i < radius; i++)
                {
                    if (vCube == null) break;
                    result.Add(vCube);
                    vCube = vCube.Dirs[currentDirection];
                    vCube.GenerateDirs();
                }
                currentDirection++;
                if (currentDirection == 6)
                    currentDirection = currentDirection % 6;
                directionPasses++;
            }
            return result;
        }
    }
}
