using FMOD.Studio;
using FMod_Feature;
using FMODUnity;
using R3;
using UnityEngine.InputSystem.Haptics;
using WKosArch.Sound_Feature;

namespace FModSound_Feature.Implementation
{
    public class FModSoundFeature : ISoundFeature
    {

        public ISoundSettings Settings { get; private set; }

        public IUiSounds UiSounds { get; }

        public SfxSounds SfxSounds { get; }

        public IMusicSounds MusicSounds { get; }

        public IHaptics Haptics { get; }

        private FModListener _fModListener;

        private bool _isMute;

        private Bus _masterBus;
        private Bus _musicBus;
        private Bus _sfxBus;
        private Bus _uiBus;


        public FModSoundFeature(SoundSettingsData soundProgressData)
        {

            Settings = new SoundSettings(new SoundSettingsDataProxy(soundProgressData));

            InitializeBus();
            Subscribe();
            _fModListener = FModListener.CreateInstance();
        }

        public void MuteAll()
        {
            _isMute = true;
            _masterBus.setMute(true);
        }

        public void UnmuteAll()
        {
            _isMute = false;
            _masterBus.setMute(false);
        }

        private void InitializeBus()
        {
            _masterBus = RuntimeManager.GetBus("bus:/");
            _musicBus = RuntimeManager.GetBus("bus:/Music");
            _sfxBus = RuntimeManager.GetBus("bus:/SFX");
            _uiBus = RuntimeManager.GetBus("bus:/UI");
        }

        private void Subscribe()
        {
            Settings.ValueProxy.MasterOn.Subscribe(value => _masterBus.setMute(!value));
            Settings.ValueProxy.UiOn.Subscribe(value => _uiBus.setMute(!value));
            Settings.ValueProxy.SfxOn.Subscribe(value => _sfxBus.setMute(!value));
            Settings.ValueProxy.MusicOn.Subscribe(value => _musicBus.setMute(!value));

            Settings.ValueProxy.MasterVolume.Subscribe(value => _masterBus.setVolume(value));
            Settings.ValueProxy.UiVolume.Subscribe(value => _uiBus.setVolume(value));
            Settings.ValueProxy.SfxVolume.Subscribe(value => _sfxBus.setVolume(value));
            Settings.ValueProxy.MusicVolume.Subscribe(value => _musicBus.setVolume(value));

        }
    }

    public class SoundSettings : ISoundSettings
    {
        public ISoundSettingsDataProxy ValueProxy { get; private set; }

        public SoundSettings(ISoundSettingsDataProxy soundProgressDataProxy)
        {
            ValueProxy = soundProgressDataProxy;
        }
    }
}