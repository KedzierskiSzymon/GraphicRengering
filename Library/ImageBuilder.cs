using System;
using System.Collections.Generic;
using System.Drawing;
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
            uint[,] pixels = _colorBuffer.Pixels;
            Bitmap bitmap = new Bitmap(_colorBuffer.Width, _colorBuffer.Height);

            for (int i = 0; i < _colorBuffer.Width; i++)
            {
                for (int j = 0; j < _colorBuffer.Height; j++)
                {
                    uint pixel = pixels[i, j];

                    int B = (int)(pixel & 0xff); pixel >>= 8;
                    int G = (int)(pixel & 0xff); pixel >>= 8;
                    int R = (int)(pixel & 0xff); pixel >>= 8;
                    int A = (int)(pixel & 0xff);

                    bitmap.SetPixel(i, _colorBuffer.Height - j - 1, Color.FromArgb(A, R, G, B));
                }
            }

            bitmap.Save(filename, ImageFormat.Bmp);

            if (openFile)
                System.Diagnostics.Process.Start(filename);
        }
    }
}
