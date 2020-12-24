using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Triangle : Figure
    {
        public int[] X;
        public int[] Y;

        public Triangle(Point[] points)
        {
            Vertices = points;

            Triangles = new Int3[1];
            Triangles[0] = new Int3(0, 1, 2);
        }

        public Triangle(Point p1, Point p2, Point p3)
        {
            Vertices = new Point[]
            { 
                p1,
                p2,
                p3
            };

            Triangles = new Int3[1];
            Triangles[0] = new Int3(0, 1, 2);
        }

        public Triangle(Triangle triangle)
        {
            Vertices = new Point[]
            {
                triangle.Vertices[0],
                triangle.Vertices[1],
                triangle.Vertices[2]
            };

            Triangles = new Int3[1];
            Triangles[0] = new Int3(0, 1, 2);
        }
    }
}
