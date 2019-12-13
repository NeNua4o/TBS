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

            for (int i = 0; i < Mission.CommonShapeList.sub_zones.Count; i++)
            {
                var sz = Mission.CommonShapeList.sub_zones[i];
                var pi = sz.points_info;
                
                var vlist = new List<Vector3>();
                var clist = new List<Vector3>();
                var col = new Vector3(0.5f, 0.5f, 0);
                for (int j = 0; j < pi.points.Length; j++)
                {
                    var pt = pi.points[j];
                    vlist.Add(new Vector3(
                        pt.x * Consts.XZ_SCALE/2, 
                        20 * Consts.Y_SCALE, 
                        pt.y * Consts.XZ_SCALE/2));
                    clist.Add(col);
                }
                var szv = new Volume()
                {
                    PrimitiveType = PrimitiveType.Points,
                    Vertices = vlist.ToArray(),
                    Colors = clist.ToArray()
                };
                szv.GenVC();
                Subzones.Add(szv);
            }

            for (int i = 0; i < Mission.Objects.Length; i++)
            {
                var oj = Mission.Objects[i];
                var vlist = new List<Vector3>();
                var clist = new List<Vector3>();
                var col = new Vector3(0.5f, 0, 0.5f);
                var a = oj.Pos.Split(',');
                var st = new Vector3(
                    Single.Parse(a[0].Replace('.', ',')) * Consts.XZ_SCALE / 2,
                    Single.Parse(a[1].Replace('.', ',')) * Consts.Y_SCALE,
                    Single.Parse(a[2].Replace('.', ',')) * Consts.XZ_SCALE / 2);
                while (st.Y >= 1024)
                {
                    st.Y -= 1024;
                }
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

            
        }
    }
}
