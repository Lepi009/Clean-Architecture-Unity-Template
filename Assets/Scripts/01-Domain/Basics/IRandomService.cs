namespace Domain {
    public interface IRandomService {
        /// <summary>
        /// get a random value in range, min included, max excluded
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRange(int min, int max);

        /// <summary>
        /// gets a random value in the range, min and max included
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public float GetRange(float min, float max);
        public float GetNextFloatZeroToOne();
    }
}