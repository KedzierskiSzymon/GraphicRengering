using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Sphere : Figure
    {
        public Sphere(int verticalSegms, int horizontalSegms)
        {
            Indexes = new Int3[2 * horizontalSegms * verticalSegms];
            Vertices = new Point[verticalSegms * (horizontalSegms + 2)];

            for (int i = 0; i <= horizontalSegms + 1; i++)
            {
                float y = (float)(Math.Cos(i * Math.PI / (horizontalSegms + 1)));
                float r = (float)(Math.Sqrt(1 - y * y));

                for (int j = 0; j < verticalSegms; j++)
                {
                    float x = (float)(r * Math.Cos(2 * Math.PI * j / verticalSegms));
                    float z = (float)(r * Math.Sin(2 * Math.PI * j / verticalSegms));
                    Vertices[j + i * verticalSegms] = new Common.Structures.Point(x, y, z);
                }
            }

            for (int i = 0; i < horizontalSegms; i++)
            {
                for (int j = 0; j < verticalSegms; j++)
                {
                    Indexes[j + 2 * i * verticalSegms] = new Int3(
                        (j + 1) % verticalSegms + i * verticalSegms,
                        j + verticalSegms + i * verticalSegms,
                        (j + 1) % verticalSegms + verticalSegms + i * verticalSegms
                    );

                    Indexes[j + verticalSegms + 2 * i * verticalSegms] = new Int3(
                        j + verticalSegms + i * verticalSegms,
                        j + 2 * verticalSegms + i * verticalSegms,
                        (j + 1) % verticalSegms + verticalSegms + i * verticalSegms
                    );
                }
            }
        }
    }
}
