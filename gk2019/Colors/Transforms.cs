using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors
{
    class Transforms
    {
        public static void RgbToHsv(BitmapWrapper input, BitmapWrapper outH, BitmapWrapper outS, BitmapWrapper outV)
        {
            var size = input.GetSize();
            Parallel.For(0, size.Height, y => {
                for (int x = 0; x < size.Width; x++)
                {
                    (float h, float s, float v) = RgbToHsv(input.GetPixel(x, y));
                    
                    h = h / 360 * 255;
                    s *= 255;
                    v *= 255;

                    int hi = (int)h;
                    int si = (int)s;
                    int vi = (int)v;
                    outH.SetPixel(x, y, Color.FromArgb(hi, hi, hi));
                    outS.SetPixel(x, y, Color.FromArgb(si, si, si));
                    outV.SetPixel(x, y, Color.FromArgb(vi, vi, vi));
                }
            });
        }

        private static (float, float, float) RgbToHsv(Color c)
        {
            float r = c.R / 255f;
            float g = c.G / 255f;
            float b = c.B / 255f;

            float max = Math.Max(r, Math.Max(g, b));
            float min = Math.Min(r, Math.Min(g, b));
            float delta = max - min;

            float h = 0;
            if (max == min)
                h = 0;
            else if (max == r)
                h = (g - b) / delta;
            else if (max == g)
                h = 2 + (b - r) / delta;
            else if (max == b)
                h = 4 + (r - g) / delta;

            h *= 60;
            if (h < 0)
                h += 360;

            float s = 0;
            if (max > 0)
                s = delta / max;

            float v = max;

            return (h, s, v);
        }
    }
}
