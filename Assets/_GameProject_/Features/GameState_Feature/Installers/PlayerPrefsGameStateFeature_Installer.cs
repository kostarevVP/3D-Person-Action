using UnityEngine;
using WKosArch.DependencyInjection;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.Configs_Feature;
using WKosArch.SceneManagement_Feature;


namespace WKosArch.GameState_Feature
{
    [CreateAssetMenu(fileName = " PlayerPrefsGameStateFeature_Installer", menuName = "Game/Installers/PlayerPrefsGameStateFeature_Installer")]
    public class PlayerPrefsGameStateFeature_Installer : FeatureInstaller
    {
        private IGameStateProviderFeature _gameStateProviderFeature;
        public override IFeature Create(IDIContainer container)
        {
            ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();
            IConfigsFeature configsFeature = container.Resolve<IConfigsFeature>();

            _gameStateProviderFeature = new PlayerPrefsGameStateProviderFeature(configsFeature);

            BindAsSingle(container, _gameStateProviderFeature);

            var observerGameState = _gameStateProviderFeature.LoadGameState();
            _gameStateProviderFeature.LoadGameSettingsState();

            sceneManagementService.LoadScene(_gameStateProviderFeature.GameState.SceneLoadingProxy.Value.SceneIndexToLoad.Value);

            return _gameStateProviderFeature;
        }

        public override void Dispose()
        {
            _gameStateProviderFeature.SaveGameState();
            _gameStateProviderFeature.SaveGameSettingsState();
        }

        private void BindAsSingle(IDIContainer container, IGameStateProviderFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[PlayerPrefsGameStateProvider - IGameStateProviderFeature] Create and Bind as Single", Color.cyan);
        }
    } 
}
