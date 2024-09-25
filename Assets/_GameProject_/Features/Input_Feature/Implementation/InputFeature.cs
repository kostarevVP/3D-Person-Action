using System;
using UnityEngine;

namespace Input_Feature
{
    public class InputFeature : IInputFeature
    {
        public event Action<Vector2> OnMoveVectorEvent;
        public event Action<Vector2> OnLookVectorEvent;
        public event Action<bool> OnAttakEvent;
        public event Action<bool> OnInteractEvent;
        public event Action<bool> OnCrouchEvent;
        public event Action<bool> OnJumpEvent;
        public event Action<bool> OnSprintEvent;
        public event Action<bool> OnPauseEvent;
        public event Action<bool> OnNextEvent;
        public event Action<bool> OnPreviousEvent;

        public void OnAttak(bool value)
        {
            OnAttakEvent?.Invoke(value);
        }

        public void OnCrouch(bool value)
        {
           OnCrouchEvent?.Invoke(value);
        }

        public void OnInteract(bool value)
        {
            OnInteractEvent?.Invoke(value);
        }

        public void OnJump(bool value)
        {
            OnJumpEvent?.Invoke(value);
        }

        public void OnLook(Vector2 vector)
        {
           OnLookVectorEvent?.Invoke(vector);
        }

        public void OnMove(Vector2 vector)
        {
            OnMoveVectorEvent?.Invoke(vector);
        }

        public void OnNext(bool value)
        {
           OnNextEvent?.Invoke(value);
        }

        public void OnPause(bool value)
        {
            OnPauseEvent?.Invoke(value);
        }

        public void OnPrevious(bool value)
        {
           OnPreviousEvent?.Invoke(value);
        }

        public void OnSprint(bool value)
        {
            OnSprintEvent?.Invoke(value);
        }
    }
}
