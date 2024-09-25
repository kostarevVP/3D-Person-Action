using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using WKosArch.Extentions;
using WKosArch.Services.UIService;

namespace WKosArch.Services.StaticDataServices
{
    public class ConfigDataFeature : IConfigDataFeature
    {
        private const string GameProgressPath = "NewGameProgressData";
        private const string UISceneConfigsFolderPath = "SceneConfigs";
        private const string QualitySettingsPath = "URPRenderersConfig";

        public GameProgressConfig GameProgressConfig => _gameProgressStaticData;
        public Dictionary<string, UISceneConfig> SceneConfigsMap => _sceneConfigsMap;
        public Dictionary<RenderingQuality, RenderPipelineAsset> RenderQualityConfigMap => _renderQualityConfigMap;


        private IAssetProviderFeature _assetProviderService;

        private GameProgressConfig _gameProgressStaticData;
        private Dictionary<string, UISceneConfig> _sceneConfigsMap = new();
        private Dictionary<RenderingQuality, RenderPipelineAsset> _renderQualityConfigMap = new();



        public ConfigDataFeature(IAssetProviderFeature assetProviderService)
        {
            _assetProviderService = assetProviderService;

            LoadGameProgressConfig();
            LoadSceneConfigs();
            LoadQualityConfigs();
        }

        public void Dispose() =>
            Clear();

        private void LoadGameProgressConfig()
        {
            _gameProgressStaticData = _assetProviderService.Load<GameProgressConfig>(GameProgressPath);
        }

        private void LoadSceneConfigs()
        {
            var scenesConfigs = _assetProviderService.LoadAll<UISceneConfig>(UISceneConfigsFolderPath);

            foreach (var sceneConfigs in scenesConfigs)
            {
                sceneConfigs.InitilizationMapps();
                foreach (var scene in sceneConfigs.SceneName)
                {
                    if (!_sceneConfigsMap.ContainsKey(scene))
                        _sceneConfigsMap[scene] = sceneConfigs;
                    else
                        Log.PrintError($"You try add Scene WindowConfigs that is have another WindowConfig");
                }
            }
        }

        private void LoadQualityConfigs()
        {
            var qualityConfig = _assetProviderService.Load<URPRenderersConfig>(QualitySettingsPath);
            qualityConfig.Init();
            _renderQualityConfigMap = qualityConfig.RendererQualityMap;
        }

        private void Clear()
        {
            _gameProgressStaticData = null;
            _sceneConfigsMap.Clear();
            _renderQualityConfigMap.Clear();
        }
    }
}
