using Enums;

namespace Interfaces
{
    public interface IInputProvider
    {
        public float GetAxisInput(Axis axis);
        public bool GetAction(InputAction action);
        public bool GetActionPressed(InputAction action);
    }
}
