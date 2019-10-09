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
    public enum NodeType
    {
        Polygon,
        EdgesList,
        VerticesList,
        Edge,
        Vertex
    }

    class GeometricNode : TreeNode
    {
        public PlaneStructure Structure { get; set; }
        public NodeType Type { get; set; }

        public GeometricNode (String name, NodeType type, PlaneStructure structure) : base (name)
        {
            Structure = structure;
            Type = type;
        }
    }

    class Hierarchy
    {
        private TreeView treeView;
        private PolygonManager polygonManager;

        private PlaneStructure structureSelected = null;

        public Hierarchy(TreeView treeView, PolygonManager polygonManager)
        {
            this.treeView = treeView;
            this.polygonManager = polygonManager;

            treeView.AfterSelect += ItemSelected;
            treeView.MouseClick += HierarchyClick;
            treeView.MouseDown += HierarchyMouseDown;
        }

        private void HierarchyMouseDown(object sender, MouseEventArgs e)
        {
            if (treeView.HitTest(e.X, e.Y).Node != null)
                return;

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Add polygon");
            contextMenu.Show(treeView, e.X, e.Y);
        }

        private void HierarchyClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var clickNode = treeView.GetNodeAt(e.X, e.Y);
            treeView.SelectedNode = clickNode;
            var knownNode = clickNode as GeometricNode;

            var contextMenu = new ContextMenuStrip();
            switch (knownNode.Type)
            {
                case NodeType.Edge:
                    contextMenu.Items.Add("Remove");
                    contextMenu.Items.Add("Split");
                    break;
                case NodeType.Vertex:
                    contextMenu.Items.Add("Remove");
                    break;
                case NodeType.EdgesList:
                    return;
                case NodeType.VerticesList:
                    return;
                case NodeType.Polygon:
                    contextMenu.Items.Add("Remove");
                    break;
            }

            contextMenu.Show(treeView, e.X, e.Y);
        }

        public PlaneStructure GetStructureSelected()
        {
            return structureSelected;
        }

        public void Update()
        {
            treeView.Nodes.Clear();
            var polygons = polygonManager.GetPolygons();

            for (int i = 0; i < polygons.Count; i++)
            {
                var polygonNode = new GeometricNode($"Polygon{i}", NodeType.Polygon, polygons[i]);

                var edgesNode = new GeometricNode("Edges", NodeType.EdgesList, null);
                var verticesNode = new GeometricNode("Vertices", NodeType.VerticesList, null);

                var edges = polygons[i].GetEdges();
                for (int j = 0; j < edges.Count; j++)
                    edgesNode.Nodes.Add(new GeometricNode($"Edge{j}", NodeType.Edge, edges[j]));

                var vertices = polygons[i].GetVertices();
                for (int j = 0; j < vertices.Count; j++)
                    verticesNode.Nodes.Add(new GeometricNode($"Vertex{j}", NodeType.Vertex, vertices[j]));

                polygonNode.Nodes.Add(edgesNode);
                polygonNode.Nodes.Add(verticesNode);

                treeView.Nodes.Add(polygonNode);
            }
        }

        private void ItemSelected(object sender, TreeViewEventArgs e)
        {
            var node = e.Node as GeometricNode;
            if (node.Type == NodeType.EdgesList || node.Type == NodeType.VerticesList)
            {
                structureSelected = null;
                return;
            }

            polygonManager.ClearDrawColor(Color.Black);
            structureSelected = node.Structure;
            node.Structure.DrawingColor = Color.Red;

            polygonManager.UpdateSelectedStructure(structureSelected);
        }
    }
}
