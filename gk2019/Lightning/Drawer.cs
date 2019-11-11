using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Lightning
{
    class ActiveEdge
    {
        public ActiveEdge(Edge edge)
        {
            YMax = edge.End.Position.Y;
            X = edge.Begin.Position.X;

            if (edge.Begin.Position.Y == edge.End.Position.Y)
                MInverted = float.MaxValue;
            else
                MInverted = (edge.End.Position.X - edge.Begin.Position.X) / (float)(edge.End.Position.Y - edge.Begin.Position.Y);
        }
        public int YMax { get; set; }
        public float X { get; set; }
        public float MInverted { get; set; }
    }

    class Drawer
    {
        private BmpWrapper image;
        private BmpWrapper normalMap;
        private Grid grid;
        public Drawer(Bitmap imageBitmap, Bitmap normalMap, Grid grid)
        {
            this.image = new BmpWrapper(imageBitmap);
            this.normalMap = new BmpWrapper(normalMap);
            this.grid = grid;
        }

        public void SetImage(Bitmap bitmap)
        {
            image = new BmpWrapper(bitmap);
        }

        public void SetNormalMap(Bitmap bitmap)
        {
            normalMap = new BmpWrapper(bitmap);
        }
        
        private Color GetPixelColor(int x, int y, Point triangle)
        {
            var coefficients = Variables.Coefficients.IsRandom ? grid.GetRandomCoefficientsForTriangle(triangle.X, triangle.Y) : Variables.Coefficients;
            var lambert = GetLambertColor(x, y, coefficients);
            var reflection = GetReflectionColor(x, y, coefficients);

            for (int i = 0; i < 3; i++)
            {
                lambert[i] += reflection[i];
                if (lambert[i] < 0) lambert[i] = 0;
                if (lambert[i] > 1) lambert[i] = 1;
            }

            return Color.FromArgb((int)(lambert[0] * 255f), (int)(lambert[1] * 255f), (int)(lambert[2] * 255f));
        }

        private float[] GetLambertColor(int x, int y, CoefficientsClass coefficients)
        {
            var kd = coefficients.Kd;
            var lightColor = Variables.Light.LightColor.ToArray();
            var objectColor = GetObjectColor(x, y);
            
            var normalVector = Variables.NormalVectors.IsConst ? Variables.NormalVectors.GetConstNormalVector() : normalMap.GetPixelAsNormalVector(x, y);
            var lightVector = Variables.Light.IsConst ? Variables.Light.GetLightVector(x, y) : Variables.Light.GetLightVector(x, y);
            var multiplier = kd * Vector3.Dot(normalVector, lightVector);

            for (int i = 0; i < 3; i++)
                objectColor[i] *= lightColor[i] * multiplier;

            return objectColor;
        }

        private  float[] GetReflectionColor(int x, int y, CoefficientsClass coefficients)
        {
            var m = coefficients.M;
            var ks = coefficients.Ks;
            var lightColor = Variables.Light.LightColor.ToArray();
            var objectColor = GetObjectColor(x, y);

            var normalVector = Variables.NormalVectors.IsConst ? Variables.NormalVectors.GetConstNormalVector() : normalMap.GetPixelAsNormalVector(x, y);
            var lightVector = Variables.Light.IsConst ? Variables.Light.GetLightVector(x, y) : Variables.Light.GetLightVector(x, y);
            var R = 2f * Vector3.Dot(normalVector, lightVector) * normalVector - lightVector;

            var cosM = (float)Vector3.Dot(Vector3.UnitZ, R);
            if (cosM < 0) cosM = 0;
            cosM = (float)Math.Pow(cosM, m);

            for (int i = 0; i < 3; i++)
                objectColor[i] *= lightColor[i] * cosM * ks;

            return objectColor;
        }

        private float[] GetObjectColor(int x, int y)
        {
            if (Variables.ObjectColor.IsConst)
                return Variables.ObjectColor.ObjectColor.ToArray();
            else
                return image.GetPixel(x, y).ToArray();
        }

        #region PolygonFill
        public void FillPolygon(List<Vertex> vertices, Color[,] colorsArray, Point triangle)
        {
            var sorted = vertices.Select((x, i) => new KeyValuePair<Vertex, int>(x, i)).OrderBy(x => x.Key.Position.Y).ToList();
            var min = vertices[sorted.First().Value].Position.Y;
            var max = vertices[sorted.Last().Value].Position.Y;

            var activeList = new List<ActiveEdge>();
            var activeVertices = new List<int>();
            
            int activeIndex = 0;

            for (int y = min; y <= max; y++)
            {
                while (activeIndex < sorted.Count && sorted[activeIndex].Key.Position.Y == y)
                {
                    var currentVertex = sorted[activeIndex].Key;
                    var currentIndex = sorted[activeIndex].Value;

                    int previousIndex = currentIndex > 0 ? currentIndex - 1 : vertices.Count - 1;
                    int nextIndex = (currentIndex + 1) % vertices.Count;

                    var previousVertex = vertices[previousIndex];
                    var nextVertex = vertices[nextIndex];

                    UpdateAET(currentVertex, previousVertex, activeList);
                    UpdateAET(currentVertex, nextVertex, activeList);
                    
                    activeIndex++;
                }

                activeList.Sort((e1, e2) => e1.X.CompareTo(e2.X));

                for(int i  = 0; i < activeList.Count - 1; i += 2)
                {
                    for (int x = (int)Math.Round(activeList[i].X); x < (int)Math.Round(activeList[i + 1].X); x++)
                        colorsArray[y, x] = GetPixelColor(x, y, triangle);
                }

                foreach (var edge in activeList)
                    edge.X += edge.MInverted;
            }
        }

        private void UpdateAET(Vertex current, Vertex next, List<ActiveEdge> activeList)
        {
            if (current.Position.Y < next.Position.Y)
            {
                var newEdge = new Edge(current, next);
                activeList.Add(new ActiveEdge(newEdge));
            }
            else
            {
                activeList.RemoveAll(x => x.YMax == current.Position.Y);
            }
        }

        public static void WriteColormapToBitmap(Bitmap processedBitmap, Color[,] colorArray)
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
                        int colorX = x / bytesPerPixel;
                        currentLine[x] = colorArray[y, colorX].B;
                        currentLine[x + 1] = colorArray[y, colorX].G;
                        currentLine[x + 2] = colorArray[y, colorX].R;
                    }
                });

                processedBitmap.UnlockBits(bitmapData);
            }
        }
        #endregion
    }
}
