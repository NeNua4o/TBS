using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;

namespace ClientV1.Models.Map
{
    public class HeightMap
    {
        public Volume Map;
        public List<Volume> Maps = new List<Volume>();

        public HeightMap(string filename)
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
                List<Vector3> tvertices = new List<Vector3>();
                List<Vector2> tuvs = new List<Vector2>();
                int k = 0;
                for (int y = 0; y < mapsize; y++)
                    for (int x = 0; x < mapsize; x++)
                    {
                        var t = th[k];
                        tvertices.Add(new Vector3(x * Consts.XZ_SCALE, t * Consts.Y_SCALE, y * Consts.XZ_SCALE));
                        tuvs.Add(new Vector2(x / (float)mapsize, y / (float)mapsize));

                        /*
                        var a = t/(float)max;
                        tuvs.Add(new Vector3(a, a, 0));
                        */

                        k++;
                    }
                List<Vector3> tvertices2 = new List<Vector3>();
                List<Vector2> tuvs2 = new List<Vector2>();
                for (int y = 0; y < mapsize - 1; y++)
                {
                    for (int x = 0; x < mapsize - 1; x++)
                    {
                        tvertices2.Add(tvertices[x + y * 1536]);
                        tvertices2.Add(tvertices[x + (y + 1) * 1536]);
                        tvertices2.Add(tvertices[(x + 1) + (y + 1) * 1536]);

                        
                        tvertices2.Add(tvertices[x + y * 1536]);
                        tvertices2.Add(tvertices[(x + 1) + (y + 1) * 1536]);
                        tvertices2.Add(tvertices[(x + 1) + y * 1536]);
                        

                        tuvs2.Add(tuvs[x + y * 1536]);
                        tuvs2.Add(tuvs[x + (y + 1) * 1536]);
                        tuvs2.Add(tuvs[(x + 1) + (y + 1) * 1536]);
                        
                        tuvs2.Add(tuvs[x + y * 1536]);
                        tuvs2.Add(tuvs[(x + 1) + (y + 1) * 1536]);
                        tuvs2.Add(tuvs[(x + 1) + y * 1536]);
                    }
                }

                var part = 307200;
                var holes = (int)(tvertices2.Count / (float)part);
                var tv3 = tvertices2.ToArray();
                var tt3 = tuvs2.ToArray();
                for (int i = 0; i < holes; i++)
                {
                    var map = new Volume()
                    {
                        PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType.Triangles,
                    };
                    map.Vertices = new Vector3[part];
                    Array.Copy(tv3, i * part, map.Vertices, 0, part);
                    map.UVs = new Vector2[part];
                    Array.Copy(tt3, i * part, map.UVs, 0, part);
                    map.GenVT();
                    Maps.Add(map);
                }
                /*
                Map = new Volume()
                {
                    PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType.Triangles,
                    Vertices = tvertices2.ToArray(),
                    UVs = tuvs2.ToArray()
                };
                Map.GenVT();
                */
            }
        }
    }
}
