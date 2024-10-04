using System;
using System.Collections.Generic;
using System.Linq;

namespace WKosArch.DependencyInjection
{
    //public class DiContainer : IDiContainer
    //{
    //    private readonly Dictionary<(string, Type), DIEntry> _factoriesMap = new();
    //    private readonly HashSet<(string, Type)> _cachedKeysForResolving = new();

    //    private readonly IDiContainer _parentDiContainer;
    //    private readonly List<IDiContainer> _childContainers = new();



    //    public DiContainer(IDiContainer parentDiContainer = null)
    //    {
    //        _parentDiContainer = parentDiContainer;

    //        if(_parentDiContainer != null)
    //            _parentDiContainer.AddChildContainer(this);
    //    }

    //    public DIBuilder<T> RegisterSingleton<T>(Func<DiContainer, T> factory)
    //    {
    //        return RegisterSingleton("", factory);
    //    }

    //    public DIBuilder<T> RegisterSingleton<T>(string tag, Func<DiContainer, T> factory)
    //    {
    //        var key = (tag, typeof(T));

    //        return RegisterSingleton(key, factory);
    //    }

    //    public DIBuilder<T> Register<T>(Func<DiContainer, T> factory)
    //    {
    //        return Register("", factory);
    //    }

    //    public DIBuilder<T> Register<T>(string tag, Func<DiContainer, T> factory)
    //    {
    //        var key = (tag, typeof(T));

    //        return Register(key, factory);
    //    }


    //    public T Resolve<T>(string tag = "")
    //    {
    //        var type = typeof(T);
    //        var key = (tag, type);

    //        if (_cachedKeysForResolving.Contains(key))
    //        {
    //            throw new Exception($"Cyclic dependencies. Key: {key}");
    //        }

    //        _cachedKeysForResolving.Add(key);

    //        List<IDiContainer> registeredContainers = new List<IDiContainer>();

    //        // Check the current container
    //        if (_factoriesMap.ContainsKey(key))
    //        {
    //            registeredContainers.Add(this);
    //        }

    //        // Check the parent container
    //        if (_parentDiContainer is DiContainer parentContainer)
    //        {
    //            try
    //            {
    //                parentContainer.Resolve<T>(tag);
    //                registeredContainers.Add(parentContainer);
    //            }
    //            catch {/*Ignore exception and continue*/}
    //        }

    //        // Check child containers
    //        foreach (var childContainer in _childContainers)
    //        {
    //            try
    //            {
    //                childContainer.Resolve<T>(tag);
    //                registeredContainers.Add(childContainer);
    //            }
    //            catch {/*Ignore exception and continue*/}
    //        }

    //        // If no containers have the key, raise an error
    //        if (registeredContainers.Count == 0)
    //        {
    //            _cachedKeysForResolving.Remove(key);
    //            throw new Exception($"There is no factory registered for key: {key}");
    //        }

    //        // If more than one container has the key, raise an error
    //        if (registeredContainers.Count > 1)
    //        {
    //            _cachedKeysForResolving.Remove(key);
    //            throw new Exception($"Multiple factories registered for key: {key}");
    //        }

    //        // Resolve from the appropriate container
    //        var result = registeredContainers.First().ResolveFromCurrent<T>();

    //        _cachedKeysForResolving.Remove(key);
    //        return result;
    //    }

    //    public T ResolveFromCurrent<T>(string tag = "")
    //    {
    //        var type = typeof(T);
    //        var key = (tag, type);

    //        if (_factoriesMap.TryGetValue(key, out DIEntry dIEntry))
    //        {
    //            return dIEntry.Resolve<T>();
    //        }

    //        throw new Exception($"No factory registered for key: {key}");
    //    }

    //    private DIBuilder<T> RegisterSingleton<T>((string, Type) key, Func<DiContainer, T> factory)
    //    {
    //        if (_factoriesMap.ContainsKey(key))
    //        {
    //            throw new Exception("Already has factory entry for key: " + key);
    //        }

    //        var diEntry = new DIEntrySingleton<T>(this, factory);

    //        _factoriesMap[key] = diEntry;

    //        return new DIBuilder<T>(diEntry);
    //    }

    //    private DIBuilder<T> Register<T>((string, Type) key, Func<DiContainer, T> factory)
    //    {
    //        if (_factoriesMap.ContainsKey(key))
    //        {
    //            throw new Exception("Already has factory entry for type: " + key.Item2.Name);
    //        }

    //        var diEntry = new DIEntryTransient<T>(this, factory);

    //        _factoriesMap[key] = diEntry;

    //        return new DIBuilder<T>(diEntry);
    //    }

    //    public void AddChildContainer(IDiContainer container)
    //    {
    //        _childContainers.Add(container);
    //    }

    //    public void Dispose()
    //    {
    //        _factoriesMap.Clear();
    //        _cachedKeysForResolving.Clear();
    //        _childContainers.Clear();
    //    }
    //}
}