using R3;

namespace WKosArch.Sound_Feature
{
    public interface ISoundSettingsDataProxy
    {
        ReactiveProperty<bool> MasterOn { get; }
        ReactiveProperty<float> MasterVolume { get; }
        ReactiveProperty<bool> MusicOn { get; }
        ReactiveProperty<float> MusicVolume { get; }
        ReactiveProperty<bool> SfxOn { get; }
        ReactiveProperty<float> SfxVolume { get; }
        ReactiveProperty<bool> UiOn { get; }
        ReactiveProperty<float> UiVolume { get; }
    }
}