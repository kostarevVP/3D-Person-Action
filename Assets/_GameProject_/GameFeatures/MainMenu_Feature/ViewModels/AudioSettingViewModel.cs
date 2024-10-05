using R3;
using UnityEngine;
using WKosArch.DependencyInjection;
using WKosArch.Extensions;
using WKosArch.FModSound_Feature;
using WKosArch.GameState_Feature;
using WKosArch.SceneManagement_Feature;

namespace WKosArch.Sound_Feature
{
    public class AudioSettingViewModel : WindowViewModel
    {
        private const float MIN_VOLUME = 0.0001f;
        private const float MAX_VOLUME = 1f;


        public Observable<float> MusicVolume => _soundDataProxy.MusicVolume;
        public Observable<float> SFXVolume => _soundDataProxy.SfxVolume;
        public Observable<float> UiVolume => _soundDataProxy.UiVolume;

        public Observable<bool> MusicOn => _soundDataProxy.MusicOn;
        public Observable<bool> SFXOn => _soundDataProxy.SfxOn;
        public Observable<bool> UIOn => _soundDataProxy.UiOn;

        public Observable<bool> HapticToggle => _hapticEnabled;


        private ReactiveProperty<bool> _hapticEnabled = new();

        private SoundSettingsStateProxy _soundDataProxy => DiContainer.Resolve<ISoundFeature<FModSound>>().Settings;

        private float _previousMusicVolumeValue;
        private float _previousSFXVolumeValue;
        private float _previousUIVolumeValue;
        private bool _hapticToggle;


        public override void Subscribe()
        {
            base.Subscribe();
            InitializePreviousVolumeValues();
        }

        public void SetMusicValue(float value)
        {
            if (value <= MIN_VOLUME)
            {
                _previousMusicVolumeValue = MIN_VOLUME;
                _soundDataProxy.MusicVolume.OnNext(MIN_VOLUME);
                SwitchMusic(isEnabled: false);
            }
            else
            {
                if (!_soundDataProxy.MusicOn.Value)
                    SwitchMusic(isEnabled: true);

                _previousMusicVolumeValue = _soundDataProxy.MusicVolume.Value;
                _soundDataProxy.MusicVolume.OnNext(value);
            }
        }
        public void SetSFXValue(float value)
        {
            if (value <= MIN_VOLUME)
            {
                _previousSFXVolumeValue = _soundDataProxy.SfxVolume.Value;
                _soundDataProxy.SfxVolume.OnNext(MIN_VOLUME);
                SwitchSFX(isEnabled: false);
            }
            else
            {
                if (!_soundDataProxy.SfxOn.Value)
                    SwitchSFX(isEnabled: true);

                _previousSFXVolumeValue = _soundDataProxy.SfxVolume.Value;
                _soundDataProxy.SfxVolume.OnNext(value);
            }
        }
        public void SetUiValue(float value)
        {
            if (value <= MIN_VOLUME)
            {
                _soundDataProxy.UiVolume.OnNext(MIN_VOLUME);
                SwitchUI(isEnabled: false);
            }
            else
            {
                if (!_soundDataProxy.UiOn.Value)
                    SwitchUI(isEnabled: true);

                _previousUIVolumeValue = _soundDataProxy.UiVolume.Value;
                _soundDataProxy.UiVolume.OnNext(value);
            }
        }

        public void SwitchMusic(bool isEnabled)
        {
            _soundDataProxy.MusicOn.OnNext(isEnabled);

            if (isEnabled)
            {
                if (_soundDataProxy.MusicVolume.Value <= _previousMusicVolumeValue)
                    _soundDataProxy.MusicVolume.OnNext(_previousMusicVolumeValue);

                if (_soundDataProxy.MusicVolume.Value == MIN_VOLUME)
                    _soundDataProxy.MusicVolume.OnNext(MAX_VOLUME);
            }
            else
            {
                _soundDataProxy.MusicVolume.OnNext(MIN_VOLUME);
            }
        }
        public void SwitchSFX(bool isEnabled)
        {
            _soundDataProxy.SfxOn.Value = isEnabled;

            if (isEnabled)
            {

                if (_soundDataProxy.SfxVolume.Value <= _previousSFXVolumeValue)
                    _soundDataProxy.SfxVolume.OnNext(_previousSFXVolumeValue);

                if (_soundDataProxy.SfxVolume.Value == MIN_VOLUME)
                    _soundDataProxy.SfxVolume.OnNext(MAX_VOLUME);
            }
            else
            {
                _soundDataProxy.SfxVolume.OnNext(MIN_VOLUME);
            }

            _soundDataProxy.SfxOn.OnNext(isEnabled);
        }
        public void SwitchUI(bool isEnabled)
        {
            _soundDataProxy.UiOn.Value = isEnabled;

            if (isEnabled)
            {
                if (_soundDataProxy.UiVolume.Value <= _previousUIVolumeValue)
                    _soundDataProxy.UiVolume.OnNext(_previousUIVolumeValue);

                if (_soundDataProxy.UiVolume.Value == MIN_VOLUME)
                    _soundDataProxy.UiVolume.OnNext(MAX_VOLUME);
            }
            else
            {
                _soundDataProxy.UiVolume.OnNext(MIN_VOLUME);
            }

            _soundDataProxy.UiOn.OnNext(isEnabled);
        }

        public void SwitchHaptic(bool isEnabled)
        {
            _hapticEnabled.OnNext(isEnabled);
            StaticDI.Resolve<ISceneManagementFeature>().LoadScene(2);
        }


        private void InitializePreviousVolumeValues()
        {
            _previousMusicVolumeValue = _soundDataProxy.MusicVolume.Value;
            _previousSFXVolumeValue = _soundDataProxy.SfxVolume.Value;
            _previousUIVolumeValue = _soundDataProxy.UiVolume.Value;
        }
    }
}