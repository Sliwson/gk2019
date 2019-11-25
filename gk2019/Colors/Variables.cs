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
        D65
    }

    public class LabSettings
    {
        public ColorProfile ColorProfile { get; set; }
        public Illuminant Illuminant { get; set; }
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
            LabSettings = new LabSettings();
        }

        public ColorRepresentation ColorRepresentation { get; set; }
        public LabSettings LabSettings { get; set; }

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
    }
}
