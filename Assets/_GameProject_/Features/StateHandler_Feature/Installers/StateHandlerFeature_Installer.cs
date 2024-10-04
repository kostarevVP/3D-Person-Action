using UnityEngine;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.DependencyInjection;
using WKosArch.GameState_Feature;


[CreateAssetMenu(fileName = "StateHandlerFeature_Installer", menuName = "Game/Installers/StateHandlerFeature_Installer")]
public class StateHandlerFeature_Installer : FeatureInstaller
{
    private IStateHandlerFeature _feature;

    public override IFeature Create(DIContainer container)
    {
        IGameStateProviderFeature gameStateProvider = container.Resolve<IGameStateProviderFeature>();

        _feature = new StateHandlerFeature(gameStateProvider);

        RegisterFeatureAsSingleton(container, _feature);

        return _feature;
    }

    public override void Dispose() { }

    private void RegisterFeatureAsSingleton(DIContainer container, IStateHandlerFeature feature)
    {
        container.RegisterInstance(feature);
        Log.PrintColor($"[StateHandlerFeature - IStateHandlerFeature] Create and RegisterSingleton", Color.cyan);
    }
}
