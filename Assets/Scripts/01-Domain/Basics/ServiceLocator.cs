using Domain.EventBus;

namespace Domain {
    public static class ServiceLocator {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        public static IDomainLogger Logger { get; private set; }
        public static IEventBus EventBus { get; private set; }
        public static ICoroutineRunner CoroutineRunner { get; private set; }
        public static IRandomService RandomService { get; private set; }

        #endregion

        //include all constructors here
        #region Constructors

        public static void Initialize(IDomainLogger logger, IEventBus eventBus, ICoroutineRunner coroutineRunner, IRandomService randomService) {
            Logger = logger;
            EventBus = eventBus;
            CoroutineRunner = coroutineRunner;
            RandomService = randomService;
        }

        #endregion
    }
}