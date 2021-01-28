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

            Vector3 diffuseValue = diffuseFactor * Diffuse;
            Vector3 specularValue = specularFactor * Specular;

            if (spotFactor > CutOff)
            {
                Vector3 texSample = new Vector3(1, 1, 1);
                Vector3 color = new Vector3(0, 0, 0);

                if (Texture != null)
                {
                    int width = (int)Math.Round(point.TexturePosition.X * (Texture.Width - 1));
                    int height = (int)Math.Round(point.TexturePosition.Y * (Texture.Height - 1));

                    texSample = Texture[width, height].ToVector3();

                    if (Texture.CalculateLight)
                        texSample = Vector3.Cross((Ambient + diffuseValue), texSample + specularValue);

                    color = texSample;
                }
                else
                {
                    color = diffuseValue + specularValue + Ambient;
                }

                color = Vector3.Saturate(color);

                return color;
            }

            return _overRangeColor;
        }
    }
}
