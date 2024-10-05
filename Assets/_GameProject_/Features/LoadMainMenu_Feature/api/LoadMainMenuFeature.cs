using WKosArch.Extensions;
using WKosArch.UI_Feature;
using WKosArch.Sound_Feature;
using WKosArch.SceneManagement_Feature;
using WKosArch.GameState_Feature;

public class LoadMainMenuFeature : ILoadMainMenuFeature, ISaveGameState
{
    private readonly ISceneManagementFeature _sceneManagementService;
    private readonly IUserInterfaceFeature _ui;


    public LoadMainMenuFeature(ISceneManagementFeature sceneManagementService, IUserInterfaceFeature ui)
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

    }

    public void LoadGameLevelEnvironment()
    {
        Log.PrintYellow("Load MainMenu environment");

        ShowUI();

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
        _ui.Show<AudioSettingViewModel>();
    }

    public void SaveGameState(GameStateProxy progress)
    {

    }

    public void LoadGameState(GameStateProxy progress)
    {
        
    }
}
