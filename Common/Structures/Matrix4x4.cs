using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Matrix4x4
    {
        public Vector4[] Vector4Rows { get; private set; }


        public Matrix4x4(Vector4 a, Vector4 b, Vector4 c, Vector4 d)
        {
            Vector4Rows = new Vector4[] { a, b, c, d };
        }

        public Matrix4x4()
        {
            Vector4Rows = new Vector4[4];

            for (int i = 0; i < 4; i++)
                Vector4Rows[i] = new Vector4();
        }

        public Matrix4x4(float[,] value)
        {
            Vector4[] v4x4 = new Vector4[4];

            for (int i = 0; i < 4; i++)
                v4x4[i] = new Vector4(value[i, 0], value[i, 1], value[i, 2], value[i, 3]);

            Vector4Rows = v4x4;
        }

        public Matrix4x4(Vector4[] value)
        {
            Vector4[] v4x4 = new Vector4[4];

            for (int i = 0; i < 4; i++)
                v4x4[i] = new Vector4(value[i].X, value[i].Y, value[i].Z, value[i].W);

            Vector4Rows = v4x4;
        }

        public Matrix4x4 Transpose()
        {
            float[,] matrix = new float[4, 4];

            for (int i = 0; i < 4; i++)
            {
                matrix[0, i] = Vector4Rows[i].X;
                matrix[1, i] = Vector4Rows[i].Y;
                matrix[2, i] = Vector4Rows[i].Z;
                matrix[3, i] = Vector4Rows[i].W;
            }

            return new Matrix4x4(matrix);
        }

        #region Operators
        public Vector4 this[int key]
        {
            get => Vector4Rows[key];
            set => Vector4Rows[key] = value;
        }

        public static Matrix4x4 operator -(Matrix4x4 vector4x4)
        {
            Matrix4x4 matrix = new Matrix4x4();

            for (int i = 0; i < 4; i++)
                matrix[i] = -vector4x4[i];

            return matrix;
        }

        public static Matrix4x4 operator *(Matrix4x4 matrixA, Matrix4x4 matrixB)
        {
            float[,] matrix = new float[4, 4];

            matrixB = matrixB.Transpose();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                    matrix[i, j] = Vector4.Dot(matrixA[i], matrixB[j]);
            }

            return new Matrix4x4(matrix);
        }
        #endregion

        public override string ToString()
        {
            string message = string.Empty;

            foreach (Vector4 vector in Vector4Rows)
                message += $"{vector.X} {vector.Y} {vector.Z} {vector.W} {Environment.NewLine}";

            return message;
        }
    }
}
