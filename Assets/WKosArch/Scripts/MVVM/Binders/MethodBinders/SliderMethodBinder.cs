using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace WKosArch.MVVM.Binders
{
    [RequireComponent(typeof(Slider))]
    public class SliderMethodBinder: FloatMethodBinder
    {
        [SerializeField] private Slider _slider;

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
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _cachedMethod?.Invoke(_viewModel, new object[] { value });
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
            if (_slider == null)
            {
                _slider = GetComponent<Slider>();
            }
        }
#endif
    }
}