using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.DependencyInjection;

namespace WKosArch.Analytics_Feature
{
    [CreateAssetMenu(fileName = "Analytic_Installer", menuName = "Game/Installers/Analytic_Installer")]
    public class Analytic_Installer : FeatureInstaller
    {
        public override IFeature Create(DIContainer container)
        {
            IAnalyticsFeature feature = new LogAnalyticsFeature();

            RegisterFeatureAsSingleton(container, feature);

            return feature;
        }
        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(DIContainer container, IAnalyticsFeature feature)
        {
            container.RegisterFactory(_ => feature).AsSingle();
            Log.PrintColor($"[LogAnalyticsFeature - IAnalyticService] Create and RegisterSingleton", Color.cyan);
        }
    }
}