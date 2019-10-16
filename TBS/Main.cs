using Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TBS
{
    public partial class Main : Form
    {
        BMap _map;
        public Main()
        {
            InitializeComponent();
        }

        private void DrawMap()
        {
            if (_map == null) return;
            Bitmap b = new Bitmap(_map.GridLayout); Graphics g = Graphics.FromImage(b);
            pb_field.Image = b; g = null; b = null;
        }

        private void b_reset_Click(object sender, EventArgs e)
        {
            _map = new BMap(4, 50);
            pb_field.Width = _map.Width;
            pb_field.Height = _map.Height;
            DrawMap();
        }
    }
}
