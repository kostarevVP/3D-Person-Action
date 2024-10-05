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

    public override IFeature Create(IDIContainer container)
    {
        IGameStateProviderFeature gameStateProvider = container.Resolve<IGameStateProviderFeature>();

        _feature = new StateHandlerFeature(gameStateProvider);

        BindAsSingle(container, _feature);

        return _feature;
    }

    public override void Dispose() { }

    private void BindAsSingle(IDIContainer container, IStateHandlerFeature feature)
    {
        container.Bind(feature).AsSingle();
        Log.PrintColor($"[StateHandlerFeature - IStateHandlerFeature] Create and Bind as Single", Color.cyan);
    }
}
