using UnityEngine;
using WKosArch.DependencyInjection;


namespace WKosArch.Domain.Contexts
{
    public sealed class SceneContext : Context
    {
        [Space]
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _scene;
#endif
        [HideInInspector] public string SceneName;

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (_scene != null)
            {
                SceneName = _scene.name;
            }
#endif
        }

        protected override IDIContainer CreateLocalContainer(IDIContainer dIContainer = null)
        {
            IDIContainer rootContainer = GameProject.GameProject.ProjectContext.Container;

            var sceneContainer = new DIContainer(rootContainer);

            StaticDI.SetCurrentContainer(sceneContainer);

            return sceneContainer;
        }
    }
}