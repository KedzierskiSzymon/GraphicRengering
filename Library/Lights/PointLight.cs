using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lights
{
    public class PointLight : Light
    {
        public override Vector3 Calculate(Point point, VertexProcessor vertexProcessor)
        {
            Vector3 N = vertexProcessor.TransformObjectToView(point.Normal, 0);
            N = N.Normalize();

            Vector3 V = vertexProcessor.TransformObjectToView(point.Coordinate * -1, 1);

            Vector3 L = Position - V;
            L = L * -1f;

            L = L.Normalize();
            V = V.Normalize();

            Vector3 R = Reflect(L * -1f, N);
            R = R.Normalize();

            float diffuseFactor = Saturate(Vector3.Dot(L, N));
            float specularFactor = Saturate(Vector3.Dot(R, V));
            specularFactor = (float)Math.Pow(specularFactor, Shininess);

            Vector3 color = diffuseFactor * Diffuse +
                 +specularFactor * Specular
                 + Ambient;

            color = Vector3.Saturate(color);

            return color;
        }
    }
}
