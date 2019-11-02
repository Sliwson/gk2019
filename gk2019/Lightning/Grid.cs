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

        public Grid(int width, int height, PictureBox pictureBox)
        {
            this.width = width;
            this.height = height;
            this.pictureBox = pictureBox;

            InitGrid();
            pictureBox.Resize += PictureBox_Resize;
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
            vertices = new List<List<Vertex>>();
            float xStep = (float)pictureBox.Width / width;
            float yStep = (float)pictureBox.Height / height;

            int y = 0;
            for (; y <= height; y++)
            {
                var row = new List<Vertex>();
                for (int x = 0; x <= width; x++)
                {
                    row.Add(new Vertex(new Point((int)Math.Round(x * xStep), (int)Math.Round(y * yStep)), 3));
                }
                vertices.Add(row);
            }

            edges = new List<Edge>();

            y = 0;
            for (; y < height; y++)
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
                    edges.Add(new Edge(vertices[y][x], vertices[y + 1][x + 1]));
                }
            }
        }

        private void RecalculateVertices()
        {
            int xStep = pictureBox.Width / width;
            int yStep = pictureBox.Height / height;

            for (int y = 0; y <= height; y++)
                for (int x = 0; x <= width; x++)
                    vertices[y][x].Position = new Point(x * xStep, y * yStep); 
        }
    }
}
