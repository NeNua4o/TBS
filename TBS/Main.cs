using Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TBS
{
    public partial class Main : Form
    {
        BMap _map;
        Pl pl1, pl2;

        public Main()
        {
            InitializeComponent();
            pl1 = new Pl() { TeamId = 1 };
            pl2 = new Pl() { TeamId = 2 };
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

        private void b_editArmies_Click(object sender, EventArgs e)
        {
            ArmyEditor ae = new ArmyEditor();
            ae.ShowDialog();
        }
    }
}
