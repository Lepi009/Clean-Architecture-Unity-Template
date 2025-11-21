using System;

namespace Domain {
    /// <summary>
    /// Right now � no, you cannot change X or Y after construction because the structs are defined as 
    /// readonly and the properties have only getters (i.e. they are immutable).
    /// This is intentional and a good design for value types in the domain layer, especially when:
    /// You want vectors to be thread-safe.
    /// You treat them as mathematical values (not objects with identity).
    /// You want predictable behavior in comparisons and hashing.    /// 
    /// </summary>
    public readonly struct Vector2DInt : IEquatable<Vector2DInt> {
        public readonly int X;
        public readonly int Y;

        #region Constructors
        public Vector2DInt(int x, int y) {
            X = x;
            Y = y;
        }
        #endregion

        public static Vector2DInt Zero => new(0, 0);
        public static Vector2DInt One => new(1, 1);
        public static Vector2DInt Up => new(0, 1);
        public static Vector2DInt Down => new(0, -1);
        public static Vector2DInt Left => new(-1, 0);
        public static Vector2DInt Right => new(1, 0);

        public int DistanceSquared(Vector2DInt other) =>
            (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);

        public float Distance(Vector2DInt other) =>
            MathF.Sqrt(DistanceSquared(other));

        public static Vector2DInt operator +(Vector2DInt a, Vector2DInt b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2DInt operator +(Vector2DInt a, int b) => new(a.X + b, a.Y + b);
        public static Vector2DInt operator +(int b, Vector2DInt a) => new(a.X + b, a.Y + b);
        public static Vector2DInt operator -(Vector2DInt a, Vector2DInt b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2DInt operator -(Vector2DInt a, int b) => new(a.X - b, a.Y - b);
        public static Vector2DInt operator -(int b, Vector2DInt a) => new(b - a.X, b - a.Y);
        public static Vector2DInt operator *(Vector2DInt a, int scalar) => new(a.X * scalar, a.Y * scalar);
        public static Vector2DInt operator /(Vector2DInt a, int scalar) => new(a.X / scalar, a.Y / scalar);
        public static bool operator ==(Vector2DInt a, Vector2DInt b) => a.Equals(b);
        public static bool operator !=(Vector2DInt a, Vector2DInt b) => !a.Equals(b);

        public bool Equals(Vector2DInt other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj) => obj is Vector2DInt other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public override string ToString() => $"({X}, {Y})";
    }

}