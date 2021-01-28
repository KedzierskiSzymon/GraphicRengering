using Common.Structures;
using Library.Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.MathMethods;

namespace Library
{
    public class Rasterizer
    {
        private ColorBuffer _colorBuffer;

        public Rasterizer(ColorBuffer colorBuffer)
        {
            _colorBuffer = colorBuffer;
        }

        public void Rasterize(Point v1, Point v2, Point v3, Color c1, Color c2, Color c3)
        {
            float x1 = CalculateCanonicalViewX(v1.Coordinate.X);
            float x2 = CalculateCanonicalViewX(v2.Coordinate.X);
            float x3 = CalculateCanonicalViewX(v3.Coordinate.X);

            float y1 = CalculateCanonicalViewY(v1.Coordinate.Y);
            float y2 = CalculateCanonicalViewY(v2.Coordinate.Y);
            float y3 = CalculateCanonicalViewY(v3.Coordinate.Y);

            _colorBuffer.MinX = (int)Math.Min(x1, Math.Min(x2, x3));
            _colorBuffer.MaxX = (int)Math.Max(x1, Math.Max(x2, x3));
            _colorBuffer.MinY = (int)Math.Min(y1, Math.Min(y2, y3));
            _colorBuffer.MaxX = (int)Math.Max(y1, Math.Max(y2, y3));

            _colorBuffer.MinX = Math.Max(_colorBuffer.MinX, 0);
            _colorBuffer.MaxX = Math.Min(_colorBuffer.MaxX, _colorBuffer.Width - 1);
            _colorBuffer.MinY = Math.Max(_colorBuffer.MinY, 0);
            _colorBuffer.MaxY = Math.Min(_colorBuffer.MaxY, _colorBuffer.Height - 1);

            float x12 = x1 - x2;
            float x13 = x1 - x3;
            float x23 = x2 - x3;
            float x32 = x3 - x2;
            float x31 = x3 - x1;

            float y12 = y1 - y2;
            float y13 = y1 - y3;
            float y23 = y2 - y3;
            float y31 = y3 - y1;

            float lambda1den = 1.0f / (-y23 * x31 + x23 * y31);
            float lambda2den = 1.0f / (y31 * x23 - x31 * y23);

            bool topleft1 = (y12 < 0 || (y12 == 0 && x12 > 0));
            bool topleft2 = (y23 < 0 || (y23 == 0 && x23 > 0));
            bool topleft3 = (y31 < 0 || (y31 == 0 && x31 > 0));

            for (int x = _colorBuffer.MinX; x <= _colorBuffer.MaxX; x++)
            {
                for (int y = _colorBuffer.MinY; y <= _colorBuffer.MaxY; y++)
                {
                    if ((x12 * (y - y1) - y12 * (x - x1) > 0 && !topleft1 || x12 * (y - y1) - y12 * (x - x1) >= 0 && topleft1) &&
                        (x23 * (y - y2) - y23 * (x - x2) > 0 && !topleft2 || x23 * (y - y2) - y23 * (x - x2) >= 0 && topleft2) &&
                        (x31 * (y - y3) - y31 * (x - x3) > 0 && !topleft3 || x31 * (y - y3) - y31 * (x - x3) >= 0 && topleft3))
                    {
                        float lambda1 = (y23 * (x - x3) - x23 * (y - y3)) * lambda1den;
                        float lambda2 = (y31 * (x - x3) - x31 * (y - y3)) * lambda2den;
                        float lambda3 = 1 - lambda1 - lambda2;

                        float depth = v1.Coordinate.Z * lambda1 + v2.Coordinate.Z * lambda2 + v3.Coordinate.Z * lambda3;

                        if (depth < _colorBuffer.Depth[x, y])
                        {
                            float r = lambda1 * c1.R + lambda2 * c2.R + lambda3 * c3.R;
                            float g = lambda1 * c1.G + lambda2 * c2.G + lambda3 * c3.G;
                            float b = lambda1 * c1.B + lambda2 * c2.B + lambda3 * c3.B;

                            _colorBuffer.Depth[x, y] = depth;

                            _colorBuffer.Color[x, y] = new  Color(r, g, b);
                        }
                    }
                }
            }
        }

        public void Rasterize(Vector3 v1, Vector3 v2, Vector3 v3, Color c1, Color c2, Color c3)
        {
            float x1 = CalculateCanonicalViewX(v1.X);
            float x2 = CalculateCanonicalViewX(v2.X);
            float x3 = CalculateCanonicalViewX(v3.X);

            float y1 = CalculateCanonicalViewY(v1.Y);
            float y2 = CalculateCanonicalViewY(v2.Y);
            float y3 = CalculateCanonicalViewY(v3.Y);

            _colorBuffer.MinX = (int)Math.Min(x1, Math.Min(x2, x3));
            _colorBuffer.MaxX = (int)Math.Max(x1, Math.Max(x2, x3));
            _colorBuffer.MinY = (int)Math.Min(y1, Math.Min(y2, y3));
            _colorBuffer.MaxY = (int)Math.Max(y1, Math.Max(y2, y3));

            _colorBuffer.MinX = Math.Max(_colorBuffer.MinX, 0);
            _colorBuffer.MaxX = Math.Min(_colorBuffer.MaxX, _colorBuffer.Width - 1);
            _colorBuffer.MinY = Math.Max(_colorBuffer.MinY, 0);
            _colorBuffer.MaxY = Math.Min(_colorBuffer.MaxY, _colorBuffer.Height - 1);

            float x12 = x1 - x2;
            float x13 = x1 - x3;
            float x23 = x2 - x3;
            float x32 = x3 - x2;
            float x31 = x3 - x1;

            float y12 = y1 - y2;
            float y13 = y1 - y3;
            float y23 = y2 - y3;
            float y31 = y3 - y1;

            float lambda1den = 1.0f / (-y23 * x31 + x23 * y31);
            float lambda2den = 1.0f / (y31 * x23 - x31 * y23);

            bool topleft1 = (y12 < 0 || (y12 == 0 && x12 > 0));
            bool topleft2 = (y23 < 0 || (y23 == 0 && x23 > 0));
            bool topleft3 = (y31 < 0 || (y31 == 0 && x31 > 0));

            for (int x = _colorBuffer.MinX; x <= _colorBuffer.MaxX; x++)
            {
                for (int y = _colorBuffer.MinY; y <= _colorBuffer.MaxY; y++)
                {
                    if ((x12 * (y - y1) - y12 * (x - x1) > 0 && !topleft1 || x12 * (y - y1) - y12 * (x - x1) >= 0 && topleft1) &&
                        (x23 * (y - y2) - y23 * (x - x2) > 0 && !topleft2 || x23 * (y - y2) - y23 * (x - x2) >= 0 && topleft2) &&
                        (x31 * (y - y3) - y31 * (x - x3) > 0 && !topleft3 || x31 * (y - y3) - y31 * (x - x3) >= 0 && topleft3))
                    {
                        float lambda1 = (y23 * (x - x3) - x23 * (y - y3)) * lambda1den;
                        float lambda2 = (y31 * (x - x3) - x31 * (y - y3)) * lambda2den;
                        float lambda3 = 1 - lambda1 - lambda2;

                        float depth = v1.Z * lambda1 + v2.Z * lambda2 + v3.Z * lambda3;

                        if (depth < _colorBuffer.Depth[x, y])
                        {
                            float r = lambda1 * c1.R + lambda2 * c2.R + lambda3 * c3.R;
                            float g = lambda1 * c1.G + lambda2 * c2.G + lambda3 * c3.G;
                            float b = lambda1 * c1.B + lambda2 * c2.B + lambda3 * c3.B;

                            _colorBuffer.Depth[x, y] = depth;

                            _colorBuffer.Color[x, y] = new Color(r, g, b);
                        }
                    }
                }
            }
        }

        public void DrawTriangle(Point v1, Point v2, Point v3, Light light, VertexProcessor vertexProcessor)
        {
            float x1 = CalculateCanonicalViewX(v1.Coordinate.X);
            float x2 = CalculateCanonicalViewX(v2.Coordinate.X);
            float x3 = CalculateCanonicalViewX(v3.Coordinate.X);

            float y1 = CalculateCanonicalViewY(v1.Coordinate.Y);
            float y2 = CalculateCanonicalViewY(v2.Coordinate.Y);
            float y3 = CalculateCanonicalViewY(v3.Coordinate.Y);

            _colorBuffer.MinX = (int)Math.Min(x1, Math.Min(x2, x3));
            _colorBuffer.MaxX = (int)Math.Max(x1, Math.Max(x2, x3));
            _colorBuffer.MinY = (int)Math.Min(y1, Math.Min(y2, y3));
            _colorBuffer.MaxY = (int)Math.Max(y1, Math.Max(y2, y3));

            _colorBuffer.MinX = Math.Max(_colorBuffer.MinX, 0);
            _colorBuffer.MaxX = Math.Min(_colorBuffer.MaxX, _colorBuffer.Width - 1);
            _colorBuffer.MinY = Math.Max(_colorBuffer.MinY, 0);
            _colorBuffer.MaxY = Math.Min(_colorBuffer.MaxY, _colorBuffer.Height - 1);

            float x12 = x1 - x2;
            float x13 = x1 - x3;
            float x23 = x2 - x3;
            float x32 = x3 - x2;
            float x31 = x3 - x1;

            float y12 = y1 - y2;
            float y13 = y1 - y3;
            float y23 = y2 - y3;
            float y31 = y3 - y1;

            float lambda1den = 1.0f / (-y23 * x31 + x23 * y31);
            float lambda2den = 1.0f / (y31 * x23 - x31 * y23);

            bool topleft1 = (y12 < 0 || (y12 == 0 && x12 > 0));
            bool topleft2 = (y23 < 0 || (y23 == 0 && x23 > 0));
            bool topleft3 = (y31 < 0 || (y31 == 0 && x31 > 0));

            for (int x = _colorBuffer.MinX; x <= _colorBuffer.MaxX; x++)
            {
                for (int y = _colorBuffer.MinY; y <= _colorBuffer.MaxY; y++)
                {
                    if ((x12 * (y - y1) - y12 * (x - x1) > 0 && !topleft1 || x12 * (y - y1) - y12 * (x - x1) >= 0 && topleft1) &&
                        (x23 * (y - y2) - y23 * (x - x2) > 0 && !topleft2 || x23 * (y - y2) - y23 * (x - x2) >= 0 && topleft2) &&
                        (x31 * (y - y3) - y31 * (x - x3) > 0 && !topleft3 || x31 * (y - y3) - y31 * (x - x3) >= 0 && topleft3))
                    {
                        float lambda1 = (y23 * (x - x3) - x23 * (y - y3)) * lambda1den;
                        float lambda2 = (y31 * (x - x3) - x31 * (y - y3)) * lambda2den;
                        float lambda3 = 1 - lambda1 - lambda2;

                        Point f = new Point(
                            new Vector3(
                                (v1.Coordinate.X * lambda1 + v2.Coordinate.X * lambda2 + v3.Coordinate.X * lambda3), 
                                (v1.Coordinate.Y * lambda1 + v2.Coordinate.Y * lambda2 + v3.Coordinate.Y * lambda3), 
                                (v1.Coordinate.Z * lambda1 + v2.Coordinate.Z * lambda2 + v3.Coordinate.Z * lambda3)),
                            new Vector3(
                                (v1.Normal.X * lambda1 + v2.Normal.X * lambda2 + v3.Normal.X * lambda3), 
                                (v1.Normal.Y * lambda1 + v2.Normal.Y * lambda2 + v3.Normal.Y * lambda3), 
                                (v1.Normal.Z * lambda1 + v2.Normal.Z * lambda2 + v3.Normal.Z * lambda3)),
                            v1.Color,
                            v1.TexturePosition.X * lambda1 + v2.TexturePosition.X * lambda2 + v3.TexturePosition.X * lambda3,
                            v1.TexturePosition.Y * lambda1 + v2.TexturePosition.Y * lambda2 + v3.TexturePosition.Y * lambda3
                );

                        Vector3 colorCalculated = light.Calculate(f, vertexProcessor);

                        Color color = new Color(
                            (float)Math.Round(colorCalculated.X * 255.0f),
                            (float)Math.Round(colorCalculated.Y * 255.0f),
                            (float)Math.Round(colorCalculated.Z * 255.0f));

                        float depth = v1.Coordinate.Z * lambda1 + v2.Coordinate.Z * lambda2 + v3.Coordinate.Z * lambda3;

                        if (depth < _colorBuffer.Depth[x, y])
                        {
                            _colorBuffer.Depth[x, y] = depth;
                            _colorBuffer.Color[x, y] = color;
                        }
                    }
                }
            }
        }

        private float CalculateCanonicalViewX(float x)
        {
            return (x + 1) * _colorBuffer.Width * 0.5f;
        }

        private float CalculateCanonicalViewY(float y)
        {
            return (y + 1) * _colorBuffer.Height * 0.5f;
        }
    }
}
