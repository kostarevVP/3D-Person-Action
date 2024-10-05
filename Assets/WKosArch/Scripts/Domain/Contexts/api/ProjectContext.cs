using System.Linq;
using UnityEngine;
using WKosArch.DependencyInjection;
using WKosArch.SceneManagement_Feature;

namespace WKosArch.Domain.Contexts
{
    public sealed class ProjectContext : Context
    {
        [Space]
        [SerializeField] private SceneContext[] _sceneContexts;

        private IDIContainer _rootContainer;
        private ISceneManagementFeature _sceneManagementFeature => _rootContainer.Resolve<ISceneManagementFeature>();

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public SceneContext GetSceneContext(string sceneName)
        {
            var result = _sceneContexts.FirstOrDefault(c => c.SceneName == sceneName);

            return result;
        }

        protected override IDIContainer CreateLocalContainer(IDIContainer dIContainer = null)
        {
            _rootContainer = new DIContainer();

            StaticDI.SetCurrentContainer(_rootContainer);

            _rootContainer.Bind(this).AsSingle();

            return _rootContainer;
        }

        private void OnDestroy()
        {
            GetSceneContext(_sceneManagementFeature.CurrentSceneName).Dispose();
            this.Dispose();
        }
    }
}