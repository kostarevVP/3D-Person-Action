using WKosArch.DependencyInjection;
using System.Linq;
using UnityEngine;

namespace WKosArch.Domain.Contexts
{
    public sealed class ProjectContext : Context
    {
        [Space]
        [SerializeField] private SceneContext[] _sceneContexts;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public SceneContext GetSceneContext(string sceneName)
        {
            var result = _sceneContexts.FirstOrDefault(c => c.SceneName == sceneName);
            
            return result;
        }

        protected override DIContainer CreateLocalContainer(DIContainer dIContainer = null)
        {
            DIContainer rootContainer = new DIContainer();

            rootContainer.RegisterFactory(_ => this).AsSingle();

            StaticDI.SetCurrentContainer(rootContainer);

            return rootContainer;
        }
    }
}