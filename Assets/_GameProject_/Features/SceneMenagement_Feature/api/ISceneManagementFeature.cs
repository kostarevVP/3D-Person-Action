using WKosArch.Domain.Features;
using System;
using WKosArch.Domain.Contexts;

namespace WKosArch.SceneManagement_Feature
{
    public interface ISceneManagementFeature : IFeature
    {
        bool SceneReadyToStart { get; set; }
        string CurrentSceneName { get; }
        int CurrentSceneIndex { get; }
        string PreviousSceneName { get; }

        event Action<string> OnSceneLoadingStarted;
        event Action<string> OnSceneChanged;
        event Action<string> OnSceneUnloaded;
        event Action<string> OnSceneLoaded;
        event Action<string> OnSceneStarted;
        event Action<SceneContext> OnContextLoaded;
        event Action<SceneContext> OnContextUnLoad;
        event Action<string> OnSceneReloadBegin;

        void LoadScene(int sceneIndex);
        void LoadScene(string sceneName);
        void OnContextLoadInvoke(SceneContext sceneContext);
        void OnContextUnLoadInvoke(SceneContext sceneContext);
        void ReloadScene();
    }
}