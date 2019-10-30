using Common.Enums;
using Common.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls
{
    public partial class TurnQueue : UserControl
    {
        public TurnQueue()
        {
            InitializeComponent();
        }

        RectangleF _dRec;
        const int _Iw = 65, _Ih = 85, _Out = 15, _Uw = 50, _Uh = 50;
        Font _font = new Font("Calibri Light", 10, FontStyle.Regular); Brush _brush = Brushes.Black; StringFormat _drawFormat = new StringFormat();

        public void SetQueue(List<Unit> units)
        {
            var end = units.Count > _Out ? _Out : units.Count;

            var b = new Bitmap(_Iw * end, _Ih); var g = Graphics.FromImage(b);

            string s;
            for (int i = 0; i < end; i++)
            {
                var unit = units[i];
                s = unit.Chars.Initiative + "";
                var initSize = g.MeasureString(s, _font);
                g.DrawString(s, _font, _brush, (i * _Iw) + ((_Iw - initSize.Width) / 2f), 0);

                g.DrawImage(unit.Icon, new RectangleF((i * _Iw) + ((_Iw - _Uw) / 2f), initSize.Height, _Uw, _Uh));

                s = unit.Chars.C_Hp + " / " + unit.Chars.B_Hp;
                var hpSize = g.MeasureString(s, _font);
                g.DrawString(s, _font, _brush, (i * _Iw) + ((_Iw - hpSize.Width) / 2f), initSize.Height + _Uh);

            }
            pb.Size = b.Size;
            pb.Image = b; g = null; b = null;
        }
    }
}
