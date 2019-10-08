using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Common
{
    public class Edge : IHitTesable
    {
        public Vertex Begin { get; set; }
        public Vertex End { get; set; }

        public bool HitTest(Point position)
        {
            
        }
    }
}
