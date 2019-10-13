using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Common;
using System.Windows.Forms;

namespace Polygons
{
    class PolygonManager
    {
        private enum MouseState
        {
            Normal,
            Dragging,
            Drawing
        }

        private PictureBox canvas;
        private ToolStripStatusLabel label;

        private List<Polygon> polygons = new List<Polygon>();

        private PlaneStructure currentStructure = null;
        private Point previousMousePosition = new Point(0, 0);
        private MouseState mouseState = MouseState.Normal;

        public event EventHandler<Polygon> OnStructureChanged;

        public PolygonManager(PictureBox canvas, ToolStripStatusLabel label)
        {
            this.canvas = canvas;
            this.label = label;

            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseUp += Canvas_MouseUp;
            canvas.Paint += CanvasDraw;
        }

        public void ClearDrawColor(Color? color = null)
        {
            Color drawingColor = color ?? Color.Black;

            foreach (var polygon in polygons)
                polygon.DrawingColor = drawingColor;
        }

        public List<Polygon> GetPolygons()
        {
            return polygons;
        }

        public void DeleteStructure(PlaneStructure structure)
        {
            if (structure is Polygon)
            {
                polygons.Remove(structure as Polygon);
                return;
            }

            var polygon = structure.UnderlyingPolygon;
            if (polygon == null)
                return;

            if (structure is Edge)
                polygon.DeleteEdge(structure as Edge);
            else if (structure is Vertex)
                polygon.DeleteVertex(structure as Vertex);

            if (polygon.GetEdges().Count <= 2)
                DeleteStructure(polygon);
        }

        public void InitPolygonAdd()
        {
            if (mouseState == MouseState.Drawing)
            {
                if (currentStructure is Polygon)
                    TryClosePolygon(currentStructure as Polygon);
                else
                    return;
            }

            if (mouseState != MouseState.Normal)
                return;

            mouseState = MouseState.Drawing;
            var polygon = new Polygon();
            currentStructure = polygon;
            polygons.Add(polygon);
            Update();
            OnStructureChanged?.Invoke(this, polygon);
        }

        private void TryClosePolygon(Polygon polygon)
        {
            var result = polygon.ForceClose();
            mouseState = MouseState.Normal;

            if (result == Polygon.ForceCloseResult.DeleteMe)
                polygons.Remove(polygon);
        }

        public void HandleMouseDown()
        {
            if (!(currentStructure is Polygon))
                return;

            if (mouseState == MouseState.Drawing && !IsMouseOver())
            {
                TryClosePolygon(currentStructure as Polygon);
                Update();
                OnStructureChanged?.Invoke(this, currentStructure as Polygon);
            }
        }

        public void InitSample()
        {
            for (int i = 0; i < 4; i++)
            {
                var p = Polygon.GetSampleSquare();
                p.Move(new Point(i * 50, i * 50));
                polygons.Add(p);
            }
        }

        public void UpdateSelectedStructure(PlaneStructure structure)
        {
            currentStructure = structure;
            Update();
        }

        public void Update()
        {
            canvas.Invalidate();
            UpdateStatusStrip();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseState == MouseState.Dragging)
                mouseState = MouseState.Normal;

            UpdateStatusStrip();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentStructure == null)
                return;

            //handle add
            if (mouseState == MouseState.Drawing)
            {
                if (!(currentStructure is Polygon))
                    return;

                var polygon = currentStructure as Polygon;
                var result = polygon.AddVertex(e.Location);
                OnStructureChanged?.Invoke(this, polygon);
                if (result == Polygon.AddVertexResult.Closed)
                    mouseState = MouseState.Normal;

                Update();
                return;
            }

            //handle dragging
            previousMousePosition = e.Location;
            if (currentStructure.HitTest(e.Location))
            {
                if (mouseState == MouseState.Normal)
                    mouseState = MouseState.Dragging;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentStructure == null || !(mouseState == MouseState.Dragging))
                return;

            var delta = new Point(e.Location.X - previousMousePosition.X, e.Location.Y - previousMousePosition.Y);
            previousMousePosition = e.Location;

            currentStructure.Move(delta);
            Update();
        }

        private void CanvasDraw(object sender, PaintEventArgs e)
        {
            foreach (var polygon in polygons)
                polygon.Draw(e.Graphics);
        }

        private void UpdateStatusStrip()
        {
            string state = "Mouse state: ";

            switch (mouseState)
            {
                case MouseState.Dragging:
                    state += "Dragging structure";
                    break;
                case MouseState.Drawing:
                    state += "Drawing polygon";
                    break;
                case MouseState.Normal:
                    state += "Normal";
                    break;
            }

            label.Text = state;
        }

        private bool IsMouseOver()
        {
            return canvas.ClientRectangle.Contains(canvas.PointToClient(Cursor.Position));
        }
    }
}
