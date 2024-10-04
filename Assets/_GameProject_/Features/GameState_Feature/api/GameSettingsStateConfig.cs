using System;
using UnityEngine;

namespace WKosArch.GameState_Feature
{
    [CreateAssetMenu(fileName = "GameSettingsStateConfig", menuName = "Game/Configs/GameSettingsStateConfig")]
    public class GameSettingsStateConfig : ScriptableObject
    {
        [Range(0f, 1f)]
        public float MasterVolume = 1f;
        public bool MasterOn = true;
        [Range(0f, 1f)]
        public float MusicVolume = 1f;
        public bool MusicOn = true;
        [Range(0f, 1f)]
        public float SfxVolume = 1f;
        public bool SfxOn = true;
        [Range(0f, 1f)]
        public float UiVolume = 1f;
        public bool UiOn = true;


        private SoundSettingsState _soundSettings => new SoundSettingsState
        {
            MasterVolume = MasterVolume,
            MasterOn = MasterOn,
            MusicVolume = MusicVolume,
            MusicOn = MusicOn,
            SfxVolume = SfxVolume,
            SfxOn = SfxOn,
            UiVolume = UiVolume,
            UiOn = UiOn,
        };

        public GameSettingState GameSettingState => new GameSettingState
        {
            SoundSettingsState = _soundSettings,
        };
    }
}
