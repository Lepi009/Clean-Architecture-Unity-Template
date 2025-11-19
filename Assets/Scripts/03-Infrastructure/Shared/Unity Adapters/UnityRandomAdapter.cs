using Domain;
using UnityEngine;

namespace Infrastructure {
    public class UnityRandomAdapter : IRandomService {

        //include all public methods here
        #region Public Methods

        public float GetRange(float min, float max) {
            return Random.Range(min, max);
        }

        public float GetNextFloatZeroToOne() {
            return Random.value;
        }

        public int GetRange(int min, int max) {
            return Random.Range(min, max);
        }

        #endregion

    }
}