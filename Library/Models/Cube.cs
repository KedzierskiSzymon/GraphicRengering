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
            Points.Add(new Point(center.X - ray, center.Y + ray, center.Z + ray));
            Points.Add(new Point(center.X + ray, center.Y + ray, center.Z + ray));
            Points.Add(new Point(center.X - ray, center.Y - ray, center.Z + ray));
            Points.Add(new Point(center.X + ray, center.Y - ray, center.Z + ray));
            Points.Add(new Point(center.X - ray, center.Y + ray, center.Z - ray));
            Points.Add(new Point(center.X + ray, center.Y + ray, center.Z - ray));
            Points.Add(new Point(center.X - ray, center.Y - ray, center.Z - ray));
            Points.Add(new Point(center.X + ray, center.Y - ray, center.Z - ray));

            Triangles.Add(new Triangle(Points[0], Points[1], Points[2]));
            Triangles.Add(new Triangle(Points[0], Points[2], Points[3]));


        }
    }
}
