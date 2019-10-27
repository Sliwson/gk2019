using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    class DrawingConstants
    {
        public static readonly double PointRadius = 3;
        public static readonly double MinimumSplitLength = 10;
    }

    class RelationConstants
    {
        public static readonly double EqualLengthEpsilon = 1;
        public static readonly double PerpendicularDotEpsilon = 1e-3;
        public static readonly int MinLeftTop = -10000;
        public static readonly int MaxRightBottom = 10000;
    }
}
