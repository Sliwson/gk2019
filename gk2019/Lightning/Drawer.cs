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
        public void FillPolygon(List<Vertex> vertices, List<Edge> edges, Graphics g)
        {
            foreach (var edge in edges)
            {
                if (edge.Begin.Position.Y > edge.End.Position.Y)
                {
                    var tmp = edge.Begin;
                    edge.Begin = edge.End;
                    edge.End = tmp;
                }
            }

            edges.Sort((e1, e2) =>
            {
                return e1.Begin.Position.Y.CompareTo(e2.Begin.Position.Y);
            });
            
            var sorted = vertices.Select((x, i) => new KeyValuePair<Vertex, int>(x, i)).OrderBy(x => x.Key.Position.Y).ToList();

            var min = vertices[sorted.First().Value].Position.Y;
            var max = vertices[sorted.Last().Value].Position.Y;

            var activeList = new List<ActiveEdge>();
            var pen = new Pen(Color.Black);

            for (int y = min; y <= max; y++)
            {
                activeList.RemoveAll(e => e.YMax == y);

                foreach (var edge in activeList)
                    edge.X += edge.MInverted;

                activeList.Sort((e1, e2) => e1.X.CompareTo(e2.X));

                for(int i  = 0; i < activeList.Count - 1; i += 2)
                {
                    g.DrawLine(pen, activeList[i].X, y, activeList[i + 1].X, y);
                }

                foreach (var edge in edges)
                {
                    if (edge.Begin.Position.Y == y)
                    {
                        var activeEdge = new ActiveEdge(edge);
                        if (activeEdge.MInverted != float.MaxValue)
                            activeList.Add(activeEdge);
                    }
                }
            }
        }
    }
}
