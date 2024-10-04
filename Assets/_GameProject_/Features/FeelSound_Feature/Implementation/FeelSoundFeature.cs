using R3;
using UnityEngine.InputSystem.Haptics;
using WKosArch.Sound_Feature;
using WKosArch.GameState_Feature;

public class FeelSoundFeature : ISoundFeature<FeelSound>
{

    public SoundSettingsStateProxy Settings { get; private set; }

    public ISounds<FeelSound> UiSounds => _feelSoundsFactory;

    public ISounds<FeelSound> SfxSounds => _feelSoundsFactory;

    public ISounds<FeelSound> MusicSounds => _feelSoundsFactory;

    public IHaptics Haptics => throw new System.NotImplementedException();

    private SoundManager _soundManager;
    private FeelSoundsFactory _feelSoundsFactory;

    public FeelSoundFeature(SoundSettingsStateProxy soundSettingsStateProxy)
    {
        Settings = soundSettingsStateProxy;
        _soundManager = SoundManager.CreateInstance();
        _feelSoundsFactory = new FeelSoundsFactory();

        Subscribe();
    }


    public void MuteAll()
    {
        _soundManager.MMSoundManager.MuteMaster();
    }

    public void UnmuteAll()
    {
        _soundManager.MMSoundManager.UnmuteMaster();
    }

    private void Subscribe()
    {
        Settings.MasterOn.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.MasterOn = value);

        Settings.UiOn.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.UIOn = value);
        Settings.SfxOn.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.SfxOn = value);
        Settings.MusicOn.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.MasterOn = value);

        Settings.MasterVolume.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.MasterVolume = value);
        Settings.UiVolume.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.UIVolume = value);
        Settings.SfxVolume.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.SfxVolume = value);
        Settings.MusicVolume.Subscribe(value => _soundManager.MMSoundManager.settingsSo.Settings.MusicVolume = value);
    }
}
