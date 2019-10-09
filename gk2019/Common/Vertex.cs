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

        public override bool HitTest(Point position)
        {
            if (position.DistanceTo(Position) <= Radius)
                return true;

            return false;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.FillEllipse(new SolidBrush(DrawingColor),  (float)(Position.X - Radius), (float)(Position.Y - Radius), (float)Radius * 2, (float)Radius * 2);
        }

        public override void Move(Point offset)
        {
            Position = Position.Add(offset);
        }
    }
}
