using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum EdgeRelation
    {
        None,
        EqualLength,
        Perpendicular
    }

    public struct RelationInfo
    {
        public Edge E1 { get; set; }
        public Edge E2 { get; set; }
        public EdgeRelation Type { get; set; }

        public RelationInfo(Edge e1, Edge e2, EdgeRelation type)
        {
            E1 = e1;
            E2 = e2;
            Type = type;
        }
    }
}
