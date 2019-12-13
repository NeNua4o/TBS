using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClientV1.Models.Mission
{
    public class ObjectsLoader
    {
        public List<Volume> Dots = new List<Volume>();

        public ObjectsLoader(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                int size = 16;
                byte[] buff = new byte[size];
                byte[] tbuff = new byte[4];
                int totalReaded;
                List<PtOpt> ptOpts = new List<PtOpt>();
                do
                {
                    totalReaded = fs.Read(buff, 0, size);
                    if (totalReaded < size)
                        break;
                    for (int i = 0; i < totalReaded; i += 16)
                    {
                        tbuff[0] = buff[0];
                        tbuff[1] = buff[1];
                        tbuff[2] = buff[2];
                        tbuff[3] = buff[3];
                        var ts = BitConverter.ToInt32(tbuff, 0);

                        tbuff[0] = buff[4];
                        tbuff[1] = buff[5];
                        tbuff[2] = buff[6];
                        tbuff[3] = buff[7];
                        var tx = BitConverter.ToSingle(tbuff, 0);

                        tbuff[0] = buff[8];
                        tbuff[1] = buff[9];
                        tbuff[2] = buff[10];
                        tbuff[3] = buff[11];
                        var ty = BitConverter.ToSingle(buff, 0);

                        tbuff[0] = buff[12];
                        tbuff[1] = buff[13];
                        tbuff[2] = buff[14];
                        tbuff[3] = buff[15];
                        var tz = BitConverter.ToSingle(buff, 0);

                        var ptOpt = ptOpts.FirstOrDefault(opt=>opt.ind==ts);
                        if (ptOpt == null)
                        {
                            ptOpt = new PtOpt() { ind = ts };
                            ptOpts.Add(ptOpt);
                        }

                        ptOpt.Vertices.Add(new Vector3(tx * Consts.XZ_SCALE, ty * Consts.Y_SCALE, tz * Consts.XZ_SCALE));
                        ptOpt.Colors.Add(new Vector3(0, 0, 1));
                    }
                }
                while (totalReaded > 0);
                fs.Close();

                for (int i = 0; i < ptOpts.Count; i++)
                {
                    var ptOpt = ptOpts[i];
                    var dot = new Volume()
                    {
                        PrimitiveType = OpenTK.Graphics.OpenGL.PrimitiveType.Lines,
                        Vertices = ptOpt.Vertices.ToArray(),
                        Colors = ptOpt.Colors.ToArray()
                    };
                    dot.GenVC();
                    Dots.Add(dot);
                }
            }
        }

        class PtOpt
        {
            public int ind;
            public List<Vector3> Vertices = new List<Vector3>();
            public List<Vector3> Colors = new List<Vector3>();
        }
    
    }


}
