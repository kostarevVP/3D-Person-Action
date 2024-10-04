using WKosArch.Extensions;
using WKosArch.SceneManagement_Feature;
using WKosArch.UI_Feature;

namespace WKosArch.Features.LoadLevelFeature
{
    public class LoadLevelFeature : ILoadLevelFeature
    {
        private readonly ISceneManagementFeature _sceneManagementService;
        private readonly IUserInterfaceFeature _ui;


        public LoadLevelFeature(ISceneManagementFeature sceneManagementService, IUserInterfaceFeature ui)
        {
            _sceneManagementService = sceneManagementService;
            _ui = ui;

            Subscribe();
        }

        private void Subscribe()
        {
            _sceneManagementService.OnSceneLoaded += SceneLoaded;
            _sceneManagementService.OnSceneStarted += SceneStarted;
        }

        private void Unsubscribe()
        {
            _sceneManagementService.OnSceneLoaded -= SceneLoaded;
            _sceneManagementService.OnSceneStarted -= SceneStarted;
        }

        private void SceneLoaded(string sceneName)
        {
            LoadGameLevelEnvironment();
        }

        private void SceneStarted(string sceneName)
        {
            ShowUI();

        }

        public void LoadGameLevelEnvironment()
        {
            Log.PrintYellow("Load level Environment");


            SpawnPlayer();

            _sceneManagementService.SceneReadyToStart = true;
        }

        private void SpawnPlayer()
        {

        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void ShowUI()
        {
            //_ui.Show<FpsInfoHudViewModel>();
            //_ui.Show<SettingWindowViewModel>();
            //_ui.Show<JoystickHudViewModel>();
            //_ui.Show<PlayerDataHudViewModel>();
            //_ui.Show<EnemyHudViewModel>();
        }
    }
}