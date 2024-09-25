using System;

namespace WKosArch.DependencyInjection
{
    public interface IDiContainer : IDisposable
    {
        void AddChildContainer(IDiContainer container);
        DIBuilder<T> Register<T>(Func<DiContainer, T> factory);
        DIBuilder<T> Register<T>(string tag, Func<DiContainer, T> factory);
        DIBuilder<T> RegisterSingleton<T>(Func<DiContainer, T> factory);
        DIBuilder<T> RegisterSingleton<T>(string tag, Func<DiContainer, T> factory);
        T Resolve<T>(string tag = "");
        T ResolveFromCurrent<T>(string tag = "");
    }
}