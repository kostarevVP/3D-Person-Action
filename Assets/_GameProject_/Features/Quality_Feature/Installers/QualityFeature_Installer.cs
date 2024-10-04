using WKosArch.DependencyInjection;
using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.Configs_Feature;

namespace WKosArch.Quality_Feature
{
    [CreateAssetMenu(fileName = "QualityFeature_Installer", menuName = "Game/Installers/QualityFeature_Installer")]

    public class QualityFeature_Installer : FeatureInstaller
    {
        [SerializeField] private int _targetFPS = 60;

        public override IFeature Create(IDiContainer container)
        {
            var _configsFeature = container.Resolve<IConfigsFeature>();

            IQualityFeature qualityFeature = new QualityFeature(_configsFeature.RenderQualityConfigMap);

            RegisterFeatureAsSingleton(container, qualityFeature);

            qualityFeature.SetFPSLimit(_targetFPS);

            return qualityFeature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, IQualityFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[QualityFeature - IQualityFeature] Create and RegisterSingleton", Color.cyan);
        }
    } 
}
