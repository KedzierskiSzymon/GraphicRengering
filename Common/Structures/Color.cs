using Common.Constants;
using System;
using System.Globalization;
using System.Windows;

namespace Common.Structures
{
    public class Color
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Color(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public Color(uint color)
        {
            A = (byte)(color | 0xff000000);
            R = (byte)(color | 0x00ff0000);
            G = (byte)(color | 0x0000ff00);
            B = (byte)(color | 0x000000ff);
        }


        public uint ColorToUInt()
        {
            uint color = 0x00000000;

            color |= A; 
            color <<= 8;
            color |= R; 
            color <<= 8;
            color |= G; 
            color <<= 8;
            color |= B;

            return color;
        }

        public static uint ColorToUInt(Color colorRGB)
        {
            uint color = 0x00000000;
            color |= colorRGB.A; 
            color <<= 8;
            color |= colorRGB.R; 
            color <<= 8;
            color |= colorRGB.G; 
            color <<= 8;
            color |= colorRGB.B;

            return color;
        }

        public static Color operator *(float factor, Color color)
        {
            color.A = (byte)(color.A * factor);
            color.R = (byte)(color.R * factor);
            color.G = (byte)(color.G * factor);
            color.B = (byte)(color.B * factor);

            return color;
        }

        public override string ToString()
        {
            string message = $"Red = '{R}'  Green = '{G}'  Blue = '{B}'  Alpha = '{A}'";

            return message;
        }

        public static Color Red => new Color(0xff000000);
        public static Color Green => new Color(0x00ff0000);
        public static Color Blue => new Color(0x0000ff00);
        public static Color Black => new Color(0x00000000);
        public static Color White => new Color(0xffffff00);
    }
}
