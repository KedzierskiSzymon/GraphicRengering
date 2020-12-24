using Common.Structures;
using Library;
using Library.Models;
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
            Scene scene = new Scene(400, 600);

            ColorBuffer colorBuffer = new ColorBuffer(400, 600);
            Rasterizer rasterizer = new Rasterizer(colorBuffer);
            ImageBuilder imageBuilder = new ImageBuilder(colorBuffer);

            //colorBuffer.ClearColor(Color.Red.ColorToUInt());
            colorBuffer.ClearColor(0x00101010);
            colorBuffer.ClearDepth(10);

            VertexProcessor vertexProcessor = new VertexProcessor();
            vertexProcessor.SetPerspective(45, 1, 1, 1000);
            vertexProcessor.SetLookAt(new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 1), new Vector3(0, 1, 0));

            vertexProcessor.SetIdentity();
            vertexProcessor.Scale(5, 2, 2);
            vertexProcessor.Translate(new Vector3(1, 2, 8));
            vertexProcessor.Transform();

            Pyramid pyramid = new Pyramid(4);
            pyramid.Print(rasterizer, vertexProcessor);

            vertexProcessor.SetIdentity();
            vertexProcessor.Scale(3, 3, 3);
            vertexProcessor.Rotate(45, new Vector3(1, 0, 0));
            vertexProcessor.Translate(new Vector3(-1, -7, 10));
            vertexProcessor.Transform();

            Sphere sphere = new Sphere(10, 10);
            sphere.Print(rasterizer, vertexProcessor);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(-1, -2, 10));
            vertexProcessor.Scale(2.5f, 5, 2.5f);
            vertexProcessor.Rotate(90, new Vector3(0, 0, 1));
            vertexProcessor.Transform();

            Pyramid pyramid2 = new Pyramid(16);
            pyramid2.Print(rasterizer, vertexProcessor);

            imageBuilder.SaveImage("SzymonKędzierski_l5.bmp");
        }
    }
}
