using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            drawer = new Drawer(Properties.Resources.hp, Properties.Resources.normal_1, grid);

            lightTimer.Start();
        }
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();

            Color[,] colorArray = new Color[canvas.Height, canvas.Width];
            var size = grid.GetSize();

            Parallel.For(0, size.Height, y =>
            {
                for (int x = 0; x < size.Width * 2; x++)
                {
                    var triangle = grid.GetTriangleVertices(x, y);
                    drawer.FillPolygon(triangle, colorArray, new Point(x, y));
                }
            });

            using (var bitmap = new Bitmap(canvas.Width, canvas.Height, PixelFormat.Format24bppRgb))
            {
                Drawer.WriteColormapToBitmap(bitmap, colorArray);
                e.Graphics.DrawImage(bitmap, 0, 0);
            }

            grid.Paint(e.Graphics);

            timer.Stop();
            lightTimer.Interval = (int)timer.ElapsedMilliseconds;
            var fps = 1000 / timer.ElapsedMilliseconds;
            this.Text = $"Lightning (Last draw: {fps} fps)";

            GC.Collect();
        }

        private void lightTimer_Tick(object sender, EventArgs e)
        {
            if (Variables.Light.IsConst)
                return;

            Variables.Light.Update((float)lightTimer.Interval / 1000, canvas.Width, canvas.Height);
            canvas.Invalidate();
        }

        private void selectTextureButton_Click(object sender, EventArgs e)
        {
            var bmp = GetBitmapFileDialog();
            if (bmp == null)
                return;

            Cursor.Current = Cursors.WaitCursor;
            drawer.SetImage(bmp);
            canvas.Invalidate();
            Cursor.Current = Cursors.Default;
        }

        private void selectNormalButton_Click(object sender, EventArgs e)
        {
            var bmp = GetBitmapFileDialog();
            if (bmp == null)
                return;

            Cursor.Current = Cursors.WaitCursor;
            drawer.SetNormalMap(bmp);
            canvas.Invalidate();
            Cursor.Current = Cursors.Default;
        }

        private Bitmap GetBitmapFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "bmp files (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var stream = dialog.OpenFile();

                var img = Image.FromStream(stream);
                return new Bitmap(img);
            }

            return null;
        }
    }
}

