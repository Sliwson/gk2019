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

        private Color GetPixelColor(int x, int y, Point triangle, List<(Point, Color)> vertexColors)
        {
            switch (Variables.ColorMode)
            {
                case FillColorMode.Precise:
                    return GetPixelColorPrecise(x, y, triangle);
                case FillColorMode.Interpolated:
                    return GetPixelColorBaricentric(x, y, vertexColors);
                case FillColorMode.Hybrid:
                    return GetPixelColorHybrid(x, y);
            }

            return Color.White;
        }

        private Color GetPixelColorPrecise(int x, int y, Point triangle)
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

        private Color GetPixelColorBaricentric(int x, int y, List<(Point, Color)> triangle)
        {
            var A = triangle[0].Item1;
            var B = triangle[1].Item1;
            var C = triangle[2].Item1;

            var AB = (B.X - A.X, B.Y - A.Y);
            var AP = (x - A.X, y - A.Y);
            var BC = (C.X - B.X, C.Y - B.Y);
            var BP = (x - B.X, y - B.Y);
            var AC = (C.X - A.X, C.Y - A.Y);

            var pABC = GetTriangleField(AB.Item1, AB.Item2, AC.Item1, AC.Item2);
            var pAPB = GetTriangleField(AB.Item1, AB.Item2, AP.Item1, AP.Item2);
            var pBPC = GetTriangleField(BC.Item1, BC.Item2, BP.Item1, BP.Item2);
            var pAPC = GetTriangleField(AC.Item1, AC.Item2, AP.Item1, AP.Item2);

            var alpha = pBPC / pABC;
            var beta = pAPC / pABC;
            var gamma = pAPB / pABC;

            var cA = triangle[0].Item2;
            var cB = triangle[1].Item2;
            var cC = triangle[2].Item2;

            float r = alpha * cA.R + beta * cB.R + gamma * cC.R;
            float g = alpha * cA.G + beta * cB.G + gamma * cC.G;
            float b = alpha * cA.B + beta * cB.B + gamma * cC.B;

            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        private float GetTriangleField(int x1, int y1, int x2, int y2)
        {
            return 0.5f * Math.Abs(x1 * y2 - y1 * x2);
        }

        private Color GetPixelColorHybrid(int x, int y)
        {
            return Color.White;
        }

        private float[] GetLambertColor(int x, int y, CoefficientsClass coefficients)
        {
            var kd = coefficients.Kd;
            var lightColor = Variables.Light.LightColor.ToArray();
            var objectColor = GetObjectColor(x, y);
            
            var normalVector = Variables.NormalVectors.IsConst ? Variables.NormalVectors.GetConstNormalVector() : normalMap.GetPixelAsNormalVector(x, y);
            var lightVector = Variables.Light.IsConst ? Variables.Light.GetLightVector(x, y) : Variables.Light.GetLightVector(x, y);
            var cos = Vector3.Dot(normalVector, lightVector);
            if (cos < 0) cos = 0;

            var multiplier = kd * cos;

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

        private List<(Point, Color)> CalculateVertexColors(Point triangle)
        {
            var vertices = grid.GetTriangleVertices(triangle.X, triangle.Y);
            var returnList = new List<(Point, Color)>();

            foreach (var vertex in vertices)
                returnList.Add((vertex.Position, GetPixelColorPrecise(vertex.Position.X, vertex.Position.Y, triangle)));

            return returnList;
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
            var colorsForBaricentric = CalculateVertexColors(triangle);

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
                        colorsArray[y, x] = GetPixelColor(x, y, triangle, colorsForBaricentric);
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
