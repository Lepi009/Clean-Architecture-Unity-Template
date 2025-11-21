using System.Collections.Generic;

namespace Domain
{
    public class PointerGestureRecognizer
    {
        private const int NUMBER = 3;
        private bool[] _pressed = new bool[NUMBER];
        private bool[] _dragging = new bool[NUMBER];
        private Vector2D[] _startPos = new Vector2D[NUMBER];
        private float _dragThreshold = 8f;

        private readonly Queue<InputEvent> _generatedEvents = new();



        public void Process(InputEvent evt)
        {
            switch (evt.Type)
            {
                case InputEventType.MouseDown:
                    if (evt.MouseButton == 0 || evt.MouseButton == 1 || evt.MouseButton == 2)
                    {
                        _pressed[evt.MouseButton] = true;
                        _startPos[evt.MouseButton] = evt.Position;
                    }
                    break;

                case InputEventType.MouseMove:
                    if (_pressed[evt.MouseButton] && !_dragging[evt.MouseButton])
                    {
                        if ((evt.Position - _startPos[evt.MouseButton]).Magnitude > _dragThreshold)
                        {
                            _dragging[evt.MouseButton] = true;
                            _generatedEvents.Enqueue(new InputEvent
                            {
                                Type = InputEventType.DragStart,
                                Position = _startPos[evt.MouseButton],
                                Time = evt.Time,
                                MouseButton = evt.MouseButton
                            });
                        }
                    }

                    if (_dragging[evt.MouseButton])
                    {
                        _generatedEvents.Enqueue(new InputEvent
                        {
                            Type = InputEventType.Drag,
                            Position = evt.Position,
                            Time = evt.Time,
                            MouseButton = evt.MouseButton
                        });
                    }
                    break;

                case InputEventType.MouseUp:
                    if (_dragging[evt.MouseButton])
                    {
                        _generatedEvents.Enqueue(new InputEvent
                        {
                            Type = InputEventType.DragEnd,
                            Position = evt.Position,
                            Time = evt.Time,
                            MouseButton = evt.MouseButton
                        });
                    }
                    else if (_pressed[evt.MouseButton])
                    {
                        _generatedEvents.Enqueue(new InputEvent
                        {
                            Type = InputEventType.Click,
                            Position = evt.Position,
                            Time = evt.Time,
                            MouseButton = evt.MouseButton
                        });
                    }

                    _pressed[evt.MouseButton] = false;
                    _dragging[evt.MouseButton] = false;
                    break;
            }
        }

        public IEnumerable<InputEvent> ConsumeGeneratedEvents()
        {
            while (_generatedEvents.Count > 0)
                yield return _generatedEvents.Dequeue();
        }
    }
}
