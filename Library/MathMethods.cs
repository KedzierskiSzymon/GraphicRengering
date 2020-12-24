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
        public static float Min(params float[] values)
        {
            float min = values.Min();

            return min;
        }

        public static float Max(params float[] values)
        {
            float max = values.Max();

            return max;
        }

        public static int Min(params int[] values)
        {
            int min = values.Min();

            return min;
        }

        public static int Max(params int[] values)
        {
            int max = values.Max();

            return max;
        }
    }
}
