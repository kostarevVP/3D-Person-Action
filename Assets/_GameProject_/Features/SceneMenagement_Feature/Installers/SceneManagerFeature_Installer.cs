using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.DependencyInjection;

namespace WKosArch.SceneManagement_Feature
{
    [CreateAssetMenu(fileName = "SceneManagerFeature_Installer", menuName = "Game/Installers/SceneManagerFeature_Installer")]
    public class SceneManagerFeature_Installer : FeatureInstaller
    {
        [SerializeField] private GameObject _loadingScreenPrefab;

        public override IFeature Create(IDIContainer container)
        {
            ProjectContext projectContext = container.Resolve<ProjectContext>();

            ILoadingScreen loadingScreen = InstantiateLoadingScreen();

            ISceneManagementFeature sceneManagementService = new SceneManagementFeature(loadingScreen);

            new SceneContextFeature(projectContext, sceneManagementService);

            BindAsSingle(container, sceneManagementService);

            return sceneManagementService;
        }


        public override void Dispose() { }

        private void BindAsSingle(IDIContainer container, ISceneManagementFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[SceneManagementFeature - ISceneManagementFeature] Create and Bind as Single", Color.cyan);
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