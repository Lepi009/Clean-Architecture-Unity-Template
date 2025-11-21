namespace Domain {
    public class InputRouter {
        private readonly PointerGestureRecognizer _recognizer = new();

        private readonly InputLayerManager _layerManager;

        public InputRouter(InputLayerManager layerManager) {
            _layerManager = layerManager;
        }

        public void OnInputEvent(InputEvent evt) {
            // feed event to recognizer
            if(evt.Type is InputEventType.MouseDown or InputEventType.MouseMove or InputEventType.MouseUp)
                _recognizer.Process(evt);

            // handle original input
            PropagateToLayers(evt);

            // handle generated gesture events
            foreach(var ge in _recognizer.ConsumeGeneratedEvents())
                PropagateToLayers(ge);
        }

        private void PropagateToLayers(InputEvent evt) {
            foreach(var layer in _layerManager.LayersTopDown) {
                var result = layer.HandleInput(evt);
                if(result == InputConsumeType.Handled)
                    break;
            }
        }
    }
}