using UnityEngine;
using UnityEngine.InputSystem;

namespace Input_Feature
{
    public class InputHandler : MonoBehaviour
    {
        private const string PrefabPath = "[InputHandler]";

        private IInputFeature _inputFeature;

        public static InputHandler CreateInstance()
        {
            var prefab = Resources.Load<InputHandler>(PrefabPath);
            var inputHandler = Instantiate(prefab);

            DontDestroyOnLoad(inputHandler.gameObject);

            return inputHandler;
        }

        public void Inject(IInputFeature inputFeature)
        {
            _inputFeature = inputFeature;
        }

        public void OnAttack(InputValue inputValue)
        {
            _inputFeature.OnAttak(inputValue.isPressed);
        }

        public void OnCrouch(InputValue inputValue)
        {
            _inputFeature.OnCrouch(inputValue.isPressed);
        }

        public void OnInteract(InputValue inputValue)
        {
            _inputFeature.OnInteract(inputValue.Get<bool>());
        }

        public void OnJump(InputValue inputValue)
        {
            _inputFeature.OnJump(inputValue.isPressed);
        }

        public void OnLook(InputValue inputValue)
        {
            _inputFeature.OnLook(inputValue.Get<Vector2>());
        }

        public void OnMove(InputValue inputValue)
        {
            _inputFeature.OnMove(inputValue.Get<Vector2>());
        }

        public void OnNext(InputValue inputValue)
        {
            _inputFeature.OnNext(inputValue.isPressed);
        }

        public void OnPause(InputValue inputValue)
        {
            _inputFeature.OnPause(inputValue.isPressed);
        }

        public void OnPrevious(InputValue inputValue)
        {
            _inputFeature.OnPrevious(inputValue.isPressed);
        }

        public void OnSprint(InputValue inputValue)
        {
            _inputFeature.OnSprint(inputValue.isPressed);
        }
    }
}
