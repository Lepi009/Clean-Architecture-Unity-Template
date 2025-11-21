using Domain;
using UnityEngine;

namespace Infrastructure {
    public class UnityTimeProvider : ITimeProvider {
        public double Now => Time.realtimeSinceStartupAsDouble;
    }
}