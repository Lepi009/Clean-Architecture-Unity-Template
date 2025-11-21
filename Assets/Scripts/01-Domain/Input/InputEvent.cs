
namespace Domain {
    public struct InputEvent {
        public InputEventType Type;

        //Keyboard
        public int Key;

        //Mouse
        public int MouseButton;
        public Vector2D Position;
        public Vector2D Movement; //used for WASD/analog


        //Gamepad
        public GamepadButton GamepadButton;
        public GamepadStick GamepadStick;
        public float AnalogValue;
        public Vector2D StickVector;


        //Timing
        public float Time;
    }

    public enum InputEventType {
        //Keyboard
        KeyDown,
        KeyUp,

        //Mouse
        MouseDown,
        MouseUp,
        MouseMove,
        Scroll,

        //Gamepad
        // Gamepad buttons (digital)
        GamepadButtonDown,
        GamepadButtonUp,

        // Gamepad analog input
        GamepadStickMove,
        GamepadTrigger,

        // DPad directional movement
        GamepadDpad,

        // high-level gestures
        Click,
        DragStart,
        Drag,
        DragEnd,
        Movement
    }

    public enum GamepadButton {
        A,
        B,
        X,
        Y,
        LB,
        RB,
        LT, // treat as digital press event
        RT,
        Start,
        Back,
        LeftStickPress,
        RightStickPress,
    }

    public enum GamepadStick {
        Left,
        Right
    }
}