﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics;
using System.Collections.Generic;

namespace Tests
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            timer1.Interval = 20;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var t = end - DateTime.Now;
            label1.Text = String.Format("{0}д {1}ч {2}м {3}с {4}мс",t.Days, t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            Application.DoEvents();
        }

        Font _fnt = new Font("Calibri Light", 8, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();
        private void DrawR(ref Bitmap image)
        {
            Bitmap b = new Bitmap(210, 420); Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;



            image = b; g = null; b = null;
        }

        DateTime end = new DateTime(2020, 1, 1, 0, 0, 0);

        private void b_test_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void b_makeGrad_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(500, 30);
            Graphics gr = Graphics.FromImage(bm);

            for (int i = 0; i < 256; i++)
            {
                int
                    r = hsR.Value + i,
                    g = hsG.Value + i,
                    b = hsB.Value + i
                    ;
                while (r > 255) r -= 256;
                while (g > 255) g -= 256;
                while (b > 255) b -= 256;
                var c = Color.FromArgb(255, r, g, b);
                gr.FillRectangle(new SolidBrush(c), i, 0, 2, 30);
            }

            pb_grad.Image = bm; gr = null;
        }
    }
}
