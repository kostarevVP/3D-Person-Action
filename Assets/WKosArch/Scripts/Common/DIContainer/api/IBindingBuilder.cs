using System;

namespace WKosArch.DependencyInjection
{
    public interface IBindingBuilder<T>
    {
        IBindingBuilder<T> WithId(string tag);
        IBindingBuilder<T> FromMethod(Func<IDIContainer, T> factory);
        IBindingBuilder<T> FromInstance(T instance);
        void AsTransient();
        void AsSingle();
    }
}