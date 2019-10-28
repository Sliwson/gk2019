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

        public Vertex(Point position, double radius, Polygon underlyingPolygon = null) : base(underlyingPolygon)
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
            var DrawRadius = Algorithm.LineWeight + 2;
            graphics.FillEllipse(new SolidBrush(DrawingColor),  (float)(Position.X - DrawRadius), (float)(Position.Y - DrawRadius), (float)DrawRadius * 2, (float)DrawRadius * 2);
        }

        public void MoveIgnoringRelations(Point offset)
        {
            Position = Position.Add(offset);
        }

        public override bool Move(Point offset)
        {
            var previousPosition = new Point(Position.X, Position.Y);
            Position = Position.Add(offset);

            if (UnderlyingPolygon != null)
            {
                if (!UnderlyingPolygon.CorrectRelations(this))
                {
                    Position = previousPosition;
                    return false;
                }
            }

            return true;
        }

        public bool IsOutOfBounds()
        {
            if (Position.X < RelationConstants.MinLeftTop || Position.X > RelationConstants.MaxRightBottom ||
                Position.Y < RelationConstants.MinLeftTop || Position.Y > RelationConstants.MaxRightBottom)
                return true;

            return false;
        }

        public string GetJson()
        {
            var json = "{";
            json += $"x: {Position.X}, y: {Position.Y}";
            json += "}";

            return json;
        }
    }
}
