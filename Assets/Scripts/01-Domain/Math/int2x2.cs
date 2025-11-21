namespace Domain {

    public struct int2x2 {
        public readonly int m00, m01;
        public readonly int m10, m11;

        public static readonly int2x2 identity = new(1, 0, 0, 1);

        #region Constructors
        public int2x2(int m00, int m01, int m10, int m11) {
            this.m00 = m00;
            this.m01 = m01;
            this.m10 = m10;
            this.m11 = m11;
        }
        #endregion

        public static int2x2 operator *(int2x2 a, int2x2 b) {
            return new int2x2(
                a.m00 * b.m00 + a.m01 * b.m10,
                a.m00 * b.m01 + a.m01 * b.m11,
                a.m10 * b.m00 + a.m11 * b.m10,
                a.m10 * b.m01 + a.m11 * b.m11
            );
        }

        public override string ToString() {
            return $"|{m00}, {m01}|\n|{m10}, {m11}|";
        }

        public static Vector2DInt operator *(int2x2 m, Vector2DInt v) {
            return new Vector2DInt(
                m.m00 * v.X + m.m01 * v.Y,
                m.m10 * v.X + m.m11 * v.Y
            );
        }

        public static Vector2D operator *(int2x2 m, Vector2D v) {
            return new Vector2D(
                m.m00 * v.X + m.m01 * v.Y,
                m.m10 * v.X + m.m11 * v.Y
            );
        }
    }
}