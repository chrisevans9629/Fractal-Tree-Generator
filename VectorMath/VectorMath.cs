using System;
using BasicVector;

namespace VectorMath
{
    public static class VectorMath
    {
        public static Vector Abs(this Vector a)
        {
            return new Vector(Math.Abs(a.X), Math.Abs(a.Y));
        }
    }
}