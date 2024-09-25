using System;
#if USE_R3
using R3;
#endif

namespace WKosArch.MVVM.Binders
{
    public abstract class ObservableBinder : Binder
    {
        public abstract Type ArgumentType { get; }
    }

    public abstract class ObservableBinder<T> : ObservableBinder
    {
        public override Type ArgumentType => typeof(T);

        protected sealed override IDisposable BindInternal(IViewModel viewModel)
        {
            var property = viewModel.GetType().GetProperty(PropertyName);
            var propertyValue = property.GetValue(viewModel);

            IDisposable subscription;

#if USE_R3
            if (propertyValue is Observable<T> r3Observable)
            {
                subscription = r3Observable.Subscribe(OnPropertyChanged);
            }
            else
#endif
                    if (propertyValue is IObservable<T> systemObservable)
            {
                subscription = systemObservable.Subscribe(OnPropertyChanged);
            }
            else
            {
                throw new InvalidOperationException($"Property {PropertyName} is neither Observable<T> nor IObservable<T>.");
            }

            return subscription;
        }


        protected abstract void OnPropertyChanged(T newValue);
    }
}