using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Matrix4x4 : List<Float4>
    {
        public Matrix4x4() 
        {
            for (int i = 0; i < 4; i++)
                this.Add(new Float4());
        }

        public Matrix4x4(Float4 first, Float4 second, Float4 third, Float4 fourth)
        {
            this.Add(first);
            this.Add(second);
            this.Add(third);
            this.Add(fourth);
        }

        public static Matrix4x4 operator *(Matrix4x4 matrix, float value)
        {
            Matrix4x4 newMatrix = new Matrix4x4(
                matrix[0] * value,
                matrix[1] * value,
                matrix[2] * value,
                matrix[3] * value
            );

            return newMatrix;
        }

        public static Matrix4x4 operator*(Matrix4x4 left, Matrix4x4 right)
        {
            Matrix4x4 result = new Matrix4x4();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                        result[i][j] += left[i][k] * right[k][j];
                }
            }

            return result;
        }
    }
}
