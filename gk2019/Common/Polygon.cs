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

    public abstract class PlaneStructure : IHitTesable
    {
        public virtual Color DrawingColor { get; set; }

        public Polygon UnderlyingPolygon { get; set; }

        public PlaneStructure(Polygon underlyingPolygon)
        {
            DrawingColor = Color.Black;

            UnderlyingPolygon = underlyingPolygon;
        }

        public abstract void Draw(Graphics graphics);

        public abstract void Move(Point offset);

        public abstract bool HitTest(Point position);
    }

    public class Polygon : PlaneStructure
    {
        public enum HitTestResult
        {
            Empty,
            Vertex,
            Edge
        }

        private Color drawingColor = Color.Black;
        public override Color DrawingColor
        {
            get
            {
                return drawingColor;
            }
            set
            {
                drawingColor = value;
                foreach (var edge in edges)
                    edge.DrawingColor = value;
                foreach (var vertex in vertices)
                    vertex.DrawingColor = value;
            }
        }

        private List<Edge> edges = new List<Edge>();
        private List<Vertex> vertices = new List<Vertex>();

        private Vertex lastProcessedVertex = null;

        public Polygon() : base(null)
        {

        }

        public bool AddVertex(Point position)
        {
            var hitTest = HitTestPolygon(position);

            //check vertex hittest
            if (hitTest.Item1 == HitTestResult.Vertex)
            {
                Vertex targetVertex = hitTest.Item2 as Vertex;
                if (IsVertexGoodForClosing(targetVertex))
                {
                    if (lastProcessedVertex == null)
                        lastProcessedVertex = vertices[vertices.Count - 1];

                    edges.Add(new Edge(lastProcessedVertex, targetVertex, this));
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
            Vertex newVertex = new Vertex(position, DrawingConstants.PointRadius, this);
            vertices.Add(newVertex);

            if (lastProcessedVertex != null)
                edges.Add(new Edge(lastProcessedVertex, newVertex, this));

            lastProcessedVertex = newVertex;
            return true;
        }
        public bool SplitEdge(Edge edge)
        {
            if (edge.Length < DrawingConstants.MinimumSplitLength)
                return false;

            var splitVertex = GetSplitVertex(edge);

            lastProcessedVertex = edge.End;
            var secondEdge = new Edge(splitVertex, edge.End, this);
            edge.End = splitVertex;

            edges.Add(secondEdge);
            vertices.Add(splitVertex);

            return true;
        }

        private Vertex GetSplitVertex(Edge edge)
        {
            var begin = edge.Begin.Position;
            var end = edge.End.Position;
            Point splitPoint = new Point((begin.X + end.X) / 2, (begin.Y + end.Y) / 2);
            return new Vertex(splitPoint, DrawingConstants.PointRadius, this);
        }

        public void DeleteVertex(Vertex vertex)
        {
            Edge leftEdge = null, rightEdge = null;
            foreach (var e in edges)
            {
                if (e.End == vertex)
                    leftEdge = e;
                else if (e.Begin == vertex)
                    rightEdge = e;
            }

            vertices.Remove(vertex);
            if (leftEdge == null && rightEdge == null)
                return;

            leftEdge.End = rightEdge.End;
            edges.Remove(rightEdge);
        }

        public void DeleteEdge(Edge edge)
        {
            var splitVertex = GetSplitVertex(edge);
            
            foreach (var e in edges)
            {
                if (e.End == edge.Begin)
                    e.End = splitVertex;
                else if (e.Begin == edge.End)
                    e.Begin = splitVertex;
            }

            vertices.Remove(edge.Begin);
            vertices.Remove(edge.End);
            vertices.Add(splitVertex);
            edges.Remove(edge);
        }

        public List<Vertex> GetVertices()
        {
            return vertices;
        }

        public List<Edge> GetEdges()
        {
            return edges;
        }

        public (HitTestResult, PlaneStructure)  HitTestPolygon(Point position)
        {
            foreach (var vertex in vertices)
                if (vertex.HitTest(position))
                    return (HitTestResult.Vertex, vertex);

            foreach (var edge in edges)
                if (edge.HitTest(position))
                    return (HitTestResult.Edge, edge);

            return (HitTestResult.Empty, null);
        }

        public override bool HitTest(Point position)
        {
            return HitTestPolygon(position).Item1 != HitTestResult.Empty;
        }

        public override void Draw(Graphics graphics)
        {
            foreach (var edge in edges)
                edge.Draw(graphics);

            foreach (var vertex in vertices)
                vertex.Draw(graphics);
        }
        
        public override void Move(Point offset)
        {
            foreach (var vertex in vertices)
                vertex.Move(offset);
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
