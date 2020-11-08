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

        public static Float3 operator -(Float3 left, Float3 right)
        {
            Float3 result = new Float3(
                left[0] - right[0],
                left[1] - right[1],
                left[2] - right[2]
                );

            return result;
        }
    }
}
