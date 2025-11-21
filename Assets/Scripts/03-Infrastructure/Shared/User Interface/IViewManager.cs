using System;

namespace Infrastructure {
    public interface IViewManager {

        /// <summary>
        /// creates a single view for a given view model. If the view is already instantiated, it will still return true but it will not call construct nor it will instantiate another view
        /// </summary>
        /// <typeparam name="TPresenter"></typeparam>
        /// <param name="presenter"></param>
        /// <returns></returns>
        public void CreateSingleViewForAsync<TPresenter>(TPresenter presenter, Action<bool> OnFinished = null) where TPresenter : Presenter;
        public bool IsViewInstantiated<TPresenter>(TPresenter presenter) where TPresenter : Presenter;
        public bool TryRemoveViewOf(Presenter presenter);

        public void Register<TView, TPresenter, TViewArgs>(Func<TViewArgs> argsFactory) where TPresenter : Presenter where TView : View<TPresenter, TViewArgs>;
        public void Register<TView, TPresenter>() where TPresenter : Presenter where TView : View<TPresenter, NoArgs>;
    }
}