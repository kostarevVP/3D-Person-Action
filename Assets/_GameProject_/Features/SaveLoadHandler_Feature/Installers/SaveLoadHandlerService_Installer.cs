using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using UnityEngine;
using Assets.Game.Services.ProgressService.api;
using WKosArch.DependencyInjection;


[CreateAssetMenu(fileName = " SaveLoadHandlerService_Installer", menuName = "Game/Installers/SaveLoadHandlerService_Installer")]
public class SaveLoadHandlerService_Installer : FeatureInstaller
{
    private ISaveLoadHandlerService _feature;

    public override IFeature Create(IDiContainer container)
    {
        IProgressFeature progressService = container.Resolve<IProgressFeature>();
        ISaveLoadFeature saveLoadService = container.Resolve<ISaveLoadFeature>();

        _feature = new SaveLoadHandlerService(progressService, saveLoadService);

        RegisterFeatureAsSingleton(container, _feature);

        return _feature;
    }

    public override void Dispose() 
    {
        _feature.Clear();
    }

    private void RegisterFeatureAsSingleton(IDiContainer container, ISaveLoadHandlerService feature)
    {
        container.RegisterSingleton(_ => feature);
        Log.PrintColor($"[ISaveLoadHandlerService_Installer] Create and RegesterSingleton", Color.cyan);
    }
}
