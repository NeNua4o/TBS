using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ActionMultiSelector : UserControl
    {
        public int ActionId = -1;
        public event EventHandler ActionChanged;

        static Font _ctFont;
        static Brush _pillar;
        static RectangleF _bArea;

        public ActionMultiSelector()
        {
            InitializeComponent();
            if (_ctFont == null) _ctFont = new Font("Calibri", 18, FontStyle.Bold, GraphicsUnit.Pixel);
            if (_pillar == null) _pillar = new SolidBrush(Color.FromArgb(100, Color.Black));
            if (_bArea == null) _bArea = new RectangleF(0, 0, b_action.Width, b_action.Height);
        }

        internal void Init(string imagePath)
        {
            Bitmap b = new Bitmap(imagePath);
            b_action.BackgroundImage = new Bitmap(b, b_action.Size);
            b = null;
        }
    }
}
