using WKosArch.MVVM;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using WKosArch.Extensions;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
#endif

namespace WKosArch.UI_Feature
{
    [CreateAssetMenu(fileName = "UISceneConfig", menuName = "UI/Configs/_UISceneConfig")]
    public class UISceneConfig : ScriptableObject
    {
        [HideInInspector]
        public List<ViewModelToViewMapping> UiConfigs = new List<ViewModelToViewMapping>();
        [HideInInspector]
        public string[] SceneName;
        [HideInInspector]
        public int[] SceneIndex;

        [SerializeField]
        private List<ViewModelToViewMapping> _windowPrefabs;
        [SerializeField]
        private List<ViewModelToViewMapping> _hudPrefabs;
        [SerializeField]
        private List<ViewModelToViewMapping> _widgetPrefabs;



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

            Compose();
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

        private void Compose()
        {
            UiConfigs = _windowPrefabs
                .Concat(_hudPrefabs)
                .Concat(_widgetPrefabs)
                .Distinct()
                .ToList();
        }
#endif
    }
}
