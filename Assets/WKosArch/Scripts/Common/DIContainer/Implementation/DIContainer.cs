using System;
using System.Linq;
using System.Collections.Generic;

namespace WKosArch.DependencyInjection
{
    public class DIContainer : IDIContainer
    {
        internal readonly IDIContainer _parentContainer;
        internal readonly Dictionary<(string, Type), IDIEntry> _entriesMap = new();
        private readonly HashSet<(string, Type)> _resolutionsCache = new();

        public DIContainer(IDIContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
        }

        public IBindingBuilder<T> Bind<T>()
        {
            return new BindingBuilder<T>(this);
        }

        public IBindingBuilder<T> Bind<T>(T instance)
        {
            return new BindingBuilder<T>(this, instance);
        }

        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));

            if (_resolutionsCache.Contains(key))
            {
                throw new Exception($"DI: Cyclic dependency for tag {key.Item1} and type {key.Item2.FullName}");
            }

            _resolutionsCache.Add(key);

            try
            {
                if (_entriesMap.TryGetValue(key, out var diEntry))
                {
                    return ((IDIEntry<T>)diEntry).Resolve();
                }

                if (_parentContainer != null)
                {
                    return _parentContainer.Resolve<T>(tag);
                }
            }
            finally
            {
                _resolutionsCache.Remove(key);
            }

            throw new Exception($"Couldn't find dependency for tag {tag} and type {key.Item2.FullName}");
        }

        public void Dispose()
        {
            var disposable = _entriesMap.Values.ToList();
            disposable.Reverse();
            foreach (var entry in disposable)
            {
                entry.Dispose();
            }
        }
    }

}
