using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colors
{
    public enum ColorRepresentation
    {
        HSV,
        YCbCr,
        Lab
    }

    public struct Chromacity
    {
        public Chromacity(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }
    }

    public enum ColorProfile
    {
        sRGB
    }

    public enum Illuminant
    {
        A,
        B,
        C,
        D50,
        D55,
        D65,
        D75,
        I9300K,
        E,
        F2,
        F7,
        F11
    }

    public class LabSettings
    {
        public Chromacity RedPrimary { get; set; }
        public Chromacity GreenPrimary { get; set; }
        public Chromacity BluePrimary { get; set; }
        public Chromacity WhitePoint { get; set; }
        public float Gamma { get; set; }
    }

    class Variables
    {
        public Variables(ColorRepresentation initialRepresentation)
        {
            ColorRepresentation = initialRepresentation;
        }

        public ColorRepresentation ColorRepresentation { get; set; }

        public string[] GetRepresentationLabels()
        {
            switch (ColorRepresentation)
            {
                case ColorRepresentation.HSV:
                    return new string[] { "H", "S", "V" };
                case ColorRepresentation.YCbCr:
                    return new string[] { "Y", "Cb", "Cr" };
                case ColorRepresentation.Lab:
                    return new string[] { "L", "a", "b" };
            }

            return null;
        }

        public static Chromacity IlluminantToWhitePoint(Illuminant i)
        {
            switch (i)
            {
                case Illuminant.A:
                    return new Chromacity { X = 0.44757f, Y = 0.40744f };
                case Illuminant.B:
                    return new Chromacity { X = 0.34840f, Y = 0.35160f };
                case Illuminant.C:
                    return new Chromacity { X = 0.31006f, Y = 0.31615f };
                case Illuminant.D50:
                    return new Chromacity { X = 0.34567f, Y = 0.35850f };
                case Illuminant.D55:
                    return new Chromacity { X = 0.33242f, Y = 0.34743f };
                case Illuminant.D65:
                    return new Chromacity { X = 0.31273f, Y = 0.32902f };
                case Illuminant.D75:
                    return new Chromacity { X = 0.29902f, Y = 0.31485f };
                case Illuminant.I9300K:
                    return new Chromacity { X = 0.28480f, Y = 0.29320f };
                case Illuminant.E:
                    return new Chromacity { X = 0.33333f, Y = 0.33333f };
                case Illuminant.F2:
                    return new Chromacity { X = 0.37207f, Y = 0.37512f };
                case Illuminant.F7:
                    return new Chromacity { X = 0.31285f, Y = 0.32918f };
                case Illuminant.F11:
                    return new Chromacity { X = 0.38054f, Y = 0.37691f };
            }

            return new Chromacity { X = 0.31273f, Y = 0.32902f };
        }

    }
}
