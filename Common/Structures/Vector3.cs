using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Structures
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }


        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3()
            : this(0, 0, 0)
        {
        }

        public Vector3(Vector3 vector3)
            : this(vector3.X, vector3.Y, vector3.Z)
        {
        }

        #region Methods
        public Vector3 Normalize()
        {
            float Length = this.Length();
            float x = X / Length;
            float y = Y / Length;
            float z = Z / Length;

            return new Vector3(x, y, z);
        }

        public float Length() =>
             (float)Math.Sqrt(X * X + Y * Y + Z * Z);


        public static Vector3 Cross(Vector3 vectorA, Vector3 vectorB)
        {
            float x = vectorA.Y * vectorB.Z - vectorA.Z * vectorB.Y;
            float y = vectorA.Z * vectorB.X - vectorA.X * vectorB.Z;
            float z = vectorA.X * vectorB.Y - vectorA.Y * vectorB.X;

            Vector3 result = new Vector3(x, y, z).Normalize();

            return result;
        }

        public static float Dot(Vector3 vectorA, Vector3 vectorB)
        {
            float dot = vectorA.X * vectorB.X + vectorA.Y * vectorB.Y + vectorA.Z * vectorB.Z;

            return dot;
        }
        #endregion

        #region Operators
        public static Vector3 operator +(Vector3 vectorA, Vector3 vectorB) =>
            new Vector3(vectorA.X + vectorB.X, vectorA.Y + vectorB.Y, vectorA.Z + vectorB.Z);

        public static Vector3 operator -(Vector3 vectorA, Vector3 vectorB) =>
            new Vector3(vectorA.X - vectorB.X, vectorA.Y - vectorB.Y, vectorA.Z - vectorB.Z);

        public static Vector3 operator -(Vector3 vector3) =>
            new Vector3(-vector3.X, -vector3.Y, -vector3.Z);

        public static Vector3 operator *(Vector3 vector3, float scale) =>
            new Vector3(vector3.X * scale, vector3.Y * scale, vector3.Z * scale);
        #endregion


        public override string ToString()
        {
            string message = $"X : {X}    Y : {Y}    Z : {Z}";

            return message;
        }
    }
}
