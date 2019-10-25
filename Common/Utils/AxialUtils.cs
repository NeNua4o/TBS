using Common.Models;
using System;
using System.Collections.Generic;

namespace Common.Utils
{
    public static class AxialUtils
    {
        public static List<Axial> GetInRange(this Axial a, int n)
        {
            var res = new List<Axial>();
            for (int x = -n; x <= n; x++)
            {
                var maxy = Math.Max(-n, -x - n);
                var miny = Math.Min(n, -x + n);
                for (int y = maxy; y <= miny; y++)
                {
                    var z = -x - y;
                    var q = x + a.Q;
                    var r = z + a.R;
                    res.Add(new Axial(q, r));
                }
            }
            return res;
        }
    }
}
