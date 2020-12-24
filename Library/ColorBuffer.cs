using Library.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using static Library.MathMethods;
using Color = Common.Structures.Color;
using Point = Common.Structures.Point;

namespace Library
{
    public class ColorBuffer
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public uint[,] Pixels { get; set; }
        public float[,] DepthBuffer { get; set; }


        public ColorBuffer(int width, int height)
        {
            Pixels = new uint[width, height];
            DepthBuffer = new float[width, height];

            Width = width;
            Height = height;

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    DepthBuffer[i, j] = float.MaxValue;
        }


        public void ClearColor(uint color)
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    Pixels[i, j] = color;
        }

        public void ClearDepth(float depth)
        {
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    DepthBuffer[i, j] = depth;
        }

        public uint[,] GetPixels()
        {
            return Pixels;
        }
    }
}
