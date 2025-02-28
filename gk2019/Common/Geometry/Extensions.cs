﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point p1, Point p2)
        {
            return Math.Sqrt(p1.DistanceSquaredTo(p2));
        }

        public static double DistanceSquaredTo(this Point p1, Point p2)
        {
            Point p3 = new Point(p1.X - p2.X, p1.Y - p2.Y);
            return Math.Pow(p3.X, 2) + Math.Pow(p3.Y, 2);
        }

        public static Point Add(this Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
    }

    public static class ColorExtensions
    {
        public static float[] ToArray(this Color c)
        {
            return new float[] { c.R / 255f, c.G / 255f, c.B / 255f };
        }
    }
}
