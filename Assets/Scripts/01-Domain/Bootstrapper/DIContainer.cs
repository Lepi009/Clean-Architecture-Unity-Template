using System;
using System.Collections.Generic;

namespace Domain {
    public class DIContainer : IDisposable {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        private readonly Dictionary<Type, object> _instances = new();
        public HashSet<object> Instances => new(_instances.Values);
        private readonly List<IUpdatable> _updatables = new();
        private readonly Queue<IInitializable> _initializables = new();

        #endregion

        //include all public methods here
        #region Public Methods

        // Register a concrete instance for a service type
        public void Register<TService>(TService instance) {
            _instances[typeof(TService)] = instance;

            if(instance is IInitializable initializable
                && !_initializables.Contains(initializable)) {
                _initializables.Enqueue(initializable);
            }

            if(instance is IUpdatable updatable && !_updatables.Contains(updatable)) {
                _updatables.Add(updatable);
            }

        }

        // Resolve an instance by type
        public TService Resolve<TService>() {
            if(_instances.TryGetValue(typeof(TService), out var instance))
                return (TService)instance;

            throw new Exception($"Service {typeof(TService)} not registered");
        }

        public void InitializePending() {
            while(_initializables.TryDequeue(out var initializable)) {
                initializable.Initialize();
            }
        }

        public void UpdateAll(float deltaTime) {
            for(int i = 0; i < _updatables.Count; i++) {
                _updatables[i].Update(deltaTime);
            }
        }

        public void Dispose() {
            _initializables.Clear();
            _updatables.Clear();
            _instances.Clear();
        }

        #endregion

    }
}