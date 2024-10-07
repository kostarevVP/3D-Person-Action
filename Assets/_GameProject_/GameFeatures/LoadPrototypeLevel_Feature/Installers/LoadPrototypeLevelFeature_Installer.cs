using UnityEngine;
using WKosArch.Extensions;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.UI_Feature;
using WKosArch.DependencyInjection;
using WKosArch.SceneManagement_Feature;

namespace WKosArch.Features.LoadLevelFeature
{
    [CreateAssetMenu(fileName = "LoadPrototypeLevelFeature_Installer", menuName = "Game/Installers/LoadPrototypeLevelFeature_Installer")]
    public class LoadPrototypeLevelFeature_Installer : FeatureInstaller
    {
        ILoadPrototypeLevelFeature _loadLevelFeature;
        public override IFeature Create(IDIContainer container) 
        {
            ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();
            IUserInterfaceFeature ui = container.Resolve<IUserInterfaceFeature>();

            _loadLevelFeature = new LoadPrototypeLevelFeature(sceneManagementService, ui);

            RegisterFeatureAsSingleton(container, _loadLevelFeature);

            return _loadLevelFeature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDIContainer container, ILoadPrototypeLevelFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[LoadPrototypeLevelFeature_Installer - ILoadPrototypeLevelFeature] Create and RegisterSingleton", Color.cyan);
        }
    }
}
