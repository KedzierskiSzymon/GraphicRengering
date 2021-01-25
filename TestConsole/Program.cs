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
            #region Variables
            Vector3 cubePosition = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 cubeScale = new Vector3(1.0f, 1.0f, 1.0f);
            float cubeAngleX = 0.0f;
            float cubeAngleY = 0.0f;
            float cubeAngleZ = 0.0f;

            Vector3 conePosition = new Vector3(-1.0f, 0.0f, 0.0f);
            Vector3 coneScale = new Vector3(1.0f, 1.0f, 1.0f);
            float coneAngleX = 0.0f;
            float coneAngleY = 0.0f;
            float coneAngleZ = 0.0f;
            int coneVerticalDivisions = 7;

            Vector3 spherePosition = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 sphereScale = new Vector3(1.0f, 1.0f, 1.0f);
            float sphereAngleX = 0.0f;
            float sphereAngleY = 0.0f;
            float sphereAngleZ = 0.0f;
            int sphereVerticalDivisions = 5;
            int sphereHorizontalDivisions = 7;

            Vector3 cylinderPosition = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 cylinderScale = new Vector3(1.0f, 1.0f, 1.0f);
            float cylinderAngleX = 0.0f;
            float cylinderAngleY = 0.0f;
            float cylinderAngleZ = 0.0f;
            int cylinderVerticalDivisions = 15;
            int cylinderHorizontalDivisions = 8;

            Vector3 directionalLightPosition = new Vector3(0, 0, 1);
            Vector3 directionalLightAmbient = new Vector3(0.1f, 0.1f, 0.1f);
            Vector3 directionalLightDiffuse = new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 directionalLightSpecular = new Vector3(1, 1, 1);
            float directionalShiness = 10;

            Vector3 pointLightPosition = new Vector3(0, 0, -15);
            Vector3 pointLightAmbient = new Vector3(0, 0.6f, 0);
            Vector3 pointLightDiffuse = new Vector3(0, 0, 0.6f);
            Vector3 pointLightSpecular = new Vector3(1, 0, 0);
            float pointShiness = 10;

            Vector3 spotLightPosition = new Vector3(0, 0, -2);
            Vector3 spotLightAmbient = new Vector3(0.0f, 0.6f, 0);
            Vector3 spotLightDiffuse = new Vector3(0, 0, 0.6f);
            Vector3 spotLightSpecular = new Vector3(1, 0, 0);
            float spotShiness = 10;
            float spotCutoff = 5;
            Vector3 spotLightDirection = new Vector3(0, 0, -1);

            Vector3 eye = new Vector3(0, 0, 15);
            Vector3 center = new Vector3(0, 0, 0);
            float near = 0.001f;
            float far = 100.0f;
            float fov = 45.0f;
            float aspect = 1.0f;

            Color clearColor = new Color(0, 0, 0);
            #endregion

            #region Processors
            ColorBuffer colorBuffer = new ColorBuffer(400, 400);
            Rasterizer rasterizer = new Rasterizer(colorBuffer);
            ImageBuilder imageBuilder = new ImageBuilder(colorBuffer);

            colorBuffer.ClearColor(clearColor);
            colorBuffer.ClearDepth(1);

            VertexProcessor vertexProcessor = new VertexProcessor();

            vertexProcessor.SetPerspective(fov, aspect, near, far);
            vertexProcessor.SetIdentityView();
            vertexProcessor.SetIdentity();
            vertexProcessor.SetLookAt(eye, center, new Vector3(0, 1, 0));
            vertexProcessor.Transform();
            #endregion

            #region Lights
            PointLight pointLight = new PointLight()
            {
                Position = pointLightPosition,
                Ambient = pointLightAmbient,
                Diffuse = pointLightDiffuse,
                Specular = pointLightSpecular,
                Shininess = pointShiness
            };

            DirectionalLight directionalLight = new DirectionalLight()
            {
                Position = directionalLightPosition,
                Ambient = directionalLightAmbient,
                Diffuse = directionalLightDiffuse,
                Specular = directionalLightSpecular,
                Shininess = directionalShiness
            };

            SpotLight spotLight = new SpotLight()
            {
                Position = spotLightPosition,
                Ambient = spotLightAmbient,
                Diffuse = spotLightDiffuse,
                Specular = spotLightSpecular,
                Shininess = spotShiness,
                CutOff = (float)Math.Cos((spotCutoff * Math.PI / 180)),
                Direction = spotLightDirection
            };
            #endregion

            // Choose light
            Light light = spotLight;

            Sphere sphere = new Sphere(sphereVerticalDivisions, sphereHorizontalDivisions);
            sphere.MakeNormals();

            vertexProcessor.SetIdentity();
            vertexProcessor.Rotate(sphereAngleY, new Vector3(0, 1, 0));
            vertexProcessor.Rotate(sphereAngleX, new Vector3(1, 0, 0));
            vertexProcessor.Rotate(sphereAngleZ, new Vector3(0, 0, 1));
            vertexProcessor.Translate(new Vector3(-3.0f, 0.0f, 0.0f));
            vertexProcessor.Transform();

            sphere.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(3.0f, 0.0f, 0.0f));
            vertexProcessor.Transform();
            sphere.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(0.0f, 2.0f, 0.0f));
            vertexProcessor.Transform();
            sphere.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(0.0f, -2.0f, 0.0f));
            vertexProcessor.Transform();
            sphere.Draw(rasterizer, vertexProcessor, light, true);

            vertexProcessor.SetIdentity();
            vertexProcessor.Translate(new Vector3(0.0f, 0.0f, 0.0f));
            vertexProcessor.Transform();
            sphere.Draw(rasterizer, vertexProcessor, light, true);

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
