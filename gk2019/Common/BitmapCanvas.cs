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

        public void DrawPoint(PointF position, float radius = 1f, Color? color = null)
        {
            Color drawColor = color ?? Color.Black;

            Point startingPoint = Point.Round(position);
            int r = (int)Math.Ceiling(radius);

            for (int y = startingPoint.Y - r; y <= startingPoint.Y + r; y++)
            {
                for (int x = startingPoint.X - r; x <= startingPoint.X + r; x++)
                {
                    Point currentPoint = new Point(x, y);
                    if (IsPointOnBitmap(currentPoint))
                    {
                        if (currentPoint.DistanceTo(startingPoint) <= radius)
                            bitmap.SetPixel(currentPoint.X, currentPoint.Y, drawColor);
                    }
                }
            }
        }
    }
}
