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
        public override IFeature Create(IDiContainer container)
        {
            IAnalyticsFeature feature = new LogAnalyticsFeature();

            RegisterFeatureAsSingleton(container, feature);

            return feature;
        }
        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, IAnalyticsFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[LogAnalyticsFeature - IAnalyticService] Create and RegisterSingleton", Color.cyan);
        }
    }
}