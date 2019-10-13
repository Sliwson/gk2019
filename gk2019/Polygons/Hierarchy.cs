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

            if (e.Button != MouseButtons.Right)
                return;

            var contextMenu = new ContextMenuStrip();
            var addPolygon = contextMenu.Items.Add("Add polygon");
            addPolygon.Click += AddPolygonContextMenu;
            contextMenu.Show(treeView, e.X, e.Y);
        }

        private void HierarchyClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var clickNode = treeView.GetNodeAt(e.X, e.Y);
            treeView.SelectedNode = clickNode;
            var knownNode = clickNode as GeometricNode;

            var contextMenu = CreateContextMenu(knownNode);
            if (contextMenu != null)
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

        public void HandleHierarchyChange(object sender, Polygon polygon)
        {
            Update();

            if (polygon != null)
            {
                ExpandNode(polygon);
                StructureSelected(polygon);
            }
        }

        private void ItemSelected(object sender, TreeViewEventArgs e)
        {
            //close currently drawn polygon
            polygonManager.HandleMouseDown();

            var node = e.Node as GeometricNode;
            if (node.Type == NodeType.EdgesList || node.Type == NodeType.VerticesList)
            {
                structureSelected = null;
                return;
            }

            StructureSelected(node.Structure);
        }

        private void StructureSelected(PlaneStructure structure)
        {
            polygonManager.ClearDrawColor(Color.Black);
            structureSelected = structure;

            if (structure != null)
                structure.DrawingColor = Color.Red;
            
            polygonManager.UpdateSelectedStructure(structureSelected);
        }

        private ContextMenuStrip CreateContextMenu(GeometricNode node)
        {
            if (node.Type == NodeType.EdgesList || node.Type == NodeType.VerticesList)
                return null;

            var contextMenu = new ContextMenuStrip();

            if (node.Type == NodeType.Edge)
            {
                var split = contextMenu.Items.Add("Split");
                split.Click += SplitClick;
            }

            if (node.Type == NodeType.Edge || node.Type == NodeType.Vertex || node.Type == NodeType.Polygon)
            {
                var remove = contextMenu.Items.Add("Remove");
                remove.Click += RemoveClick;
            }

            return contextMenu;
        }

        private void ExpandNode(Polygon polygon)
        {
            if (polygon == null)
                return;

            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                var geometricNode = treeView.Nodes[i] as GeometricNode;
                if (geometricNode.Type == NodeType.Polygon && geometricNode.Structure as Polygon == polygon)
                    treeView.Nodes[i].ExpandAll();
            }
        }

        private void RemoveClick(object sender, EventArgs e)
        {
            if (!(structureSelected is PlaneStructure))
                 return;

            polygonManager.DeleteStructure(structureSelected);
            Update();
            ExpandNode(structureSelected.UnderlyingPolygon);
            StructureSelected(null);
        }

        private void SplitClick(object sender, EventArgs e)
        {
            if (!(structureSelected is Edge))
                return;

            var edge = structureSelected as Edge;
            if (edge.UnderlyingPolygon == null)
                return;

            if (edge.UnderlyingPolygon.SplitEdge(edge))
            {
                Update();
                StructureSelected(null);
                ExpandNode(edge.UnderlyingPolygon);
            }
        }

        private void AddPolygonContextMenu(object sender, EventArgs e)
        {
            polygonManager.InitPolygonAdd();
        }
    }
}
