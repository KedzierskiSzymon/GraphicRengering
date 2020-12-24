using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Color Color { get; set; }
        public Vector3 Coordinate
        {
            get
            {
                return new Vector3(X, Y, Z);
            }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        public Point(float x, float y, float z, Color color)
        {
            X = x;
            Y = y;
            Z = z;
            Color = color;

            Coordinate = new Vector3(X, Y, Z);
        }

        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            Color = Color.Red;

            Coordinate = new Vector3(X, Y, Z);
        }
    }
}
