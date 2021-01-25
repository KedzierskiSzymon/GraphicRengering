using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lights
{
    public class SpotLight : Light
    {
        public float CutOff { get; set; }
        public Vector3 Direction { get; set; }

        private readonly Vector3 _overRangeColor = new Vector3(0.1f, 0.1f, 0.1f);

        public override Vector3 Calculate(Point point, VertexProcessor vertexProcessor)
        {
			Vector3 N = vertexProcessor.TransformWorldToView(point.Normal, 0);
			N = N.Normalize();

			Vector3 V = vertexProcessor.TransformWorldToView(point.Coordinate * -1f, 1);

			Vector3 L = Position - V;
			L = L * -1f;

			L = L.Normalize();
			V = V.Normalize();

			Vector3 R = Reflect(L * -1f, N);
			R = R.Normalize();

			float diffuseFactor = Saturate(Vector3.Dot(L, N));
            float specularFactor = Saturate(Vector3.Dot(R, V));
            specularFactor = (float)Math.Pow(specularFactor, Shininess);

            Direction = Direction.Normalize();

            float spotFactor = Vector3.Dot(L, Direction);

            if (spotFactor > CutOff)
            {
                Vector3 color = diffuseFactor * Diffuse +
                     +specularFactor * Specular
                     + Ambient;

                return color;
            }

            return _overRangeColor;
        }
    }
}
