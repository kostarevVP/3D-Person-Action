using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using UnityEngine;
using WKosArch.DependencyInjection;

namespace WKosArch.SceneManagement_Feature
{
    [CreateAssetMenu(fileName = "SceneManagerFeature_Installer", menuName = "Game/Installers/SceneManagerFeature_Installer")]
    public class SceneManagerFeature_Installer : FeatureInstaller
    {
        [SerializeField] private GameObject _loadingScreenPrefab;

        public override IFeature Create(IDiContainer container)
        {
            ProjectContext projectContext = container.Resolve<ProjectContext>();

            ILoadingScreen loadingScreen = InstantiateLoadingScreen();

            ISceneManagementFeature sceneManagementService = new SceneManagementFeature(loadingScreen);

            new SceneContextFeature(projectContext, sceneManagementService);

            RegisterFeatureAsSingleton(container, sceneManagementService);

            return sceneManagementService;
        }


        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, ISceneManagementFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[SceneManagementFeature - ISceneManagementFeature] Create and RegisterSingleton", Color.cyan);
        }

        private ILoadingScreen InstantiateLoadingScreen()
        {
            ILoadingScreen loadingScreen = default;

            if (_loadingScreenPrefab != null)
            {
                var loadingScreenGo = Instantiate(_loadingScreenPrefab);
                loadingScreen = loadingScreenGo.GetComponent<ILoadingScreen>();

                DontDestroyOnLoad(loadingScreenGo);
            }

            return loadingScreen;
        }

        private void OnValidate()
        {
            if (_loadingScreenPrefab != null && _loadingScreenPrefab.GetComponent<ILoadingScreen>() == null)
            {
                Log.PrintWarning($"SceneManagerInstaller doesn't have any ILoadingScreen component.");
            }
        }
    }
}