using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Color Color { get; set; }

        public Point(float x, float y, float z, Color color)
        {
            X = x;
            Y = y;
            Z = z;
            Color = color;
        }
    }
}
