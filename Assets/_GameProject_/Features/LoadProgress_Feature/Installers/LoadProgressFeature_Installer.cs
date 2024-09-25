using Assets.Game.Services.ProgressService.api;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using WKosArch.Services.Scenes;
using UnityEngine;
using WKosArch.Services.StaticDataServices;
using WKosArch.DependencyInjection;

namespace WKosArch.Features.LoadProgressFeature
{
    [CreateAssetMenu(fileName = "LoadProgressFeature_Installer", menuName = "Game/Installers/LoadProgressFeature_Installer")]
    public class LoadProgressFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDiContainer container)
        {
            IProgressFeature progressService = container.Resolve<IProgressFeature>();
            ISaveLoadFeature saveLoadService = container.Resolve<ISaveLoadFeature>();
            IConfigDataFeature configDataService = container.Resolve<IConfigDataFeature>();
            ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();

            ILoadProgressFeature feature = new LoadProgressFeature(progressService, saveLoadService, configDataService);

            feature.LoadProgressOrInitNew();

            RegisterFeatureAsSingleton(container, feature);

            sceneManagementService.LoadScene(progressService.GameProgressData.SavedSceneIndex);

            return feature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, ILoadProgressFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[ILoadProgressFeature] Create and RegesterSingleton", Color.cyan);
        }
    } 
}
