﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Numerics;

namespace Common
{
    public class Edge : PlaneStructure, IHitTesable
    {
        public Vertex Begin { get; set; }
        public Vertex End { get; set; }
        public double Length { get { return Begin.Position.DistanceTo(End.Position); } }

        public EdgeRelation RelationType { get; set; }
        public Edge RelationEdge { get; set; }
        public int RelationId { get; set; }

        public Edge (Vertex begin, Vertex end, Polygon underlyingPolygon = null) : base(underlyingPolygon)
        {
            Begin = begin;
            End = end;

            RelationType = EdgeRelation.None;
            RelationEdge = null;
            RelationId = 0;
        }

        public override bool HitTest(Point position)
        {
            const double epsilon = 0.5;
            double distanceThroughPoint = Begin.Position.DistanceTo(position) + position.DistanceTo(End.Position);

            if (Math.Abs(Length - distanceThroughPoint) < epsilon)
                return true;

            return false;
        }

        public override void Draw(Graphics graphics)
        {
            Algorithm.BresenhamLine(Begin.Position, End.Position, DrawingColor, graphics);

            if (RelationType != EdgeRelation.None)
                DrawRelationInfo(graphics);
        }

        public void DrawFast(Graphics graphics)
        {
            graphics.DrawLine(new Pen(DrawingColor), Begin.Position, End.Position);
        }
        
        private void DrawRelationInfo(Graphics graphics)
        {
            string relationString = GetRelationString();        
            graphics.DrawString(relationString, SystemFonts.DefaultFont, Brushes.Black, GetRelationInfoPosition());
        }

        public string GetRelationString()
        {
            string relationString = "";
            if (RelationType == EdgeRelation.Perpendicular)
                relationString += "P";
            else if (RelationType == EdgeRelation.EqualLength)
                relationString += "L";

            relationString += RelationId.ToString();

            return relationString;
        }

        private Point GetRelationInfoPosition()
        {
            Point middle = GetSplitVertex().Position;

            Vector2 edgeDirection = new Vector2(End.Position.X - Begin.Position.X, End.Position.Y - Begin.Position.Y);
            edgeDirection = Vector2.Normalize(edgeDirection);
            edgeDirection = new Vector2(-edgeDirection.Y, edgeDirection.X);
            edgeDirection *= 15; //offset

            return new Point(middle.X + (int)edgeDirection.X, middle.Y + (int)edgeDirection.Y);
        }

        public override bool Move(Point offset)
        {
            var previousPositions = (new Point(Begin.Position.X, Begin.Position.Y), new Point(End.Position.X, End.Position.Y));
            Begin.MoveIgnoringRelations(offset);
            End.MoveIgnoringRelations(offset);

            if (UnderlyingPolygon != null)
            {
                if (!UnderlyingPolygon.CorrectRelations(Begin) || !UnderlyingPolygon.CorrectRelations(End))
                {
                    Begin.Position = previousPositions.Item1;
                    End.Position = previousPositions.Item2;
                    return false;
                }
            }

            return true;
        }

        public Vertex GetSplitVertex()
        {
            var begin = Begin.Position;
            var end = End.Position;
            Point splitPoint = new Point((begin.X + end.X) / 2, (begin.Y + end.Y) / 2);
            return new Vertex(splitPoint, DrawingConstants.PointRadius, UnderlyingPolygon);
        }
        public void SetRelationData(EdgeRelation type, Edge edge, int id)
        {
            RelationType = type;
            RelationEdge = edge;
            RelationId = id;
        }

        public Vector2 GetDirection()
        {
            return new Vector2(End.Position.X - Begin.Position.X, End.Position.Y - Begin.Position.Y);
        }

        public void Rotate(double radians)
        {
            float sin = (float)Math.Sin(radians);
            float cos = (float)Math.Cos(radians);

            Vector2 direction = GetDirection();
            Vector2 rotated = new Vector2(cos * direction.X - sin * direction.Y, sin * direction.X + cos * direction.Y);
            Vector2 offset = rotated - direction;

            End.Position = End.Position.Add(new Point((int)offset.X, (int)offset.Y));
        }
    }
}
