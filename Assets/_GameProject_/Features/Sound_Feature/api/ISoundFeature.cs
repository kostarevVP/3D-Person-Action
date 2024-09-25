using UnityEngine.InputSystem.Haptics;
using WKosArch.Domain.Features;

namespace WKosArch.Sound_Feature
{
    public interface ISoundFeature : IFeature
    {
        public ISoundSettings Settings { get; }
        public IUiSounds UiSounds { get; }
        public SfxSounds SfxSounds { get; }
        public IMusicSounds MusicSounds { get; }
        public IHaptics Haptics { get; }

        void MuteAll();
        void UnmuteAll();

        //void SwitchHaptic(bool isEnabled);
    }
}