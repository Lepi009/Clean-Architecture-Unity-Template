namespace Domain {
    public abstract class InputScheme {
        //include all fields and properties here (private & public)
        #region Fields and Properties

        protected readonly InputRouter _router;

        #endregion


        //include all constructors here
        #region Constructors

        public InputScheme(InputRouter router) {
            _router = router;
        }

        #endregion


        //include all public methods here
        #region Public Methods

        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void Update(float deltaTime);
        public abstract bool IsUsedThisFrame();

        #endregion


        //include all private methods here
        #region Private Methods

        #endregion

    }
}