using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Lightning
{
    class Grid
    {
        private int width = 10;
        private int height = 10;
        
        private PictureBox pictureBox;
        private List<List<Vertex>> vertices;
        private List<Edge> edges;

        private Vertex draggedVertex = null;

        public Grid(int width, int height, PictureBox pictureBox)
        {
            this.width = width;
            this.height = height;
            this.pictureBox = pictureBox;

            InitGrid();
            pictureBox.Resize += PictureBox_Resize;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
        }

        public void Resize(int width, int height)
        {
            this.width = width;
            this.height = height;
            InitGrid();
            pictureBox.Invalidate();
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            draggedVertex = null;
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedVertex != null)
            {
                draggedVertex.Position = e.Location;
                pictureBox.Invalidate();

            }
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            for (int y = 1; y < height; y++)
                for (int x = 1; x < width; x++)
                    if (vertices[y][x].HitTest(e.Location))
                    {
                        draggedVertex = vertices[y][x];
                        return;
                    }
        }

        public void Paint(Graphics g)
        {
            foreach (var edge in edges)
                edge.Draw(g);

            foreach (var vertexList in vertices)
                foreach (var vertex in vertexList)
                    vertex.Draw(g);
        }

        private void PictureBox_Resize(object sender, EventArgs e)
        {
            RecalculateVertices();
            pictureBox.Invalidate();
        }

        private void InitGrid()
        {
            InitVerticesArray();
            RecalculateVertices();
            InitEdgesArray();
        }

        private void InitVerticesArray()
        {
            vertices = new List<List<Vertex>>();
            
            for (int y = 0; y <= height; y++)
            {
                var row = new List<Vertex>();
                for (int x = 0; x <= width; x++)
                    row.Add(new Vertex(new Point(-1, -1), 1));
                vertices.Add(row);
            }
        }

        private void RecalculateVertices()
        {
            float xStep = (float)pictureBox.Width / width;
            float yStep = (float)pictureBox.Height / height;

            for (int y = 0; y <= height; y++)
                for (int x = 0; x <= width; x++)
                    vertices[y][x].Position = new Point((int)Math.Round(x * xStep), (int)Math.Round(y * yStep));
        }

        private void InitEdgesArray()
        {
            edges = new List<Edge>();

            for (int y = 0; y < height; y++)
            {
                int x = 0;
                for (; x < width; x++)
                {
                    //vertical
                    if (x > 0)
                        edges.Add(new Edge(vertices[y][x], vertices[y + 1][x]));
                    //horizontal
                    if (y > 0)
                        edges.Add(new Edge(vertices[y][x], vertices[y][x + 1]));
                    //diagonal
                    edges.Add(new Edge(vertices[y][x + 1], vertices[y + 1][x]));
                }
            }
        }
    }
}
