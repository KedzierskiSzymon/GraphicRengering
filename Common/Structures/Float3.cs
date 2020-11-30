using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Float3 : List<float>
    {
        public Float3(float firstValue, float secondValue, float thirdValue)
        {
            this.Add(firstValue);
            this.Add(secondValue);
            this.Add(thirdValue);
        }

        public float X => this[0];
        public float Y => this[1];
        public float Z => this[2];

        public float Length => (float)Math.Sqrt(X * X + Y * Y + Z * Z);

        public Float3 Normalize()
        {
            float x = X / Length;
            float y = Y / Length;
            float z = Z / Length;

            return new Float3(x, y, z);
        }

        public static Float3 operator -(Float3 left, Float3 right)
        {
            Float3 result = new Float3(
                left[0] - right[0],
                left[1] - right[1],
                left[2] - right[2]
                );

            return result;
        }

        public static Float3 operator -(Float3 vector3)
        {
            Float3 result = new Float3(
                -vector3[0],
                -vector3[1],
                -vector3[2]
            );

            return result;
        }

        public override string ToString()
        {
            string result = $"X: '{X}' Y: '{Y}' Z: '{Z}'";

            return result;
        }
    }
}
