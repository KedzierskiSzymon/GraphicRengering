using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Pyramid : Figure
    {
        public Pyramid(int segments)
        {
            if (segments < 3)
                segments = 3;

            int tSize = 2 * segments;
            int vSize = segments + 2;

            Indexes = new Int3[tSize];
            Vertices = new Point[vSize];

            float angleStep = (float)(Math.PI * 2 / segments);

            Vertices[vSize - 2] = new Point(0, 0, 0);
            Vertices[vSize - 1] = new Point(0, 1, 0);

            //Finding Vertices
            float tmpStep = 0;

            for (int i = 0; i < Vertices.Length - 2; i++, tmpStep += angleStep)
            {
                float x = (float)(1 * Math.Cos(tmpStep));
                float z = (float)(1 * Math.Sin(tmpStep));
                Vertices[i] = new Point(x, 0, z);
            }

            //Assigning vertices to trinagles
            for (int i = 0; i < tSize / 2; i++)
            {
                Int3 triangle;
                //Lower triangles
                triangle = new Int3(i % segments, (i + 1) % segments, vSize - 2);
                Indexes[i] = triangle;

                triangle = new Int3(i % segments, vSize - 1, (i + 1) % segments);
                Indexes[i + segments] = triangle;
            }
        }
    }
}
