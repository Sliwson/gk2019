using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Common
{
    public class Algorithm
    {
        public static void BresenhamLine(Point p1, Point p2, Color c, Graphics graphics)
        {
            Brush brush = new SolidBrush(c);

            int x1 = p1.X, x2 = p2.X, y1 = p1.Y, y2 = p2.Y;
            int dx = x2 - x1;
            int dy = y2 - y1;

            SetPixel(x1, y1, brush, graphics);
            if (Math.Abs(dy) > Math.Abs(dx))
                BresenhamLineHigh(x1, y1, x2, y2, brush, graphics);
            else
                BresenhamLineLow(x1, y1, x2, y2, brush, graphics);
        }

        private static void BresenhamLineLow(int x1, int y1, int x2, int y2, Brush brush, Graphics graphics)
        {
            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int dx = x2 - x1;
            int dy = y2 - y1;

            int yStep = 1;
            if (dy < 0)
            {
                dy *= -1;
                yStep *= -1;
            }

            int d = 2 * dy - dx;
            int incrE = 2 * dy; //increment used for move to E
            int incrNotE = 2 * (dy - dx); //increment used for move to NE
 
            while (x1 < x2)
            {
                x1++;

                if (d < 0) //choose E
                {
                    d += incrE;
                }
                else //choose NE or SE
                {
                    d += incrNotE;
                    y1 += yStep;
                }

                SetPixel(x1, y1, brush, graphics);
            }
        }
        
        private static void BresenhamLineHigh(int x1, int y1, int x2, int y2, Brush brush, Graphics graphics)
        {
            if (y1 > y2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int dx = x2 - x1;
            int dy = y2 - y1;

            int xStep = 1;
            if (dx < 0)
            {
                dx *= -1;
                xStep *= -1;
            }

            int d = 2 * dx - dy;
            int incrE = 2 * dx; //increment used for move to E
            int incrNotE = 2 * (dx - dy); //increment used for move to NE

            while (y1 < y2)
            {
                y1++;

                if (d < 0) //choose S
                {
                    d += incrE;
                }
                else //choose SW or SE
                {
                    d += incrNotE;
                    x1 += xStep;
                }

                SetPixel(x1, y1, brush, graphics);
            }
        }

        private static void SetPixel(int x, int y, Brush b, Graphics g)
        {
            if (g.VisibleClipBounds.Contains(x, y))
                g.FillRectangle(b, x, y, 1, 1);
        }

        private static void Swap<T>(ref T l, ref T r)
        {
            T temporary = l;
            l = r;
            r = temporary;
        }
    }
}
