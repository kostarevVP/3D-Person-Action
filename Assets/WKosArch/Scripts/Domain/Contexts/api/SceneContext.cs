using WKosArch.Game;
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


        protected override IDiContainer CreateLocalContainer(IDiContainer dIContainer = null)
        {
            IDiContainer rootContainer = Game.Game.ProjectContext.Container;
            return new DiContainer(rootContainer);
        }
    }
}