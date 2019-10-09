using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public class Vertex : IHitTesable, IDrawable, IMovable
    {
        public Point Position { get; set; }
        public double Radius { get; set; }

        public Vertex(Point position, double radius)
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

        public void Draw(BitmapCanvas canvas)
        {
            Algorithms.DrawCircle(canvas, Position, Radius, Color.Black);
        }

        public void Move(Point offset)
        {
            Position = Position.Add(offset);
        }
    }
}
