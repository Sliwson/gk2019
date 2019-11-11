using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lightning
{
    class ObjectColorClass
    {
        public ObjectColorClass(bool isConst, Color objectColor)
        {
            IsConst = isConst;
            ObjectColor = objectColor;
        }

        public bool IsConst { get; set; }
        public Color ObjectColor { get; set; }
    }

    class NormalVectorsClass
    {
        public NormalVectorsClass(bool isConst)
        {
            IsConst = isConst;
        }

        public bool IsConst { get; set; }
        public Vector3 GetConstNormalVector()
        {
            return Vector3.UnitZ;
        }
    }

    enum FillColorMode
    {
        Precise,
        Interpolated,
        Hybrid
    }

    class CoefficientsClass
    {
        public CoefficientsClass(bool isRandom, float kd, float ks, int m)
        {
            IsRandom = isRandom;
            Kd = kd;
            Ks = ks;
            M = m;
        }
        public bool IsRandom { get; set; }

        public float Kd { get; set; }

        public float Ks { get; set; }

        public int M { get; set; }
    }

    class LightClass
    {
        public LightClass(Color lightColor, bool isConst)
        {
            LightColor = lightColor;
            IsConst = isConst;
        }

        public Color LightColor { get; set; }
        public bool IsConst { get; set; }

        private Vector3 position = Vector3.UnitZ;
        private float time = 0f;

        public void Update(float dt, float width, float height)
        {
            time += dt / 3;
            position = new Vector3((float)(Math.Sin(time) + 1) * 0.4f * width, (float)(Math.Cos(time) + 1) * 0.4f * height, (float)(Math.Sin(time / 4) * 50) + 400);
        }       

        public Vector3 GetLightVector(int x, int y)
        {
            if (IsConst)
            {
                return Vector3.UnitZ;
            }
            else
            {
                return Vector3.Normalize(new Vector3(x - position.X, y - position.Y, position.Z));
            }
        }
    }

    class Variables
    {
        static Variables()
        {
            ObjectColor = new ObjectColorClass(false, Color.White);
            NormalVectors = new NormalVectorsClass(false);
            ColorMode = FillColorMode.Precise;
            Coefficients = new CoefficientsClass(false, 0.5f, 0.5f, 32);
            Light = new LightClass(Color.White, true);
        }

        public static ObjectColorClass ObjectColor { get; set; }
        public static NormalVectorsClass NormalVectors { get; set; }
        public static FillColorMode ColorMode { get; set; }
        public static CoefficientsClass Coefficients { get; set; }
        public static LightClass Light { get; set; }
    }
}
