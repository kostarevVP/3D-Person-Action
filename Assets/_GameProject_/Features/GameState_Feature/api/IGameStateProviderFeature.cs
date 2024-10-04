using R3;
using WKosArch.Domain.Features;

namespace WKosArch.GameState_Feature
{
    public interface IGameStateProviderFeature : IFocusPauseFeature
	{
        public GameStateProxy GameState { get; }
        public GameSettingStateProxy GameSettingsState { get; }

        public Observable<GameStateProxy> LoadGameState();
        public Observable<bool> SaveGameState();
        public Observable<bool> ResetGameState();
        public Observable<GameSettingStateProxy> LoadGameSettingsState();
        public Observable<bool> SaveGameSettingsState();
        public Observable<bool> ResetGameSettingsState();
    }
}