using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;

namespace WKosArch.MVVM.Binders
{
    public abstract class UnityEventBinder<T> : ObservableBinder<T>
    {
        [SerializeField]
        protected UnityEvent<T> @event;

        protected override void OnPropertyChanged(T newValue)
        {
            @event.Invoke(newValue);
        }
    }

    public abstract class ConvertibleUnityEventerBinder<T1, T2> : ObservableBinder<T1>
    {
        [SerializeField]
        private UnityEvent<T2> _event;

        protected override void OnPropertyChanged(T1 newValue)
        {
            _event.Invoke(ConvertValue(newValue));
        }

        protected abstract T2 ConvertValue(T1 newValue);
    }


}
