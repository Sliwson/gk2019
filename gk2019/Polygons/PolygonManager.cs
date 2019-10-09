﻿using System;
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
        private PictureBox canvas;
        private List<Polygon> polygons = new List<Polygon>();

        private PlaneStructure currentStructure = null;
        private Point previousMousePosition = new Point(0, 0);
        private bool canDrag = false;

        public PolygonManager(PictureBox canvas)
        {
            this.canvas = canvas;

            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseUp += Canvas_MouseUp;
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            canDrag = false;
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentStructure == null)
                return;

            previousMousePosition = e.Location;
            if(currentStructure.HitTest(e.Location))
                canDrag = true;            
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentStructure == null || !canDrag)
                return;

            var delta = new Point(e.Location.X - previousMousePosition.X, e.Location.Y - previousMousePosition.Y);
            previousMousePosition = e.Location;

            currentStructure.Move(delta);
            Update();
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

        public void Draw(Graphics graphics)
        {
            foreach (var polygon in polygons)
                polygon.Draw(graphics);
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
        }
    }
}
