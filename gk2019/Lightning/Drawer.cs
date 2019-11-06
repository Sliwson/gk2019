using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Lightning
{
    class Drawer
    {
        private class ActiveEdge
        {
            public ActiveEdge(Edge edge)
            {
                YMax = edge.End.Position.Y;
                X = edge.Begin.Position.X;

                if (edge.Begin.Position.Y == edge.End.Position.Y)
                    MInverted = float.MaxValue;
                else
                    MInverted = (edge.End.Position.X - edge.Begin.Position.X) / (float)(edge.End.Position.Y - edge.Begin.Position.Y);
            }
            public int YMax { get; set; }
            public float X { get; set; }
            public float MInverted { get; set; }
        }
        public void FillPolygon(List<Vertex> vertices, Graphics g)
        {
            var sorted = vertices.Select((x, i) => new KeyValuePair<Vertex, int>(x, i)).OrderBy(x => x.Key.Position.Y).ToList();
            var min = vertices[sorted.First().Value].Position.Y;
            var max = vertices[sorted.Last().Value].Position.Y;

            var activeList = new List<ActiveEdge>();
            var activeVertices = new List<int>();
            var pen = new Pen(Color.Black);

            int activeIndex = 0;

            for (int y = min; y <= max; y++)
            {
                while (activeIndex < sorted.Count && sorted[activeIndex].Key.Position.Y == y)
                {
                    var currentVertex = sorted[activeIndex].Key;

                    int previousIndex = activeIndex > 0 ? activeIndex - 1 : vertices.Count - 1;
                    int nextIndex = (activeIndex + 1) % vertices.Count;

                    var previousVertex = vertices[previousIndex];
                    var nextVertex = vertices[nextIndex];

                    UpdateAET(currentVertex, previousVertex, activeList);
                    UpdateAET(currentVertex, nextVertex, activeList);
                    
                    activeIndex++;
                }

                activeList.Sort((e1, e2) => e1.X.CompareTo(e2.X));

                for(int i  = 0; i < activeList.Count - 1; i += 2)
                {
                    g.DrawLine(pen, activeList[i].X, y, activeList[i + 1].X, y);
                }

                foreach (var edge in activeList)
                    edge.X += edge.MInverted;
            }
        }

        private void UpdateAET(Vertex current, Vertex next, List<ActiveEdge> activeList)
        {
            if (current.Position.Y < next.Position.Y)
            {
                var newEdge = new Edge(current, next);
                activeList.Add(new ActiveEdge(newEdge));
            }
            else
            {
                activeList.RemoveAll(x => x.YMax == current.Position.Y);
            }
        }
    }
}
