using System;
using UnityEngine;

namespace Infrastructure {
    public interface IPrefabProvider {
        void TryGetPrefabAsync(Presenter presenter, Type viewType, Action<bool, GameObject> OnFinished);
        public void CancelLoading(Presenter presenter);
    }
}