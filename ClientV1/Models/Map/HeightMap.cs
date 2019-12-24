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
                int size = 18;
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
                List<Vector3> tcols = new List<Vector3>();
                List<Vector2> tuvs = new List<Vector2>();
                for (int y = 0; y < mapsize - 1; y++)
                    for (int x = 0; x < mapsize - 1; x++)
                    {
                        float 
                            v1 = th[x + y * mapsize],
                            v2 = th[x + (y + 1) * mapsize],
                            v3 = th[(x + 1) + (y + 1) * mapsize],
                            v4 = th[(x + 1) + y * mapsize];
                        float 
                            c1 = v1 / max,
                            c2 = v2 / max,
                            c3 = v3 / max,
                            c4 = v4 / max;
                            

                        tvertices.Add(new Vector3(x * Consts.XZ_SCALE, v1 * Consts.Y_SCALE, y * Consts.XZ_SCALE));
                        tvertices.Add(new Vector3(x * Consts.XZ_SCALE, v2 * Consts.Y_SCALE, (y + 1) * Consts.XZ_SCALE));
                        tvertices.Add(new Vector3((x + 1) * Consts.XZ_SCALE, v3 * Consts.Y_SCALE, (y + 1) * Consts.XZ_SCALE));

                        tvertices.Add(new Vector3(x * Consts.XZ_SCALE, v1 * Consts.Y_SCALE, y * Consts.XZ_SCALE));
                        tvertices.Add(new Vector3((x + 1) * Consts.XZ_SCALE, v3 * Consts.Y_SCALE, (y + 1) * Consts.XZ_SCALE));
                        tvertices.Add(new Vector3((x + 1) * Consts.XZ_SCALE, v4 * Consts.Y_SCALE, y * Consts.XZ_SCALE));

                        /**/
                        tcols.Add(new Vector3(c1, c1, 0));
                        tcols.Add(new Vector3(c2, c2, 0));
                        tcols.Add(new Vector3(c3, c3, 0));

                        tcols.Add(new Vector3(c1, c1, 0));
                        tcols.Add(new Vector3(c3, c3, 0));
                        tcols.Add(new Vector3(c4, c4, 0));
                        

                        /*
                        tuvs.Add(new Vector2(x / (float)mapsize, 1-(y / (float)mapsize)));
                        tuvs.Add(new Vector2(x / (float)mapsize, 1 - ((y + 1) / (float)mapsize)));
                        tuvs.Add(new Vector2((x + 1) / (float)mapsize, 1-((y + 1) / (float)mapsize)));

                        tuvs.Add(new Vector2(x / (float)mapsize, 1-(y / (float)mapsize)));
                        tuvs.Add(new Vector2((x + 1) / (float)mapsize, 1-((y + 1) / (float)mapsize)));
                        tuvs.Add(new Vector2((x + 1) / (float)mapsize, 1-(y / (float)mapsize)));
                        */

                        /*
                        tuvs.Add(new Vector2(x / (float)mapsize, (y / (float)mapsize)));
                        tuvs.Add(new Vector2(x / (float)mapsize, ((y + 1) / (float)mapsize)));
                        tuvs.Add(new Vector2((x + 1) / (float)mapsize, ((y + 1) / (float)mapsize)));

                        tuvs.Add(new Vector2(x / (float)mapsize, (y / (float)mapsize)));
                        tuvs.Add(new Vector2((x + 1) / (float)mapsize, ((y + 1) / (float)mapsize)));
                        tuvs.Add(new Vector2((x + 1) / (float)mapsize, (y / (float)mapsize)));
                        */
                    }

                var part = (mapsize - 1) * 6 * 10;
                var holes = (int)(tvertices.Count / (float)part);
                for (int i = 0; i < holes; i++)
                {
                    var map = new Volume()
                    {
                        PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType.Triangles,
                    };
                    map.Vertices = new Vector3[part];
                    tvertices.CopyTo(i * part, map.Vertices, 0, part);

                    /**/
                    map.Colors = new Vector3[part];
                    tcols.CopyTo(i * part, map.Colors, 0, part);
                    

                    /*
                    map.UVs = new Vector2[part];
                    tuvs.CopyTo(i * part, map.UVs, 0, part);
                    */

                    map.GenVC();
                    //map.GenVT();
                    Maps.Add(map);
                }
            }
        }
    }
}
