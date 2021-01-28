using Common.Structures;
using Library.Lights;
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
        public Int3[] Indexes { get; set; }

        public Figure() { }

        public Figure(Point[] vertices, Int3[] indexes)
        {
            Vertices = vertices;
            Indexes = indexes;
        }

        public void Draw(Rasterizer rasterizer, VertexProcessor vertexProcessor, Light light)
        {
            for (int i = 0; i < Indexes.Length; i++)
            {
                rasterizer.Rasterize(
                    vertexProcessor.Tr(Vertices[Indexes[i].X].Coordinate), 
                    vertexProcessor.Tr(Vertices[Indexes[i].Y].Coordinate), 
                    vertexProcessor.Tr(Vertices[Indexes[i].Z].Coordinate),
                    new Color(
                        (light.Calculate(Vertices[Indexes[i].X], vertexProcessor).X * 255.0f), 
                        (light.Calculate(Vertices[Indexes[i].X], vertexProcessor).Y * 255.0f), 
                        (light.Calculate(Vertices[Indexes[i].X], vertexProcessor).Z * 255.0f)),
                    new Color(
                        (light.Calculate(Vertices[Indexes[i].Y], vertexProcessor).X * 255.0f), 
                        (light.Calculate(Vertices[Indexes[i].Y], vertexProcessor).Y * 255.0f), 
                        (light.Calculate(Vertices[Indexes[i].Y], vertexProcessor).Z * 255.0f)),
                    new Color(
                        (light.Calculate(Vertices[Indexes[i].Z], vertexProcessor).X * 255.0f), 
                        (light.Calculate(Vertices[Indexes[i].Z], vertexProcessor).Y * 255.0f), 
                        (light.Calculate(Vertices[Indexes[i].Z], vertexProcessor).Z * 255.0f)));
            }
        }

        public void Draw(Rasterizer rasterizer, VertexProcessor vertexProcessor, Light light, bool perPixel)
        {
            Point[] processed = new Point[Vertices.Length];

            for (int i = 0; i < Vertices.Length; ++i)
                processed[i] = vertexProcessor.Tr(Vertices[i]);

            for (int i = 0; i < Indexes.Length; ++i)
            {
                rasterizer.DrawTriangle(processed[Indexes[i].X], processed[Indexes[i].Y], processed[Indexes[i].Z], light, vertexProcessor);
            }
        }

        public void MakeNormals()
        {
            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal = new Vector3(0, 0, 0);

            for (int i = 0; i < Indexes.Length; i++)
            {
                Vector3 div1 = Vertices[Indexes[i].Y].Coordinate - Vertices[Indexes[i].X].Coordinate;
                Vector3 div2 = Vertices[Indexes[i].Z].Coordinate - Vertices[Indexes[i].X].Coordinate;
                Vector3 crossResult = Vector3.Cross(div1, div2);
                Vector3 n = crossResult.Normalize();

                Vertices[Indexes[i].X].Normal = Vertices[Indexes[i].X].Normal + n;
                Vertices[Indexes[i].Y].Normal = Vertices[Indexes[i].Y].Normal + n;
                Vertices[Indexes[i].Z].Normal = Vertices[Indexes[i].Z].Normal + n;
            }

            for (int i = 0; i < Vertices.Length; i++)
                Vertices[i].Normal = Vertices[i].Normal.Normalize();
        }

        public void MakeUV()
        {           
            //Spherical mapping: x - u, y - v
            for (int i = 0; i < Vertices.Length; i++)
            {
                //vertices[i].texturePos.x = (1 / (2 * PI)) * atan2(vertices[i].position.z, vertices[i].position.y);
                //vertices[i].texturePos.x = (((vertices[i].texturePos.x - (-1)) * (1 - 0)) / (1 - (-1)) + 0);

                Vertices[i].TexturePosition.X = 0.5f + (float)((Math.Atan2(Vertices[i].Coordinate.X, Vertices[i].Coordinate.Z)) / (2 * Math.PI));

                //vertices[i].texturePos.y = (1 / PI) * tan(vertices[i].position.y / (sqrt(vertices[i].position.x * vertices[i].position.x + vertices[i].position.y * vertices[i].position.y + vertices[i].position.z * vertices[i].position.z)));
                //vertices[i].texturePos.y = (((vertices[i].texturePos.y - (-1)) * (1 - 0)) / (1 - (-1)) + 0);

                Vertices[i].TexturePosition.Y = 0.5f + (float)(Math.Asin(Vertices[i].Coordinate.Y) / Math.PI);

                Vertices[i].TexturePosition.Z = 0.0f;
            }
        }
    }
}
