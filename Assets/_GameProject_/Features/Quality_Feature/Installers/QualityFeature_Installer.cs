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

        public override IFeature Create(IDIContainer container)
        {
            var _configsFeature = container.Resolve<IConfigsFeature>();

            IQualityFeature qualityFeature = new QualityFeature(_configsFeature.RenderQualityConfigMap);

            BindAsSingle(container, qualityFeature);

            qualityFeature.SetFPSLimit(_targetFPS);

            return qualityFeature;
        }

        public override void Dispose() { }

        private void BindAsSingle(IDIContainer container, IQualityFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[QualityFeature - IQualityFeature] Create and Bind as Single", Color.cyan);
        }
    } 
}
