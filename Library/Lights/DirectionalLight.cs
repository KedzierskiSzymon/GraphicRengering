using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Lights
{
    public class DirectionalLight : Light
    {
        public override Vector3 Calculate(Point point, VertexProcessor vertexProcessor)
        {
            Vector3 L = Position * -1f;

            Vector3 N = vertexProcessor.TransformObjectToView(point.Normal, 0);
            N = N.Normalize();

            Vector3 V = vertexProcessor.TransformObjectToView(point.Coordinate * -1f, 1);
            V = V.Normalize();

            Vector3 R = Reflect(L * -1f, N);
            R = R.Normalize();

            float diffuseFactor = Saturate(Vector3.Dot(L, N));
            float specularFactor = Saturate(Vector3.Dot(R, V));
            specularFactor = (float)Math.Pow(specularFactor, Shininess);

            Vector3 diffuseValue = diffuseFactor * Diffuse;
            Vector3 specularValue = specularFactor * Specular;

            Vector3 texSample = new Vector3(1, 1, 1);

            if (Texture != null)
            {
                //int c = texture->frame[int(point.TexturePosition.X * (texture.Width - 1) + 0.5f) + int(point.TexturePosition.Y * (texture.Height - 1) + 0.5f) * texture.Width];
                int width = (int)Math.Round(point.TexturePosition.X * (Texture.Width - 1));
                int height = (int)Math.Round(point.TexturePosition.Y * (Texture.Height - 1));

                texSample = Texture[width, height].ToVector3();
                texSample = Vector3.Cross((Ambient + diffuseValue), texSample + specularValue);
            }

            Vector3 color = texSample;
            color = Vector3.Saturate(color);

            return color;
        }
    }
}
