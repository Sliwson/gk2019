using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Common
{
    //represent const sized bitmap canvas
    public class BitmapCanvas
    {
        public Color BackgroundColor { get; set; }

        private Bitmap bitmap;
        public BitmapCanvas(Bitmap bitmap)
        {
            this.bitmap = bitmap;

            BackgroundColor = Color.White;
        }

        public bool IsPointOnBitmap(PointF position)
        {
            return IsPointOnBitmap(Point.Round(position));
        }

        public bool IsPointOnBitmap(Point position)
        {
            return position.X >= 0 && position.X < bitmap.Width && position.Y >= 0 && position.Y < bitmap.Height;
        }

        public void Clear(Color? color = null)
        {
            Color clearColor = color ?? BackgroundColor;

            for (int i = 0; i < bitmap.Height; i++)
                for (int j = 0; j < bitmap.Width; j++)
                    bitmap.SetPixel(j, i, clearColor);
        }

        public void SetPixel(Point position, Color color)
        {
            if (!IsPointOnBitmap(position))
                return;

            bitmap.SetPixel(position.X, position.Y, color);
        }

        public void SetPixel(int x, int y, Color color)
        {
            SetPixel(new Point(x, y), color);
        }

        public Color GetPixel(Point position)
        {
            if (IsPointOnBitmap(position))
                return bitmap.GetPixel(position.X, position.Y);

            return Color.White;
        }
    }
}
