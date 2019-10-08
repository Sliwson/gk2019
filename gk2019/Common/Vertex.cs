using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public class Vertex : IHitTesable
    {
        public Point Position { get; set; }
        public double Radius { get; set; }

        public bool HitTest(Point position)
        {
            if (position.DistanceTo(Position) <= Radius)
                return true;

            return false;
        }
    }
}
