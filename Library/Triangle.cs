using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Triangle
    {
        public Point[] _points;
        public readonly Point[] Points;

        public int[] X;
        public int[] Y;

        private float[] _lambdas;

        private float _dx12;
        private float _dx23;
        private float _dx31;
        private float _dy12;
        private float _dy23;
        private float _dy31;
        
        public Triangle(Point[] points)
        {
            if (points.Length != 3)
                throw new Exception($"Triange must have 3 points. Written points: '{points.Length}'");

            _points = points;
            Points = _points;
            _lambdas = new float[3];
        }

        public Triangle(Triangle triangle)
        {
            _points = new Point[]
            {
                triangle.Points[0],
                triangle.Points[1],
                triangle.Points[2]
            };

            _points = Points;
            _lambdas = new float[3];
        }

        public void CalculatePixelPoints(int width, int height)
        {
            X = _points
                .Select(p => GetValueInPixel(p.X, width))
                .ToArray();

            Y = _points
                .Select(p => GetValueInPixel(p.Y, height))
                .ToArray();

            CalculateConstantPoints();
        }

        public bool IsPixelInTriangle(int width, int height)
        {
            if (FirstCondition(width, height) &&
                SecondCondition(width, height) &&
                ThirdCondition(width, height))
            {
                return true;
            }

            return false;
        }

        public Color GetInterpolatedColor()
        {
            Color returnedColor = Points[0].Color * _lambdas[0] + Points[1].Color * _lambdas[1] + Points[2].Color * _lambdas[2];

            return returnedColor;
        }

        public float GetInterpolatedDepth()
        {
            float depth = Points[0].Z * _lambdas[0] + Points[1].Z * _lambdas[1] + Points[2].Z * _lambdas[2];

            return depth;
        }

        #region Conditions for checking if pixel is in triangle
        private bool FirstCondition(int width, int height)
        {
            int left = (X[0] - X[1]) * (height - Y[0]);
            int right = (Y[0] - Y[1]) * (width - X[0]);

            int difference = left - right;

            if (difference > 0)
                return true;

            return false;
        }

        private bool SecondCondition(int width, int height)
        {
            int left = (X[1] - X[2]) * (height - Y[1]);
            int right = (Y[1] - Y[2]) * (width - X[1]);

            int difference = left - right;

            if (difference > 0)
                return true;

            return false;
        }

        private bool ThirdCondition(int width, int height)
        {
            int left = (X[2] - X[0]) * (height - Y[2]);
            int right = (Y[2] - Y[0]) * (width - X[2]);

            int difference = left - right;

            if (difference > 0)
                return true;

            return false;
        }
        #endregion
        #region Lambdas for calculating color in triangle
        public void CalculateLambdas(int width, int height)
        {
            float lambda1 = Lambda1(width, height);
            float lambda2 = Lambda2(width, height);
            float lambda3 = 1 - lambda1 - lambda2;

            _lambdas = new float[]
            {
                lambda1,
                lambda2,
                lambda3
            };
        }

        private float Lambda1(int width, int height)
        {
            float numerator = (Y[1] - Y[2]) * (width - X[2]) + (X[2] - X[1]) * (height - Y[2]);
            float denominator = (Y[1] - Y[2]) * (X[0] - X[2]) + (X[2] - X[1]) * (Y[0] - Y[2]);

            float lambda = (denominator != 0) ? numerator / denominator : 1;

            return lambda;
        }

        private float Lambda2(int width, int height)
        {
            float numerator = (Y[2] - Y[0]) * (width - X[2]) + (X[0] - X[2]) * (height - Y[2]);
            float denominator = (Y[2] - Y[0]) * (X[1] - X[2]) + (X[0] - X[2]) * (Y[1] - Y[2]);

            float lambda = (denominator != 0) ? numerator / denominator : 1;

            return lambda;
        }
        #endregion

        private int GetValueInPixel(float value, int pictureDimensionValue)
        {
            float pixelValue = (value + 1) * pictureDimensionValue * .5f;

            return (int)Math.Floor(pixelValue);
        }

        private void CalculateConstantPoints()
        {
            _dx12 = X[0] - X[1];
            _dx23 = X[1] - X[2;
            _dx31 = X[2] - X[0];

            _dy12 = Y[0] - Y[1];
            _dy23 = Y[1] - Y[2];
            _dy31 = Y[2] - Y[0];
        }
    }
}
