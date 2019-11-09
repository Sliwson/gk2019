using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lightning
{
    class BmpWrapper
    {
        private Color[,] colors;
        private Size size;
        public BmpWrapper(Bitmap bmp)
        {
            size = bmp.Size;
            colors = new Color[bmp.Height, bmp.Width];

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    colors[y, x] = bmp.GetPixel(x, y);
        }

        public Color GetPixel(int x, int y)
        {
            return colors[y % size.Height, x % size.Width];
        }

        public Vector3 GetPixelAsNormalVector(int x, int y)
        {
            var color = GetPixel(x, y);
            float r = ((float)color.R - 127) / 128;
            float g = (127 - (float)color.G) / 128;
            float b = ((float)color.B - 127) / 128;

            return Vector3.Normalize(new Vector3(r, g, b));
        }
    }
}
