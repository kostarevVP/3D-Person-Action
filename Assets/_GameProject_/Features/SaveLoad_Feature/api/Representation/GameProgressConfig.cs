using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/ProgressConfig/GameProgressConfig", fileName = "GameProgressConfig")]
public class GameProgressConfig : ScriptableObject
{
    public SoundProgressConfig SoundProgressConfig;

    [HideInInspector] public string NewGameSceneName;
    [HideInInspector] public int NewGameSceneIndex;

    

#if UNITY_EDITOR
    [Space]
    [SerializeField] private SceneAsset _firstSceneToLoad;

    private void OnValidate()
    {
        if (_firstSceneToLoad != null)
        {
            NewGameSceneName = _firstSceneToLoad.name;
            NewGameSceneIndex = GetSceneIndexByName(NewGameSceneName);
        }
    }
#endif

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
