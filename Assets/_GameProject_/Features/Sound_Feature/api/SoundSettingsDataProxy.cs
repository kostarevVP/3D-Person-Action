using R3;

namespace WKosArch.Sound_Feature
{
    public class SoundSettingsDataProxy : ISoundSettingsDataProxy
    {
        public ReactiveProperty<float> MasterVolume { get; }
        public ReactiveProperty<bool> MasterOn { get; }
        public ReactiveProperty<float> MusicVolume { get; }
        public ReactiveProperty<bool> MusicOn { get; }
        public ReactiveProperty<float> SfxVolume { get; }
        public ReactiveProperty<bool> SfxOn { get; }
        public ReactiveProperty<float> UiVolume { get; }
        public ReactiveProperty<bool> UiOn { get; }

        public SoundSettingsDataProxy(SoundSettingsData originData)
        {
            MasterVolume = new ReactiveProperty<float>(originData.MasterVolume);
            MasterOn = new ReactiveProperty<bool>(originData.MasterOn);
            MusicVolume = new ReactiveProperty<float>(originData.MusicVolume);
            MusicOn = new ReactiveProperty<bool>(originData.MusicOn);
            SfxVolume = new ReactiveProperty<float>(originData.SfxVolume);
            SfxOn = new ReactiveProperty<bool>(originData.SfxOn);
            UiVolume = new ReactiveProperty<float>(originData.UiVolume);
            UiOn = new ReactiveProperty<bool>(originData.UiOn);

            MasterVolume.Skip(1).Subscribe(value => originData.MasterVolume = value);
            MasterOn.Skip(1).Subscribe(value => originData.MasterOn = value);
            MusicVolume.Skip(1).Subscribe(value => originData.MusicVolume = value);
            MusicOn.Skip(1).Subscribe(value => originData.MusicOn = value);
            SfxVolume.Skip(1).Subscribe(value => originData.SfxVolume = value);
            SfxOn.Skip(1).Subscribe(value => originData.SfxOn = value);
            UiVolume.Skip(1).Subscribe(value => originData.UiVolume = value);
            UiOn.Skip(1).Subscribe(value => originData.UiOn = value);
        }


    }
}