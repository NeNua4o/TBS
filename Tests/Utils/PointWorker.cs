using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Structures;

namespace Tests.Utils
{
    class PointWorker
    {
        private static PointWorker _instance;
        private Random _rng;

        private PointWorker()
        {
            _rng = new Random();
        }

        public static PointWorker GetInstance()
        {
            if (_instance == null)
                _instance = new PointWorker();
            return _instance;
        }

        public List<VPoint> GetUniqDots(int xmin, int ymin, int xmax, int ymax, int ptCount)
        {
            // Generate dots.
            var res = new List<VPoint>();
            int k = 0;
            while (k < ptCount)
            {
                // Make dot with uniq pos.
                var newx = _rng.Next(xmin, xmax + 1);
                var newy = _rng.Next(ymin, ymax + 1);
                while (AnyHas(res, newx, newy))
                {
                    newx = _rng.Next(xmin, xmax + 1);
                    newy = _rng.Next(ymin, ymax + 1);
                }
                res.Add(new VPoint(newx, newy));
                k++;
            }
            return res;
        }

        private bool AnyHas(List<VPoint> points, int x, int y)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] == null) continue;
                if (points[i].X == x && points[i].Y == y)
                    return true;
            }
            return false;
        }


    }
}
