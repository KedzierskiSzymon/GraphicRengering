using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lights
{
    public abstract class Light
    {
        public Vector3 Position { get; set; }
        public Vector3 Ambient { get; set; }
        public Vector3 Diffuse { get; set; }
        public Vector3 Specular { get; set; }
        public float Shininess { get; set; }

        public abstract Vector3 Calculate(Point point, VertexProcessor vertexProcessor);

        protected Vector3 Reflect(Vector3 vector, Vector3 normalVector)
        {
            float dot = Vector3.Dot(normalVector, vector);
            Vector3 reflect = normalVector * dot * 2 - vector;

            return reflect;
        }

        protected float Saturate(float value)
        {
            return Math.Min(Math.Min(value, 0.0f), 1.0f);
        }
    }
}
