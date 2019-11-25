using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colors
{
    class BitmapWrapper
    {
        private Color[,] colors;
        private Size size;
        private Bitmap bitmap;

        public BitmapWrapper(Bitmap bmp)
        {
            Init(bmp);
        }

        public BitmapWrapper(Image img)
        {
            Init(new Bitmap(img));
        }

        public BitmapWrapper(int width, int height)
        {
            size = new Size(width, height);
            colors = new Color[height, width];
        }

        private void Init(Bitmap bmp)
        {
            size = bmp.Size;
            colors = new Color[bmp.Height, bmp.Width];
            bitmap = bmp;

            unsafe //fast bmp to pixels array
            {
                BitmapData bitmapData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                int bytesPerPixel = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int colorX = x / bytesPerPixel;
                        byte B = currentLine[x]; 
                        byte G = currentLine[x + 1];
                        byte R = currentLine[x + 2];
                        byte A = currentLine[x + 3];
                        colors[y, colorX] = Color.FromArgb(R, G, B, A);
                    }
                });

                bmp.UnlockBits(bitmapData);
            }
        }

        public Color GetPixel(int x, int y)
        {
            return colors[y, x];
        }

        public void SetPixel(int x, int y, Color c)
        {
            colors[y, x] = c;
        }

        public Size GetSize()
        {
            return size;
        }

        public Bitmap GetUnderlyingBitmap()
        {
            return bitmap;
        }

        public void WriteToBitmap(Bitmap target)
        {
            unsafe
            {
                BitmapData bitmapData = target.LockBits(new Rectangle(0, 0, target.Width, target.Height), ImageLockMode.ReadWrite, target.PixelFormat);

                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(target.PixelFormat) / 8;
                int heightInPixels = bitmapData.Height;
                int widthInBytes = bitmapData.Width * bytesPerPixel;
                byte* PtrFirstPixel = (byte*)bitmapData.Scan0;

                Parallel.For(0, heightInPixels, y =>
                {
                    byte* currentLine = PtrFirstPixel + (y * bitmapData.Stride);
                    for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                    {
                        int colorX = x / bytesPerPixel;
                        currentLine[x] = colors[y, colorX].B;
                        currentLine[x + 1] = colors[y, colorX].G;
                        currentLine[x + 2] = colors[y, colorX].R;
                    }
                });

                target.UnlockBits(bitmapData);
            }
        }

        public void SaveToFileDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = ".";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(size.Width, size.Height);
                WriteToBitmap(bmp);
                bmp.Save(dialog.FileName, ImageFormat.Jpeg);
            }
        }

        public static BitmapWrapper GetFromFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "bmp files (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg|png files (*.png)|*.png";
            dialog.InitialDirectory = ".";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var stream = dialog.OpenFile();
                try
                {
                    var img = Image.FromStream(stream);
                    return new BitmapWrapper(img);
                }
                catch
                {
                    MessageBox.Show("Error while parsing file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return null;
        }

        public static void Transform(BitmapWrapper source, BitmapWrapper target, Func<Color, Color> transform)
        {
            if (!source.GetSize().Equals(target.GetSize()))
                throw new ArgumentException("Bitmaps sizes does not match");

            var size = target.GetSize();
            Parallel.For(0, size.Height, y =>
            {
                for (int x = 0; x < size.Width; x++)
                    target.SetPixel(x, y, transform(source.GetPixel(x, y)));
            });
        }
    }
}
