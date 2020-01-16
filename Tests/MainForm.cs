using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System;
using System.Linq;
using VoronoiLib;
using VoronoiLib.Structures;
using System.Collections.Generic;

namespace Tests
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        Font _fnt = new Font("Calibri Light", 8, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();
        private void DrawR(PictureBox pb)
        {
            Bitmap b = new Bitmap(pb.Width, pb.Height); Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            for (int i = 0; i < _pts.Count; i++)
            {
                var pt = _pts[i];
                //g.DrawLine(Pens.Black, (float)pt.x, (float)pt.y, (float)pt.x + 1, (float)pt.y);

                var rc = pt.Cell;
                if (rc.Count == 0) continue;
                /*
                for (int j = 0; j < rc.Count; j++)
                {
                    var edge = rc[j];
                    g.DrawLine(new Pen(Color.FromArgb(0, i, 0)), (float)edge.Start.X, (float)edge.Start.Y, (float)edge.End.X, (float)edge.End.Y);
                }
                */
                /*
                var c = rc.Select(edge => new PointF((float)edge.Start.X, (float)edge.Start.Y)).ToArray();
                g.FillPolygon(new SolidBrush(Color.FromArgb(0, i, 0)), c);
                */

                g.DrawEllipse(Pens.Black, (float)pt.X-1, (float)pt.Y-1, 2, 2);
            }

            /**/
            for (int i = 0; i < _edges.Count; i++)
            {
                var edge = _edges[i];
                g.DrawLine(Pens.Gray, (float)edge.Start.X, (float)edge.Start.Y, (float)edge.End.X, (float)edge.End.Y);
            }
            
            

            pb.Image = b; g = null; b = null;
        }

        double _ptCount, _width, _height;

        Random _rng = new Random();

        List<FortuneSite> _pts;
        List<Point> _cnts;
        List<VEdge> _edges;

        private void button1_Click(object sender, EventArgs e)
        {
            _ptCount = (double)nud_ptCount.Value;
            _width = (double)nud_width.Value;
            _height = (double)nud_height.Value;
            pictureBox1.Width = (int)_width;
            pictureBox1.Height = (int)_height;

            // Generate dots.
            _pts = new List<FortuneSite>();
            int k = 0;
            while (k < _ptCount)
            {
                // Make dot with uniq pos.
                var pt = new FortuneSite(_rng.Next(0, (int)_width + 1), _rng.Next(0, (int)_height + 1));
                while (AnyHas(_pts, pt))
                {
                    pt = new FortuneSite(_rng.Next(0, (int)_width + 1), _rng.Next(0, (int)_height + 1));
                }
                _pts.Add(pt);
                k++;
            }

            // Voronoy diagramm.
            _edges = FortAlg(_pts, 0, 0, _width, _height);

            // Collecting edges.
            for (int i = 0; i < _pts.Count; i++)
            {
                _pts[i].Cell.AddRange(FindEdges(_pts[i]));
                _pts[i].OrderEdges();
            }

            // Check result;
            DrawR(pictureBox1);
        }

        private IEnumerable<VEdge> FindEdges(FortuneSite dot)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private bool AnyHas(List<FortuneSite> arr, FortuneSite item)
        {
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] == null) continue;
                if (arr[i].X == item.X && arr[i].Y == item.Y)
                    return true;
            }
            return false;
        }

        private List<VEdge> FortAlg(List<FortuneSite> arr, double xmin, double ymin, double xmax, double ymax)
        {
            var res = new List<VEdge>();
            var raw = FortunesAlgorithm.Run(arr, xmin, ymin, xmax, ymax);
            if (raw.Count == 0)
                return res;
            var tmp = raw.First;
            res.Add(tmp.Value);
            while (tmp.Next!=null)
            {
                tmp = tmp.Next;
                res.Add(tmp.Value);
            }
            
            return res;
        }
    }

    class Site
    {

    }


    class Pt
    {
        public double x, y;
        public Pt() { }
        public Pt(double x, double y) { this.x = x; this.y = y; }
    }
}
