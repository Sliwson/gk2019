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
        sRGB,
        AdobeRGB,
        AppleRGB,
        CIERGB,
        WideGamut,
        PAL
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

    public class ParametersBase
    {
        public Chromacity RedPrimary { get; set; }
        public Chromacity GreenPrimary { get; set; }
        public Chromacity BluePrimary { get; set; }
        public float Gamma { get; set; }
    }
    public class LabSettings : ParametersBase
    {
        public Chromacity WhitePoint { get; set; }
    }

    public class PredefinedColorProfile : ParametersBase
    {
        public Illuminant WhitePoint { get; set; }
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
        
        public static PredefinedColorProfile ColorProfileToLabSettings(ColorProfile c)
        {
            switch (c)
            {
                case ColorProfile.sRGB:
                    return new PredefinedColorProfile
                    {
                        RedPrimary = new Chromacity(0.64f, 0.33f),
                        GreenPrimary = new Chromacity(0.3f, 0.6f),
                        BluePrimary = new Chromacity(0.15f, 0.06f),
                        WhitePoint = Illuminant.D65,
                        Gamma = 2.2f
                    };
                case ColorProfile.AdobeRGB:
                    return new PredefinedColorProfile
                    {

                        RedPrimary = new Chromacity(0.64f, 0.33f),
                        GreenPrimary = new Chromacity(0.21f, 0.71f),
                        BluePrimary = new Chromacity(0.15f, 0.06f),
                        WhitePoint = Illuminant.D65,
                        Gamma = 2.2f
                    };
                case ColorProfile.AppleRGB:
                    return new PredefinedColorProfile
                    {
                        RedPrimary = new Chromacity(0.625f, 0.34f),
                        GreenPrimary = new Chromacity(0.28f, 0.595f),
                        BluePrimary = new Chromacity(0.155f, 0.07f),
                        WhitePoint = Illuminant.D65,
                        Gamma = 1.8f
                    };
                case ColorProfile.CIERGB:
                    return new PredefinedColorProfile
                    {
                        RedPrimary = new Chromacity(0.735f, 0.265f),
                        GreenPrimary = new Chromacity(0.274f, 0.7170f),
                        BluePrimary = new Chromacity(0.167f, 0.009f),
                        WhitePoint = Illuminant.E,
                        Gamma = 2.2f
                    };
                case ColorProfile.WideGamut:
                    return new PredefinedColorProfile
                    {
                        RedPrimary = new Chromacity(0.7347f, 0.2653f),
                        GreenPrimary = new Chromacity(0.1152f, 0.8264f),
                        BluePrimary = new Chromacity(0.1566f, 0.0177f),
                        WhitePoint = Illuminant.D50,
                        Gamma = 1.2f
                    };
                case ColorProfile.PAL:
                    return new PredefinedColorProfile
                    {
                        RedPrimary = new Chromacity(0.64f, 0.33f),
                        GreenPrimary = new Chromacity(0.29f, 0.6f),
                        BluePrimary = new Chromacity(0.15f, 0.06f),
                        WhitePoint = Illuminant.D65,
                        Gamma = 1.95f
                    };
            }

            return new PredefinedColorProfile
            {
                RedPrimary = new Chromacity(0.64f, 0.33f),
                GreenPrimary = new Chromacity(0.3f, 0.6f),
                BluePrimary = new Chromacity(0.15f, 0.06f),
                WhitePoint = Illuminant.D65,
                Gamma = 2.2f
            };
        }
    }
}
