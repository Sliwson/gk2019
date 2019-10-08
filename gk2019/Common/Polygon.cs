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

    public class Polygon
    {
        public enum HitTestResult
        {
            Empty,
            Vertex,
            Edge
        }

        private Vertex lastVertex = null;
        private List<Edge> edges = new List<Edge>();
        private List<Vertex> vertices = new List<Vertex>();

        public bool AddVertex(Vertex v)
        {

        }

        public HitTestResult HitTest(Point P)
        {

        }

        private 
    }
}
