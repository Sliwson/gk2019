using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private BitmapCanvas bitmapCanvas;
        private List<Polygon> polygons = new List<Polygon>();

        public Form1()
        {
            InitializeComponent();

            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            canvas.Image = bitmap;
            bitmapCanvas = new BitmapCanvas(bitmap);
            bitmapCanvas.Clear(Color.Yellow);


            for (int i = 0; i < 4; i++)
            {
                var p = Polygon.GetSampleSquare();
                p.Move(new Point(i * 100, i * 100));
                p.Draw(bitmapCanvas);
                polygons.Add(p);
            }

            UpdateTreeView();
        }

        public void UpdateTreeView()
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                var polygonNode = new TreeNode($"Polygon{i}");

                var edgesNode = new TreeNode("Edges");
                var verticesNode = new TreeNode("Vertices");

                for (int j = 0; j < polygons[i].GetEdges().Count; j++)
                    edgesNode.Nodes.Add($"Edge{j}");

                for (int j = 0; j < polygons[i].GetVertices().Count; j++)
                    verticesNode.Nodes.Add($"Vertex{j}");

                polygonNode.Nodes.Add(edgesNode);
                polygonNode.Nodes.Add(verticesNode);

                hierarchy.Nodes.Add(polygonNode);
            }
        }
    }
}
