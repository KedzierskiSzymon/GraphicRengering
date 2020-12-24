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
        public Point[] Vertices { get; set; }
        public Int3[] Triangles { get; set; }

        public Figure(Point[] vertices, Int3[] triangles)
        {
            Vertices = vertices;
            Triangles = triangles;
        }

        public Figure()
        {
        }

        public void Print(Rasterizer rasterizer, VertexProcessor vertexProcessor)
        {
            for (int i = 0; i < Triangles.Length; i++)
            {
                Vector3 vector1 = vertexProcessor.Tr(Vertices[Triangles[i].X].Coordinate);
                Vector3 vector2 = vertexProcessor.Tr(Vertices[Triangles[i].Y].Coordinate);
                Vector3 vector3 = vertexProcessor.Tr(Vertices[Triangles[i].Z].Coordinate);

                rasterizer.Rasterize(vector1, vector2, vector3,
                0xff000000,
                0x00ff0000,
                0x0000ff00,
                0x000000ff);
            }
        }
    }
}
