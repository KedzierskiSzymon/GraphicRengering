using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Float4 : List<float>
    {
        public Float4()
        {
            this.Add(default);
            this.Add(default);
            this.Add(default);
            this.Add(default);
        }

        public Float4(float firstValue, float secondValue, float thirdValue, float fourthValue)
        {
            this.Add(firstValue);
            this.Add(secondValue);
            this.Add(thirdValue);
            this.Add(fourthValue);
        }

        public Float4(Float3 vector, float fourthValue)
        {
            this.Add(vector[0]);
            this.Add(vector[1]);
            this.Add(vector[2]);
            this.Add(fourthValue);
        }

        public float X => this[0];
        public float Y => this[1];
        public float Z => this[2];
        public float W => this[3];

        public static float Dot(Float4 left, Float4 right)
        {
            float dot = left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;

            return dot;
        }

        public static Float4 operator *(Float4 vector, float value)
        {
            Float4 newVector = new Float4
            (
                vector[0] * value,
                vector[1] * value,
                vector[2] * value,
                vector[3] * value
            );

            return newVector;
        }

        public static Float4 operator *(Float4 left, Matrix4x4 right)
        {
            Float4 newVector = new Float4();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    newVector[i] += left[j] * right[j][i];
            }

            return newVector;
        }

        public static Float4 operator -(Float4 value)
        {
            return new Float4(-value.X, -value.Y, -value.Z, -value.W);
        }

        public override string ToString()
        {
            string result = $"X: '{X}' Y: '{Y}' Z: '{Z}' W: {W}";

            return result;
        }
    }
}
