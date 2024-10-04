using FMOD;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;
using Unity.Assertions;
using System;
using System.Linq;
using System.Collections.Generic;
using WKosArch.Sound_Feature;

namespace WKosArch.FModSound_Feature
{
    public class FModSoundFactory : ISounds<FModSound>, ISoundConfigLoader, IDisposable
    {
        private const float MAX_VOLUME = 1f;

        private Dictionary<SoundType, FModSoundConfig> _fModSoundSceneConfigMap = new();
        private Dictionary<SoundType, FModSoundConfig> _fModSoundsGlobalConfigMap = new();

        private List<FModSound> _cashedSceneSounds = new();
        private List<FModSound> _cashedGlobalSounds = new();


        public void LoadSoundSceneConfigMap(Dictionary<SoundType, SoundConfig> soundsConfigMap)
        {
            ClearSceneCache();

            foreach (var kvp in soundsConfigMap)
            {
                _fModSoundSceneConfigMap.Add(kvp.Key, kvp.Value as FModSoundConfig);
            }
        }

        public void LoadSoundGlobalConfigMap(Dictionary<SoundType, SoundConfig> soundConfigMap)
        {
            _fModSoundsGlobalConfigMap.Clear();
            _cashedGlobalSounds.Clear();

            foreach (var kvp in soundConfigMap)
            {
                _fModSoundsGlobalConfigMap.Add(kvp.Key, kvp.Value as FModSoundConfig);
            }
        }


        public void PlayOnce(SoundType soundType, Vector3 position = new Vector3())
        {
            RuntimeManager.PlayOneShot(GetSoundConfig(soundType).Name);
        }

        public void PlayOnceAttached(SoundType soundType, GameObject target)
        {
            RuntimeManager.PlayOneShotAttached(GetSoundConfig(soundType).Name, target);
        }

        public FModSound Play(SoundType soundType, Vector3 position = new Vector3(), float volume = MAX_VOLUME)
        {
            var config = GetSoundConfig(soundType);
            EventDescription eventDescription = RuntimeManager.GetEventDescription(config.Name);
            FModParameter[] parameters = config.Parameters;

            RESULT result = eventDescription.createInstance(out EventInstance instance);
            Assert.IsTrue(result == RESULT.OK, "createInstance error: " + result);

            FModSound sound = new FModSound
            {
                EventInstance = instance,
                IDs = new PARAMETER_ID[parameters.Length]
            };

            result = instance.start();
            Assert.IsTrue(result == RESULT.OK, "start error: " + result);

            instance.setVolume(volume);

            instance.set3DAttributes(position.To3DAttributes());

            for (var i = 0; i < parameters.Length; i++)
            {
                string name = _fModSoundSceneConfigMap[soundType].Parameters[i].Parameter.Name;

                RESULT isOk = eventDescription.getParameterDescriptionByName(name, out var parameterDescription);
                Assert.IsTrue(isOk == RESULT.OK, "getParameterDescriptionByName error: " + result);

                sound.IDs[i] = parameterDescription.id;

                instance.setParameterByID(sound.IDs[i], parameters[i].Parameter.Value);
            }

            sound.OnStopPlaying += DeleteFromCache;

            AddSoundToCache(soundType, sound);

            return sound;
        }

        public FModSound PlayAttached(SoundType soundType, GameObject target, float volume = MAX_VOLUME)
        {
            var sound = Play(soundType);

            sound.EventInstance.setVolume(volume);
            sound.EventInstance.set3DAttributes(target.To3DAttributes());

            return sound;
        }

        public void SetParameters(SoundType soundType, float[] parameters)
        {
            var sound = GetSoundFromCache(soundType);
            SetParameters(sound, parameters);
        }

        public void SetParameters(FModSound fModSound, float[] parameters)
        {
            if (fModSound == null) return;

            var length = Math.Min(fModSound.IDs.Length, parameters.Length);

            for (var i = 0; i < length; i++)
            {
                fModSound.EventInstance.setParameterByID(fModSound.IDs[i], parameters[i]);
            }
        }

        public void SetParameter(SoundType soundType, int index, float value)
        {
            var sound = GetSoundFromCache(soundType);
            SetParameter(sound, index, value);
        }

        public void SetParameter(FModSound fModSound, int index, float value)
        {
            if (fModSound == null) return;

            if (fModSound.IDs.Length - 1 < index) return;

            fModSound.EventInstance.setParameterByID(fModSound.IDs[index], value);
        }

        public void SetVolume(SoundType soundType, float volume)
        {
            var sound = GetSoundFromCache(soundType);
            SetVolume(sound, volume);
        }

        public void SetVolume(FModSound fModSound, float volume)
        {
            if (fModSound == null) return;

            fModSound.EventInstance.setVolume(volume);
        }

        public void Pause(SoundType soundType)
        {
            var sound = GetSoundFromCache(soundType);
            Pause(sound);
        }
        public void Pause(FModSound sound)
        {
            if (sound != null && sound.EventInstance.isValid())
            {
                sound.EventInstance.setPaused(true);
            }
        }

        public void UnPause(SoundType soundType)
        {
            var sound = GetSoundFromCache(soundType);
            UnPause(sound);
        }
        public void UnPause(FModSound sound)
        {
            if (sound != null && sound.EventInstance.isValid())
            {
                sound.EventInstance.setPaused(false);
            }
        }

        public void Stop(SoundType soundType, bool immediate = false)
        {
            var sound = GetSoundFromCache(soundType);
            Stop(sound, immediate);
        }

        public void Stop(FModSound sound, bool immediate = false)
        {
            if (sound != null && sound.EventInstance.isValid())
            {
                if (immediate)
                    sound.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                else
                    sound.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                sound.EventInstance.release();

                sound.StopPlaying();
            }
        }


        public void ClearSceneCache()
        {
            foreach (var sound in _cashedSceneSounds)
            {
                Stop(sound, immediate: true);
            }

            _fModSoundSceneConfigMap.Clear();
            _cashedSceneSounds.Clear();
        }

        public void ClearGlobalCache()
        {
            foreach (var sound in _cashedGlobalSounds)
            {
                Stop(sound, immediate: true);
            }

            _fModSoundsGlobalConfigMap.Clear();
            _cashedGlobalSounds.Clear();
        }

        public void Dispose()
        {
            ClearSceneCache();
            ClearGlobalCache();
        }


        private void AddSoundToCache(SoundType soundType, FModSound sound, float volume = 1f)
        {
            if (_fModSoundSceneConfigMap.TryGetValue(soundType, out var sceneConfig))
            {
                sound.SoundType = soundType;
                _cashedSceneSounds.Add(sound);
            }
            else if (_fModSoundsGlobalConfigMap.TryGetValue(soundType, out var globalConfig))
            {
                sound.SoundType = soundType;
                _cashedGlobalSounds.Add(sound);
            }
            else
            {
                throw new Exception($"SoundType {soundType} not found in _fModSoundSceneConfigMap or in _fModSoundsGlobalConfigMap");
            }
        }

        private void DeleteFromCache(FModSound sound)
        {
            sound.OnStopPlaying -= DeleteFromCache;
            RemoveFromCashedList(sound);
        }

        private FModSoundConfig GetSoundConfig(SoundType soundType)
        {
            if (_fModSoundSceneConfigMap.TryGetValue(soundType, out var sceneConfig))
            {
                return sceneConfig;
            }
            else if (_fModSoundsGlobalConfigMap.TryGetValue(soundType, out var globalConfig))
            {
                return globalConfig;
            }
            else
            {
                throw new Exception($"SoundType {soundType} not found in _fModSoundSceneConfigMap or in _fModSoundsGlobalConfigMap");
            }
        }

        private FModSound GetSoundFromCache(SoundType soundType)
        {
            var sceneSound = _cashedSceneSounds.FirstOrDefault(sound => sound.SoundType == soundType);

            if (sceneSound != null)
            {
                return sceneSound;
            }

            var globalSound = _cashedGlobalSounds.FirstOrDefault(sound => sound.SoundType == soundType);

            if (globalSound != null)
            {
                return globalSound;
            }

            throw new Exception($"SoundType {soundType} not found in cached scene or global sounds.");
        }

        private void RemoveFromCashedList(FModSound sound)
        {
            if (_cashedGlobalSounds.Contains(sound))
            {
                _cashedGlobalSounds.Remove(sound);
            }
            else if (_cashedSceneSounds.Contains(sound))
            {
                _cashedSceneSounds.Remove(sound);
            }
            else
            {
                throw new Exception($"SoundType {sound} not found in _cashedSceneSounds or _cashedGlobalSounds.");
            }
        }

    }


    public static class FModSoundExtensions
    {
        public static void Pause(this FModSound sound)
        {
            if (sound != null && sound.EventInstance.isValid())
            {
                sound.EventInstance.setPaused(true);
            }
        }

        public static void UnPause(this FModSound sound)
        {
            if (sound != null && sound.EventInstance.isValid())
            {
                sound.EventInstance.setPaused(false);
            }
        }

        public static void SetParameter(this FModSound fModSound, int index, float value)
        {
            if (fModSound == null) return;

            if (fModSound.IDs.Length - 1 < index) return;

            fModSound.EventInstance.setParameterByID(fModSound.IDs[index], value);
        }

        public static void SetParameters(this FModSound fModSound, float[] parameters)
        {
            if (fModSound == null) return;

            var length = Math.Min(fModSound.IDs.Length, parameters.Length);

            for (var i = 0; i < length; i++)
            {
                fModSound.EventInstance.setParameterByID(fModSound.IDs[i], parameters[i]);
            }
        }

        public static void SetVolume(this FModSound fModSound, float volume)
        {
            if (fModSound == null) return;

            fModSound.EventInstance.setVolume(volume);
        }

        public static void Stop(this FModSound sound, bool immediate = false)
        {
            if (sound != null && sound.EventInstance.isValid())
            {
                if (immediate)
                    sound.EventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                else
                    sound.EventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

                sound.StopPlaying();

                sound.EventInstance.release();
            }
        }
    }
}
