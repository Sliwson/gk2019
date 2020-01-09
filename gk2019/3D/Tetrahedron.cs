using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _3D
{
    class Tetrahedron
    {
        public List<Vector3> vertices = new List<Vector3>();
        public List<(int, int)> edges = new List<(int, int)>();

        public Tetrahedron(float scale = 1f)
        {
            vertices.Add(new Vector3(-1f, 0f, -1f));
            vertices.Add(new Vector3(1f, 0f, -1f));
            vertices.Add(new Vector3(1f, 0f, 1f));
            vertices.Add(new Vector3(0f, 2f, 0f));

            for (int i = 0; i < vertices.Count; i++)
                vertices[i] *= scale;

            edges.Add((0, 1));
            edges.Add((1, 2));
            edges.Add((2, 0));
            edges.Add((0, 3));
            edges.Add((1, 3));
            edges.Add((2, 3));
        }
        
        public List<Vector3> GetVertices()
        {
            return vertices;
        }

        public List<(int, int)> GetEdges()
        {
            return edges;
        }
    }
}
