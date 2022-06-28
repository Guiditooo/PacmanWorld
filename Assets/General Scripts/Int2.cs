using System;
using UnityEngine;

namespace GeneralFunctions
{
    [Serializable]
    public struct Int2
    {
        public int X;
        public int Y;
        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Int2(Vector2 other)
        {
            X = Mathf.RoundToInt(other.x);
            Y = Mathf.RoundToInt(other.y);
        }
        public Int2(Vector3 other)
        {
            X = Mathf.RoundToInt(other.x);
            Y = Mathf.RoundToInt(other.y);
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
        public static Int2 ToInt2(Vector2 other)
        {
            return new Int2((int)other.x, (int)other.y);
        }
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
        public Vector3 ToVector3()
        {
            return new Vector2(X, Y);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
