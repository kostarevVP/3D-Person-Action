using WKosArch.Extentions;
using WKosArch.Services.Scenes;
using WKosArch.Services.UIService.UI;
using WKosArch.Sound_Feature;

public class LoadMainMenuFeature : ILoadMainMenuFeature
{
    private readonly ISaveLoadHandlerService _saveLoadHandler;
    private readonly ISceneManagementFeature _sceneManagementService;
    private readonly IUserInterface _ui;



    public LoadMainMenuFeature(ISceneManagementFeature sceneManagementService,
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

        LoadGameLevelEnvironment();

    }

    private void SceneStarted(string sceneName)
    {
        ShowUI();

    }

    public void LoadGameLevelEnvironment()
    {
        Log.PrintYellow("Load MainMenu environment");


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
        _ui.Show<AudioSettingViewModel>();
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
    }
}
