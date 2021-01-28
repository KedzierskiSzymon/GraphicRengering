using Common.Structures;
using Library;
using Library.Lights;
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
            #region Processors
            ColorBuffer colorBuffer = new ColorBuffer(1000, 1000);
            Rasterizer rasterizer = new Rasterizer(colorBuffer);
            ImageBuilder imageBuilder = new ImageBuilder(colorBuffer);

            colorBuffer.ClearColor(new Color(0, 0, 0));
            colorBuffer.ClearDepth(1);

            VertexProcessor vertexProcessor = new VertexProcessor();

            Vector3 eye = new Vector3(0, 0, 15);
            Vector3 center = new Vector3(0, 0, 0);

            vertexProcessor.SetPerspective(45, 1.0f, 0.001f, 100);
            vertexProcessor.SetIdentityView();
            vertexProcessor.SetIdentity();
            vertexProcessor.SetLookAt(eye, center, new Vector3(0, 1, 0));
            vertexProcessor.Transform();
            #endregion

            #region Lights
            PointLight pointLight = new PointLight()
            {
                Position = new Vector3(-2, 0, -16),
                Ambient = new Vector3(0.5f, 0.5f, 0),
                Diffuse = new Vector3(0.9f, 0, 0.9f),
                Specular = new Vector3(1, 0, 0),
                Shininess = 10
            };

            DirectionalLight directionalLight = new DirectionalLight()
            {
                Position = new Vector3(0, -5, 1),
                Ambient = new Vector3(0.3f, 0.3f, 0.3f),
                Diffuse = new Vector3(0.0f, 0.0f, 0.5f),
                Specular = new Vector3(0.4f, 0.9f, 0.4f),
                Shininess = 10
            };

            SpotLight spotLight = new SpotLight()
            {
                Position = new Vector3(0, 0, -2),
                Ambient = new Vector3(0.0f, 0.6f, 0),
                Diffuse = new Vector3(0, 0, 0.6f),
                Specular = new Vector3(1, 0, 0),
                Shininess = 10,
                CutOff = (float)Math.Cos((2 * Math.PI / 180)),
                Direction = new Vector3(0, 0, -1)
            };
            #endregion

            Texture textureBlue = new Texture(512, 512);
            textureBlue.LoadTextureFromFile("blue.png");

            Texture texturePaint = new Texture(512, 512);
            texturePaint.LoadTextureFromFile("text.png");

            Texture textureBrick = new Texture(512, 512);
            textureBrick.LoadTextureFromFile("brick.png");

            // Choose light
            Light light = spotLight;

            //Figure figure = new Pyramid(3);
            //Figure figure = new Sphere(15, 15);
            Figure figure = new Sphere(20, 20);
            figure.MakeNormals();
            figure.MakeUV();

            vertexProcessor.SetIdentity();
            vertexProcessor.Rotate(0, new Vector3(0, 1, 0));
            vertexProcessor.Rotate(0, new Vector3(1, 0, 0));
            vertexProcessor.Rotate(0, new Vector3(0, 0, 1));
            vertexProcessor.Translate(new Vector3(-3.0f, 0.0f, 0.0f));
            vertexProcessor.Transform();

            figure.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(3.0f, 0.0f, 0.0f));
            vertexProcessor.Transform();
            light.Texture = textureBlue;
            light.Texture.CalculateLight = false;
            figure.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(0.0f, 3.0f, 0.0f));
            vertexProcessor.Transform();
            light.Texture = textureBlue;
            light.Texture.CalculateLight = true;
            figure.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(0.0f, -2.5f, 0.0f));
            vertexProcessor.Transform();
            light.Texture = textureBrick;
            light.Texture.CalculateLight = true;
            figure.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(0.0f, 0.0f, 0.0f));
            vertexProcessor.Transform();
            light.Texture = texturePaint;
            light.Texture.CalculateLight = true;
            figure.Draw(rasterizer, vertexProcessor, light, true);

            /*
            Pyramid pyramid = new Pyramid(4);
            pyramid.MakeNormals();
            pyramid.Draw(rasterizer, vertexProcessor, light);

            vertexProcessor.SetIdentity();
            vertexProcessor.Scale(3, 3, 3);
            vertexProcessor.Rotate(45, new Vector3(1, 0, 0));
            vertexProcessor.Translate(new Vector3(-1, -7, 10));
            vertexProcessor.Transform();

            Sphere sphere = new Sphere(10, 10);
            sphere.MakeNormals();
            sphere.Draw(rasterizer, vertexProcessor, light);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(-1, -2, 10));
            vertexProcessor.Scale(2.5f, 5, 2.5f);
            vertexProcessor.Rotate(90, new Vector3(0, 0, 1));
            vertexProcessor.Transform();

            Pyramid pyramid2 = new Pyramid(16);
            pyramid2.MakeNormals();
            pyramid2.Draw(rasterizer, vertexProcessor, light);
            */
            imageBuilder.SaveImage("SzymonKędzierski_l8.bmp");
        }
    }
}
