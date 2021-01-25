using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ImageBuilder
    {
        private ColorBuffer _colorBuffer;

        public ImageBuilder(ColorBuffer colorBuffer)
        {
            _colorBuffer = colorBuffer;
        }

        public void SaveImage(string filename, bool openFile = true)
        {
            Common.Structures.Color[,] pixels = _colorBuffer.Color;
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(_colorBuffer.Width, _colorBuffer.Height);

            for (int width = 0; width < _colorBuffer.Width; width++)
            {
                for (int height = 0; height < _colorBuffer.Height; height++)
                {
                    Common.Structures.Color pixel = pixels[width, height];

                    int B = GetValidColor((int)pixel.B);
                    int G = GetValidColor((int)pixel.G);
                    int R = GetValidColor((int)pixel.R);
                    int A = GetValidColor((int)pixel.A);

                    bitmap.SetPixel(width, _colorBuffer.Height - height - 1, System.Drawing.Color.FromArgb(A, R, G, B));
                }
            }

            bitmap.Save(filename, ImageFormat.Bmp);

            if (openFile)
                System.Diagnostics.Process.Start(filename);
        }

        private int GetValidColor(int color)
        {
            if (color < byte.MinValue)
                return byte.MinValue;

            if (color > byte.MaxValue)
                return byte.MaxValue;

            return color;
        }
    }
}
