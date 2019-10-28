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
            Drawing,
            PutingSample
        }

        private PictureBox canvas;

        private List<Polygon> polygons = new List<Polygon>();

        private PlaneStructure currentStructure = null;
        private Point previousMousePosition = new Point(0, 0);
        private MouseState mouseState = MouseState.Normal;

        public event EventHandler<Polygon> OnStructureChanged;
        
        public delegate void CursorChanger(Cursor c);
        private CursorChanger ChangeCursor;

        public delegate void StatusStripChanger(string s);
        private StatusStripChanger ChangeStatusStrip;

        public PolygonManager(PictureBox canvas, CursorChanger cursorChanger, StatusStripChanger statusStripChanger)
        {
            this.canvas = canvas;
            ChangeCursor = cursorChanger;
            ChangeStatusStrip = statusStripChanger;

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
            if (mouseState == MouseState.Drawing && currentStructure is Polygon)
                TryClosePolygon(currentStructure as Polygon);
            
            if (mouseState != MouseState.Normal)
                return;

            mouseState = MouseState.Drawing;
            var polygon = new Polygon();
            currentStructure = polygon;
            polygons.Add(polygon);
            Update();
            OnStructureChanged?.Invoke(this, polygon);
        }

        public void InitSamplePolygonAdd()
        {
            if (mouseState == MouseState.Drawing && currentStructure is Polygon)
                TryClosePolygon(currentStructure as Polygon);

            mouseState = MouseState.PutingSample;
            Update();
            if (currentStructure is Polygon)
                OnStructureChanged?.Invoke(this, currentStructure as Polygon);
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
            if (mouseState == MouseState.Dragging)
                Canvas_MouseUp(this, null);

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

        public string GetPolygonString(Polygon polygon)
        {
            if (!polygons.Contains(polygon))
                return "Unidentified polygon";
            else
                return $"Polygon{polygons.IndexOf(polygon)}";
        }

        public void UpdateSelectedStructure(PlaneStructure structure)
        {
            currentStructure = structure;
            Update();
        }

        public void Update()
        {
            canvas.Invalidate();
            UpdateGui();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseState == MouseState.Dragging)
                mouseState = MouseState.Normal;

            UpdateGui();
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            //handle add sample
            if (mouseState == MouseState.PutingSample)
            {
                var polygon = Polygon.GetSampleSquare();
                polygon.Move(e.Location);
                polygons.Add(polygon);
                OnStructureChanged?.Invoke(this, polygon);
                Update();
                mouseState = MouseState.Normal;
                return;
            }

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

            if (!currentStructure.Move(delta))
            {
                MessageBox.Show("Failure during relations computation");
                mouseState = MouseState.Normal;
            }

            Update();
        }

        private void CanvasDraw(object sender, PaintEventArgs e)
        {
            foreach (var polygon in polygons)
                polygon.Draw(e.Graphics);
        }

        private void UpdateGui()
        {
            var state = "Mouse state: ";
            var cursor = Cursors.Default;

            switch (mouseState)
            {
                case MouseState.Dragging:
                    cursor = Cursors.Hand;
                    state += "Dragging structure";
                    break;
                case MouseState.Drawing:
                    cursor = Cursors.Hand;
                    state += "Drawing polygon";
                    break;
                case MouseState.Normal:
                    cursor = Cursors.Default;
                    state += "Normal";
                    break;
                case MouseState.PutingSample:
                    cursor = Cursors.Hand;
                    state += "Ready to put sample";
                    break;
            }

            ChangeStatusStrip(state);
            ChangeCursor(cursor);
        }

        private bool IsMouseOver()
        {
            return canvas.ClientRectangle.Contains(canvas.PointToClient(Cursor.Position));
        }
    }
}
