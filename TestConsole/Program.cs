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

            ColorBuffer buffer = new ColorBuffer(400, 600);
            Color bufferColor = new Color(0x0000ff, false);
            Color triangleColor = new Color(0x888888, false);

            Color red = new Color(0xff0000, false);
            Color green = new Color(0x00ff00, false);
            Color blue = new Color(0x0000ff, false);

            buffer.ClearColor(bufferColor);
            buffer.ClearDepth();

            Point[] trianglePoints = new Point[]
            {
                new Point(1, 0, 0.5f, triangleColor),
                new Point(-1, 0, 0.5f, triangleColor),
                new Point(1, 1, 0.5f, triangleColor),

            };
            
            Point[] trianglePoints2 = new Point[]
            {
                new Point(-1.5f, 0, 1, red),
                new Point(0, 1, 0.5f, green),
                new Point(0.5f, -1, 0, blue),

            };
            Triangle triangle = new Triangle(trianglePoints);
            Triangle triangle2 = new Triangle(trianglePoints2);
            buffer.Print(triangle);
            buffer.Print(triangle2);

            buffer.SaveImage("SzymonKędzierski_l2.png", true);

            //DateTime dateTo = DateTime.Now;

            //Console.WriteLine($"Duration time: {(dateTo - dateFrom).TotalMilliseconds} ms");
            //Console.ReadKey();
        }
    }
}
