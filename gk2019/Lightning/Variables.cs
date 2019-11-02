using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lightning
{
    struct ObjectColorStruct
    {
        public ObjectColorStruct(bool isConst, Color objectColor)
        {
            IsConst = isConst;
            ObjectColor = objectColor;
        }

        public bool IsConst { get; set; }
        public Color ObjectColor { get; set; }
    }

    struct NormalVectorsStruct
    {
        public NormalVectorsStruct(bool isConst)
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

    struct CoefficientsStruct
    {
        public CoefficientsStruct(bool isRandom, float kd, float ks, int m)
        {
            IsRandom = isRandom;
            this.kd = kd;
            this.ks = ks;
            this.m = m;
            random = new Random();
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
        float Ks
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
        int M
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

        private Random random;
    }

    struct LightStruct
    {
        public LightStruct(Color lightColor, bool isConst)
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
            ObjectColor = new ObjectColorStruct(true, Color.White);
            NormalVectors = new NormalVectorsStruct(true);
            ColorMode = FillColorMode.Precise;
            Coefficients = new CoefficientsStruct(false, 0, 0, 1);
            Light = new LightStruct(Color.White, true);
        }

        public static ObjectColorStruct ObjectColor { get; set; }
        public static NormalVectorsStruct NormalVectors { get; set; }
        public static FillColorMode ColorMode { get; set; }
        public static CoefficientsStruct Coefficients { get; set; }
        public static LightStruct Light { get; set; }
    }
}
