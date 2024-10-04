using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.UI_Feature;
using WKosArch.DependencyInjection;
using WKosArch.SceneManagement_Feature;


[CreateAssetMenu(fileName = "LoadMainMenuFeature_Installer", menuName = "Game/Installers/LoadMainMenuFeature_Installer")]
public class LoadMainMenuFeature_Installer : FeatureInstaller
{
    public override IFeature Create(DIContainer container)
    {
        ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();
        IUserInterfaceFeature ui = container.Resolve<IUserInterfaceFeature>();
        IStateHandlerFeature saveLoadHandlerService = container.Resolve<IStateHandlerFeature>();


        ILoadMainMenuFeature feature = new LoadMainMenuFeature(sceneManagementService, ui);


        saveLoadHandlerService.AddGameStateHolder(feature as ISaveGameState);
        saveLoadHandlerService.InformSaveHolders();

        RegisterFeatureAsSingleton(container, feature);

        return feature;
    }

    public override void Dispose() { }

    private void RegisterFeatureAsSingleton(DIContainer container, ILoadMainMenuFeature feature)
    {
        container.RegisterFactory(_ => feature).AsSingle();
        Log.PrintColor($"[LoadMainMenuFeature_Installer] Create and RegisterSingleton", Color.cyan);
    }
}
