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
            float x = vector4.X * matrix[0].X + vector4.Y * matrix[1].X + vector4.Z * matrix[2].X + vector4.W * matrix[3].X;
            float y = vector4.X * matrix[0].Y + vector4.Y * matrix[1].Y + vector4.Z * matrix[2].Y + vector4.W * matrix[3].Y;
            float z = vector4.X * matrix[0].Z + vector4.Y * matrix[1].Z + vector4.Z * matrix[2].Z + vector4.W * matrix[3].Z;
            float w = vector4.X * matrix[0].W + vector4.Y * matrix[1].W + vector4.Z * matrix[2].W + vector4.W * matrix[3].W;

            Vector4 result = new Vector4(x, y, z, w);

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
