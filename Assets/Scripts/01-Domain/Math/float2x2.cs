namespace Domain {
    public struct float2x2 {
        public readonly float m00, m01;
        public readonly float m10, m11;

        public static readonly float2x2 identity = new(1f, 0f, 0f, 1f);

        #region Constructors

        public float2x2(float m00, float m01, float m10, float m11) {
            this.m00 = m00;
            this.m01 = m01;
            this.m10 = m10;
            this.m11 = m11;
        }

        #endregion

        public static float2x2 operator *(float2x2 a, float2x2 b) {
            return new float2x2(
                a.m00 * b.m00 + a.m01 * b.m10,
                a.m00 * b.m01 + a.m01 * b.m11,
                a.m10 * b.m00 + a.m11 * b.m10,
                a.m10 * b.m01 + a.m11 * b.m11
            );
        }

        public override string ToString() {
            return $"|{m00}, {m01}|\n|{m10}, {m11}|";
        }

        public static Vector2D operator *(float2x2 m, Vector2D v) {
            return new Vector2D(
                m.m00 * v.X + m.m01 * v.Y,
                m.m10 * v.X + m.m11 * v.Y
            );
        }
    }
}