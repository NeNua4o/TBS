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
using Voronoy;
using Voronoy.Structures;

namespace Tests
{
    public partial class MainForm : Form
    {
        List<VtPoint> _pts = new List<VtPoint>();
        List<VEdge> _edges = new List<VEdge>();
        List<FortuneSite> _sites = new List<FortuneSite>();

        public MainForm()
        {
            InitializeComponent();
        }

        Font _fnt = new Font("Calibri Light", 8, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();
        Pen _trP = new Pen(Color.White, 1);//new Pen(Color.Blue, 1);
        Pen _ptP = new Pen(Color.Fuchsia, 1);//new Pen(Color.Fuchsia, 1);
        Pen _plP = new Pen(Color.Blue, 1);
        Brush _ptB = new SolidBrush(Color.Red);
        Brush _ptB2 = new SolidBrush(Color.Yellow);
        private void DrawR(PictureBox pb)
        {
            Bitmap b = new Bitmap(pb.Width, pb.Height); Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.FillRectangle(Brushes.Black, 0, 0, pb.Width, pb.Height);

            /**/
            for (int i = 0; i < _edges.Count; i++)
            {
                var tr = _edges[i];
                g.DrawLine(
                    _trP,
                    (float)tr.Start.X + __xs, (float)tr.Start.Y + __ys,
                    (float)tr.End.X + __xs, (float)tr.End.Y + __ys
                    );
            }


            if (_pts != null)
                for (int i = 0; i < _pts.Count; i++)
                {
                    var pt = _pts[i];
                    g.FillEllipse(_ptB, (int)pt.X - 2 + __xs, (int)pt.Y - 2 + __ys, 4, 4);
                }

            if (_sites != null)
                for (int i = 0; i < _sites.Count; i++)
                {
                    var pt = _sites[i];
                    g.FillEllipse(_ptB2, (int)pt.X - 2 + __xs, (int)pt.Y - 2 + __ys, 4, 4);
                }

            pb.Image = b;g = null; b = null;
        }

        const float __border = 2f;
        const float __xs = 1f;
        const float __ys = 1f;
        int _ll = 0;


        private void button1_Click(object sender, EventArgs e)
        {
            var ptCount = (double)nud_ptCount.Value;
            var width = (double)nud_width.Value;
            var height = (double)nud_height.Value;
            var lloyd = (double)nud_Lloyd.Value;
            pictureBox1.Width = (int)(width + __border);
            pictureBox1.Height = (int)(height + __border);

            var dots = PointWorker.GetInstance().GetUniqDots(0, 0, (int)width, (int)height, (int)ptCount);
            //_pts = dots;

            var minX = dots.Min(dot => dot.X);
            var minY = dots.Min(dot => dot.Y);
            var maxX = dots.Max(dot => dot.X);
            var maxY = dots.Max(dot => dot.Y);
            pb1.Value = 0;
            var sw = new Stopwatch();

            var sites = PointWorker.GetInstance().DotsToSites(dots);
            var voronoyPartEdges = FortunesAlgorithm.Run(sites, minX, minY, maxX, maxY);

            _edges = voronoyPartEdges.ToList();
            DrawR(pictureBox1);
            Application.DoEvents();

            for (int i = 0; i < lloyd; i++)
            {
                sw.Restart();
                List<FortuneSite> newC = PointWorker.GetInstance().Relax(voronoyPartEdges, minX, minY, maxX, maxY);
                //_sites = newC;
                DrawR(pictureBox1);
                Application.DoEvents();

                sw.Restart();
                voronoyPartEdges = FortunesAlgorithm.Run(newC, minX, minY, maxX, maxY);
                _edges = voronoyPartEdges.ToList();
                //_pts = PointWorker.GetInstance().SitesToDots(newC);
                DrawR(pictureBox1);
                Application.DoEvents();
            }

            pb1.Value = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
        }

    }
}
