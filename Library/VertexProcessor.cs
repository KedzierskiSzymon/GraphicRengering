using Common.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.MathMethods;

namespace Library
{
    public class VertexProcessor
    {
        private Matrix4x4 _obj2World;
        private Matrix4x4 _world2View;
        private Matrix4x4 _view2Proj;

        public VertexProcessor()
        {
            _world2View = new Matrix4x4();
            _view2Proj = new Matrix4x4();
            _obj2World = new Matrix4x4
            (
                new Float4(1,   0,  0,  0),
                new Float4(0,   1,  0,  0),
                new Float4(0,   0,  1,  0),
                new Float4(0,   0,  0,  1)
            );
        }

        public void SetPerspective(float fovY, float aspect, float near, float far)
        {
            fovY *= (float)Math.PI / 360;
            float f = (float)(Math.Cos(fovY) / Math.Sin(fovY));

            float m11 = f / aspect;
            float m22 = f;
            float m33 = (far + near) / (near - far);
            float m44 = 2 * far * near / (near - far);

            _view2Proj = new Matrix4x4
            (
                new Float4(m11,  0,      0 ,     0),
                new Float4(0,    m22,    0 ,     0),
                new Float4(0,    0,      m33 ,   -1),
                new Float4(0,    0,      0 ,     m44)
            );
            _view2Proj = new Matrix4x4
            (
                new Float4(m11,  0,      0 ,     0),
                new Float4(0,    m22,    0 ,     0),
                new Float4(0,    0,      m33 ,   -1),
                new Float4(0,    0,      0 ,     m44)
            );
        }

        public void SetLookAt(Float3 eye, Float3 center, Float3 up)
        {
            Float3 f = center - eye;
            Normalize(f);
            Normalize(up);

            Float3 s = Cross(f, up);
            Float3 u = Cross(s, f);

            _world2View = new Matrix4x4
            (
                new Float4(s.X,     u.X,   -f.X,    0),
                new Float4(s.Y,     u.Y,   -f.Y,    0),
                new Float4(s.Z,     u.Z,   -f.Z,    0),
                new Float4(0,       0,      0,      1)
            );

            Matrix4x4 matrix = new Matrix4x4();
            matrix[3] = new Float4(-eye.X, -eye.Y, -eye.Z, 1);
            _world2View *= matrix;
        }

        public void MultiplyByTranslation(Float3 value)
        {
            Matrix4x4 matrix = new Matrix4x4
            (
                new Float4(1,           0,          0,          0),
                new Float4(0,           1,          0,          0),
                new Float4(0,           0,          1,          0),
                new Float4(value.X,     value.Y,    value.Z,    1)
            );

            _obj2World *= matrix;
        }

        public void MultiplyByScale(Float3 value)
        {
            Matrix4x4 matrix = new Matrix4x4
            (
                new Float4(value.X,     0,          0,          0),
                new Float4(0,           value.Y,    0,          0),
                new Float4(0,           0,          value.Z,    0),
                new Float4(0,           0,          0,          1)
            );

            _obj2World *= matrix;
        }

        public void MultiplyByRotation(float angle, Float3 value)
        {
            float sinus = (float)Math.Sin(angle * Math.PI / 180);
            float cosinus = (float)Math.Cos(angle * Math.PI / 180);
            Normalize(value);

            float m11 = value.X * value.X * (1 - cosinus) + cosinus;
            float m12 = value.X * value.Y * (1 - cosinus) + value.Z * sinus;
            float m13 = value.X * value.Z * (1 - cosinus) - value.Y * sinus;
            float m21 = value.Y * value.X * (1 - cosinus) - value.Z * sinus;
            float m22 = value.Y * value.Y * (1 - cosinus) + cosinus;
            float m23 = value.Y * value.Z * (1 - cosinus) + value.X * sinus;
            float m31 = value.Z * value.X * (1 - cosinus) + value.Y * sinus;
            float m32 = value.Z * value.Y * (1 - cosinus) - value.X * sinus;
            float m33 = value.Z * value.Z * (1 - cosinus) + cosinus;

            Matrix4x4 matrix = new Matrix4x4
            (
                new Float4(m11,     m12,    m13,    0),
                new Float4(m21,     m22,    m23,    0),
                new Float4(m31,     m32,    m33,    0),
                new Float4(0,       0,      0,      1)
            );

            _obj2World *= matrix;
        }

        public Float3 Tr(Float3 value)
        {
            Float4 vector = new Float4(value.X, value.Y, value.Z, 1) * _view2Proj;

            Float3 result = new Float3(
                vector.X / vector.W, 
                vector.Y / vector.W, 
                vector.Z / vector.Z);

            return result;
        }
    }
}
