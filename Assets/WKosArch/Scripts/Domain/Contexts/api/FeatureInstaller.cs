using System;
using WKosArch.Domain.Features;
using UnityEngine;
using WKosArch.DependencyInjection;

namespace WKosArch.Domain.Contexts
{
    public abstract class FeatureInstaller : ScriptableObject, IFeatureInstaller, IDisposable
    {
        public abstract IFeature Create(DIContainer container);
        public abstract void Dispose();
    }
}