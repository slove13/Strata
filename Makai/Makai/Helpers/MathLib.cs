using Microsoft.Xna.Framework;
using System;
namespace Makai.Helpers
{
    public static class MathLib
    {

        public static float GoldenRatio { get { return 1.61803398875f; } }

        /// <summary>
        /// Returns TRUE if integer is even. Returns FALSE if integer is odd
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEven(int i)
        {
            return i % 2 == 0;
        }
        /// <summary>
        /// Returns TRUE if integer is odd. Returns FALSE if integer is even
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsOdd(int i)
        {
            return i % 2 != 0;
        }

        /// <summary>
        /// Sum of all positive integers from 0 to i
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int SumOfIntegers(int i)
        {
            return i * (i + 1) / 2;
        }

        public static Vector3 Normal(Vector3 top, Vector3 left, Vector3 right)
        {
            return Vector3.Cross(left - top, right - top);
        }
        public static Vector3 NormalizedNormal(Vector3 top, Vector3 left, Vector3 right)
        {
            Vector3 normal = Normal(top, left, right);
            normal.Normalize();
            return normal;
        }
        public static Vector3 Center(Vector3 top, Vector3 left, Vector3 right)
        {
            return new Vector3((top.X + left.X + right.X) / 3, (top.Y + left.Y + right.Y) / 3, (top.Z + left.Z + right.Z) / 3);
        }

        /// <summary>
        /// Returns area of a triangle from its vertex locations using Heron's formula
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static double TriangleArea(Vector3 top, Vector3 left, Vector3 right)
        {
            double rightSide = Vector3.Distance(top, right);
            double leftSide = Vector3.Distance(top, left);
            double bottomSide = Vector3.Distance(left, right);
            double perimeter = (rightSide + leftSide + bottomSide) / 2;

            return Math.Sqrt(perimeter * (perimeter - rightSide) * (perimeter - leftSide) * (perimeter - bottomSide));
        }
    }
}