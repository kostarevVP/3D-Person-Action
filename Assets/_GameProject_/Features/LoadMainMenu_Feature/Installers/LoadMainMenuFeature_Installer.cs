using WKosArch.DependencyInjection;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using UnityEngine;
using WKosArch.Services.Scenes;
using WKosArch.Services.UIService.UI;
using WKosArch.Services.UIService;


[CreateAssetMenu(fileName = " LoadMainMenuFeature_Installer", menuName = "Game/Installers/LoadMainMenuFeature_Installer")]
public class LoadMainMenuFeature_Installer : FeatureInstaller
{

    public override IFeature Create(IDiContainer container)
    {
        ISceneManagementFeature sceneManagementService = container.Resolve<ISceneManagementFeature>();
        IUserInterface ui = container.Resolve<IUiFeature>().UI;
        ISaveLoadHandlerService saveLoadHandler = container.Resolve<ISaveLoadHandlerService>();

        ILoadMainMenuFeature feature = new LoadMainMenuFeature(sceneManagementService, ui, saveLoadHandler);

        RegisterFeatureAsSingleton(container, feature);

        return feature;
    }

    public override void Dispose() { }

    private void RegisterFeatureAsSingleton(IDiContainer container, ILoadMainMenuFeature feature)
    {
        container.RegisterSingleton(_ => feature);
        Log.PrintColor($"[LoadMainMenuFeature_Installer] Create and RegisterSingleton", Color.cyan);
    }
}
