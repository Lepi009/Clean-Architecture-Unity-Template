using Domain;
using UnityEngine.EventSystems;

public class UnityUILayer : IInputLayer {
    public InputLayerType Type => InputLayerType.SystemOverlay;


    public InputConsumeType HandleNewInput(InputCommand command) {
        // only consume if Unity UI currently wants the event
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) {
            if(command is SubmitInputCommand || command is ZoomComand)
                return InputConsumeType.Handled;
        }

        if(EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null) {
            if(command is SubmitInputCommand) {
                return InputConsumeType.Handled;
            }
        }

        return InputConsumeType.NotHandled;
    }
}