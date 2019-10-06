using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Common
{
    //represent const sized bitmap canvas
    public class BitmapCanvas
    {
        private Bitmap bitmap;
        public BitmapCanvas(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }

        public void Clear(Color color)
        {
            for (int i = 0; i < bitmap.Height; i++)
                for (int j = 0; j < bitmap.Width; j++)
                    bitmap.SetPixel(j, i, color);
        }
    }
}
