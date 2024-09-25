using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using WKosArch.Services.Scenes;
using WKosArch.Services.UIService;
using WKosArch.Services.UIService.UI;
using WKosArch.DependencyInjection;

namespace WKosArch.Features.LoadLevelFeature
{
    [CreateAssetMenu(fileName = "LoadLevelFeature_Installer", menuName = "Game/Installers/LoadLevelFeature_Installer")]
    public class LoadLevelFeature_Installer : FeatureInstaller
    {
        ILoadLevelFeature _loadLevelFeature;
        public override IFeature Create(IDiContainer container)
        {
            ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();
            IUserInterface ui = container.Resolve<IUiFeature>().UI;
            ISaveLoadHandlerService saveLoadHandler = container.Resolve<ISaveLoadHandlerService>();

            _loadLevelFeature = new LoadLevelFeature(sceneManagementService,
                ui, saveLoadHandler);

            RegisterFeatureAsSingleton(container, _loadLevelFeature);

            return _loadLevelFeature;
        }

        public override void Dispose() 
        {
            _loadLevelFeature?.Dispose();
        }

        private void RegisterFeatureAsSingleton(IDiContainer container, ILoadLevelFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[ILoadLevelFeature] Create and RegesterSingleton", Color.cyan);
        }
    }
}
