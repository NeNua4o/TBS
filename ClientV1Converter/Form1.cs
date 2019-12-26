using ClientV1.Models;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClientV1Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void b_pickDir_Click(object sender, EventArgs e)
        {

        }

        private void LoadHeightMap(string filename, out List<Pt3D> pt, out List<PtUV> uv, out List<PtC> col)
        {
            pt = new List<Pt3D>();
            uv = new List<PtUV>();
            col = new List<PtC>();

            List<short> th = new List<short>();
            var max = short.MinValue;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                int size = 18;
                byte[] buff = new byte[size];
                byte[] tbuff = new byte[2];
                int totalReaded;
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
                    }
                }
                while (totalReaded > 0);
                fs.Close();
            }

            var mapsize = (int)Math.Sqrt(th.Count);
            if (th.Count % mapsize != 0)
                return;
            for (int y = 0; y < mapsize - 1; y++)
                for (int x = 0; x < mapsize - 1; x++)
                {
                    float v1 = th[x + y * mapsize];
                    float c1 = v1 / max;
                    pt.Add(new Pt3D(x, v1, y));
                    uv.Add(new PtUV(x / (float)mapsize, y / (float)mapsize));
                }
        }
    }
}
