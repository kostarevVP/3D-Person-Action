using Cysharp.Threading.Tasks;
using WKosArch.Domain.Contexts;

namespace WKosArch.SceneManagement_Feature
{
    public class SceneContextFeature
    {
        private ProjectContext _projectContext;
        private ISceneManagementFeature _sceneManagementService;

        public SceneContextFeature(ProjectContext projectContext, ISceneManagementFeature sceneManagementService)
        {
            _projectContext = projectContext;
            _sceneManagementService = sceneManagementService;

            _sceneManagementService.OnSceneChanged += LoadSceneContext;
        }

        private async void LoadSceneContext(string sceneName)
        {
            DestroyContext(_sceneManagementService.CurrentSceneName);
            await LoadContext(sceneName);
        }
    

        private async UniTask<SceneContext> LoadContext(string sceneName)
        {
            SceneContext sceneContext = _projectContext.GetSceneContext(sceneName);

            if (sceneContext != null)
            {
                await sceneContext.InitializeAsync();
            }

            _sceneManagementService.OnContextLoadInvoke(sceneContext);

            return sceneContext;
        }

        private void DestroyContext(string sceneName)
        {
            SceneContext sceneContext = _projectContext.GetSceneContext(sceneName);

            _sceneManagementService.OnContextUnLoadInvoke(sceneContext);

            if (sceneContext != null)
            {
                sceneContext.Destroy();
            }
        }
    }
}