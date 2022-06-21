using System;
using UnityEngine;

namespace GeneralFunctions
{
    [Serializable]
    public struct Int2  /*System.Object*/
    {

        public int X;
        public int Y;
        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Int2 zero
        {
            get
            {
                return new Int2(0, 0);
            }
        }
        public static Int2 one
        {
            get
            {
                return new Int2(1, 1);
            }
        }

        public static Int2 operator+(Int2 a, Int2 b) => new Int2(a.X + b.X, a.Y + b.Y);
        public static Int2 operator-(Int2 a, Int2 b) => new Int2(a.X - b.X, a.Y - b.Y);
        public static Int2 operator*(Int2 a, int b) => new Int2(a.X * b, a.Y * b);
        public static Int2 operator/(Int2 a, int b)
        {
            if (b == 0) return a;
            return new Int2(a.X / b, a.Y / b);
        }
        public static bool operator ==(Int2 a, Int2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Int2 a, Int2 b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public bool Equals(Int2 other)
        {
            return this == other;
        }
        public override string ToString()
        {
            return X.ToString() + "-" + Y.ToString();
        }
    }
}
