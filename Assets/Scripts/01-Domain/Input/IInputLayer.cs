namespace Domain {
    public interface IInputLayer {
        // Called to handle a raw input event. Return how the event was consumed.
        InputConsumeType HandleInput(InputEvent evt);

        InputLayerType Type { get; }
    }

    public enum InputLayerType {
        SystemOverlay,
        Popups,
        HUD,
        Gameplay
    }
}