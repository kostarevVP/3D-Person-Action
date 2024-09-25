using Assets.Game.Services.ProgressService.api;

namespace WKosArch.Sound_Feature
{
    public class SoundFeature : ISoundSettings
    {
        public SoundManager SoundManager { get; private set; }

        public float MasterVolume => SoundManager.MMSoundManager.settingsSo.Settings.MasterVolume;
        public bool MasterMute => !SoundManager.MMSoundManager.settingsSo.Settings.MasterOn;

        public float MusicVolume => SoundManager.MMSoundManager.settingsSo.Settings.MusicVolume;

        public bool MusicMute => !SoundManager.MMSoundManager.settingsSo.Settings.MusicOn;

        public float SfxVolume => SoundManager.MMSoundManager.settingsSo.Settings.SfxVolume;

        public bool SfxMute => !SoundManager.MMSoundManager.settingsSo.Settings.SfxOn;

        public float UiVolume => SoundManager.MMSoundManager.settingsSo.Settings.UIVolume;

        public bool UiMute => !SoundManager.MMSoundManager.settingsSo.Settings.UIOn;

        public bool HapticsEnabled => SoundManager.HapticReceiver.hapticsEnabled;

        public ISoundSettingsDataProxy ValueProxy {  get; private set; }
        public SoundFeature(SoundSettingsData soundProgressData)
        {
            SoundManager = SoundManager.CreateInstance();
            ValueProxy = new SoundSettingsDataProxy(soundProgressData);
        }

        public void MuteMusic() => 
            SoundManager.MMSoundManager.MuteMusic();

        public void MuteSfx() => 
            SoundManager.MMSoundManager.MuteSfx();

        public void MuteUI() => 
            SoundManager.MMSoundManager.MuteUI();

        public void SetVolumeMusic(float value) => 
            SoundManager.MMSoundManager.SetVolumeMusic(value);

        public void SetVolumeSfx(float value) => 
            SoundManager.MMSoundManager.SetVolumeSfx(value);

        public void SetVolumeUI(float value) => 
            SoundManager.MMSoundManager.SetVolumeUI(value);

        public void UnmuteMusic() => 
            SoundManager.MMSoundManager.UnmuteMusic();

        public void UnmuteSfx() => 
            SoundManager.MMSoundManager.UnmuteSfx();

        public void UnmuteUI() => 
            SoundManager.MMSoundManager.UnmuteUI();

        public void MuteAll() => 
            SoundManager.MMSoundManager.MuteMaster();

        public void UnmuteAll() => 
            SoundManager.MMSoundManager.UnmuteMaster();

        public void SwitchHaptic(bool isEnabled) => 
            SoundManager.HapticReceiver.hapticsEnabled = isEnabled;



        public void SaveProgress(GameProgressData progress)
        {

        }

        public void LoadProgress(GameProgressData progress)
        {

        }
    }
}