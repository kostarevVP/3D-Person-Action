using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using WKosArch.Domain.Contexts;
using WKosArch.Extensions;

namespace WKosArch.SceneManagement_Feature
{
    public class SceneManagementFeature : ISceneManagementFeature
    {
        public event Action<string> OnSceneLoadingStarted;
        public event Action<string> OnSceneChanged;
        public event Action<string> OnSceneUnloaded;
        public event Action<string> OnSceneLoaded;
        public event Action<string> OnSceneStarted;
        public event Action<SceneContext> OnContextLoaded;
        public event Action<SceneContext> OnContextUnLoad;
        public event Action<string> OnSceneReloadBegin;
        public bool SceneReadyToStart { get; set; }
        public string CurrentSceneName => _currentSceneName;
        public string PreviousSceneName => _previousSceneName;
        public int CurrentSceneIndex => _currentSceneIndex;

        private int _currentSceneIndex;
        private string _currentSceneName;
        private string _previousSceneName;

        private ILoadingScreen _loadingScreen;


        public SceneManagementFeature(ILoadingScreen loadingScreen = null)
        {
            _loadingScreen = loadingScreen;

            SceneManager.sceneLoaded += (scene, _) =>
            {
                OnSceneLoaded?.Invoke(scene.name);
            };
            SceneManager.sceneUnloaded += scene =>
            {
                OnSceneUnloaded?.Invoke(scene.name);
                _previousSceneName = scene.name;
            };
        }

        public void LoadScene(string sceneName) => 
            LoadSceneAsync(sceneName);

        public void ReloadScene()
        {
            OnSceneReloadBegin?.Invoke(_currentSceneName);
            LoadScene(_currentSceneName);
        }

        public void OnContextLoadInvoke(SceneContext sceneContext) => 
            OnContextLoaded?.Invoke(sceneContext);

        public void OnContextUnLoadInvoke(SceneContext sceneContext) => 
            OnContextUnLoad?.Invoke(sceneContext);

        public void LoadScene(int sceneIndex)
        {
            string sceneName = GetSceneNameFromIndex(sceneIndex);

            LoadScene(sceneName);
        }

        private string GetSceneNameFromIndex(int sceneIndex)
        {
            var path = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
            var lastSlash = path.LastIndexOf('/');
            var nameWithExtension = path.Substring(lastSlash + 1);
            var lastDot = nameWithExtension.LastIndexOf('.');
            var sceneName = nameWithExtension.Substring(0, lastDot);
            return sceneName;
        }

        private int GetBuildIndexFromSceneName(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            if (scene != null)
            {
                return scene.buildIndex;
            }
            else
            {
                Debug.LogError("Scene " + sceneName + " not found in build settings!");
                return -1; // Return -1 or handle the error as needed
            }
        }

        private async void LoadSceneAsync(string sceneName)
        {
            OnSceneLoadingStarted?.Invoke(sceneName);

            if (_loadingScreen != null)
            {
                await ShowAnimation(_loadingScreen.Show);
            }

            var async = SceneManager.LoadSceneAsync(sceneName);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
            {
                await UniTask.Yield();
            }

            async.allowSceneActivation = true;

            OnSceneChanged?.Invoke(sceneName);



            while (!SceneReadyToStart)
            {
                await UniTask.Yield();
            }

            if (_loadingScreen != null)
            {
                await ShowAnimation(_loadingScreen.Hide);
            }

            OnSceneStarted?.Invoke(sceneName);

            _currentSceneIndex = GetBuildIndexFromSceneName(sceneName);
            _currentSceneName = sceneName;

        }

        private static async UniTask ShowAnimation(Action<Action> method)
        {
            var isCompleted = false;

            void OnComplete()
            {
                isCompleted = true;
            }

            method(OnComplete);

            while (!isCompleted)
            {
                await UniTask.Yield();
            }
        }
    }
}