using Common.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Common.Structures.Color;

namespace Library
{
    public class Texture
    {
        public int Width { get; }
        public int Height { get; }

        public bool CalculateLight { get; set; }

        private Color[,] _color;

        public Texture(int width, int height)
        {
            Width = width;
            Height = height;

            _color = new Color[Width, Height];
        }

        public void LoadTextureFromFile(string filename)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(@"D:\studia\II stopien\2\Modelowanie i analiza systemów grafiki komputerowej\GraphicRengering\TestConsole\"  + filename);

            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    System.Drawing.Color pixelColor = bitmap.GetPixel(w, h);
                    _color[w, h] = new Color(pixelColor.R, pixelColor.G, pixelColor.B);
                }               
            }
        }

        public Color this[int width, int height]
        {
            get => _color[width, height];
        }

    }
}
