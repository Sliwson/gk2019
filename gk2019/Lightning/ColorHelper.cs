using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightning
{
    class ColorHelper
    {
        public float R;
        public float G;
        public float B;
        
        public ColorHelper(Color color)
        {
            this.R = (float)color.R / 255;
            this.G = (float)color.G / 255;
            this.B = (float)color.B / 255;
            Clamp();
        }

        public ColorHelper(float R, float G, float B)
        {
            this.R = (float)R;
            this.G = (float)G;
            this.B = (float)B;
            Clamp();
        }

        public static ColorHelper operator *(ColorHelper lhs, float rhs)
        {
            return new ColorHelper(lhs.R * rhs, lhs.G * rhs, lhs.B * rhs);
        }

        public static ColorHelper operator *(ColorHelper lhs, ColorHelper rhs)
        {
            return new ColorHelper(lhs.R * rhs.R, lhs.G * rhs.G, lhs.B * rhs.B);
        }

        public static ColorHelper operator +(ColorHelper lhs, ColorHelper rhs)
        {
            return new ColorHelper(lhs.R + rhs.R, lhs.G + rhs.G, lhs.B + rhs.B);
        }

        public void Clamp()
        {
            Clamp(ref R);
            Clamp(ref G);
            Clamp(ref B);
        }

        public Color ToColor()
        {
            return Color.FromArgb((int)(R * 255), (int)(G * 255), (int)(B * 255));
        }

        private void Clamp(ref float value)
        {
            if (value < 0) value = 0;
            if (value > 1) value = 1;
        }
    }
}
