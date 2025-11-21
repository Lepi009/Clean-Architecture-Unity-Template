using Domain;
using UnityEngine.EventSystems;

namespace Presentation
{
    public class UnityUILayer : IInputLayer
    {
        public InputLayerType Type => InputLayerType.SystemOverlay;

        public InputConsumeType HandleInput(InputEvent evt)
        {
            // only consume if Unity UI currently wants the event
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                if (evt.Type is InputEventType.MouseDown || evt.Type is InputEventType.Scroll) return InputConsumeType.Handled;
            }

            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
            {
                if ((evt.Type is InputEventType.GamepadButtonDown && evt.GamepadButton == GamepadButton.A)
                || (evt.Type is InputEventType.GamepadDpad))
                {
                    return InputConsumeType.Handled;
                }
            }

            return InputConsumeType.NotHandled;
        }
    }
}