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
            _obj2World = new Matrix4x4();
            _world2View = new Matrix4x4();
            _view2Proj = new Matrix4x4();

            _obj2Proj = new Matrix4x4();
            _obj2View = new Matrix4x4();
        }

        public void SetPerspective(float fovY, float aspect, float near, float far)
        {
            _view2Proj = new Matrix4x4();

            fovY *= (float)Math.PI / 360;
            float f = (float)(Math.Cos(fovY) / Math.Sin(fovY));

            _view2Proj[0] = new Vector4(f / aspect, 0, 0, 0);
            _view2Proj[1] = new Vector4(0, f, 0, 0);
            _view2Proj[2] = new Vector4(0, 0, (far + near) / (near - far), 2 * far * near / (near - far));
            _view2Proj[3] = new Vector4(0, 0, -1, 0);
        }

        public void SetLookAt(Vector3 eye, Vector3 center, Vector3 up)
        {
            Vector3 f = center - eye;
            f = f.Normalize();
            up = up.Normalize();

            Vector3 s = Vector3.Cross(f, up);
            Vector3 u = Vector3.Cross(s, f);

            _world2View[0] = new Vector4(s.X, s.Y, s.Z, -eye.X);
            _world2View[1] = new Vector4(u.X, u.Y, u.Z, -eye.Y);
            _world2View[2] = new Vector4(-f.X, -f.Y, -f.Z, -eye.Z);
            _world2View[3] = new Vector4(0, 0, 0, 1);
        }

        public void SetIdentityView()
        {
            _world2View[0] = new Vector4(1, 0, 0, 0);
            _world2View[1] = new Vector4(0, 1, 0, 0);
            _world2View[2] = new Vector4(0, 0, 1, 0);
            _world2View[3] = new Vector4(0, 0, 0, 1);
        }

        public void SetIdentity()
        {
            _obj2World[0] = new Vector4(1, 0, 0, 0);
            _obj2World[1] = new Vector4(0, 1, 0, 0);
            _obj2World[2] = new Vector4(0, 0, 1, 0);
            _obj2World[3] = new Vector4(0, 0, 0, 1);
        }

        public void Transform()
        {
            _obj2View = _world2View * _obj2World;
            _obj2Proj = _view2Proj * _obj2View;
        }

        public void Rotate(float angle, Vector3 vector)
        {
            float sinus = (float)Math.Sin(angle * Math.PI / 180);
            float cosinus = (float)Math.Cos(angle * Math.PI / 180);
            vector = vector.Normalize();
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(vector.X * vector.X * (1 - cosinus) + cosinus, vector.Y * vector.X * (1 - cosinus) - vector.Z * sinus, vector.X * vector.Z * (1 - cosinus) + vector.Y * sinus, 0),
                new Vector4(vector.X * vector.Y * (1 - cosinus) + vector.Z * sinus, vector.Y * vector.Y * (1 - cosinus) + cosinus, vector.Y * vector.Z * (1 - cosinus) - vector.X * sinus, 0),
                new Vector4(vector.X * vector.Z * (1 - cosinus) - vector.Y * sinus, vector.Y * vector.Z * (1 - cosinus) + vector.X * sinus, vector.Z * vector.Z * (1 - cosinus) + cosinus, 0),
                new Vector4(0, 0, 0, 1)
                );

            _obj2World = matrix * _obj2World;
        }

        public void Translate(Vector3 vector)
        {
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(1, 0, 0, vector.X),
                new Vector4(0, 1, 0, vector.Y),
                new Vector4(0, 0, 1, vector.Z),
                new Vector4(0, 0, 0, 1)
                );

            _obj2World = matrix * _obj2World;
        }

        public void Scale(Vector3 vector)
        {
            Matrix4x4 matrix = new Matrix4x4(
                new Vector4(vector.X, 0, 0, 0),
		        new Vector4(0, vector.Y, 0, 0),
		        new Vector4(0, 0, vector.Z, 0),
		        new Vector4(0, 0, 0, 1)
                );

            _obj2World = matrix * _obj2World;
        }

        public Vector3 Tr(Vector3 vector3)
        {
            Vector4 v4 = new Vector4(vector3.X, vector3.Y, vector3.Z, 1) * _obj2Proj;

            if (v4.W != 0)
            {
                return new Vector3(v4.X, v4.Y, v4.Z);
            }

            return new Vector3(v4.X / v4.W, v4.Y / v4.W, v4.Z / v4.W);
        }

        public Point Tr(Point point)
        {
            Vector4 v4 = new Vector4(point.Coordinate.X, point.Coordinate.Y, point.Coordinate.Z, 1) * _obj2Proj;

            if (v4.W == 0)
            {
                return new Point(v4.X, v4.Y, v4.Z, point.Normal.X, point.Normal.Y, point.Normal.Z);
            }

            return new Point(v4.X / v4.W, v4.Y / v4.W, v4.Z / v4.W, point.Normal.X, point.Normal.Y, point.Normal.Z);
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

        public Vector3 TransformObjectToView(Vector3 vector3, float w)
        {
            Vector4 vector = new Vector4(vector3, w) * _obj2View;

            if (w != 0)
            {
                return new Vector3(
                    vector.X / w,
                    vector.Y / w,
                    vector.Z / w
                    );
            }

            return new Vector3(
                vector.X,
                vector.Y,
                vector.Z
                );
        }

        public Vector3 TransformWorldToView(Vector3 vector3, float w)
        {
            Vector4 vector = new Vector4(vector3, w) * _world2View;

            if (w != 0)
            {
                return new Vector3(
                    vector.X / w,
                    vector.Y / w,
                    vector.Z / w
                    );
            }

            return new Vector3(
                vector.X,
                vector.Y,
                vector.Z
                );
        }
    }
}
