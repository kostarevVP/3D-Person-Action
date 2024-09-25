using System;

namespace WKosArch.MVVM.Binders
{
    public class ReactivePropertyBinder<T> : GenericPropertyBinder
    {
        public override Type ParameterType => typeof(T);

        private event Action<T> _action;

        protected override IDisposable BindInternal(IViewModel viewModel)
        {
            _action = (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), viewModel, PropertyName);

            return null;
        }

        public void Perform(T value)
        {
            _action?.Invoke(value);
        }
    }
}