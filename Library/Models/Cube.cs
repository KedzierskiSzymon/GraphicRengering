using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Cube : Figure
    {
        public Cube(Point center, float ray)
        {
            Vertices = new Point[8];
            Indexes = new Int3[12];


            Vertices[0] = new Point(center.Coordinate.X - ray, center.Coordinate.Y + ray, center.Coordinate.Z + ray, Color.Red);
            Vertices[1] = new Point(center.Coordinate.X + ray, center.Coordinate.Y + ray, center.Coordinate.Z + ray, Color.Green);
            Vertices[2] = new Point(center.Coordinate.X - ray, center.Coordinate.Y - ray, center.Coordinate.Z + ray, Color.Blue);
            Vertices[3] = new Point(center.Coordinate.X + ray, center.Coordinate.Y - ray, center.Coordinate.Z + ray, Color.Red);
            Vertices[4] = new Point(center.Coordinate.X - ray, center.Coordinate.Y + ray, center.Coordinate.Z - ray, Color.Green);
            Vertices[5] = new Point(center.Coordinate.X + ray, center.Coordinate.Y + ray, center.Coordinate.Z - ray, Color.Blue);
            Vertices[6] = new Point(center.Coordinate.X - ray, center.Coordinate.Y - ray, center.Coordinate.Z - ray, Color.Red);
            Vertices[7] = new Point(center.Coordinate.X + ray, center.Coordinate.Y - ray, center.Coordinate.Z - ray, Color.Green);

            Indexes[0] = new Int3(0, 1, 2);
            Indexes[1] = new Int3(0, 2, 3);

            Indexes[2] = new Int3(3, 2, 6);
            Indexes[3] = new Int3(3, 6, 7);

            Indexes[4] = new Int3(7, 6, 5);
            Indexes[5] = new Int3(7, 5, 4);

            Indexes[6] = new Int3(4, 5, 1);
            Indexes[7] = new Int3(4, 1, 0);

            Indexes[8] = new Int3(4, 0, 7);
            Indexes[9] = new Int3(0, 3, 7);

            Indexes[10] = new Int3(1, 5, 6);
            Indexes[11] = new Int3(1, 6, 2);
        }
    }
}
