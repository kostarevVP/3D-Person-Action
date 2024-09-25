using Unity.Transforms;
using WKosArch.Extentions;
using WKosArch.Services.Scenes;
using WKosArch.Services.UIService.UI;

namespace WKosArch.Features.LoadLevelFeature
{
    public class LoadLevelFeature : ILoadLevelFeature
    {
        private readonly ISaveLoadHandlerService _saveLoadHandler;
        private readonly ISceneManagementFeature _sceneManagementService;
        private readonly IUserInterface _ui;


        private LocalTransform _playerSpawnPoint;

        public LoadLevelFeature(ISceneManagementFeature sceneManagementService,
            IUserInterface ui,
            ISaveLoadHandlerService saveLoadHandler)
        {
            _sceneManagementService = sceneManagementService;
            _ui = ui;
            _saveLoadHandler = saveLoadHandler;
            _saveLoadHandler.AddSaveLoadHolder(this);

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
            _saveLoadHandler.InformLoadHolders();

            LoadGameLevelEnviroment();

        }

        private void SceneStarted(string sceneName)
        {
            ShowUI();

        }

        public void LoadGameLevelEnviroment()
        {
            Log.PrintYellow("Load level environment");


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


        public void SaveProgress(GameProgressData progress)
        {
            //var sceneIndex = _sceneManagementService.CurrentSceneIndex;
            //progress.SceneProgressData.SceneIndex = sceneIndex;
            //progress.SceneProgressData.PlayerProgressData.LocalTransform = _factorySystem.GetLocalTransform(_player);
        }

        public void LoadProgress(GameProgressData progress)
        {
            var sceneIndex = _sceneManagementService.CurrentSceneIndex;
            if (sceneIndex != progress.SceneProgressData.SceneIndex)
                return;

            _playerSpawnPoint = progress.SceneProgressData.PlayerProgressData.LocalTransformData.ToLocalTranform();
        }
    }
}