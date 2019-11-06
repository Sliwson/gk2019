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
                ProcessUsingLockbitsAndUnsafeAndParallel(bitmap, colorArray);
                var pixel = bitmap.GetPixel(10, 10);

                e.Graphics.DrawImage(bitmap, 0, 0);
            }

            grid.Paint(e.Graphics);

            timer.Stop();
            var fps = 1000 / timer.ElapsedMilliseconds;
            this.Text = $"Lightning (Last draw: {fps} fps)";
        }
        private void ProcessUsingLockbitsAndUnsafeAndParallel(Bitmap processedBitmap, Color[,] colorArray)
        {
            unsafe
            {
                BitmapData bitmapData = processedBitmap.LockBits(new Rectangle(0, 0, processedBitmap.Width, processedBitmap.Height), ImageLockMode.ReadWrite, processedBitmap.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(processedBitmap.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int oldBlue = currentLine[x];
                        int oldGreen = currentLine[x + 1];
                        int oldRed = currentLine[x + 2];

                        int colorX = x / bytesPerPixel;
                        currentLine[x] = colorArray[y, colorX].R;
                        currentLine[x + 1] = colorArray[y, colorX].G;
                        currentLine[x + 2] = colorArray[y, colorX].B;
                    }
                });

                processedBitmap.UnlockBits(bitmapData);
            }
        }
    }
}

