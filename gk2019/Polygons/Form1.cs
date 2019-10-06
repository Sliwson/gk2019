using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private BitmapCanvas bitmapCanvas;

        public Form1()
        {
            InitializeComponent();

            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            canvas.Image = bitmap;
            bitmapCanvas = new BitmapCanvas(bitmap);
            bitmapCanvas.Clear(Color.Yellow);
        }
    }
}
