using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class ActionSelector : UserControl
    {
        public int ActionId = -1;
        public event EventHandler ActionChanged;

        static Font _ctFont;
        static Brush _pillar;
        static RectangleF _bArea;

        public ActionSelector()
        {
            InitializeComponent();
            if (_ctFont == null) _ctFont = new Font("Calibri", 18, FontStyle.Bold, GraphicsUnit.Pixel);
            if (_pillar == null) _pillar = new SolidBrush(Color.FromArgb(100, Color.Black));
            if (_bArea == null) _bArea = new RectangleF(0, 0, b_action.Width, b_action.Height);
        }

        public void Init(string fileName)
        {
            var b = new Bitmap(fileName);
            b_action.BackgroundImage = new Bitmap(b, b_action.Size);
        }

        public void Set(Act action)
        {
            if (action == null) return;
            Bitmap b = new Bitmap(action.Icon, b_action.Size);
            Graphics g = Graphics.FromImage(b);
            if (action.CoolTime > 0)
            {
                g.FillRectangle(_pillar, _bArea);
                var size = g.MeasureString(action.CoolTime + "", _ctFont);
                g.DrawString(action.CoolTime + "", _ctFont, Brushes.White, (b_action.Width - size.Width) / 2f, 0);
            }
            b_action.BackgroundImage = b;
            g = null;
            b = null;
            ActionId = action.Id;
        }

        private void b_action_Click(object sender, EventArgs e)
        {
            ActionChanged?.Invoke(this, null);
        }
    }
}
