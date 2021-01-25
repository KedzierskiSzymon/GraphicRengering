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
        public Triangle(Point p1, Point p2, Point p3, Color color1, Color color2, Color color3)
        {
            Vertices = new Point[]
            { 
                p1,
                p2,
                p3
            };

            Vertices[0].Color = color1;
            Vertices[1].Color = color2;
            Vertices[2].Color = color3;

            Indexes = new Int3[1];
            Indexes[0] = new Int3(0, 1, 2);
        }

        public Triangle(Triangle triangle)
        {
            Vertices = new Point[]
            {
                triangle.Vertices[0],
                triangle.Vertices[1],
                triangle.Vertices[2]
            };

            Indexes = new Int3[1];
            Indexes[0] = new Int3(0, 1, 2);
        }
    }
}
