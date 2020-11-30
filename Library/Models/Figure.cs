using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public abstract class Figure
    {
        public IList<Point> Points { get; set; }
        public IList<Triangle> Triangles { get; set; }

        public Figure()
        {
            Points = new List<Point>();
            Triangles = new List<Triangle>();
        }
    }
}
