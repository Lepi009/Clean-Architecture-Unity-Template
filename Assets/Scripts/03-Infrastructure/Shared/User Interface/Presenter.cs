using System;
using Domain.EventBus;

namespace Infrastructure {
    public abstract class Presenter : IDisposable {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        private bool _disposed;
        public bool IsDisposed => _disposed;
        protected IViewManager _viewManager;
        protected readonly SubscriptionGroup _subGroup = new();

        #endregion


        //include all constructors here
        #region Constructors

        public Presenter(IViewManager viewManager) {
            _viewManager = viewManager;
        }

        public void Dispose() {
            if(_disposed) return;

            Cleanup();
            _viewManager.TryRemoveViewOf(this);
            _subGroup.Dispose();
            _disposed = true;
        }

        #endregion


        //include all public methods here
        #region Public Methods

        protected virtual void Cleanup() { }

        #endregion


        //include all private methods here
        #region Private Methods

        #endregion

    }
}