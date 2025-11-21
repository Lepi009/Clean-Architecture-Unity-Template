using System;
using System.Collections.Generic;

namespace Domain {
    public readonly struct RectInt : IEquatable<RectInt> {
        public readonly int MinX, MinY, Width, Height;
        /// <summary>
        /// the max x which is still inside
        /// </summary>
        public int MaxX => MinX + Width - 1;
        /// <summary>
        /// the max y which is still inside
        /// </summary>
        public int MaxY => MinY + Height - 1;
        public Vector2DInt MinPosition => new(MinX, MinY);
        public Vector2D Center => MinPosition + LocalCenter;
        public Vector2D LocalCenter => new(Width / 2.0f, Height / 2.0f);
        public Vector2DInt Size => new(Width, Height);

        #region Constructors
        public RectInt(int minX, int minY, int width, int height) {
            MinX = minX; MinY = minY; Width = width; Height = height;
        }

        public RectInt(Vector2DInt position, Vector2DInt size)
            : this(position.X, position.Y, size.X, size.Y) { }

        public RectInt(int minX, int minY, Vector2DInt size)
            : this(minX, minY, size.X, size.Y) { }

        public RectInt(Vector2DInt position, int width, int height)
            : this(position.X, position.Y, width, height) { }

        #endregion

        public bool Equals(RectInt other) =>
            MinX == other.MinX && MinY == other.MinY && Width == other.Width && Height == other.Height;
        public override bool Equals(object obj) => obj is RectInt other && Equals(other);
        public override int GetHashCode() => HashCode.Combine(MinX, MinY, Width, Height);
        public override string ToString() => $"Min:({MinX}, {MinY}, Max:({MaxX}, {MaxY})";

        // Add operator: grows rect by 'value' on all sides
        public static RectInt operator +(RectInt rect, Vector2DInt v) {
            return new RectInt(
                rect.MinPosition + v, rect.Size
            );
        }

        // Subtract operator: shrinks rect by 'value' on all sides
        public static RectInt operator -(RectInt rect, Vector2DInt v) {
            return new RectInt(
                rect.MinPosition - v, rect.Size
            );
        }

        public bool Contains(Vector2DInt v)
            => MinX <= v.X && v.X <= MaxX &&
                   MinY <= v.Y && v.Y <= MaxY;
    }

    public static class RectIntExtension {
        public static bool IntersectsAnyCell(this RectInt rect, IEnumerable<Vector2DInt> cells) {
            foreach(var cell in cells) {
                if(rect.Contains(cell))
                    return true;
            }
            return false;
        }
    }
}