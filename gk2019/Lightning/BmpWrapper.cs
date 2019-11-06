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
        public BmpWrapper(Bitmap bmp)
        {
            colors = new Color[bmp.Height, bmp.Width];

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    colors[y, x] = bmp.GetPixel(x, y);
        }

        public Color GetPixel(int x, int y)
        {
            return colors[y, x];
        }

        public Vector3 GetPixelAsNormalVector(int x, int y)
        {
            var color = colors[y, x];
            float r = ((float)color.R - 127) / 128;
            float g = ((float)color.G - 127) / 128;
            float b = ((float)color.B) / 255;

            return Vector3.Normalize(new Vector3(r, g, b));
        }
    }
}
