using Common.Constants;
using System;
using System.Globalization;
using System.Windows;

namespace Common.Structures
{
    public class Color
    {
        private const byte RGB_LENGTH = 6;
        private const byte RGBA_LENGTH = 8;

        #region Channel bytes
        private const byte RED_FIRST_BYTE = 0;
        private const byte RED_SECOND_BYTE = 1;

        private const byte GREEN_FIRST_BYTE = 2;
        private const byte GREEN_SECOND_BYTE = 3;

        private const byte BLUE_FIRST_BYTE = 4;
        private const byte BLUE_SECOND_BYTE = 5;

        private const byte ALPHA_FIRST_BYTE = 6;
        private const byte ALPHA_SECOND_BYTE = 7;
        #endregion

        private uint _value;

        private byte _red;
        private byte _green;
        private byte _blue;
        private byte _alpha;

        public byte R
        {
            get
            {
                return _red;
            }
            set
            {
                _red = value;
                RecalculateValue();
            }
        }
        public byte G
        {
            get
            {
                return _green;
            }
            set
            {
                _green = value;
                RecalculateValue();
            }
        }
        public byte B
        {
            get
            {
                return _blue;
            }
            set
            {
                _blue = value;
                RecalculateValue();
            }
        }
        public byte A
        {
            get
            {
                return _alpha;
            }
            set
            {
                _alpha = value;
                RecalculateValue();
            }
        }
        public uint Value => _value;
        public string ValueRgba
        {
            get
            {
                return ConvertToRgbaText(_value);
            }
        }

        #region Constructors
        public Color(Color color)
        {
            _value = color.Value;
        }

        public Color(uint colorValue, bool withAlpha)
        {
            SetColor(colorValue, withAlpha);
        }

        public Color(byte red, byte green, byte blue, byte alpha = byte.MaxValue)
        {
            _red = red;
            _green = green;
            _blue = blue;
            _alpha = alpha;

            RecalculateValue();
        }

        private Color() { }
        #endregion

        public void SetColor(uint colorValue, bool withAlpha)
        {
            string colorValueText = colorValue.ToString(Formats.Hexadecimal);

            if (!withAlpha)
                colorValueText += "ff";

            colorValueText = colorValueText.PadLeft(RGBA_LENGTH, '0');

            if (colorValueText.Length == RGBA_LENGTH)
            {
                bool isValidColorValue = uint.TryParse(colorValueText, NumberStyles.HexNumber, null, out _value);

                if (isValidColorValue)
                {
                    SetChannels();

                    return;
                }                 
            }

            throw new Exception($"Invalid color value. (color value = '{colorValueText}')");
        }

        public override string ToString()
        {
            string info = $"0x{ConvertToRgbaText(_value)}";

            return info;
        }

        public static Color Red
        {
            get
            {
                return new Color(0xff0000, false);
            }
        }

        public static Color Green
        {
            get
            {
                return new Color(0x00ff00, false);
            }
        }

        public static Color Blue
        {
            get
            {
                return new Color(0x0000ff, false);
            }
        }

        public static Color Black
        {
            get
            {
                return new Color(0x000000, false);
            }
        }

        public static Color White
        {
            get
            {
                return new Color(0xffffff, false);
            }
        }

        #region Operators
        public static Color operator *(float value, Color color)
        {
            Color returnedColor = new Color()
            {
                R = (byte)(color.R * value),
                G = (byte)(color.G * value),
                B = (byte)(color.B * value),
                A = color.A,
            };

            return returnedColor;
        }

        public static Color operator *(Color color, float value)
        {
            Color returnedColor = new Color()
            {
                R = (byte)(color.R * value),
                G = (byte)(color.G * value),
                B = (byte)(color.B * value),
                A = color.A,
            };

            return returnedColor;
        }

        public static Color operator +(Color left, Color right)
        {
            Color returnedColor = new Color()
            {
                R = (byte)(left.R + right.R),
                G = (byte)(left.G + right.G),
                B = (byte)(left.B + right.B),
                A = left.A
            };

            return returnedColor;
        }
        #endregion

        private void SetChannels()
        {
            char[] currentColorValueHexText = ConvertToRgbaText(Value).ToCharArray();

            string redText = $"{currentColorValueHexText[RED_FIRST_BYTE]}{currentColorValueHexText[RED_SECOND_BYTE]}";
            string greenText = $"{currentColorValueHexText[GREEN_FIRST_BYTE]}{currentColorValueHexText[GREEN_SECOND_BYTE]}";
            string blueText = $"{currentColorValueHexText[BLUE_FIRST_BYTE]}{currentColorValueHexText[BLUE_SECOND_BYTE]}";
            string alphaText = $"{currentColorValueHexText[ALPHA_FIRST_BYTE]}{currentColorValueHexText[ALPHA_SECOND_BYTE]}";

            try
            {
                _red = byte.Parse(redText, NumberStyles.HexNumber);
                _green = byte.Parse(greenText, NumberStyles.HexNumber);
                _blue = byte.Parse(blueText, NumberStyles.HexNumber);
                _alpha = byte.Parse(alphaText, NumberStyles.HexNumber);

            }
            catch (Exception e)
            {
                string message = $"Invalid color values. Color = '{string.Join("", currentColorValueHexText)}'. Message: {Environment.NewLine}" +
                    $"'{e.Message}'";
            }
        }

        private void RecalculateValue()
        {
            string red = R.ToString(Formats.Hexadecimal);
            string green = G.ToString(Formats.Hexadecimal);
            string blue = B.ToString(Formats.Hexadecimal);
            string alpha = A.ToString(Formats.Hexadecimal);

            string colorValueText = red + green + blue + alpha;
            uint colorValue = uint.Parse(colorValueText, NumberStyles.HexNumber);
            _value = colorValue;
        }

        private string ConvertToRgbaText(uint value)
        {
            string valueText = value.ToString(Formats.Hexadecimal);
            valueText = valueText.PadLeft(RGBA_LENGTH, '0');

            return valueText;
        }
    }
}
