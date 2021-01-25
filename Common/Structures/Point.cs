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
        public Color Color { get; set; }
        public Vector3 Coordinate { get; set; }
        public Vector3 Normal { get; set; }

        public Point(float x, float y, float z, Color color)
        {
            Coordinate = new Vector3(x, y, z);
            Color = color;
        }

        public Point(float x, float y, float z, float normalX, float normalY, float normalZ)
        {
            Coordinate = new Vector3(x, y, z);
            Normal = new Vector3(normalX, normalY, normalZ);
            Color = new Color(255, 255, 255);
        }

        public Point(float x, float y, float z)
        {
            Coordinate = new Vector3(x, y, z);
            Color = new Color(255, 255, 255);            
        }

        public Point(Vector3 coordinate, Vector3 normal, Color color)
        {
            Coordinate = coordinate;
            Normal = normal;
            Color = color;
        }
    }
}
