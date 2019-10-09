using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public class Edge : IHitTesable, IGeometric
    {
        public Vertex Begin { get; set; }
        public Vertex End { get; set; }

        public double Length { get { return Begin.Position.DistanceTo(End.Position); } }

        public Edge (Vertex begin, Vertex end)
        {
            Begin = begin;
            End = end;
        }

        public bool HitTest(Point position)
        {
            const double epsilon = 0.5;
            double distanceThroughPoint = Begin.Position.DistanceTo(position) + position.DistanceTo(End.Position);

            if (Math.Abs(Length - distanceThroughPoint) < epsilon)
                return true;

            return false;
        }

        public void Draw(BitmapCanvas canvas)
        {
            Algorithms.DrawLine(canvas, Begin.Position, End.Position, Color.Black);
        }

        public void Move(Point offset)
        {
            Begin.Move(offset);
            End.Move(offset);
        }
    }
}
