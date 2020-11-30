using Common.Structures;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Scene
    {
        private readonly ColorBuffer _colorBuffer;
        private readonly VertexProcessor _vertexProcessor;

        public IList<Figure> Objects { get; set; }

        public Scene(int width, int height)
        {
            _colorBuffer = new ColorBuffer(width, height);
            _vertexProcessor = new VertexProcessor();

            Objects = new List<Figure>();
        }

        public void SetPerspective(float fovY, float aspect, float near, float far)
        {
            _vertexProcessor.SetPerspective(fovY, aspect, near, far);
        }

        public void SetLookAt(Float3 eye, Float3 center, Float3 up)
        {
            _vertexProcessor.SetLookAt(eye, center, up);
        }

        public void Translate(Float3 value)
        {
            _vertexProcessor.Translate(value);

        }

        public void Rotate(float angle, Float3 value)
        {
            _vertexProcessor.Rotate(angle, value);
        }

        public void Scale(Float3 value)
        {
            _vertexProcessor.Scale(value);
        }

        public void Tr(Figure figure)
        {
            foreach (Point point in figure.Points)
                point.Coordinate = _vertexProcessor.Tr(point.Coordinate);
        }

        public void SetIdentity()
        {
            _vertexProcessor.SetIdentity();
        }

        public void Transform()
        {
            _vertexProcessor.Transform();
            _vertexProcessor.SetIdentity();
        }

        public void Print()
        {
            foreach (Figure figure in Objects)
            {
                foreach (Triangle triangle in figure.Triangles)
                    _colorBuffer.Print(triangle);
            }

            _colorBuffer.SaveImage("SzymonKędzierski_l5.png", true);
        }
    }
}
