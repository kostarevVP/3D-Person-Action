using R3;

namespace WKosArch.GameState_Feature
{
    public class GameStateProxy
    {
        public ReactiveProperty<PlayerStateProxy> PlayerStateProxy { get; }
        public ReactiveProperty<SceneLoadingStateProxy> SceneLoadingProxy { get; }

        public GameStateProxy(GameState originGameState)
        {
            PlayerStateProxy = new ReactiveProperty<PlayerStateProxy>(new PlayerStateProxy(originGameState.PlayerState));
            SceneLoadingProxy = new ReactiveProperty<SceneLoadingStateProxy>(new SceneLoadingStateProxy(originGameState.SceneLoadingState));
        }
    }
}