using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class MathMethods
    {
        public static int Min(int left, int right)
        {
            if (left < right)
                return left;

            return right;
        }

        public static int Max(int left, int right)
        {
            if (left > right)
                return left;

            return right;
        }

        public static float Min(float left, float right)
        {
            if (left < right)
                return left;

            return right;
        }

        public static float Max(float left, float right)
        {
            if (left > right)
                return left;

            return right;
        }

        public static void Normalize(IList<float> vector)
        {
            float maxValue = vector
                .Max(v => Math.Abs(v));

            float factor = 1 / maxValue;

            for (int i = 0; i < vector.Count; i++)
                vector[i] *= factor;
        }

        public static Float3 Cross(Float3 left, Float3 right)
        {
            float x = left[1] * right[2] - left[2] * right[1];
            float y = left[2] * right[0] - left[0] * right[2];
            float z = left[0] * right[1] - left[1] * right[0];

            Float3 crossProduct = new Float3(x, y, z);

            return crossProduct;
        }
    }
}
