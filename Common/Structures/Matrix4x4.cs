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
            : this(new Float4(), new Float4(), new Float4(), new Float4())
        {
        }

        public Matrix4x4(Float4 first, Float4 second, Float4 third, Float4 fourth)
        {
            this.Clear();

            this.Add(first);
            this.Add(second);
            this.Add(third);
            this.Add(fourth);
        }

        public void Transpose()
        {        
            Matrix4x4 transposedMatrix = new Matrix4x4();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    transposedMatrix[i][j] = this[j][i];
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    this[i][j] = transposedMatrix[i][j];
            }
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

            right.Transpose();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i][j] += Float4.Dot(left[i], right[j]);
                    //for (int k = 0; k < 4; k++)
                        //result[i][j] += left[i][k] * right[j][k];
                        
                }
            }

            return result;
        }

        public override string ToString()
        {
            string result = string.Empty;

            foreach (Float4 row in this)
                result += $"{row} {Environment.NewLine}";

            return result;
        }
    }
}
