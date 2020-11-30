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
            // Color definitions

            // Primitives definitions
            Point[] trianglePoints = new Point[]
            {
                new Point(-1, -1, 6f, Color.Black),
                new Point(-1, 0, 6f, Color.Black),
                new Point(1, -1, 6f, Color.Black),

            };
            
            Point[] trianglePoints2 = new Point[]
            {
                new Point(0, 0, 6, Color.Red),
                new Point(0.5f, 1, 6, Color.Green),
                new Point(1, 0.5f, 6, Color.Blue),

            };
            Triangle triangle = new Triangle(trianglePoints);
            Triangle triangle2 = new Triangle(trianglePoints2);

            scene.SetPerspective(45, 1, 1, 1000);
            scene.SetLookAt(new Float3(0f, 0.0f, 0), new Float3(1, 0.0f, 1), new Float3(0, 1, 0));
            scene.Objects.Add(triangle2);
            scene.SetIdentity();
            
            scene.Translate(new Float3(1, 0, 0));
            scene.Translate(new Float3(5, 0, 0));
            scene.Scale(new Float3(3, 3, 3));
            scene.Rotate(50, new Float3(0, 0, 1));
            //scene.Translate(scene.Objects[0], new Float3(1, 0, 0));
            scene.Transform();
            scene.Tr(scene.Objects[0]);

            //scene.Objects.Add(triangle2);
            //scene.InitializeFigures();


            //scene.Translate(scene.Objects[0], new Float3(0, 0, 0));
            //scene.Rotate(scene.Objects[0], 50, new Float3(0, 0, 1));

            //scene.Tr(scene.Objects[0]);


            //vertexProcessor.Rotate(45, new Float3(0, -1, 0));

            scene.Print();
        }
    }
}
