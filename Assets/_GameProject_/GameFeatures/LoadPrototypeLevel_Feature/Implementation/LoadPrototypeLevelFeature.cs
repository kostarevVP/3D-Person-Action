using System;
using WKosArch.Extensions;
using WKosArch.SceneManagement_Feature;
using WKosArch.UI_Feature;

namespace WKosArch.Features.LoadLevelFeature
{
    public class LoadPrototypeLevelFeature : ILoadPrototypeLevelFeature, IDisposable
    {
        private readonly ISceneManagementFeature _sceneManagementService;
        private readonly IUserInterfaceFeature _ui;


        public LoadPrototypeLevelFeature(ISceneManagementFeature sceneManagementService, IUserInterfaceFeature ui)
        {
            _sceneManagementService = sceneManagementService;
            _ui = ui;

            Subscribe();
        }


        private void SceneLoaded(string sceneName) => 
            LoadGameLevelEnvironment();

        private void SceneStarted(string sceneName)
        {
            
        }

        private void LoadGameLevelEnvironment()
        {
            Log.PrintYellow("Load level Environment");

            ShowUI();
            SpawnPlayer();

            _sceneManagementService.SceneReadyToStart = true;
        }

        private void SpawnPlayer()
        {

        }

        private void ShowUI()
        {
            //_ui.Show<FpsInfoHudViewModel>();
            //_ui.Show<SettingWindowViewModel>();
            //_ui.Show<JoystickHudViewModel>();
            //_ui.Show<PlayerDataHudViewModel>();
            //_ui.Show<EnemyHudViewModel>();
        }

        public void Dispose() => 
            Unsubscribe();

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
    }
}