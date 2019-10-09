using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Common;

namespace Polygons
{

    class GeometricNode : TreeNode
    {
        public PlaneStructure Structure { get; set; }

        public GeometricNode (String name, PlaneStructure structure) : base (name)
        {
            Structure = structure;
        }
    }

    class Hierarchy
    {
        private TreeView treeView;
        private PolygonManager polygonManager;

        public Hierarchy(TreeView treeView, PolygonManager polygonManager)
        {
            this.treeView = treeView;
            this.polygonManager = polygonManager;

            treeView.AfterSelect += ItemSelected;
        }

        public void Update()
        {
            var polygons = polygonManager.GetPolygons();
            for (int i = 0; i < polygons.Count; i++)
            {
                var polygonNode = new GeometricNode($"Polygon{i}", polygons[i]);

                var edgesNode = new TreeNode("Edges");
                var verticesNode = new TreeNode("Vertices");

                var edges = polygons[i].GetEdges();
                for (int j = 0; j < edges.Count; j++)
                    edgesNode.Nodes.Add(new GeometricNode($"Edge{j}", edges[j]));

                var vertices = polygons[i].GetVertices();
                for (int j = 0; j < vertices.Count; j++)
                    verticesNode.Nodes.Add(new GeometricNode($"Vertex{j}", vertices[j]));

                polygonNode.Nodes.Add(edgesNode);
                polygonNode.Nodes.Add(verticesNode);

                treeView.Nodes.Add(polygonNode);
            }
        }

        private void ItemSelected(object sender, TreeViewEventArgs e)
        {
            if (!(e.Node is GeometricNode))
                return;

            polygonManager.ClearDrawColor(Color.Black);
            var node = e.Node as GeometricNode;
            node.Structure.DrawingColor = Color.Red;
            polygonManager.Draw();
        }
    }
}
