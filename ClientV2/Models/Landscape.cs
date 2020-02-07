using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace ClientV2.Models
{
    class Landscape : Volume
    {
        Random _rng; 
        public Landscape(int w, int h)
        {
            _rng = new Random();
            Vertices = new Vector3[w * h];
            Colors = new Vector3[w * h];
            for (int z = 0; z < h; z++)
            {
                for (int x = 0; x < w; x++)
                {
                    var ny = _rng.Next(0, 1000) / 1000f;
                    var nx = x / 100f;
                    var nz = z / 100f;
                    var ind = z * w + x;
                    Vertices[ind] = new Vector3(nx, ny, nz);
                    Colors[ind] = new Vector3(ny, ny, ny);
                }
            }
        }

        float __col = 255;
        float __xs = 20;
        float __ys = 20;
        float __zs = 30;

        class ColH
        {
            public byte r,g,b;
            public float Hue;
        }
        List<ColH> _hues = new List<ColH>();
        public Landscape(string filename)
        {
            Bitmap b = new Bitmap(Image.FromFile(filename));
            var bd = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            var vers = new List<Vector3>();
            var cols = new List<Vector3>();
            
            unsafe
            {
                float h;
                byte* ind;
                var scan0 = (byte*)bd.Scan0.ToPointer();

                for (int y = 0; y < bd.Height; y++)
                {
                    for (int x = 0; x < bd.Width; x++)
                    {
                        ind = scan0 + y * bd.Stride + x * 3;
                        var colH = new ColH()
                        {
                            r = ind[2],
                            g = ind[1],
                            b = ind[0],
                            Hue = Color.FromArgb(ind[2], ind[1], ind[0]).GetHue()
                        };
                        if (_hues.Any(hue => hue.r == colH.r && hue.g == colH.g && hue.b == colH.b))
                            continue;
                        _hues.Add(colH);
                    }
                }

                _hues = _hues.OrderBy(hue => hue.Hue).ToList();

                for (int y = 0; y < bd.Height-1; ++y)
                {
                    for (int x = 0; x < bd.Width-1; ++x)
                    {
                        ind = scan0 + y * bd.Stride + x * 3;
                        cols.Add(GetC(ind[2], ind[1], ind[0]));
                        h = GetH(ind[2], ind[1], ind[0]); 
                        vers.Add(GetP(x, h, y));

                        ind = scan0 + y * bd.Stride + (x + 1) * 3;
                        cols.Add(GetC(ind[2], ind[1], ind[0]));
                        h = GetH(ind[2], ind[1], ind[0]);
                        vers.Add(GetP(x + 1, h, y));

                        ind = scan0 + (y + 1) * bd.Stride + (x + 1) * 3;
                        cols.Add(GetC(ind[2], ind[1], ind[0]));
                        h = GetH(ind[2], ind[1], ind[0]); 
                        vers.Add(GetP(x + 1, h, y + 1));

                        ind = scan0 + (y + 1) * bd.Stride + x * 3;
                        cols.Add(GetC(ind[2], ind[1], ind[0]));
                        h = GetH(ind[2], ind[1], ind[0]); 
                        vers.Add(GetP(x, h, y + 1));
                    }
                }
                
            }
            Debug.WriteLine(_hues.Min(j1 => j1.Hue) + " - " + _hues.Max(j1 => j1.Hue));
            Vertices = vers.ToArray();
            Colors = cols.ToArray();
        }// constructor
        
        private Vector3 GetC(byte r, byte g, byte b)
        {
            return new Vector3(r / __col, g / __col, b / __col);
        }

        private float GetH(byte r, byte g, byte b)
        {
            //return br = (0.2126f * r + 0.7152f * g + 0.0722f * b);
            var h = _hues.FirstOrDefault(hue => hue.r == r && hue.g == g && hue.b == b);
            if (h == null)
                return -1;
            var ind = _hues.IndexOf(h);
            return -ind;
        }

        private Vector3 GetP(int x, float y, int z)
        {
            return new Vector3(x / __xs, y / __zs, z / __ys);
        }
    }
}
