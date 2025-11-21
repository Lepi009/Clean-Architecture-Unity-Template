using UnityEngine;
using Domain;
using Domain.EventBus;
using Infrastructure;
using Application;

namespace Presentation {
    public class Bootstrapper : MonoBehaviour {
        //include all fields and properties here (private & public)
        #region Fields and Propertie

        private DIContainer _container;

        #endregion

        //include all events here
        #region Events

        #endregion

        #region Unity Callbacks

        private void Awake() {
            _container = new DIContainer();

            // Register concrete instances or services
            _container.Register<IDomainLogger>(
                new UnityLogger()
            );

            _container.Register<ITimeProvider>(new UnityTimeProvider());

            _container.Register<IEventBus>(
                new DecoupledEventBus(_container.Resolve<ITimeProvider>())
            );

            _container.Register<IRandomService>(new UnityRandomAdapter());

            _container.Register<ICoroutineRunner>(UnityCoroutineRunner.Create());

            ServiceLocator.Initialize(
                _container.Resolve<IDomainLogger>(),
                _container.Resolve<IEventBus>(),
                _container.Resolve<ICoroutineRunner>(),
                _container.Resolve<IRandomService>()
            );

            //input
            _container.Register(new InputLayerManager(
                new IInputLayer[] {
                    new UnityUILayer()
                }
            ));

            //platform-dependent injection
            _container.Register<IFileProvider>(
    #if UNITY_WEBGL
                new WebRequestFileProvider()
    #else
                new ReadAllTextFileProvider()
    #endif
            );
        }

        #endregion
    }
}