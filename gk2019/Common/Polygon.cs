using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

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

        public enum AddVertexResult
        {
            Added,
            Closed,
            Failed
        }

        public enum ForceCloseResult
        {
            Closed,
            DeleteMe,
            IsOk
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
        private int relationCounter = 0;

        public Polygon() : base(null)
        {

        }

        public List<Vertex> GetVertices()
        {
            return vertices;
        }

        public List<Edge> GetEdges()
        {
            return edges;
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

        public AddVertexResult AddVertex(Point position)
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
                    return AddVertexResult.Closed;
                }
                else
                {
                    return AddVertexResult.Failed;
                }
            }

            //disallow adding vertices on edges
            if (hitTest.Item1 == HitTestResult.Edge)
                return AddVertexResult.Failed;

            //add normal vertex
            Vertex newVertex = new Vertex(position, DrawingConstants.PointRadius, this);
            vertices.Add(newVertex);

            if (lastProcessedVertex != null)
                edges.Add(new Edge(lastProcessedVertex, newVertex, this));

            lastProcessedVertex = newVertex;
            return AddVertexResult.Added;
        }

        public bool SplitEdge(Edge edge)
        {
            if (edge.Length < DrawingConstants.MinimumSplitLength)
                return false;

            RemoveRelation(edge);

            var splitVertex = edge.GetSplitVertex();

            lastProcessedVertex = edge.End;
            var secondEdge = new Edge(splitVertex, edge.End, this);
            edge.End = splitVertex;

            edges.Add(secondEdge);
            vertices.Add(splitVertex);

            return true;
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

            RemoveRelation(leftEdge);
            RemoveRelation(rightEdge);

            leftEdge.End = rightEdge.End;
            edges.Remove(rightEdge);
        }

        public void DeleteEdge(Edge edge)
        {
            if (!edges.Contains(edge))
                return;

            RemoveRelation(edge);

            var splitVertex = edge.GetSplitVertex();
            
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
        
        public ForceCloseResult ForceClose()
        {
            if (edges.Count <= 1)
                return ForceCloseResult.DeleteMe;

            Dictionary<Vertex, int> dict = new Dictionary<Vertex, int>();
            foreach (var v in vertices)
                dict.Add(v, 0);
            
            foreach (var e in edges)
            {
                dict[e.Begin] += 1;
                dict[e.End] += 1;
            }

            var singleVertices = dict.Where(x => x.Value == 1);
            if (singleVertices.Count() == 0)
                return ForceCloseResult.IsOk;
            else if (singleVertices.Count() != 2)
                return ForceCloseResult.DeleteMe;

            var v1 = singleVertices.First();
            var v2 = singleVertices.Last();

            lastProcessedVertex = v2.Key;
            AddVertex(v1.Key.Position);
            return ForceCloseResult.Closed;
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

        public bool AddRelation(RelationInfo relation)
        {
            if (!ValidateIncomingRelation(relation))
                return false;

            var e1 = relation.E1;
            var e2 = relation.E2;

            e1.SetRelationData(relation.Type, e2, relationCounter);
            e2.SetRelationData(relation.Type, e1, relationCounter);
            
            if (!CorrectRelations(e1.Begin))
            {
                //rollback relation add
                e1.SetRelationData(EdgeRelation.None, null, 0);
                e2.SetRelationData(EdgeRelation.None, null, 0);
                return false;
            }

            relationCounter++;
            return true;
        }

        public void RemoveRelation(Edge edge)
        {
            if (!edges.Contains(edge) || edge.RelationType == EdgeRelation.None)
                return;

            var pair = edge.RelationEdge;
            edge.SetRelationData(EdgeRelation.None, null, 0);
            if (pair != null)
                pair.SetRelationData(EdgeRelation.None, null, 0);
        }

        private bool ValidateIncomingRelation(RelationInfo relation)
        {
            var e1 = relation.E1;
            var e2 = relation.E2;

            if (!edges.Contains(e1) || !edges.Contains(e2))
                return false;

            if (e1.RelationType != EdgeRelation.None || e2.RelationType != EdgeRelation.None || relation.Type == EdgeRelation.None)
                return false;

            return true;
        }

        public bool CorrectRelations(Vertex startingVertex)
        {
            return true;
        }

        public static Polygon GetSampleSquare()
        {
            Polygon p = new Polygon();
            p.AddVertex(new Point(0, 0));
            p.AddVertex(new Point(0, 150));
            p.AddVertex(new Point(150, 150));
            p.AddVertex(new Point(150, 0));
            p.AddVertex(new Point(0, 0));

            var edges = p.GetEdges();
            var r1 = new RelationInfo(edges[0], edges[1], EdgeRelation.EqualLength);
            var r2 = new RelationInfo(edges[2], edges[3], EdgeRelation.Perpendicular);
            p.AddRelation(r1);
            p.AddRelation(r2);

            return p;
        }

        private bool IsVertexGoodForClosing(Vertex v)
        {
            if (lastProcessedVertex == v)
                return false;

            int neighbours = 0;

            foreach (var edge in edges)
                if (edge.Begin == v || edge.End == v)
                    neighbours++;

            //we can close rectangle if given vertex has one neighbour and we have at least 2 edges
            return neighbours == 1 && edges.Count > 1;
        }
    }
}
