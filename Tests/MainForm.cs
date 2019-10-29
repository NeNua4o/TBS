using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Utils;
using System.Threading;
using System.Drawing.Text;
using System.Diagnostics;

namespace Tests
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private int[] _rngs, _rngsC;

        private void b_rtest_Click(object sender, EventArgs e)
        {
            var inst = RandomUtils.Instance();
            _rngs = new int[101];
            while (true)
            {
                var ind = inst.Get100();
                _rngs[ind]++;
                Application.DoEvents();
                if (_rngs[ind] == 200)
                    break;
            }

            _rngsC = new int[101];
            while (true)
            {
                var ind = inst.Get100();
                _rngsC[ind]++;
                Application.DoEvents();
                if (_rngsC[ind] == 200)
                    break;
            }

            DrawR();
        }

        Font _fnt = new Font("Calibri Light", 8, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                Debug.Write(i*4 / 100 + " ");
                Debug.WriteLine(i*4 / 100f);
            }
        }

        private void DrawR()
        {
            Bitmap b = new Bitmap(210, 420); Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            var y = 0;
            if (_rngs != null)
                for (int i = 0; i < _rngs.Length; i++)
                {
                    g.FillRectangle(Brushes.Black, i*2, y, 2, _rngs[i]);
                    //g.DrawString(_rngs[i] + "", _fnt, _brush, i * 15, y, _drawFormat);
                }

            y = 210;
            if (_rngsC != null)
                for (int i = 0; i < _rngs.Length; i++)
                {
                    g.FillRectangle(Brushes.Black, i*2, y, 2, _rngsC[i]);
                    //g.DrawString(_rngsC[i] + "", _fnt, _brush, i * 15, y, _drawFormat);
                }

            pb_rtest.Image = b; g = null; b = null;
        }
    }
}
