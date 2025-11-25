namespace Domain {
    public interface IInputLayer {
        InputLayerType Type { get; }
        InputConsumeType HandleNewInput(InputCommand command);
    }

    public enum InputLayerType {
        SystemOverlay,
        Popups,
        HUD,
        Gameplay
    }
}