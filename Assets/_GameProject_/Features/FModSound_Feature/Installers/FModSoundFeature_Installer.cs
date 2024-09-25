using WKosArch.DependencyInjection;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using UnityEngine;
using FMod_Feature;
using Assets.Game.Services.ProgressService.api;
using FModSound_Feature.Implementation;
using WKosArch.Sound_Feature;


[CreateAssetMenu(fileName = "FModSoundFeature_Installer", menuName = "Game/Installers/FModSoundFeature_Installer")]
public class FModSoundFeature_Installer : FeatureInstaller
{
    [SerializeField]
    private FModEventsConfig _fModEventsConfig;

    private ISoundFeature _feature;

    public override IFeature Create(IDiContainer container)
    {
        //var fModListener = FModListener.CreateInstance();
        var soundProgress = container.Resolve<IProgressFeature>().GameProgressData.GlobalProgress.SoundProgressData;
        var saveLoadHolder = container.Resolve<ISaveLoadHandlerService>();
        SoundSettingsData soundProgressData = container.Resolve<IProgressFeature>().GameProgressData.GlobalProgress.SoundProgressData;


        _feature = new FModSoundFeature(soundProgressData);

        //FModManager fModManager = FModManager.CreateInstnace();
        //fModManager.FModFeature = _feature;

        //saveLoadHolder.AddSaveLoadHolder(_feature);

        //RegisterFModManagerAsSingleton(container, fModManager);

        RegisterFeatureAsSingleton(container, _feature);

        return _feature;
    }

    public override void Dispose() 
    {

    }

    private void RegisterFeatureAsSingleton(IDiContainer container, ISoundFeature feature)
    {
        container.RegisterSingleton(_ => feature);
        Log.PrintColor($"[ISoundFeature] Create and RegisterSingleton", Color.cyan);
    }
    private void RegisterFModManagerAsSingleton(IDiContainer container, FModManager fModManager)
    {
        container.RegisterSingleton(_ => fModManager);
        Log.PrintColor($"[FModManager] Create and RegisterSingleton", Color.cyan);
    }
}
