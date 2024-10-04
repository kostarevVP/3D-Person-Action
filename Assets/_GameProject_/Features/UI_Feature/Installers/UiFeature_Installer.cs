using System.Collections.Generic;
using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.DependencyInjection;
using WKosArch.Configs_Feature;
using WKosArch.SceneManagement_Feature;

namespace WKosArch.UI_Feature
{
    [CreateAssetMenu(fileName = "IUserInterfaceFeature", menuName = "Game/Installers/IUserInterfaceFeature")]
    public class UiFeature_Installer : FeatureInstaller
    {
        IUserInterfaceFeature _uiFeature;

        ISceneManagementFeature _sceneManagementFeature;
        IConfigsFeature _configsFeature;

        public override IFeature Create(IDiContainer container)
        {
            _sceneManagementFeature = container.Resolve<ISceneManagementFeature>();
            _configsFeature = container.Resolve<IConfigsFeature>();

            _uiFeature = new UserInterfaceFeature(container);

            RegisterFeatureAsSingleton(container, _uiFeature);

            Subscribe();

            return _uiFeature;
        }

        public override void Dispose() =>
            Unsubscribe();

        private void Subscribe() =>
            _sceneManagementFeature.OnSceneLoaded += SceneLoaded;

        private void Unsubscribe() =>
            _sceneManagementFeature.OnSceneLoaded -= SceneLoaded;

        private void RegisterFeatureAsSingleton(IDiContainer container, IUserInterfaceFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[UserInterfaceFeature - IUserInterfaceFeature] Create and RegisterSingleton", Color.cyan);
        }

        /// <summary>
        ///     SubScene from DOTS after open get same callBack SceneLoaded
        /// That why there is TryGet before was like below
        /// var config = _staticDataService.SceneConfigsMap[sceneName];
        /// </summary>
        /// <param name="sceneName"></param>
        private void SceneLoaded(string sceneName)
        {
            if (_configsFeature.SceneUiConfigMap.TryGetValue(sceneName, out Dictionary<string, MVVM.View> configMap))
            {
                _uiFeature.Build(configMap);
            }
            else
            {
                Log.PrintWarning($"Try to load for {sceneName} SceneConfig {configMap} in SceneConfigsMap result false");
            }
        }
    }
}