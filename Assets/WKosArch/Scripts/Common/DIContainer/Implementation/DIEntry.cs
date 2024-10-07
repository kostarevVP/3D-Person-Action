using System;

namespace WKosArch.DependencyInjection
{
    public abstract class DIEntry : IDIEntry
    {
        protected IDIContainer Container { get; }
        public bool IsSingleton { get; set; }

        protected DIEntry() { }

        protected DIEntry(IDIContainer container)
        {
            Container = container;
        }

        public abstract void Dispose();
    }

    public class DIEntry<T> : DIEntry, IDIEntry<T>
    {
        private readonly Func<IDIContainer, T> _factory;
        private T _instance;
        private IDisposable _disposableInstance;

        public DIEntry(IDIContainer container, Func<IDIContainer, T> factory) : base(container)
        {
            _factory = factory;
        }

        public DIEntry(T instance)
        {
            _instance = instance;

            if (_instance is IDisposable disposableInstance)
            {
                _disposableInstance = disposableInstance;
            }

            IsSingleton = true;
        }

        public T Resolve()
        {
            if (IsSingleton)
            {
                if (_instance == null)
                {
                    _instance = _factory(Container);

                    if (_instance is IDisposable disposableInstance)
                    {
                        _disposableInstance = disposableInstance;
                    }
                }

                return _instance;
            }

            return _factory(Container);
        }

        public override void Dispose()
        {
            _disposableInstance?.Dispose();
        }
    }
}
