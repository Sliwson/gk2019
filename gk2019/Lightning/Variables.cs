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
        public Vector3 GetNormalVector(int x, int y)
        {
            if (IsConst)
            {
                return Vector3.UnitZ;
            }
            else
            {
                //TODO: read from normalmap
                return Vector3.UnitZ;
            }
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
            this.kd = kd;
            this.ks = ks;
            this.m = m;
        }
        public bool IsRandom { get; set; }

        private float kd;
        public float Kd
        {
            get
            {
                if (IsRandom)
                    return (float)random.NextDouble() * 2 - 1;
                else
                    return kd;
            }
            set
            {
                kd = value;
            }
        }

        private float ks;
        public float Ks
        {
            get
            {
                if (IsRandom)
                    return (float)random.NextDouble() * 2 - 1;
                else
                    return ks;
            }
            set
            {
                ks = value;
            }
        }

        private int m;
        public int M
        {
            get
            {
                if (IsRandom)
                    return random.Next(1, 100);
                else
                    return m;
            }
            set
            {
                m = value;
            }
        }

        private Random random = new Random();
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
