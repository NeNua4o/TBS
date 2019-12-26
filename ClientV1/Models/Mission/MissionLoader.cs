using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ClientV1.Models.Mission
{
    public class MissionLoader
    {
        public Mission Mission;

        public List<Volume> Subzones = new List<Volume>();
        public List<Volume> Objects = new List<Volume>();

        public MissionLoader(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                var xmlS = new XmlSerializer(typeof(Mission));
                Mission = (Mission)xmlS.Deserialize(fs);
                fs.Close();
            }

            

            for (int i = 0; i < Mission.Objects.Length; i++)
            {
                var oj = Mission.Objects[i];
                var vlist = new List<Vector3>();
                var clist = new List<Vector3>();
                var col = new Vector3(0.5f, 0, 0.5f);
                var a = oj.Pos.Split(',');
                float
                    x = Single.Parse(a[1].Replace('.', ',')),
                    y = Single.Parse(a[2].Replace('.', ',')),
                    z = Single.Parse(a[0].Replace('.', ','))
                    ;

                var st = new Vector3(x * Consts.XZ_SCALE / 2, y * Consts.Y_SCALE, z * Consts.XZ_SCALE / 2);
                /*
                while (st.Y >= 1024)
                {
                    st.Y -= 1024;
                }
                */
                vlist.Add(st);
                
                clist.Add(col);
                vlist.Add(new Vector3(st.X, st.Y + 1, st.Z));
                clist.Add(col);

                var szv = new Volume()
                {
                    PrimitiveType = PrimitiveType.Lines,
                    Vertices = vlist.ToArray(),
                    Colors = clist.ToArray()
                };
                szv.GenVC();
                Objects.Add(szv);
            }

            Mission = null;
        }
    }
}
