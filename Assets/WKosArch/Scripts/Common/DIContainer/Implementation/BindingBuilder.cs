using System;

namespace WKosArch.DependencyInjection
{
    public class BindingBuilder<T> : IBindingBuilder<T>
    {
        private readonly DIContainer _container;
        private string _tag;
        private Func<IDIContainer, T> _factory;
        private T _instance;
        private bool _isSingleton;
        private bool _isInstanceBinding;

        public BindingBuilder(DIContainer container)
        {
            _container = container;
        }

        public BindingBuilder(DIContainer container, T instance)
        {
            _container = container;
            _instance = instance;
            _isInstanceBinding = true;
        }

        public IBindingBuilder<T> WithId(string tag)
        {
            _tag = tag;
            return this;
        }

        public IBindingBuilder<T> FromMethod(Func<IDIContainer, T> factory)
        {
            _factory = factory;
            return this;
        }

        public IBindingBuilder<T> FromInstance(T instance)
        {
            _instance = instance;
            _isInstanceBinding = true;
            return this;
        }

        public void AsTransient()
        {
            _isSingleton = false;
            Register();
        }

        public void AsSingle()
        {
            _isSingleton = true;
            Register();
        }

        private void Register()
        {
            var key = (_tag, typeof(T));

            if (_container._entriesMap.ContainsKey(key))
            {
                throw new Exception(
                    $"DI: Binding with tag {_tag} and type {typeof(T).FullName} has already been registered");
            }

            IDIEntry<T> diEntry;

            if (_isInstanceBinding)
            {
                diEntry = new DIEntry<T>(_instance)
                {
                    IsSingleton = true 
                };
            }
            else
            {
                if (_factory == null)
                {
                    throw new Exception("Factory method must be provided for FromMethod bindings");
                }

                diEntry = new DIEntry<T>(_container, _factory)
                {
                    IsSingleton = _isSingleton
                };
            }

            _container._entriesMap[key] = diEntry;
        }
    }
}