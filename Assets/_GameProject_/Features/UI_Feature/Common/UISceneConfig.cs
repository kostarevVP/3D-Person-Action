using WKosArch.MVVM;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using WKosArch.Extentions;

namespace WKosArch.Services.UIService
{
    [CreateAssetMenu(fileName = "UI_SceneConfig", menuName = "UI/Configs/_UI_SceneConfig")]
    public class UISceneConfig : ScriptableObject
    {
        [SerializeField] private List<ViewModelToViewMapping> _windowPrefabs;
        [SerializeField] private List<ViewModelToViewMapping> _hudPrefabs;
        [SerializeField] private List<ViewModelToViewMapping> _widgetPrefabs;

        private readonly Dictionary<string, View> _windowMappings = new();
        private readonly Dictionary<string, View> _hudMappings = new();
        private readonly Dictionary<string, View> _widgetMappings = new();

        public Dictionary<string, View> WindowMappings => _windowMappings;
        public Dictionary<string, View> HudMappings => _hudMappings;
        public Dictionary<string, View> WidgetMappings => _widgetMappings;


        [HideInInspector] public string[] SceneName;
        [HideInInspector] public int[] SceneIndex;

#if UNITY_EDITOR
        [Space]
        [SerializeField] private SceneAsset[] _scenes;

        private void OnValidate()
        {
            if (_scenes != null)
            {
                SceneName = new string[_scenes.Length];
                SceneIndex = new int[_scenes.Length];

                for (int i = 0; i < _scenes.Length; i++)
                {
                    if (_scenes[i] != null)
                    {
                        var sceneName = _scenes[i].name;

                        SceneName[i] = sceneName;
                        SceneIndex[i] = GetSceneIndexByName(sceneName);
                    }
                    else
                    {
                        Log.PrintWarning($"Not add Scene to UISceneConfig {this}");
                    }
                }
            }

            //RefreshMappings(_windowMappings, _windowPrefabs);
            //RefreshMappings(_hudMappings, _hudPrefabs);
            //RefreshMappings(_widgetMappings, _widgetPrefabs);

        }
#endif

        public void InitilizationMapps()
        {
            RefreshMappings(_windowMappings, _windowPrefabs);
            RefreshMappings(_hudMappings, _hudPrefabs);
            RefreshMappings(_widgetMappings, _widgetPrefabs);
        }

        private void RefreshMappings(Dictionary<string, View> maping, List<ViewModelToViewMapping> prefabMappings)
        {
            foreach (var prefabMapping in prefabMappings)
            {
                maping.TryAdd(prefabMapping.ViewModelTypeFullName, prefabMapping.PrefabView);
            }
        }


        private int GetSceneIndexByName(string sceneName)
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;

            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneNameInBuildSettings = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                if (sceneNameInBuildSettings == sceneName)
                    return i;
            }

            Debug.LogError("Scene not found in build settings: " + sceneName);
            return -1;
        }
    }
}
