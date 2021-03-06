using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using UnityEngine;

namespace InputSystems
{
    public class PlayerInputProvider : MonoBehaviour, IInputProvider
    {
        private const string JumpButton = "Jump";

        private HashSet<InputAction> _requestedActions = new HashSet<InputAction>();

        public float GetAxisInput(Axis axis)
        {

            return Input.GetAxisRaw(axis.ToUnityAxis());
        }

        public bool GetAction(InputAction action)
        {
            switch (action)
            {
                case InputAction.Jump:
                    return Input.GetButton(JumpButton);
                default:
                    return false;
            }
        }

        public bool GetActionPressed(InputAction action)
        {
            return _requestedActions.Contains(action);
        }

        private void Update()
        {
            CaptureInput();
        }

        private void CaptureInput()
        {
            if (Input.GetButtonUp(JumpButton))
            {
                _requestedActions.Remove(InputAction.Jump);
            }

            if (Input.GetButtonDown(JumpButton))
            {
                _requestedActions.Add(InputAction.Jump);
            }

        }
    }
}
