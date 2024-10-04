#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.SceneManagement;
#endif
using UnityEngine;


namespace WKosArch.GameState_Feature
{
    [CreateAssetMenu(fileName = "GameStateConfig", menuName = "Game/Configs/GameStateConfig")]
    public class GameStateConfig : ScriptableObject
    {
#if UNITY_EDITOR
        [Space]
        [SerializeField] private SceneAsset _firstSceneToLoad;

        private void OnValidate()
        {
            if (_firstSceneToLoad != null)
            {
                FirstSceneToLoadName = _firstSceneToLoad.name;
                FirstSceneToLoadIndex = GetSceneIndexByName(FirstSceneToLoadName);
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
#endif
        [HideInInspector] public string FirstSceneToLoadName;
        [HideInInspector] public int FirstSceneToLoadIndex;

        [Space(24)]
        public PlayerState PlayerState;
        [Space(14)]
        public SceneState[] SceneStates;


        public GameState GameState => new GameState
        {
            PlayerState = PlayerState,
            SceneLoadingState = new SceneLoadingState
            {
                SceneIndexToLoad = FirstSceneToLoadIndex,
                SceneStates = SceneStates,
            },
        };
    }
}
