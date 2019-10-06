using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point p1, Point p2)
        {
            Point p3 = new Point(p1.X - p2.X, p1.Y - p2.Y);
            return Math.Sqrt(Math.Pow(p3.X, 2) + Math.Pow(p3.Y, 2));
        }
    }
}
