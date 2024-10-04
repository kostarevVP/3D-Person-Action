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
        public override IFeature Create(IDiContainer container)
        {
            ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();
            IConfigsFeature configsFeature = container.Resolve<IConfigsFeature>();

            _gameStateProviderFeature = new PlayerPrefsGameStateProviderFeature(configsFeature);

            RegisterFeatureAsSingleton(container, _gameStateProviderFeature);

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

        private void RegisterFeatureAsSingleton(IDiContainer container, IGameStateProviderFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[PlayerPrefsGameStateProvider - IGameStateProviderFeature] Create and RegisterSingleton", Color.cyan);
        }
    } 
}
