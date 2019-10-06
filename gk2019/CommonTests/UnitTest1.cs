using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System.Drawing;

namespace CommonTests
{
    [TestClass]
    public class UnitTest1
    {
        private BitmapCanvas bitmapCanvas;
        private readonly Size size = new Size(100, 100);

        [TestInitialize]
        public void InitBitmapCanvas()
        {
            bitmapCanvas = new BitmapCanvas(new Bitmap(size.Width, size.Height));
            bitmapCanvas.Clear(Color.White);
        }

        [TestMethod]
        public void OutOfRangeArguments()
        {
            bitmapCanvas.Clear(Color.White);

            try
            {
                bitmapCanvas.SetPixel(-1, -1, Color.Black);
                bitmapCanvas.SetPixel(100, 100, Color.Black);
                bitmapCanvas.SetPixel(new Point(100, 100), Color.Black);
                bitmapCanvas.SetPixel(new Point(1, -10), Color.Black);
            }
            catch
            {
                Assert.Fail("No exeption should be thrown");
            }

            Assert.IsTrue(IsWhite(), "After out of bounds drawing canvas should be white");
        }

        [TestMethod]
        public void SimpleCircle()
        {
            bitmapCanvas.Clear();
            Point point = new Point(0, 0);
            Algorithms.DrawCircle(bitmapCanvas, point, 1, Color.Black);
            int black = Color.Black.ToArgb();

            Assert.AreEqual(bitmapCanvas.GetPixel(point).ToArgb(), black);
            Assert.AreEqual(bitmapCanvas.GetPixel(new Point(point.X + 1, point.Y)).ToArgb(), black);
        }

        [TestMethod]
        public void SimpleLine()
        {
            bitmapCanvas.Clear();
            Point begin = new Point(0, 0);
            Point end = new Point(99, 99);
            Point middle = new Point(99 / 2, 99 / 2);

            Algorithms.DrawLine(bitmapCanvas, begin, end, Color.Black);
            int black = Color.Black.ToArgb();

            Assert.AreEqual(bitmapCanvas.GetPixel(begin).ToArgb(), black);
            Assert.AreEqual(bitmapCanvas.GetPixel(end).ToArgb(), black);
            Assert.AreEqual(bitmapCanvas.GetPixel(middle).ToArgb(), black);
        }

        private bool IsWhite()
        {
            for (int i = 0; i < size.Height; i++)
                for (int j = 0; j < size.Width; j++)
                {
                    Color c = bitmapCanvas.GetPixel(new Point(j, i));
                    if (c.ToArgb() != Color.White.ToArgb())
                        return false;
                }
                    

            return true;
        }
    }
}
