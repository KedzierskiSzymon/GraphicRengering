using Common.Structures;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime dateFrom = DateTime.Now;

            // Color definitions
            ColorBuffer buffer = new ColorBuffer(400, 600);
            Color bufferColor = new Color(0x0000ff, false);
            Color triangleColor = new Color(0x888888, false);

            Color red = new Color(0xff0000, false);
            Color green = new Color(0x00ff00, false);
            Color blue = new Color(0x0000ff, false);

            buffer.ClearColor(bufferColor);
            buffer.ClearDepth();

            // Primitives definitions
            Point[] trianglePoints = new Point[]
            {
                new Point(0, 0, 0.5f, triangleColor),
                new Point(-1, 0, 0.5f, triangleColor),
                new Point(1, 1, 0.5f, triangleColor),

            };
            
            Point[] trianglePoints2 = new Point[]
            {
                new Point(1, 0, 0.5f, red),
                new Point(-1, 0, 0.5f, green),
                new Point(1, 1, 0.5f, blue),

            };
            Triangle triangle = new Triangle(trianglePoints);
            Triangle triangle2 = new Triangle(trianglePoints2);

            // Vertex buffer
            VertexProcessor vertexProcessor = new VertexProcessor();
            vertexProcessor.SetPerspective(45, 1, 4, 1000);
            //vertexProcessor.MultiplyByRotation(100, new Float3(1, 1, 0));

            triangle.Points[0].Coordinate = vertexProcessor.Tr(triangle.Points[0].Coordinate);
            triangle.Points[1].Coordinate = vertexProcessor.Tr(triangle.Points[1].Coordinate);
            triangle.Points[2].Coordinate = vertexProcessor.Tr(triangle.Points[2].Coordinate);

            triangle2.Points[0].Coordinate = vertexProcessor.Tr(triangle2.Points[0].Coordinate);
            triangle2.Points[1].Coordinate = vertexProcessor.Tr(triangle2.Points[1].Coordinate);
            triangle2.Points[2].Coordinate = vertexProcessor.Tr(triangle2.Points[2].Coordinate);

            buffer.Print(triangle);
            buffer.Print(triangle2);

            buffer.SaveImage("SzymonKędzierski_l4.png", true);

            //DateTime dateTo = DateTime.Now;

            //Console.WriteLine($"Duration time: {(dateTo - dateFrom).TotalMilliseconds} ms");
            //Console.ReadKey();
        }
    }
}
