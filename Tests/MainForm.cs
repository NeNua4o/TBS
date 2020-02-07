using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System;
using System.Linq;
using Tests.Structures;
using System.Collections.Generic;
using OpenTK;
using Tests.Utils;
using System.Threading;
using System.Diagnostics;

namespace Tests
{
    public partial class MainForm : Form
    {
        VPoint[] _pts;
        List<VTriangle> _delTries = new List<VTriangle>();
        List<VTriangle> _badTries = new List<VTriangle>();
        List<VEdge> _edges = new List<VEdge>();

        public MainForm()
        {
            InitializeComponent();
        }

        Font _fnt = new Font("Calibri Light", 8, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();
        Pen _trP = new Pen(Color.Blue, 1);
        Pen _ptP = new Pen(Color.Fuchsia, 1);
        private void DrawR(PictureBox pb)
        {
            Bitmap b = new Bitmap(pb.Width, pb.Height); Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            for (int i = 0; i < _delTries.Count; i++)
            {
                var tr = _delTries[i];
                g.DrawLine(_trP, tr.A.X + __border, tr.A.Y + __border, tr.B.X + __border, tr.B.Y + __border);
                g.DrawLine(_trP, tr.B.X + __border, tr.B.Y + __border, tr.C.X + __border, tr.C.Y + __border);
                g.DrawLine(_trP, tr.C.X + __border, tr.C.Y + __border, tr.A.X + __border, tr.A.Y + __border);
                //g.DrawEllipse(Pens.Red, tr.CirC.X - 3, tr.CirC.Y - 3, 6, 6);
                //g.DrawEllipse(Pens.Blue, tr.CirC.X - tr.CirR, tr.CirC.Y - tr.CirR, tr.CirR * 2, tr.CirR * 2);
            }

            if (_pts != null)
                for (int i = 0; i < _pts.Length; i++)
                {
                    var pt = _pts[i];
                    g.DrawEllipse(_ptP, (int)pt.X - 1 + __border, (int)pt.Y - 1 + __border, 2, 2);
                }

            pb.Image = b;g = null; b = null;
        }

        const float __border = 20f;
        private void button1_Click(object sender, EventArgs e)
        {
            var ptCount = (double)nud_ptCount.Value;
            var width = (double)nud_width.Value;
            var height = (double)nud_height.Value;
            var lloyd = (double)nud_Lloyd.Value;
            pictureBox1.Width = (int)(width + __border * 2);
            pictureBox1.Height = (int)(height + __border * 2);

            var dots = PointWorker.GetInstance().GetUniqDots(0, 0, (int)width, (int)height, (int)ptCount);
            _pts = dots.ToArray();

            _delTries.Clear();

            var triangulation = new Triangulation(dots);
            List<VTriangle> delTries = triangulation.CalcDelanua(__border);
            _delTries.AddRange(delTries);
            
            DrawR(pictureBox1);
            pictureBox1.Image.Save("Delone.png");
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

    }
}
