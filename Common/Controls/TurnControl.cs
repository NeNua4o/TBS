using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class TurnControl : UserControl
    {
        public event EventHandler SkipClicked;
        public TurnControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var b = new Bitmap("Icons/z_skip.png");
            b_skip.Image = new Bitmap(b, b_skip.Size);
            b = new Bitmap("Icons/z_def.png");
            b_def.Image = new Bitmap(b, b_def.Size);
            b = new Bitmap("Icons/z_wait.png");
            b_wait.Image = new Bitmap(b, b_wait.Size);
        }

        private void b_skip_Click(object sender, EventArgs e) { SkipClicked?.Invoke(null, null); }
    }
}
