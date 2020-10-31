using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using static Library.MathMethods;

namespace Library
{
    public class ColorBuffer
    {
        private const int BYTES_PER_PIXEL = 4;

        private const int POINTER_RED = 2;
        private const int POINTER_GREEN = 1;
        private const int POINTER_BLUE = 0;
        private const int POINTER_ALPHA = 3;

        private const double BUFFER_DEPTH = -1;

        private int _width;
        private int _height;
        private Color _color;

        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;

        private Color[,] _pixels;
        private double[,] _depth;

        public ColorBuffer(int width, int height)
        {
            SetSize(width, height);
        }

        public void Print(Triangle triangle)
        {
            SetMinAndMaxValuesForTriangle(triangle);

            for (int width = _minX; width <= _maxX; width++)
            {
                for (int height = _minY; height <= _maxY; height++)
                {
                    if (triangle.IsPixelInTriangle(width, height))
                    {
                        triangle.CalculateLambdas(width, height);
                        double depth = triangle.GetInterpolatedDepth();

                        if (depth > _depth[width, height])
                        {
                            _depth[width, height] = depth;
                            _pixels[width, height] = triangle.GetInterpolatedColor();
                        }                      
                    }                        
                }
            }
        }

        public void SetSize(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception($"Size values must be greather than 0 (width = '{width}' height = '{height}')");

            _width = width;
            _height = height;

            _pixels = new Color[_width, _height];
            _depth = new double[_width, _height];
        }

        public void ClearColor(Color color)
        {
            _color = color;
            ClearColor();
        }

        public void ClearColor(uint color, bool withAlpha)
        {
            _color = new Color(color, withAlpha);
            ClearColor();
        }

        public void ClearColor()
        {
            for (int width = 0; width < _width; width++)
            {
                for (int height = 0; height < _height; height++)
                    _pixels[width, height] = _color;
            }
        }

        public void ClearDepth()
        {
            for (int width = 0; width < _width; width++)
            {
                for (int height = 0; height < _height; height++)
                    _depth[width, height] = BUFFER_DEPTH;
            }
        }

        public void SaveImage(string filepath, bool openImage=false)
        {
            // Source: https://stackoverflow.com/questions/24701703/c-sharp-faster-alternatives-to-setpixel-and-getpixel-for-bitmaps-for-windows-f

            using (Bitmap bitmap = new Bitmap(_width, _height))
            {
                try
                {
                    BitmapData dstData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                    unsafe
                    {
                        byte* dstPointer = (byte*)dstData.Scan0;

                        for (int height = 0; height < _height; height++)
                        {
                            for (int width = 0; width < _width; width++)
                            {
                                dstPointer[POINTER_BLUE] = _pixels[width, height].Blue;
                                dstPointer[POINTER_GREEN] = _pixels[width, height].Green;
                                dstPointer[POINTER_RED] = _pixels[width, height].Red;
                                dstPointer[POINTER_ALPHA] = _pixels[width, height].Alpha;

                                dstPointer += BYTES_PER_PIXEL;
                            }
                        }
                    }

                    bitmap.UnlockBits(dstData);
                    bitmap.Save(filepath);
                }
                catch (Exception e) 
                {
                    string message = $"Problem with saving image {filepath}. Error message: {Environment.NewLine}" +
                        $"'{e.Message}'";

                    MessageBox.Show(message);
                }
            }

            if (openImage)
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = filepath;
                Process.Start(start);
            }
        }

        private void SetMinAndMaxValuesForTriangle(Triangle triangle)
        {
            triangle.CalculatePixelPoints(_width, _height);

            int minX = triangle.X.Min();
            int maxX = triangle.X.Max();

            int minY = triangle.Y.Min();
            int maxY = triangle.Y.Max();

            _minX = Max(minX, 0);
            _maxX = Min(maxX, _width - 1);
            _minY = Max(minY, 0);
            _maxY = Min(maxY, _height - 1);
        }
    }
}
