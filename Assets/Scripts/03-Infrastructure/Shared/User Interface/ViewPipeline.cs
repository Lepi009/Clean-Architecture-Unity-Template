using System;
using System.Collections.Generic;
using Domain;
using UnityEngine;

namespace Infrastructure {
    public class ViewPipeline : IViewManager {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        private readonly Dictionary<Type, IViewFactory> _viewFactories = new();
        private readonly Dictionary<Type, Type> _presenterToView = new();
        private readonly Dictionary<Presenter, View> _viewConnections = new();
        private readonly IPrefabProvider _prefabProvider;

        #endregion


        //include all constructors here
        #region Constructors

        public ViewPipeline(IPrefabProvider prefabProvider) {
            _prefabProvider = prefabProvider;
        }

        #endregion


        //include all public methods here
        #region Public Methods

        public void CreateSingleViewForAsync<TPresenter>(TPresenter presenter, Action<bool> OnFinished) where TPresenter : Presenter {
            //find view type from presenter type
            if(!_presenterToView.TryGetValue(presenter.GetType(), out var viewType)) {
                ServiceLocator.Logger.LogError($"Cannot find ViewType of Presenter: {presenter}");
                OnFinished?.Invoke(false);
                return;
            }

            //check if already instantiated and return true if so
            if(_viewConnections.ContainsKey(presenter)) {
                OnFinished?.Invoke(true);
                return;
            }

            _prefabProvider.TryGetPrefabAsync(presenter, viewType, (succ, prefab) => {
                if(succ) {
                    // Find factory
                    if(!_viewFactories.TryGetValue(viewType, out var factory)) {
                        ServiceLocator.Logger.LogError($"No factory registered for view {viewType}");
                        OnFinished?.Invoke(false);
                        return;
                    }

                    // Internally construct view with correct types
                    View viewInstance = factory.CreateView(prefab, presenter);

                    // Track connection
                    _viewConnections[presenter] = viewInstance;

                    OnFinished?.Invoke(true);
                }
                else {
                    OnFinished?.Invoke(false);
                }
            });

        }

        public bool IsViewInstantiated<TPresenter>(TPresenter presenter) where TPresenter : Presenter {
            return _viewConnections.ContainsKey(presenter);
        }

        public void Register<TView, TPresenter, TArgs>(Func<TArgs> argsFactory)
             where TPresenter : Presenter where TView : View<TPresenter, TArgs> {
            _presenterToView[typeof(TPresenter)] = typeof(TView);
            _viewFactories[typeof(TView)] = new ViewFactory<TView, TPresenter, TArgs>(argsFactory);
        }

        public void Register<TView, TPresenter>() where TPresenter : Presenter where TView : View<TPresenter, NoArgs> {
            Register<TView, TPresenter, NoArgs>(() => new NoArgs());
        }

        public bool TryRemoveViewOf(Presenter presenter) {
            if(_viewConnections.TryGetValue(presenter, out var viewInstance)) {
                _viewConnections.Remove(presenter);
                if(viewInstance != null)
                    GameObject.Destroy(viewInstance.gameObject);
            }
            _prefabProvider.CancelLoading(presenter);
            return true;
        }

        #endregion
    }
}