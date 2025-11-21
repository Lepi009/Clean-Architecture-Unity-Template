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
    public readonly struct Vector3D : IEquatable<Vector3D> {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        #region Constructors

        public Vector3D(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion

        public float Magnitude => MathF.Sqrt(X * X + Y * Y + Z * Z);
        public float SqrMagnitude => X * X + Y * Y + Z * Z;
        public Vector3D Normalized => Magnitude > 0 ? this / Magnitude : Zero;

        public static Vector3D Zero => new(0, 0, 0);
        public static Vector3D One => new(1, 1, 1);
        public static Vector3D Up => new(0, 1, 0);
        public static Vector3D Down => new(0, -1, 0);
        public static Vector3D Left => new(-1, 0, 0);
        public static Vector3D Right => new(1, 0, 0);
        public static Vector3D Forward => new(0, 0, 1);
        public static Vector3D Back => new(0, 0, -1);

        public float DistanceTo(Vector3D other) =>
            MathF.Sqrt(MathF.Pow(X - other.X, 2) + MathF.Pow(Y - other.Y, 2) + MathF.Pow(Z - other.Z, 2));

        public float Dot(Vector3D other) => X * other.X + Y * other.Y + Z * other.Z;

        public static Vector3D operator +(Vector3D a, Vector3D b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Vector3D operator -(Vector3D a, Vector3D b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Vector3D operator *(Vector3D a, float scalar) => new(a.X * scalar, a.Y * scalar, a.Z * scalar);
        public static Vector3D operator /(Vector3D a, float scalar) => new(a.X / scalar, a.Y / scalar, a.Z / scalar);
        public static bool operator ==(Vector3D a, Vector3D b) => a.Equals(b);
        public static bool operator !=(Vector3D a, Vector3D b) => !a.Equals(b);

        public bool Equals(Vector3D other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        public override bool Equals(object obj) => obj is Vector3D other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
        public override string ToString() => $"({X}, {Y}, {Z})";
    }

}