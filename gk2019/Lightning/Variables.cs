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

        public Vector3 GetConstLightVector()
        {
            return Vector3.UnitZ;
        }
    }

    class Variables
    {
        static Variables()
        {
            ObjectColor = new ObjectColorClass(true, Color.White);
            NormalVectors = new NormalVectorsClass(true);
            ColorMode = FillColorMode.Precise;
            Coefficients = new CoefficientsClass(false, 0, 0, 1);
            Light = new LightClass(Color.White, true);
        }

        public static ObjectColorClass ObjectColor { get; set; }
        public static NormalVectorsClass NormalVectors { get; set; }
        public static FillColorMode ColorMode { get; set; }
        public static CoefficientsClass Coefficients { get; set; }
        public static LightClass Light { get; set; }
    }
}
