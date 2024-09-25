using System;
using UnityEngine;
using WKosArch.Domain.Features;

namespace Input_Feature
{
    public interface IInputFeature : IFeature
    {
        event Action<Vector2> OnMoveVectorEvent;
        event Action<Vector2> OnLookVectorEvent;
        event Action<bool> OnAttakEvent;
        event Action<bool> OnInteractEvent;
        event Action<bool> OnCrouchEvent;
        event Action<bool> OnJumpEvent;
        event Action<bool> OnSprintEvent;

        event Action<bool> OnPauseEvent;
        event Action<bool> OnNextEvent;
        event Action<bool> OnPreviousEvent;

        void OnMove(Vector2 vector);
        void OnLook(Vector2 vector);
        void OnAttak(bool value);
        void OnInteract(bool value);
        void OnCrouch(bool value);
        void OnJump(bool value);
        void OnSprint(bool value);

        void OnPause(bool value);
        void OnNext(bool value);
        void OnPrevious(bool value);
    }
}