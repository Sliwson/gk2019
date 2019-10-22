﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Numerics;

namespace Common
{
    public class Algorithm
    {
        #region Bresenham

        public static void BresenhamLine(Point p1, Point p2, Color c, Graphics graphics)
        {
            Brush brush = new SolidBrush(c);

            int x1 = p1.X, x2 = p2.X, y1 = p1.Y, y2 = p2.Y;
            int dx = x2 - x1;
            int dy = y2 - y1;

            SetPixel(x1, y1, brush, graphics);
            if (Math.Abs(dy) > Math.Abs(dx))
                BresenhamLineHigh(x1, y1, x2, y2, brush, graphics);
            else
                BresenhamLineLow(x1, y1, x2, y2, brush, graphics);
        }

        private static void BresenhamLineLow(int x1, int y1, int x2, int y2, Brush brush, Graphics graphics)
        {
            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int dx = x2 - x1;
            int dy = y2 - y1;

            int yStep = 1;
            if (dy < 0)
            {
                dy *= -1;
                yStep *= -1;
            }

            int d = 2 * dy - dx;
            int incrE = 2 * dy; //increment used for move to E
            int incrNotE = 2 * (dy - dx); //increment used for move to NE
 
            while (x1 < x2)
            {
                x1++;

                if (d < 0) //choose E
                {
                    d += incrE;
                }
                else //choose NE or SE
                {
                    d += incrNotE;
                    y1 += yStep;
                }

                SetPixel(x1, y1, brush, graphics);
            }
        }
        
        private static void BresenhamLineHigh(int x1, int y1, int x2, int y2, Brush brush, Graphics graphics)
        {
            if (y1 > y2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int dx = x2 - x1;
            int dy = y2 - y1;

            int xStep = 1;
            if (dx < 0)
            {
                dx *= -1;
                xStep *= -1;
            }

            int d = 2 * dx - dy;
            int incrE = 2 * dx; //increment used for move to E
            int incrNotE = 2 * (dx - dy); //increment used for move to NE

            while (y1 < y2)
            {
                y1++;

                if (d < 0) //choose S
                {
                    d += incrE;
                }
                else //choose SW or SE
                {
                    d += incrNotE;
                    x1 += xStep;
                }

                SetPixel(x1, y1, brush, graphics);
            }
        }

        private static void SetPixel(int x, int y, Brush b, Graphics g)
        {
            if (g.VisibleClipBounds.Contains(x, y))
                g.FillRectangle(b, x, y, 1, 1);
        }

        private static void Swap<T>(ref T l, ref T r)
        {
            T temporary = l;
            l = r;
            r = temporary;
        }

        #endregion
        #region Relations

        public static bool CorrectRelation(Polygon clonedPolygon, Vertex startingVertex)
        {
            var edges = clonedPolygon.GetEdges();
            var i = GetStartingEdgeIndex(edges, startingVertex);
            
            //forward iteration
            for (int j = 0; j < edges.Count; j++)
            {
                if (CheckRelationForEdge(edges[i]))
                    break;

                i = (i + 1) % edges.Count;
            }

            //backward iteration
            i = (GetStartingEdgeIndex(edges, startingVertex) - 1 + edges.Count) % edges.Count;
            for (int j = 0; j < edges.Count; j++)
            {
                if (CheckRelationForEdge(edges[i]))
                    break;

                i = (i - 1 + edges.Count) % edges.Count;
            }

            return true;
        }

        private static int GetStartingEdgeIndex(List<Edge> edges, Vertex startingVertex)
        {
            int i = 0;
            for (; i < edges.Count; i++)
            {
                if (edges[i].Begin == startingVertex)
                    break;
            }

            return i;
        }

        private static bool CheckRelationForEdge(Edge edge)
        {
            if (edge.RelationType == EdgeRelation.None)
                return true;

            if (edge.RelationType == EdgeRelation.EqualLength)
                return Math.Abs(edge.Length - edge.RelationEdge.Length) < RelationConstants.EqualLengthEpsilon;
        }

        private static bool AreEdgesPerpendicular(Edge e1, Edge e2)
        {
            Vector2 dir1 = new Vector2(e1.Begin.Position.X - e1.End.Position.X, e1.Begin.Position.Y - e1.End.Position.Y);
            Vector2 dir2 = new Vector2(e2.Begin.Position.X - e2.End.Position.X, e2.Begin.Position.Y - e2.End.Position.Y);
           
            //normalizing in order to have length independent epsilon
            return Vector2.Dot(Vector2.Normalize(dir1), Vector2.Normalize(dir2)) < RelationConstants.PerpendicularDotEpsilon;
        }

        #endregion
    }
}
