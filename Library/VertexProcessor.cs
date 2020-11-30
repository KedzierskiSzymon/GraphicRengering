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
        readonly static Float3 UP = new Float3(0, 0, 0);

        private Matrix4x4 _obj2World;
        private Matrix4x4 _world2View;
        private Matrix4x4 _view2Proj;

        private Matrix4x4 _obj2Proj;
        private Matrix4x4 _obj2View;

        public VertexProcessor()
        {
            _world2View = GetIdentity();
            _view2Proj = new Matrix4x4();
            _obj2World = GetIdentity();
        }

        public void SetPerspective(float fovY, float aspect, float near, float far)
        {
            _view2Proj = new Matrix4x4();

            fovY *= (float)Math.PI / 360;
            float f = (float)(Math.Cos(fovY) / Math.Sin(fovY));

            float m11 = f / aspect;
            float m22 = f;
            float m33 = (far + near) / (near - far);
            float m34 = 2 * far * near / (near - far);

            _view2Proj = new Matrix4x4
            (
                new Float4(m11,  0,      0 ,     0),
                new Float4(0,    m22,    0 ,     0),
                new Float4(0,    0,      m33 ,  -1),
                new Float4(0,    0,      m34,    0)
            );

            _view2Proj.Transpose();
        }
        
        public void SetLookAt(Float3 from, Float3 to, Float3 tmp = null)
        {
            tmp = tmp ?? UP;
            _world2View = new Matrix4x4();

            Float3 forward = (to - from).Normalize();
            Float3 right = Cross(tmp, forward);
            Float3 up = Cross(forward, right);

            _world2View[0] = new Float4(right.X, right.Y, right.Z, 0);
            _world2View[1] = new Float4(up.X, up.Y, up.Z, 0);
            _world2View[2] = new Float4(-forward.X, -forward.Y, -forward.Z, 0);
            _world2View[3] = new Float4(-from.X, -from.Y, -from.Z, 1);
        }
        
        /* 
        public void SetLookAt(Float3 eye, Float3 center, Float3 up)
        {
            Float3 f = center - eye;
            f.Normalize();
            up.Normalize();

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
        */

        public void Translate(Float3 value)
        {
            Matrix4x4 matrix = new Matrix4x4
            (
                new Float4(1,           0,          0,          0),
                new Float4(0,           1,          0,          0),
                new Float4(0,           0,          1,          0),
                new Float4(value.X,     value.Y,    value.Z,    1)
            );

            _obj2World = matrix * _obj2World;
        }

        public void Scale(Float3 value)
        {
            Matrix4x4 matrix = new Matrix4x4
            (
                new Float4(value.X,     0,          0,          0),
                new Float4(0,           value.Y,    0,          0),
                new Float4(0,           0,          value.Z,    0),
                new Float4(0,           0,          0,          1)
            );

            _obj2World = matrix * _obj2World;
        }

        public void Rotate(float angle, Float3 axis)
        {
            float sinus = (float)Math.Sin(angle * Math.PI / 180);
            float cosinus = (float)Math.Cos(angle * Math.PI / 180);

            axis = -axis;
            axis.Normalize();

            float m11 = axis.X * axis.X * (1 - cosinus) + cosinus;
            float m12 = axis.X * axis.Y * (1 - cosinus) + axis.Z * sinus;
            float m13 = axis.X * axis.Z * (1 - cosinus) + axis.Y * sinus;
            float m21 = axis.Y * axis.X * (1 - cosinus) - axis.Z * sinus;
            float m22 = axis.Y * axis.Y * (1 - cosinus) + cosinus;
            float m23 = axis.Y * axis.Z * (1 - cosinus) + axis.X * sinus;
            float m31 = axis.Z * axis.X * (1 - cosinus) + axis.Y * sinus;
            float m32 = axis.Z * axis.Y * (1 - cosinus) - axis.X * sinus;
            float m33 = axis.Z * axis.Z * (1 - cosinus) + cosinus;

            Matrix4x4 matrix = new Matrix4x4
            (
                new Float4(m11,     m12,    m13,    0),
                new Float4(m21,     m22,    m23,    0),
                new Float4(m31,     m32,    m33,    0),
                new Float4(0,       0,      0,      1)
            );

            _obj2World = _obj2World * matrix;
        }

        public Float3 Tr(Float3 value)
        {
            Float4 vector = new Float4(value.X, value.Y, value.Z, 1) * _obj2Proj;

            Float3 result = new Float3(
                vector.X / vector.W, 
                vector.Y / vector.W, 
                vector.Z / vector.W);

            return result;
        }

        public void Transform()
        {
            _obj2View = _obj2World * _world2View;
            _obj2Proj = _obj2View * _view2Proj;
        }

        public void SetIdentity()
        {
            _obj2World = GetIdentity();
        }

        private Matrix4x4 GetIdentity()
        {
            return new Matrix4x4
            (
                new Float4(1, 0, 0, 0),
                new Float4(0, 1, 0, 0),
                new Float4(0, 0, 1, 0),
                new Float4(0, 0, 0, 1)
            );
        }
    }
}
