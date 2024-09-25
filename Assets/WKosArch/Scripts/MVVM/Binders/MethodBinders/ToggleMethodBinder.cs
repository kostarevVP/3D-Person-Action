using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace WKosArch.MVVM.Binders
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleMethodBinder : BoolMethodBinder
    {
        [SerializeField] private Toggle _toggle;

        private IViewModel _viewModel;
        private MethodInfo _cachedMethod;

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(bool toggleState)
        {
            _cachedMethod?.Invoke(_viewModel, new object[] { toggleState });
        }

        protected override IDisposable BindInternal(IViewModel viewModel)
        {
            _viewModel = viewModel;
            _cachedMethod = viewModel.GetType().GetMethod(MethodName);

            return base.BindInternal(viewModel);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_toggle == null)
            {
                _toggle = GetComponent<Toggle>();
            }
        }
#endif
    }
}