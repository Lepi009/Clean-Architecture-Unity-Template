using System.Collections;
using Domain;
using UnityEngine;

namespace Infrastructure {
    public class UnityCoroutineRunner : MonoBehaviour, ICoroutineRunner {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        #endregion


        //include all constructors here
        #region Constructors

        public static UnityCoroutineRunner Create() {
            return new GameObject("UnityCoroutineRunner").AddComponent<UnityCoroutineRunner>();
        }

        #endregion


        //include all public methods here
        #region Public Methods
        void ICoroutineRunner.StartCoroutine(IEnumerator coroutine) {
            StartCoroutine(coroutine);
        }
        #endregion


        //include all private methods here
        #region Private Methods

        #endregion

    }
}