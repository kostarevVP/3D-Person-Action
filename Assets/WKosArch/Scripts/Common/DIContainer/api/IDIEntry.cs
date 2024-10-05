using System;

namespace WKosArch.DependencyInjection
{
    public interface IDIEntry : IDisposable { }

    public interface IDIEntry<T> : IDIEntry
    {
        T Resolve();
        bool IsSingleton { get; set; }
    }
}
