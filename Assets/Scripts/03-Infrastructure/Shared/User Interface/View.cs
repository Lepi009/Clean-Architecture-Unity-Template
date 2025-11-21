using UnityEngine;

namespace Infrastructure {
    public abstract class View : MonoBehaviour {

    }

    public abstract class View<TPresenter, TArgs> : View where TPresenter : Presenter {
        private TPresenter _presenter;
        public TPresenter Presenter {
            get => _presenter;
            set {
                _presenter = value;
            }
        }

        public abstract void Construct(TArgs args);
    }

    public struct NoArgs { }
}