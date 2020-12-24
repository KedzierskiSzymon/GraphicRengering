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
        readonly static Vector3 UP = new Vector3(0, 0, 0);

        private Matrix4x4 _obj2World;
        private Matrix4x4 _world2View;
        private Matrix4x4 _view2Proj;

        private Matrix4x4 _obj2Proj;
        private Matrix4x4 _obj2View;


        public VertexProcessor()
        {

        }

        public void SetPerspective(float fovY, float aspect, float near, float far)
        {
            _view2Proj = new Matrix4x4();

            fovY *= (float)Math.PI / 360;
            float f = (float)(Math.Cos(fovY) / Math.Sin(fovY));

            _view2Proj[0] = new Vector4(f / aspect, 0, 0, 0);
            _view2Proj[1] = new Vector4(0, f, 0, 0);
            _view2Proj[2] = new Vector4(0, 0, (far + near) / (near - far), -1);
            _view2Proj[3] = new Vector4(0, 0, 2 * far * near / (near - far));
            _view2Proj = _view2Proj.Transpose();
        }

        public void SetLookAt(Vector3 from, Vector3 to, Vector3 tmp = null)
        {
            tmp = tmp ?? UP;
            _world2View = new Matrix4x4();

            Vector3 forward = (to - from).Normalize();
            Vector3 right = Vector3.Cross(tmp, forward);
            Vector3 up = Vector3.Cross(forward, right);

            _world2View[0] = new Vector4(right.X, right.Y, right.Z, 0);
            _world2View[1] = new Vector4(up.X, up.Y, up.Z, 0);
            _world2View[2] = new Vector4(-forward.X, -forward.Y, -forward.Z, 0);
            _world2View[3] = new Vector4(-from.X, -from.Y, -from.Z, 1);
        }

        public void Transform()
        {
            _obj2View = _obj2World * _world2View;
            _obj2Proj = _obj2View * _view2Proj;
        }

        public Vector3 Tr(Vector3 vector3)
        {
            Vector4 v4 = new Vector4(vector3.X, vector3.Y, vector3.Z, 1) * _obj2Proj;

            return new Vector3(v4.X / v4.W, v4.Y / v4.W, v4.Z / v4.W);
        }

        public void Scale(float scale)
        {
            Scale(scale, scale, scale);
        }

        public void Scale(float x, float y, float z)
        {
            float[,] vals = new float[,] { 
                { x, 0, 0, 0 }, 
                { 0, y, 0, 0 }, 
                { 0, 0, z, 0 }, 
                { 0, 0, 0, 1 } 
            };

            Matrix4x4 v = new Matrix4x4(vals);

            _obj2World = v *  _obj2World;
        }

        public void Translate(Vector3 vector3)
        {
            float[,] vals = new float[,] { 
                { 1, 0, 0, 0 }, 
                { 0, 1, 0, 0 }, 
                { 0, 0, 1, 0 }, 
                { vector3.X, vector3.Y, vector3.Z, 1 } 
            };

            Matrix4x4 v = new Matrix4x4(vals);

            _obj2World = v *  _obj2World;
        }

        public void Rotate(float angle, Vector3 axis)
        {
            float sinus = (float)Math.Sin(angle * Math.PI / 180);
            float cosinus = (float)Math.Cos(angle * Math.PI / 180);

            axis = -axis;

            axis = axis.Normalize();

            float[,] vals = new float[,] {
                { axis.X * axis.X * (1 - cosinus) + cosinus, axis.Y * axis.X * (1 - cosinus) + axis.Z * sinus, axis.X * axis.Z * (1 - cosinus) + axis.Y * sinus, 0},
                { axis.X * axis.Y * (1 - cosinus) - axis.Z * sinus, axis.Y * axis.Y * (1 - cosinus) + cosinus, axis.Y * axis.Z * (1 - cosinus) + axis.X * sinus, 0},
                { axis.X * axis.Z * (1 - cosinus) + axis.Y * sinus, axis.Y * axis.Z * (1 - cosinus) - axis.X * sinus, axis.Z * axis.Z * (1 - cosinus) + cosinus, 0},
                { 0 , 0 , 0 , 1 } };

            Matrix4x4 v = new Matrix4x4(vals);

            _obj2World =_obj2World * v;
        }


        public void SetIdentity()
        {
            float[,] matrix = new float[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };

            _obj2World = new Matrix4x4(matrix);
        }
    }
}
