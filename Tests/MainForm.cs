using System;
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
        }

        Font _fnt = new Font("Calibri Light", 8, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();
        private void DrawR(ref Bitmap image)
        {
            Bitmap b = new Bitmap(210, 420); Graphics g = Graphics.FromImage(b);
            g.TextRenderingHint = TextRenderingHint.AntiAlias;



            image = b; g = null; b = null;
        }

        private void b_test_Click(object sender, EventArgs e)
        {
        }
    }
}
