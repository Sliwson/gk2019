using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public class Algorithms
    {
        //naive implementation for now
        public static void DrawCircle(BitmapCanvas canvas, Point position, double radius = 1, Color? color = null)
        {
            Color drawColor = color ?? Color.Black;

            int r = (int)Math.Ceiling(radius);

            for (int y = position.Y - r; y <= position.Y + r; y++)
            {
                for (int x = position.X - r; x <= position.X + r; x++)
                {
                    Point currentPoint = new Point(x, y);
                    if (canvas.IsPointOnBitmap(currentPoint))
                    {
                        if (currentPoint.DistanceTo(position) <= radius)
                            canvas.SetPixel(currentPoint, drawColor);
                    }
                }
            }
        }

        public static void DrawLine(BitmapCanvas canvas, Point a, Point b, Color? color = null)
        {
            if (!canvas.IsPointOnBitmap(a) || !canvas.IsPointOnBitmap(b))
                return;

            Color drawColor = color ?? Color.Black;
            
            if (a.X > b.X)
            {
                Point tmp = a;
                a = b;
                b = tmp;
            }


            //vertical
            if (a.X == b.X)
            {
                if (a.Y == b.Y)
                {
                    canvas.SetPixel(a, drawColor);
                    return;
                }

                int y = a.Y;
                do
                {
                    canvas.SetPixel(new Point(a.X, y), drawColor);
                    y += b.Y > y ? 1 : -1;
                } while (y != b.Y);
                return;
            }

            //y = ax + b
            Point diff = new Point(b.X - a.X, b.Y - a.Y);
            double angle = (double)diff.Y / (double)diff.X;
            double height = a.Y - angle * a.X;

            for (int x = a.X; x <= b.X; x++)
            {
                int y = (int)Math.Round(angle * x + height);
                canvas.SetPixel(new Point(x, y), drawColor);
            }          
        }
    }
}
