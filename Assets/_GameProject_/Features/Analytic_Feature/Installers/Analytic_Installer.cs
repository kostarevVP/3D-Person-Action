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
        public override IFeature Create(IDIContainer container)
        {
            IAnalyticsFeature feature = new LogAnalyticsFeature();

            BindAsSingle(container, feature);

            return feature;
        }
        public override void Dispose() { }

        private void BindAsSingle(IDIContainer container, IAnalyticsFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[LogAnalyticsFeature - IAnalyticService] Create and Bind as Single", Color.cyan);
        }
    }
}