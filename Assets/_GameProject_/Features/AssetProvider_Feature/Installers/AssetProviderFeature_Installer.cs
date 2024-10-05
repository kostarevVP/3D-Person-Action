using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.DependencyInjection;
using WKosArch.Extensions;

namespace WKosArch.AssetProvider_Feature
{
    [CreateAssetMenu(fileName = "AssetProviderFeature_Installer", menuName = "Game/Installers/AssetProviderFeature_Installer")]
    public class AssetProviderFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDIContainer container)
        {
            IAssetProviderFeature _feature = new AssetProviderFeature();

            BindAsSingle(container, _feature);
            return _feature;
        }

        public override void Dispose() { }

        private void BindAsSingle(IDIContainer container, IAssetProviderFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[AssetProviderFeature - IAssetProviderFeature] Create and Bind as Single", Color.cyan);
        }
    } 
}
