using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.DependencyInjection;
using WKosArch.AssetProvider_Feature;

namespace WKosArch.Configs_Feature
{
    [CreateAssetMenu(fileName = "ConfigsFeature_Installer", menuName = "Game/Installers/ConfigsFeature_Installer")]
    public class ConfigsFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDIContainer container)
        {
            IAssetProviderFeature assetProviderService = container.Resolve<IAssetProviderFeature>();

            IConfigsFeature feature = new ConfigsFeature(assetProviderService);

            BindAsSingle(container, feature);

            return feature;
        }

        public override void Dispose() { }

        private void BindAsSingle(IDIContainer container, IConfigsFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[ConfigsFeature - IConfigsFeature] Create and Bind as Single", Color.cyan);
        }
    } 
}
