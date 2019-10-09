using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Common;

namespace Polygons
{
    class PolygonManager
    {
        private BitmapCanvas canvas;
        private List<Polygon> polygons = new List<Polygon>();
        public PolygonManager(BitmapCanvas canvas)
        {
            this.canvas = canvas;
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

        public void Draw()
        {
            canvas.Clear(Color.White);

            foreach (var polygon in polygons)
                polygon.Draw(canvas);

            canvas.Update();
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
    }
}
