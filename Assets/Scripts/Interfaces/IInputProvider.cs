using Enums;

namespace Interfaces
{
    public interface IInputProvider
    {
        public float GetAxisInput(Axis axis);
        public bool GetActionPressed(InputAction action);
    }
}
