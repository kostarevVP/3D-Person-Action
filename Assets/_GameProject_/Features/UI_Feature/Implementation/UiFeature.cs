using WKosArch.DependencyInjection;
using WKosArch.Extentions;
using WKosArch.Services.Scenes;
using WKosArch.Services.StaticDataServices;
using WKosArch.Services.UIService.UI;

namespace WKosArch.Services.UIService
{
    public class UiFeature : IUiFeature
    {
        private readonly ISceneManagementFeature _sceneManagementService;
        private readonly IConfigDataFeature _configDataService;

        public IUserInterface UI { get; private set; }

        public UiFeature(IConfigDataFeature configDataService, ISceneManagementFeature sceneManagementService,
            IDiContainer container)
        {
            _configDataService = configDataService;
            _sceneManagementService = sceneManagementService;

            UI = new UserInterface(container);

            _sceneManagementService.OnSceneLoadingStarted += SceneLoaded;
        }

        public void Dispose()
        {
            UI.Dispose();
            _sceneManagementService.OnSceneLoadingStarted -= SceneLoaded;
        }

        private void SceneLoaded(string sceneName)
        {
            //SubScene from DOTS after open get same callBack SceneLoaded
            //Thats why there is TryGet before was like below
            //var config = _staticDataService.SceneConfigsMap[sceneName];
            if (_configDataService.SceneConfigsMap.TryGetValue(sceneName, out var config))
            {
                UI.Build(config);
            }
            else
            {
                Log.PrintWarning($"Try to load for {sceneName} SceneConfig {config} in SceneConfigsMap result false");
            }
        }
    }
}