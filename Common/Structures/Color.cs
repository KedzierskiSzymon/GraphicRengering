using Common.Constants;
using System;
using System.Globalization;
using System.Windows;

namespace Common.Structures
{
    public class Color
    {
        public float A => 0;
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Color(uint color)
        {
            R = (byte)(color | 0x00ff0000);
            G = (byte)(color | 0x0000ff00);
            B = (byte)(color | 0x000000ff);
        }

        public static Color VectorToColor(Vector3 vector)
        {
            byte r = (byte)(Clamp(vector.X) * 255);
            byte g = (byte)(Clamp(vector.Y) * 255);
            byte b = (byte)(Clamp(vector.Z) * 255);

            return new Color(r, g, b);
        }

        public Vector3 ToVector3()
        {
            Vector3 vector3 = new Vector3(R, G, B);
            vector3 = vector3.Normalize();

            return vector3;
        }

        public static Color operator *(float factor, Color color)
        {
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

        private static float Clamp(float value, float min = 0, float max = 1)
        {
            if (value > max)
                return max;

            if (value < min)
                return min;

            return value;
        }
    }
}
