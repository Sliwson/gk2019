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

            var contextMenu = CreateContextMenu(null);
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
            //remove polygon, //remove edge // remove vertex, //add polygon //add edge //add vertex
            var polygons = polygonManager.GetPolygons();
            if (treeView.Nodes.Count < polygons.Count)
            {
                var node = CreatePolygonNode(polygons.Last(), treeView.Nodes.Count);
                treeView.Nodes.Add(node);
            }
            else if (treeView.Nodes.Count > polygons.Count)
            {
                RemoveMissingNode();
            }

            //treeView.Nodes.Count == polygons.Count
            for (int i = 0; i < polygons.Count; i++)
                UpdateNode(treeView.Nodes[i] as GeometricNode, polygons[i], i);
        }

        private GeometricNode CreatePolygonNode(Polygon polygon, int i)
        {
            var polygonNode = new GeometricNode($"Polygon{i}", NodeType.Polygon, polygon);

            var edgesNode = new GeometricNode("Edges", NodeType.EdgesList, null);
            var verticesNode = new GeometricNode("Vertices", NodeType.VerticesList, null);

            var edges = polygon.GetEdges();
            for (int j = 0; j < edges.Count; j++)
                edgesNode.Nodes.Add(new GeometricNode("", NodeType.Edge, edges[j]));

            UpdateEdgesLabels(edgesNode.Nodes);

            var vertices = polygon.GetVertices();
            for (int j = 0; j < vertices.Count; j++)
                verticesNode.Nodes.Add(new GeometricNode($"Vertex{j}", NodeType.Vertex, vertices[j]));

            polygonNode.Nodes.Add(edgesNode);
            polygonNode.Nodes.Add(verticesNode);

            return polygonNode;
        }

        private void UpdateEdgesLabels(TreeNodeCollection edgeNodes)
        {
            for (int i = 0; i < edgeNodes.Count; i++)
            {
                var geometricNode = edgeNodes[i] as GeometricNode;
                var edge = geometricNode.Structure as Edge;
                var label = $"Edge{i}";

                if (edge.RelationType != EdgeRelation.None)
                    label += $" [{edge.GetRelationString()}]";

                geometricNode.Text = label;
            }
        }

        private void RemoveMissingNode()
        {
            var polygons = polygonManager.GetPolygons();
            bool removed = false;

            for (int i = 0; i < polygons.Count; i++)
            {
                if ((treeView.Nodes[i] as GeometricNode).Structure != polygons[i])
                {
                    treeView.Nodes[i].Remove();
                    removed = true;
                }
            }

            if (!removed)
                treeView.Nodes.RemoveAt(treeView.Nodes.Count - 1);
        }

        private void UpdateNode(GeometricNode node, Polygon polygon, int i)
        {
            if (!node.Structure.Equals(polygon))
            {
                node = CreatePolygonNode(polygon, i);
                return;
            }

            UpdateEdges(node.Nodes[0].Nodes, polygon.GetEdges());
            UpdateVertices(node.Nodes[1].Nodes, polygon.GetVertices());
        }

        private void UpdateEdges(TreeNodeCollection edgesList, List<Edge> polygonEdgesList)
        {
            int i = 0;
            for (; i < polygonEdgesList.Count && i < edgesList.Count; i++)
                if ((edgesList[i] as GeometricNode).Structure != polygonEdgesList[i])
                    break;

            while (i < edgesList.Count)
                edgesList.RemoveAt(i);

            for (; i < polygonEdgesList.Count; i++)
                edgesList.Add(new GeometricNode("", NodeType.Edge, polygonEdgesList[i]));

            UpdateEdgesLabels(edgesList);
        }

        private void UpdateVertices(TreeNodeCollection verticesList, List<Vertex> polygonVertexList)
        {
            int i = 0;
            for (; i < polygonVertexList.Count && i < verticesList.Count; i++)
                if ((verticesList[i] as GeometricNode).Structure != polygonVertexList[i])
                    break;

            while (i < verticesList.Count)
                verticesList.RemoveAt(i);

            for (; i < polygonVertexList.Count; i++)
                verticesList.Add(new GeometricNode($"Vertex{i}", NodeType.Vertex, polygonVertexList[i]));
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
            else
                treeView.SelectedNode = null;

            polygonManager.UpdateSelectedStructure(structureSelected);
        }

        private TreeNode FindTreeNode(PlaneStructure structure)
        {
            //polygons
            foreach (GeometricNode node in treeView.Nodes)
            {
                if (node.Structure == structure)
                    return node;

                //edges
                foreach (GeometricNode edgeNode in node.Nodes[0].Nodes)
                    if (edgeNode.Structure == structure)
                        return edgeNode;

                //vertices
                foreach (GeometricNode vertexNode in node.Nodes[1].Nodes)
                    if (vertexNode.Structure == structure)
                        return vertexNode;
            }

            return null;
        }

        private ContextMenuStrip CreateContextMenu(GeometricNode node)
        {
            var contextMenu = new ContextMenuStrip();

            if (node != null)
            {
                if (node.Type == NodeType.Edge)
                {
                    var split = contextMenu.Items.Add("Split");
                    split.Click += SplitClick;

                    var edge = node.Structure as Edge;
                    if (edge.RelationType != EdgeRelation.None)
                    {
                        var removeRelation = contextMenu.Items.Add("Remove relation");
                        removeRelation.Click += RemoveRelationClick;
                    }
                }

                if (node.Type == NodeType.Edge || node.Type == NodeType.Vertex || node.Type == NodeType.Polygon)
                {
                    var remove = contextMenu.Items.Add("Remove");
                    remove.Click += RemoveClick;
                }

                if (node.Type != NodeType.EdgesList && node.Type != NodeType.VerticesList)
                    contextMenu.Items.Add("-");
            }

            var addPolygon = contextMenu.Items.Add("Add polygon");
            addPolygon.Click += AddPolygonContextMenu;

            var addSamplePolygon = contextMenu.Items.Add("Add sample polygon");
            addSamplePolygon.Click += AddSamplePolygonContextMenu;

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

        private void RemoveRelationClick(object sender, EventArgs e)
        {
            if (!(structureSelected is Edge))
                return;

            var edge = structureSelected as Edge;
            if (edge.UnderlyingPolygon == null)
                return;

            edge.UnderlyingPolygon.RemoveRelation(edge);
            Update();
            polygonManager.Update();
        }

        private void AddPolygonContextMenu(object sender, EventArgs e)
        {
            treeView.SelectedNode = null;
            polygonManager.InitPolygonAdd();
        }

        private void AddSamplePolygonContextMenu(object sender, EventArgs e)
        {
            treeView.SelectedNode = null;
            polygonManager.InitSamplePolygonAdd();
        }

    }
}
