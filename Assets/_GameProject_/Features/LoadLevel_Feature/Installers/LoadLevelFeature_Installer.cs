using UnityEngine;
using WKosArch.Extensions;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.UI_Feature;
using WKosArch.DependencyInjection;
using WKosArch.SceneManagement_Feature;

namespace WKosArch.Features.LoadLevelFeature
{
    [CreateAssetMenu(fileName = "LoadLevelFeature_Installer", menuName = "Game/Installers/LoadLevelFeature_Installer")]
    public class LoadLevelFeature_Installer : FeatureInstaller
    {
        ILoadLevelFeature _loadLevelFeature;
        public override IFeature Create(DIContainer container) 
        {
            ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();
            IUserInterfaceFeature ui = container.Resolve<IUserInterfaceFeature>();

            _loadLevelFeature = new LoadLevelFeature(sceneManagementService, ui);

            RegisterFeatureAsSingleton(container, _loadLevelFeature);

            return _loadLevelFeature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(DIContainer container, ILoadLevelFeature feature)
        {
            container.RegisterFactory(_ => feature).AsSingle();
            Log.PrintColor($"[ILoadLevelFeature] Create and RegisterSingleton", Color.cyan);
        }
    }
}
