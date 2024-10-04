using R3;

namespace WKosArch.GameState_Feature
{
    public class GameSettingStateProxy
    {
        public ReactiveProperty<SoundSettingsStateProxy> SoundSettingsStateProxy;

        public GameSettingStateProxy(GameSettingState gameSettingStateOrigin)
        {
            SoundSettingsStateProxy = new ReactiveProperty<SoundSettingsStateProxy>(new SoundSettingsStateProxy(gameSettingStateOrigin.SoundSettingsState));
        }
    }
}