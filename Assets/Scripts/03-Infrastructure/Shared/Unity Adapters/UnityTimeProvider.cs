using Domain;
using UnityEngine;

namespace Application {
    public class UnityTimeProvider : ITimeProvider {
        public double Now => Time.realtimeSinceStartupAsDouble;
    }
}