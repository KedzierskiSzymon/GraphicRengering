using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Vector4 : Vector3
    {
        public float W { get; set; }

        #region Constructors
        public Vector4(float x, float y, float z, float w = 0) 
            : base(x, y, z)
        {
            W = w;
        }

        public Vector4() 
            : base()
        {
            W = 0;
        }

        public Vector4(Vector3 v, float w) 
            : base(v)
        {
            W = w;
        }
        #endregion

        #region Operators
        public static Vector4 operator *(Vector4 vector4, float scalar) =>
            new Vector4(vector4.X * scalar,
                        vector4.Y * scalar,
                        vector4.Z * scalar,
                        vector4.W * scalar
                       );

        public static Vector4 operator *(Vector4 vector4, int scalar) =>
            new Vector4(vector4.X * scalar,
                        vector4.Y * scalar,
                        vector4.Z * scalar,
                        vector4.W * scalar
                       );

        public static Vector4 operator *(Vector4 vector4, Matrix4x4 matrix)
        {
            float[] temp = new float[4] { 0, 0, 0, 0 };

            for (int i = 0; i < 4; ++i)
            {
                temp[i] += vector4.X * matrix[i].X;
                temp[i] += vector4.Y * matrix[i].Y;
                temp[i] += vector4.Z * matrix[i].Z;
                temp[i] += vector4.W * matrix[i].W;
            }
            Vector4 result = new Vector4(temp[0], temp[1], temp[2], temp[3]);

            return result;
        }

        public static Vector4 operator -(Vector4 vector4)
        {
            return new Vector4(-vector4.X, -vector4.Y, -vector4.Z, -vector4.W);
        }


        public static float Dot(Vector4 vectorA, Vector4 vectorB)
        {
            float dot = vectorA.X * vectorB.X + vectorA.Y * vectorB.Y + vectorA.Z * vectorB.Z + vectorA.W * vectorB.W;

            return dot;
        }
        #endregion
    }
}
