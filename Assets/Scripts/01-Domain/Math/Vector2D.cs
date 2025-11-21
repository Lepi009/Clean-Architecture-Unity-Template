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
    public readonly struct Vector2D : IEquatable<Vector2D> {
        public readonly float X;
        public readonly float Y;

        #region Constructors
        public Vector2D(float x, float y) {
            X = x;
            Y = y;
        }

        public Vector2D(Vector3D v) {
            X = v.X;
            Y = v.Z;
        }

        #endregion

        public float Magnitude => MathF.Sqrt(X * X + Y * Y);
        public float SqrMagnitude => X * X + Y * Y;
        public Vector2D Normalized => Magnitude > 0 ? this / Magnitude : Zero;

        public static Vector2D Zero => new(0, 0);
        public static Vector2D One => new(1, 1);
        public static Vector2D Up => new(0, 1);
        public static Vector2D Down => new(0, -1);
        public static Vector2D Left => new(-1, 0);
        public static Vector2D Right => new(1, 0);

        public float DistanceTo(Vector2D other) =>
            MathF.Sqrt(MathF.Pow(X - other.X, 2) + MathF.Pow(Y - other.Y, 2));

        public float Dot(Vector2D other) => X * other.X + Y * other.Y;

        public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2D operator *(Vector2D a, float scalar) => new(a.X * scalar, a.Y * scalar);
        public static Vector2D operator /(Vector2D a, float scalar) => new(a.X / scalar, a.Y / scalar);
        public static Vector2D operator +(Vector2D a, Vector2DInt b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2D operator +(Vector2DInt a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
        public static bool operator ==(Vector2D a, Vector2D b) => a.Equals(b);
        public static bool operator !=(Vector2D a, Vector2D b) => !a.Equals(b);

        public bool Equals(Vector2D other) => X.Equals(other.X) && Y.Equals(other.Y);
        public override bool Equals(object obj) => obj is Vector2D other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y);
        public override string ToString() => $"({X}, {Y})";
    }

}