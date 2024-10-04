using UnityEngine;
using System.Collections.Generic;
using WKosArch.Sound_Feature;
using System.Linq;



#if UNITY_EDITOR
using UnityEngine.SceneManagement;
using WKosArch.Extensions;
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "SoundsSceneConfig", menuName = "Game/Configs/SoundConfig/SoundsSceneConfig")]
public class SoundsSceneConfig : ScriptableObject
{
    [HideInInspector]
    public List<SoundConfigMapping> SoundConfigs = new List<SoundConfigMapping>();

    [HideInInspector] 
    public string[] SceneName;
    [HideInInspector] 
    public int[] SceneIndex;

    [SerializeField]
    private List<SoundConfigMapping> _uiSoundsMapping;
    [SerializeField]
    private List<SoundConfigMapping> _sfxSoundsMapping;
    [SerializeField]
    private List<SoundConfigMapping> _musicSoundsMapping;



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
                    Log.PrintWarning($"Not add Scene to FModSoundsSceneConfig {this}");
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

#endif

    private void Compose()
    {
        SoundConfigs = _uiSoundsMapping
            .Concat(_sfxSoundsMapping)
            .Concat(_musicSoundsMapping)
            .Distinct()
            .ToList();
    }
}
