using Common.Structures;
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

        public void Rasterize(Vector3 v1, Vector3 v2, Vector3 v3, uint a, uint r, uint g, uint b)
        {
            float y1 = (v1.Y + 1) * _colorBuffer.Height * 0.5f;
            float y2 = (v2.Y + 1) * _colorBuffer.Height * 0.5f;
            float y3 = (v3.Y + 1) * _colorBuffer.Height * 0.5f;

            float x1 = (v1.X + 1) * _colorBuffer.Width * 0.5f;
            float x2 = (v2.X + 1) * _colorBuffer.Width * 0.5f;
            float x3 = (v3.X + 1) * _colorBuffer.Width * 0.5f;

            float dx12 = x2 - x1;
            float dx23 = x3 - x2;
            float dx31 = x1 - x3;
            float dy12 = y2 - y1;
            float dy23 = y3 - y2;
            float dy31 = y1 - y3;

            int minx = (int)(Min(x1, x2, x3) + 0.5f);
            int maxx = (int)(Max(x1, x2, x3) + 0.5f);
            int miny = (int)(Min(y1, y2, y3) + 0.5f);
            int maxy = (int)(Max(y1, y2, y3) + 0.5f);

            minx = Max(minx, 0);
            maxx = Min(maxx, _colorBuffer.Width - 1);
            miny = Max(miny, 0);
            maxy = Min(maxy, _colorBuffer.Height - 1);

            if (miny > maxy || minx > maxx)
                return;


            for (int y = miny; y <= maxy; y++)
            {
                for (int x = minx; x <= maxx; x++)
                {
                    float tl1 = float.Epsilon, tl2 = float.Epsilon, tl3 = float.Epsilon;

                    if (dy12 > 0 || (dy12 == 0 && dx12 < 0)) tl1 = 0;
                    if (dy23 > 0 || (dy23 == 0 && dx23 < 0)) tl2 = 0;
                    if (dy31 > 0 || (dy31 == 0 && dx31 < 0)) tl3 = 0;

                    if (dx12 * (y - y1) - dy12 * (x - x1) <= 0 + tl1 &&
                        dx23 * (y - y2) - dy23 * (x - x2) <= 0 + tl2 &&
                        dx31 * (y - y3) - dy31 * (x - x3) <= 0 + tl3)
                    {
                        float lambdaDenom = dy31 * dx23 - dx31 * dy23;

                        float lambdaA = (-dy23 * (x - x3) + dx23 * (y - y3)) / lambdaDenom;
                        float lambdaB = (-dy31 * (x - x3) + dx31 * (y - y3)) / lambdaDenom;
                        float lambdaC = 1 - lambdaA - lambdaB;

                        uint R = (uint)(r * lambdaA) & 0x00ff0000;
                        uint G = (uint)(g * lambdaB) & 0x0000ff00;
                        uint B = (uint)(b * lambdaC) & 0x000000ff;

                        float depth = (lambdaA * v1.Z + lambdaB * v2.Z + lambdaC * v3.Z);

                        if (depth < _colorBuffer.DepthBuffer[x, y])
                        {
                            _colorBuffer.DepthBuffer[x, y] = depth;
                            _colorBuffer.Pixels[x, y] = (a | R | G | B);
                        }
                    }
                }
            }
        }
    }
}
