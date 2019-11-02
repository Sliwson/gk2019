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

        private void PictureBox_Resize(object sender, EventArgs e)
        {
            RecalculateVertices();
        }

        private void InitGrid()
        {
            vertices = new List<List<Vertex>>();
            int xStep = pictureBox.Width / width;
            int yStep = pictureBox.Height / height;

            int y = 0;
            for (; y <= height; y++)
            {
                var row = new List<Vertex>();
                for (int x = 0; x <= width; x++)
                {
                    row.Add(new Vertex(new Point(x * xStep, y * yStep), 3));
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
                    edges.Add(new Edge(vertices[y][x], vertices[y + 1][x]));
                    //horizontal
                    edges.Add(new Edge(vertices[y][x], vertices[y][x + 1]));
                    //diagonal
                    edges.Add(new Edge(vertices[y][x], vertices[y + 1][x + 1]));
                }

                //last column vertical
                edges.Add(new Edge(vertices[y][x], vertices[y + 1][x]));
            }

            //last row horizontal
            for (int x = 0; x < width; x++)
                edges.Add(new Edge(vertices[y][x], vertices[y][x + 1]));
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
