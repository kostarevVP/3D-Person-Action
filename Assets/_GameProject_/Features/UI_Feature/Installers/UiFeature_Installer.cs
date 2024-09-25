using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Services.Scenes;
using WKosArch.Services.StaticDataServices;
using UnityEngine;
using WKosArch.Extentions;
using WKosArch.DependencyInjection;

namespace WKosArch.Services.UIService

{
    [CreateAssetMenu(fileName = "UiFeature_Installer", menuName = "Game/Installers/UiFeature_Installer")]
    public class UiFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDiContainer container)
        {
            ISceneManagementFeature sceneMenegmentService = container.Resolve<ISceneManagementFeature>();
            IConfigDataFeature configDataService = container.Resolve<IConfigDataFeature>();

            IUiFeature feature = new UiFeature(configDataService, sceneMenegmentService, container);

            RegisterFeatureAsSingleton(container, feature);

            container.RegisterSingleton(_ => feature.UI);
            Log.PrintColor($"[UI] Create and RegesterSingleton", Color.cyan);

            return feature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, IUiFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[IUiFeature] Create and RegesterSingleton", Color.cyan);
        }
    }
}