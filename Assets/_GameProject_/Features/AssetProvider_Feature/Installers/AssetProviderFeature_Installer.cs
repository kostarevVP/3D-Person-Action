using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using UnityEngine;
using WKosArch.DependencyInjection;

namespace WKosArch.Services.AssetProviderService
{
    [CreateAssetMenu(fileName = "AssetProviderFeature_Installer", menuName = "Game/Installers/AssetProviderFeature_Installer")]
    public class AssetProviderFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDiContainer container)
        {
            IAssetProviderFeature _feature = new AssetProviderFeature();

            RegisterFeatureAsSingleton(container, _feature);
            return _feature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, IAssetProviderFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[IAssetProviderFeature] Create and RegesterSingleton", Color.cyan);
        }
    } 
}
