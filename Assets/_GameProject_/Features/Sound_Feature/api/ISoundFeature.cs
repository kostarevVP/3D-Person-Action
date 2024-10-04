using UnityEngine.InputSystem.Haptics;
using WKosArch.Domain.Features;
using WKosArch.GameState_Feature;

namespace WKosArch.Sound_Feature
{
    public interface ISoundFeature<TSound> : IFeature where TSound : ISound
    {
        public SoundSettingsStateProxy Settings { get; }
        public ISounds<TSound> UiSounds { get; }
        public ISounds<TSound> SfxSounds { get; }
        public ISounds<TSound> MusicSounds { get; }
        public IHaptics Haptics { get; }

        void MuteAll();
        void UnmuteAll();

        //void SwitchHaptic(bool isEnabled);
    }
}