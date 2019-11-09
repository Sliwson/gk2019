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

        private Vector3 normalVector = Vector3.UnitZ;
        private float time = 0f;

        public void Update(float dt, float width, float height)
        {
            time += dt / 5;
            normalVector = Vector3.Normalize(new Vector3((float)Math.Sin(time), (float)Math.Cos(time), 0.5f));
        }

        public Vector3 GetLightVector()
        {
            if (IsConst)
                return Vector3.UnitZ;
            else
                return normalVector;
        }
    }

    class Variables
    {
        static Variables()
        {
            ObjectColor = new ObjectColorClass(true, Color.White);
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
