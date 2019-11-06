using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightning
{
    public partial class Form1 : Form
    {
        private Grid grid;
        private Drawer drawer;
        public Form1()
        {
            InitializeComponent();
            grid = new Grid(5, 5, canvas);
            drawer = new Drawer();
        }
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();

            Color[,] colorArray = new Color[canvas.Height, canvas.Width];
            var triangles = grid.GetAllTriangles();

            Parallel.ForEach(triangles, triangle => {
                drawer.FillPolygon(triangle, colorArray);
            });

            using (var bitmap = new Bitmap(canvas.Width, canvas.Height, PixelFormat.Format24bppRgb))
            {
                Drawer.WriteColormapToBitmap(bitmap, colorArray);
                var pixel = bitmap.GetPixel(10, 10);

                e.Graphics.DrawImage(bitmap, 0, 0);
            }

            grid.Paint(e.Graphics);

            timer.Stop();
            var fps = 1000 / timer.ElapsedMilliseconds;
            this.Text = $"Lightning (Last draw: {fps} fps)";
        }
    }
}

