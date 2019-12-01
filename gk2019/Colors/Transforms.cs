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
        public static void RgbToGrayscale(BitmapWrapper input, BitmapWrapper output)
        {
            BitmapWrapper.Transform(input, output, RgbToGrayscale);
        }

        public static Color RgbToGrayscale(Color c)
        {
            int y = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
            if (y > 255)
                y = 255;
            else if (y < 0)
                y = 0;

            return Color.FromArgb(y, y, y);
        }

        public static void RgbToLab(BitmapWrapper input, BitmapWrapper outL, BitmapWrapper outA, BitmapWrapper outB)
        {

        }

        public static void RgbToYCbCr(BitmapWrapper input, BitmapWrapper outY, BitmapWrapper outCb, BitmapWrapper outCr)
        {
            var size = input.GetSize();
            Parallel.For(0, size.Height, h => {
                for (int x = 0; x < size.Width; x++)
                {
                    (float y, float cb, float cr) = RgbToYCbCr(input.GetPixel(x, h));
                    if (y > 255) y = 255;
                    if (cb > 255) cb = 255;
                    if (cr > 255) cr = 255;

                    int yi = (int)y;
                    int cbi = (int)cb;
                    int cri = (int)cr;
                    outY.SetPixel(x, h, Color.FromArgb(yi, yi, yi));
                    outCb.SetPixel(x, h, Color.FromArgb(127, 255 - cbi, cbi));
                    outCr.SetPixel(x, h, Color.FromArgb(cri, 255 - cri, 127));
                }
            });
        }

        private static (float, float, float) RgbToYCbCr(Color c)
        {
            float y = (0.299f * c.R + 0.587f * c.G + 0.114f * c.B);
            float cb = 128 + (-0.169f * c.R - 0.331f * c.G + 0.5f * c.B);
            float cr = 128 + (0.5f * c.R - 0.419f * c.G - -0.081f * c.B);

            return (y, cb, cr);
        }

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
