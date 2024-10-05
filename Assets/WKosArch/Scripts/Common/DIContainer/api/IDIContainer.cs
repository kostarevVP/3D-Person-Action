using System;

namespace WKosArch.DependencyInjection
{
    public interface IDIContainer : IDisposable
    {
        IBindingBuilder<T> Bind<T>();
        IBindingBuilder<T> Bind<T>(T instance);
        T Resolve<T>(string tag = null);
    }

}
