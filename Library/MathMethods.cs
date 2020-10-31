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
    }
}
