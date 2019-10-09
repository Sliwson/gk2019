using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public interface IHitTesable
    {
        bool HitTest(Point position);
    }

    public interface IDrawable
    {
        void Draw(BitmapCanvas canvas);
    }

    public interface IMovable
    {
        void Move(Point offset);
    }

    public class Polygon : IDrawable, IMovable
    {
        public enum HitTestResult
        {
            Empty,
            Vertex,
            Edge
        }

        private List<Edge> edges = new List<Edge>();
        private List<Vertex> vertices = new List<Vertex>();

        private Vertex lastProcessedVertex = null;
        private readonly double vertexRadius = 3;

        public bool AddVertex(Point position)
        {
            var hitTest = HitTest(position);

            //check vertex hittest
            if (hitTest.Item1 == HitTestResult.Vertex)
            {
                Vertex targetVertex = hitTest.Item2 as Vertex;
                if (IsVertexGoodForClosing(targetVertex))
                {
                    if (lastProcessedVertex == null)
                        lastProcessedVertex = vertices[vertices.Count - 1];

                    edges.Add(new Edge(lastProcessedVertex, targetVertex));
                    lastProcessedVertex = targetVertex;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            //disallow adding vertices on edges
            if (hitTest.Item1 == HitTestResult.Edge)
                return false;

            //add normal vertex
            Vertex newVertex = new Vertex(position, vertexRadius);
            vertices.Add(newVertex);

            if (lastProcessedVertex != null)
                edges.Add(new Edge(lastProcessedVertex, newVertex));

            lastProcessedVertex = newVertex;
            return true;
        }

        public (HitTestResult, IMovable)  HitTest(Point position)
        {
            foreach (var vertex in vertices)
                if (vertex.HitTest(position))
                    return (HitTestResult.Vertex, vertex);

            foreach (var edge in edges)
                if (edge.HitTest(position))
                    return (HitTestResult.Edge, edge);

            return (HitTestResult.Empty, null);
        }

        public void Draw(BitmapCanvas canvas)
        {
            foreach (var edge in edges)
                edge.Draw(canvas);

            foreach (var vertex in vertices)
                vertex.Draw(canvas);
        }
        
        public void Move(Point offset)
        {
            foreach (var edge in edges)
                edge.Move(offset);
        }

        public static Polygon GetSampleSquare()
        {
            Polygon p = new Polygon();
            p.AddVertex(new Point(10, 10));
            p.AddVertex(new Point(10, 100));
            p.AddVertex(new Point(100, 100));
            p.AddVertex(new Point(100, 10));
            p.AddVertex(new Point(10, 10));
            return p;
        }

        private bool IsVertexGoodForClosing(Vertex v)
        {
            int neighbours = 0;

            foreach (var edge in edges)
                if (edge.Begin == v || edge.End == v)
                    neighbours++;

            //we can close rectangle if given vertex has one neighbour and we have at least 2 edges
            return neighbours == 1 && edges.Count > 1;
        }
    }
}
