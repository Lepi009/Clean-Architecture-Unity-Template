namespace Domain {
    public interface InputCommand { }

    //some default commands:
    public readonly record struct SubmitInputCommand() : InputCommand;

    public readonly record struct DragInputCommand(DragEventType Phase, Vector2D Delta) : InputCommand;

    public readonly record struct LeaveInputCommand() : InputCommand;

    public readonly record struct MovementCommand(Vector2D Delta) : InputCommand;

    public readonly record struct ZoomComand(Vector2D Delta) : InputCommand;

    public enum DragEventType {
        DragStart, Dragging, DragEnd
    }
}