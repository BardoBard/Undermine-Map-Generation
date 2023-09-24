using System;

namespace Map_Generator.Math
{
    public class Vector2Int/*<T> where T : IComparable<T>*/ //TODO: check if it's worth having multiple types of Vector2
    {
        protected bool Equals(Vector2Int other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector2Int)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }

        public static readonly Vector2Int Zero = new(0, 0);
        public static readonly Vector2Int One = new(1, 1);
        public static readonly Vector2Int Up = new(0, 1);
        public static readonly Vector2Int Down = new(0, -1);
        public static readonly Vector2Int Left = new(-1, 0);
        public static readonly Vector2Int Right = new(1, 0);
        public int x = 0;
        public int y = 0;

        public Vector2Int()
        {
        }

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.x + b.x, a.y + b.y);
        public static Vector2Int operator +(Vector2Int a, int b) => new(a.x + b, a.y + b);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.x - b.x, a.y - b.y);
        public static Vector2Int operator -(Vector2Int a, int b) => new(a.x - b, a.y - b);
        public static Vector2Int operator *(Vector2Int a, Vector2Int b) => new(a.x * b.x, a.y * b.y);
        public static Vector2Int operator *(Vector2Int a, int n) => new(a.x * n, a.y * n);
        public static Vector2Int operator /(Vector2Int a, int n) => new(a.x / n, a.y / n);
        public static Vector2Int operator /(Vector2Int a, Vector2Int b) => new(a.x / b.x, a.y / a.y);
    
        public static Vector2Int operator -(Vector2Int a) => new(-a.x, -a.y);
    
        public static bool operator ==(Vector2Int a, Vector2Int b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2Int a, Vector2Int b) => !(a == b);
    }
}