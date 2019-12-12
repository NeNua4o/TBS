using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;

namespace ClientV1.Models.Map
{
    public class HeighMap
    {
        public Volume Map;

        public HeighMap(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                int size = 6;
                byte[] buff = new byte[size];
                byte[] tbuff = new byte[2];
                int totalReaded;
                List<short> th = new List<short>();
                List<byte> tb = new List<byte>();
                var max = short.MinValue;
                var maxb = byte.MinValue;
                do
                {
                    totalReaded = fs.Read(buff, 0, size);
                    for (int i = 0; i < totalReaded; i += 3)
                    {
                        tbuff[0] = buff[i + 1];
                        tbuff[1] = buff[i];
                        var t = BitConverter.ToInt16(tbuff, 0);
                        while (t >= 1024) t -= 1024;
                        while (t < 0) t += 1024;
                        th.Add(t);
                        if (t > max) max = t;

                        var t2 = buff[i + 2];
                        tb.Add(t2);
                        if (t2 > maxb)
                            maxb = t2;
                    }
                }
                while (totalReaded > 0);
                fs.Close();

                var mapsize = (int)Math.Sqrt(th.Count);
                if (th.Count % mapsize != 0)
                    return;
                var hmapsize = mapsize / 2f;
                var hmax = max / 2f;
                List<Vector3> Poss = new List<Vector3>();
                List<Vector3> t3 = new List<Vector3>();
                int k = 0;
                for (int y = 0; y < mapsize; y++)
                    for (int x = 0; x < mapsize; x++)
                    {
                        var t = th[k];
                        Poss.Add(new Vector3(x * Consts.XZ_SCALE, t * Consts.Y_SCALE, y * Consts.XZ_SCALE));
                        var a = t/(float)max;
                        t3.Add(new Vector3(a, a, a));
                        k++;
                    }

                Map = new Volume()
                {
                    PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType.Points,
                    Vertices = Poss.ToArray(),
                    Colors = t3.ToArray()
                };
            }
        }
    }
}
