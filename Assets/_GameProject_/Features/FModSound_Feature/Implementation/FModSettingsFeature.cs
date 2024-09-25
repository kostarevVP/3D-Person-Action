using FMOD;
using FMOD.Studio;
using FMODUnity;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using WKosArch.Sound_Feature;

namespace FMod_Feature
{
    public class FModSettingsFeature
    {
        public float MasterVolume => _masterBus.getVolume(out var value) == RESULT.OK ? value : 0;
        public bool MasterMute => _masterBus.getMute(out var value) == RESULT.OK ? value : false;
        public float MusicVolume => _musicBus.getVolume(out var value) == RESULT.OK ? value : 0;
        public bool MusicMute => _musicBus.getMute(out var value) == RESULT.OK ? value : false;
        public float SfxVolume => _sfxBus.getVolume(out var value) == RESULT.OK ? value : 0;
        public bool SfxMute => _sfxBus.getMute(out var value) == RESULT.OK ? value : false;
        public float UiVolume => _uiBus.getVolume(out var value) == RESULT.OK ? value : 0;
        public bool UiMute => _uiBus.getMute(out var value) == RESULT.OK ? value : false;

        public bool HapticsEnabled => false;

        public ISoundSettingsDataProxy SoundProgressDataProxy { get; private set; }

        private FModSoundConfig[] _fModSoundConfigs;

        private List<EventInstance> _eventInstances = new();

        private Bus _masterBus;
        private Bus _musicBus;
        private Bus _sfxBus;
        private Bus _uiBus;

        private SoundData[] _soundDatas;


        public FModSettingsFeature(FModEventsConfig fModEventsConfig, SoundSettingsData soundProgressData)
        {
            _fModSoundConfigs = fModEventsConfig.FModSoundConfigs;

            InitializeBus();
            InitializeSoundData();
            SoundProgressDataProxy = new SoundSettingsDataProxy(soundProgressData);
            Subscribe();
        }

        private void Subscribe()
        {
            SoundProgressDataProxy.MasterVolume.Subscribe(value => { _musicBus.setVolume(value); });
        }

        private void InitializeBus()
        {
            _masterBus = RuntimeManager.GetBus("bus:/");
            _musicBus = RuntimeManager.GetBus("bus:/Music");
            _sfxBus = RuntimeManager.GetBus("bus:/SFX");
            _uiBus = RuntimeManager.GetBus("bus:/UI");
        }

        #region ISoundFeature

        public void MuteAll() => _masterBus.setMute(true);

        public void MuteMusic() => _musicBus.setMute(true);

        public void MuteSfx() => _sfxBus.setMute(true);

        public void MuteUI() => _sfxBus.setMute(true);

        public void SetVolumeMusic(float value) => _musicBus.setVolume(value);

        public void SetVolumeSfx(float value) => _sfxBus.setVolume(value);

        public void SetVolumeUI(float value) => _uiBus.setVolume(value);

        public void SwitchHaptic(bool isEnabled) =>
            Log.PrintYellow($"NEED TO EMPLEMENT SwitchHaptic in FModFeature, value now = {isEnabled}");

        public void UnmuteAll() => _masterBus.setMute(false);

        public void UnmuteMusic() => _musicBus.setMute(false);

        public void UnmuteSfx() => _sfxBus.setMute(false);

        public void UnmuteUI() => _uiBus.setMute(false);
        #endregion


        #region ToTheFuture
        public void PlayOneShot(EventReferenceName eventName, Vector3 worldPosition)
        {
            var eventReference = GetReferenceFromName(eventName);
            PlayOneShot(eventReference, worldPosition);
        }

        public EventInstance CreateInstance(EventReferenceName eventName)
        {
            var eventReference = GetReferenceFromName(eventName);

            return CreateInstance(eventReference);
        }

        private void PlayOneShot(EventReference sound, Vector3 worldPosition)
        {
            RuntimeManager.PlayOneShot(sound, worldPosition);
        }

        private EventInstance CreateInstance(EventReference eventReference)
        {
            var eventInstance = RuntimeManager.CreateInstance(eventReference);
            _eventInstances.Add(eventInstance);
            return eventInstance;
        }

        private EventReference GetReferenceFromName(EventReferenceName eventName)
        {
            EventReference eventReference = new EventReference();

            //if (_fModEventsConfig.VehicleEventNameMap.TryGetValue(eventName, out eventReference))
            //{
            //    return eventReference;
            //}
            //else
            //{
            //    Log.PrintError($"Cant find {eventName} in EventReferenceMap");
            //    throw new System.NotImplementedException();
            //}

            return eventReference;
        }
        #endregion

        public FModSoundConfig GetSoundById(int id)
        {
            foreach (var soundConfig in _fModSoundConfigs)
            {
                if (soundConfig.Id == id)
                {
                    return soundConfig;
                }
            }
            throw new ApplicationException($"Sound with id {id} not found");
        }
        public EventDescription GetEventDefinition(FModSoundConfig soundDefinition)
        {
            var soundData = _soundDatas[soundDefinition.RuntimeIndex];
            if (!soundData.EventDescription.isValid())
                throw new ApplicationException($"Event {_fModSoundConfigs[soundDefinition.RuntimeIndex].Name} not valid");
            return soundData.EventDescription;
        }

        public PARAMETER_DESCRIPTION GetFloatParameterDescription(
            FModSoundConfig soundDefinition, FloatParameterDefinition parameter)
        {
            var soundData = _soundDatas[soundDefinition.RuntimeIndex];

            var parameterIndex = parameter.RuntimeIndex;
            if (soundData.FloatParameterDefinitions.Length <= parameterIndex)
                throw new ApplicationException($"Event {_fModSoundConfigs[soundDefinition.RuntimeIndex].Name} don't " +
                                               $"have {parameterIndex + 1} parameters");

            var parameterDefinition = soundData.FloatParameterDefinitions[parameterIndex];
            return parameterDefinition;
        }

        public void InitializeSoundData()
        {
            _soundDatas = new SoundData[_fModSoundConfigs.Length];
            for (var index = 0; index < _fModSoundConfigs.Length; index++)
            {
                var soundDefinition = _fModSoundConfigs[index];

                soundDefinition.RuntimeIndex = index;

                ref var data = ref _soundDatas[index];

                data.EventDescription = RuntimeManager.GetEventDescription(soundDefinition.Name);

                data.FloatParameterDefinitions = new PARAMETER_DESCRIPTION[soundDefinition.FloatParameters.Length];
                for (int i = 0; i < soundDefinition.FloatParameters.Length; i++)
                {
                    ref var parameterDefinition = ref data.FloatParameterDefinitions[i];
                    var result = data.EventDescription
                                     .getParameterDescriptionByName(soundDefinition.FloatParameters[i].Name, out parameterDefinition);
                    if (result != RESULT.OK)
                    {
                        UnityEngine.Debug.LogError($"Event \"{soundDefinition.Name}\" parameter \"{soundDefinition.FloatParameters[i].Name}\" not found");
                    }

                    soundDefinition.FloatParameters[i].RuntimeIndex = i;
                }
            }
        }

        public void Dispose()
        {
            foreach (var eventInstance in _eventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();
            }
        }

        private void LoadSoundProgressData(GameProgressData progress)
        {
            var soundProgress = progress.GlobalProgress.SoundProgressData;

            _masterBus.setVolume(soundProgress.MasterVolume);
            _masterBus.setMute(soundProgress.MasterOn);
            _musicBus.setVolume(soundProgress.MusicVolume);
            _musicBus.setMute(soundProgress.MusicOn);
            _sfxBus.setVolume(soundProgress.SfxVolume);
            _sfxBus.setMute(soundProgress.SfxOn);
            _uiBus.setVolume(soundProgress.UiVolume);
            _uiBus.setMute(soundProgress.UiOn);
        }

        private void SaveSoundProgressData(GameProgressData progress)
        {
            var soundProgress = progress.GlobalProgress.SoundProgressData;

            _masterBus.getVolume(out soundProgress.MasterVolume);
            _masterBus.getMute(out soundProgress.MasterOn);
            _musicBus.getVolume(out soundProgress.MusicVolume);
            _musicBus.getMute(out soundProgress.MusicOn);
            _sfxBus.getVolume(out soundProgress.SfxVolume);
            _sfxBus.getMute(out soundProgress.SfxOn);
            _uiBus.getVolume(out soundProgress.UiVolume);
            _uiBus.getMute(out soundProgress.UiOn);
        }

        public void SaveProgress(GameProgressData progress)
        {
            SaveSoundProgressData(progress);
        }

        public void LoadProgress(GameProgressData progress)
        {
            LoadSoundProgressData(progress);
        }

        private struct SoundData
        {
            public EventDescription EventDescription;
            public PARAMETER_DESCRIPTION[] FloatParameterDefinitions;
        }
    }
}

