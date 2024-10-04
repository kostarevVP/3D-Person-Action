using System;

namespace WKosArch.GameState_Feature
{
    [Serializable]
    public class SoundSettingsState
    {
            public float MasterVolume;
            public bool MasterOn;
            public float MusicVolume;
            public bool MusicOn;
            public float SfxVolume;
            public bool SfxOn;
            public float UiVolume;
            public bool UiOn;
    }
}