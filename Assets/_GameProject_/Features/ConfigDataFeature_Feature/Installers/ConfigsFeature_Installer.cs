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
        public override IFeature Create(IDiContainer container)
        {
            IAssetProviderFeature assetProviderService = container.Resolve<IAssetProviderFeature>();

            IConfigsFeature feature = new ConfigsFeature(assetProviderService);

            RegisterFeatureAsSingleton(container, feature);

            return feature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, IConfigsFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[ConfigsFeature - IConfigsFeature] Create and RegisterSingleton", Color.cyan);
        }
    } 
}
