using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using WKosArch.AssetProvider_Feature;
using WKosArch.GameState_Feature;
using WKosArch.UI_Feature;
using WKosArch.Quality_Feature;
using WKosArch.Sound_Feature;
using WKosArch.MVVM;

namespace WKosArch.Configs_Feature
{
    public class ConfigsFeature : IConfigsFeature
    {
        private const string GameStateConfigPath = "Default_GameStateConfig";
        private const string GameSettingsStateConfigPath = "Default_GameSettingsStateConfig";

        private const string UISceneConfigsFolderPath = "SceneConfigs";
        private const string QualitySettingsPath = "URPRenderersConfig";
        private const string SoundSceneConfigsFolderPath = "SceneSoundsConfig";


        public GameStateConfig GameStateConfig { get; private set; }
        public GameSettingsStateConfig GameSettingsStateConfig { get; private set; }

        public Dictionary<RenderingQuality, RenderPipelineAsset> RenderQualityConfigMap {  get; private set; }
        public Dictionary<string, Dictionary<string, View>> SceneUiConfigMap { get; private set; }
        public Dictionary<string, Dictionary<SoundType, SoundConfig>> SceneSoundConfigsMap { get; private set; }

        private IAssetProviderFeature _assetProviderService;



        public ConfigsFeature(IAssetProviderFeature assetProviderService)
        {
            _assetProviderService = assetProviderService;

            RenderQualityConfigMap = new();
            SceneUiConfigMap = new();
            SceneSoundConfigsMap = new();

            LoadGameStateConfig();
            LoadGameSettingsStateConfig();
            LoadSceneUiConfigs();
            LoadQualityConfigs();
            LoadSceneSoundConfigs();
        }

        private void LoadGameStateConfig()
        {
            GameStateConfig = _assetProviderService.Load<GameStateConfig>(GameStateConfigPath);
        }

        private void LoadGameSettingsStateConfig()
        {
            GameSettingsStateConfig = _assetProviderService.Load<GameSettingsStateConfig>(GameSettingsStateConfigPath);
        }

        public void Dispose() =>
            Clear();


        private void LoadQualityConfigs()
        {
            var qualityConfig = _assetProviderService.Load<URPRenderersConfig>(QualitySettingsPath);
            RenderQualityConfigMap = ConfigUtils.CreateDictionary<RenderingQuality, RenderPipelineAsset, URPRenderersConfigMapping>(qualityConfig.URPRenderersConfigMapping);
        }

        private void LoadSceneUiConfigs()
        {
            var scenesConfigs = _assetProviderService.LoadAll<UISceneConfig>(UISceneConfigsFolderPath);

            foreach (var sceneConfigs in scenesConfigs)
            {
                foreach (var scene in sceneConfigs.SceneName)
                {
                    var sceneConfigMap = ConfigUtils
                        .CreateDictionary<string, View, ViewModelToViewMapping>(sceneConfigs.UiConfigs);

                    if (SceneUiConfigMap.ContainsKey(scene))
                    {
                        ConfigUtils.MergeMaps(SceneUiConfigMap[scene], sceneConfigMap);
                    }
                    else
                    {
                        SceneUiConfigMap[scene] = sceneConfigMap;
                    }
                }
            }
        }

        private void LoadSceneSoundConfigs()
        {
            var scenesConfigs = _assetProviderService.LoadAll<SoundsSceneConfig>(SoundSceneConfigsFolderPath);

            foreach (var sceneConfig in scenesConfigs)
            {
                foreach (var scene in sceneConfig.SceneName)
                {
                    var sceneConfigMap = ConfigUtils
                        .CreateDictionary<SoundType, SoundConfig, SoundConfigMapping>(sceneConfig.SoundConfigs);

                    if (SceneSoundConfigsMap.ContainsKey(scene))
                    {
                        ConfigUtils.MergeMaps(SceneSoundConfigsMap[scene], sceneConfigMap);
                    }
                    else
                    {
                        SceneSoundConfigsMap[scene] = sceneConfigMap;
                    }
                }
            }
        }

        private void Clear()
        {
            RenderQualityConfigMap.Clear();
            SceneUiConfigMap.Clear();
            SceneSoundConfigsMap.Clear();
        }

}

    public static class ConfigUtils
    {
        /// <summary>
        /// Merges two dictionaries by adding key-value pairs from the newMap into the existingMap.
        /// If a key from the newMap already exists in the existingMap, it will be ignored, and a warning will be logged.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the dictionaries.</typeparam>
        /// <typeparam name="TValue">The type of values in the dictionaries.</typeparam>
        /// <param name="existingMap">The dictionary that will be updated with values from newMap.</param>
        /// <param name="newMap">The dictionary containing new key-value pairs to add to the existingMap.</param>
        public static void MergeMaps<TKey, TValue>(Dictionary<TKey, TValue> existingMap, Dictionary<TKey, TValue> newMap)
        {
            foreach (var kvp in newMap)
            {
                if (!existingMap.ContainsKey(kvp.Key))
                {
                    existingMap.Add(kvp.Key, kvp.Value);
                }
                else
                {
                    Debug.LogWarning($"You try to add key {kvp.Key} several times from different configs!");
                }
            }
        }

        /// <summary>
        /// Creates a dictionary from a list of mappings, where each mapping contains a key-value pair.
        /// If a key already exists in the dictionary, a warning is logged and the value is not added.
        /// This method should be called with explicit type arguments for TKey, TValue, and TMapping.
        /// For example: <c>ConfigUtils.CreateDictionary&lt;TKey, TValue, TMapping&gt;(mappings)</c>.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <typeparam name="TMapping">The type of the mapping class, which must inherit from Mapping&lt;TKey, TValue&gt;.</typeparam>
        /// <param name="mappings">The list of mappings to convert into a dictionary.</param>
        /// <returns>A dictionary with keys and values from the provided mappings.</returns>
        public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue, TMapping>(List<TMapping> mappings)
            where TMapping : Mapping<TKey, TValue>
        {
            var map = new Dictionary<TKey, TValue>();

            foreach (var mapping in mappings)
            {
                if (!map.ContainsKey(mapping.Key))
                {
                    map.Add(mapping.Key, mapping.Value);
                }
                else
                {
                    Debug.LogWarning($"Key {mapping.Key} already exists in the dictionary!");
                }
            }

            return map;
        }
    }
}
