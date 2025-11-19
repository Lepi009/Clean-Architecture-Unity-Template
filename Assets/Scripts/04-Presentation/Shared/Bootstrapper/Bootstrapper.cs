using UnityEngine;
using Domain;
using Domain.EventBus;
using Infrastructure;

namespace Presentation {
    public class Bootstrapper : MonoBehaviour {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        [Header("Mono Behaviours")]
        [SerializeField] private GameObject PrefabLoggingUI;
        //[SerializeField] private UnityCoroutineRunner _unityCoroutineRunner;

        private DIContainer _container;

        #endregion

        //include all events here
        #region Events

        #endregion

        #region Unity Callbacks

        private void Awake() {
            _container = new DIContainer();
/*
            // Register concrete instances or services
            _container.Register<IDomainLogger>(
                new UnityLogger()
            );

            _container.Register<ITimeProvider>(new UnityTimeProvider());

            _container.Register<IEventBus>(
                new DecoupledEventBus(_container.Resolve<ITimeProvider>())
            );
            //input
            _container.Register(new InputLayerManager(
                new IInputLayer[] {
                    new UnityUILayer()
                }
            ));*/

        }

        #endregion
    }
}