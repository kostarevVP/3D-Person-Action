using R3;
using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem.Haptics;
using WKosArch.GameState_Feature;
using WKosArch.Sound_Feature;

namespace WKosArch.FModSound_Feature
{
    public class FModSoundFeature : ISoundFeature<FModSound>, ISoundConfigLoader, IDisposable
    {
        public SoundSettingsStateProxy Settings { get; private set; }

        public ISounds<FModSound> UiSounds => _fModFactory;
        public ISounds<FModSound> SfxSounds => _fModFactory;
        public ISounds<FModSound> MusicSounds => _fModFactory;

        public IHaptics Haptics { get; }

        public FModListener FModListener => _fModListener;
        public bool IsMute => _isMute;

        private FModListener _fModListener;
        private FModSoundFactory _fModFactory;

        private CompositeDisposable _compositeDisposable = new();

        private bool _isMute;

        private Bus _masterBus;
        private Bus _musicBus;
        private Bus _sfxBus;
        private Bus _uiBus;



        public FModSoundFeature(SoundSettingsStateProxy soundSettingsStateProxy)
        {
            Settings = soundSettingsStateProxy;

            _fModFactory = new FModSoundFactory();

            _fModListener = FModListener.CreateInstance();

            InitializeBus();
            Subscribe();
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
            _compositeDisposable.Add(Settings.MasterOn.Subscribe(value => _masterBus.setMute(!value)));
            _compositeDisposable.Add(Settings.UiOn.Subscribe(value => _uiBus.setMute(!value)));
            _compositeDisposable.Add(Settings.SfxOn.Subscribe(value => _sfxBus.setMute(!value)));
            _compositeDisposable.Add(Settings.MusicOn.Subscribe(value => _musicBus.setMute(!value)));

            _compositeDisposable.Add(Settings.MasterVolume.Subscribe(value => _masterBus.setVolume(value)));
            _compositeDisposable.Add(Settings.UiVolume.Subscribe(value => _uiBus.setVolume(value)));
            _compositeDisposable.Add(Settings.SfxVolume.Subscribe(value => _sfxBus.setVolume(value)));
            _compositeDisposable.Add(Settings.MusicVolume.Subscribe(value => _musicBus.setVolume(value)));
        }

        public void LoadSoundGlobalConfigMap(Dictionary<SoundType, SoundConfig> soundConfigMap)
        {
            var soundConfigLoader = _fModFactory as ISoundConfigLoader;

            soundConfigLoader.LoadSoundGlobalConfigMap(soundConfigMap);
        }

        public void LoadSoundSceneConfigMap(Dictionary<SoundType, SoundConfig> soundConfigMap)
        {
            var soundConfigLoader = _fModFactory as ISoundConfigLoader;

            soundConfigLoader.LoadSoundSceneConfigMap(soundConfigMap);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}