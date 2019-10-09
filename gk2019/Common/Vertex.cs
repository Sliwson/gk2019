using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public class Vertex : PlaneStructure, IHitTesable
    {
        public Point Position { get; set; }
        public double Radius { get; set; }

        public Vertex(Point position, double radius) : base()
        {
            Position = position;
            Radius = radius;
        }

        public bool HitTest(Point position)
        {
            if (position.DistanceTo(Position) <= Radius)
                return true;

            return false;
        }

        public override void Draw(BitmapCanvas canvas)
        {
            Algorithms.DrawCircle(canvas, Position, Radius, DrawingColor);
        }

        public override void Move(Point offset)
        {
            Position = Position.Add(offset);
        }
    }
}
