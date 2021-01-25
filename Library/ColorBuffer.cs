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

        public Color[,] Color { get; set; }
        public float[,] Depth { get; set; }

        public int MinX { get; set; }
        public int MaxX { get; set; }
        public int MinY { get; set; }
        public int MaxY { get; set; }


        public ColorBuffer(int width, int height)
        {
            Color = new Color[width, height];
            Depth = new float[width, height];

            Width = width;
            Height = height;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    Depth[i, j] = float.MaxValue;
            }
        }


        public void ClearColor(Color color)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    Color[i, j] = color;
            }
        }

        public void ClearDepth(float depth)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                    Depth[i, j] = depth;
            }
        }
    }
}
