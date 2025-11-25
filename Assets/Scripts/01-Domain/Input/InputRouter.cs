namespace Domain {
    public class InputRouter {

        private readonly InputLayerManager _layerManager;

        public InputRouter(InputLayerManager layerManager) {
            _layerManager = layerManager;
        }

        public void OnInputCommand(InputCommand command) {
            PropagateToLayers(command);
        }

        private void PropagateToLayers(InputCommand command) {
            foreach(var layer in _layerManager.LayersTopDown) {
                var result = layer.HandleNewInput(command);
                if(result == InputConsumeType.Handled)
                    break;
            }
        }
    }
}