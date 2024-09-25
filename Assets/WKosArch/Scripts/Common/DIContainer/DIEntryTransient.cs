using System;

namespace WKosArch.DependencyInjection
{
    public sealed class DIEntryTransient<T>: DIEntry<T>
    {
        public DIEntryTransient(DiContainer diContainer, Func<DiContainer, T> factory) : base(diContainer, factory) { }
        
        public override T Resolve()
        {
            return Factory(DiContainer);
        }
    }
}