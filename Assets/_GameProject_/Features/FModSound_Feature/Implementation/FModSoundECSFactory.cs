using System;
using System.Linq;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using WKosArch.Sound_Feature;

namespace WKosArch.FModSound_Feature
{
    public class FModSoundECSFactory : IFModSoundECSFactory, ISoundConfigLoader
    {
        private Dictionary<int, SoundData> _soundDataMap = new();
        private int _globalSoundDataCount;


        public EventDescription GetEventDescription(int id)
        {
            if (_soundDataMap.TryGetValue(id, out var soundData))
                return soundData.EventDescription;
            else
                throw new Exception($"Not found SoundData for {id}");
        }
        public PARAMETER_DESCRIPTION[] GetParameterDescriptions(int id)
        {
            if (_soundDataMap.TryGetValue(id, out var soundData))
                return soundData.ParameterDefinitions;
            else
                throw new Exception($"Not found SoundData for {id}");
        }

        public void LoadSoundGlobalConfigMap(Dictionary<SoundType, SoundConfig> soundConfigMap)
        {
            _globalSoundDataCount = soundConfigMap.Count;
            AddToSoundDataMap(soundConfigMap);
        }
        public void LoadSoundSceneConfigMap(Dictionary<SoundType, SoundConfig> soundConfigMap)
        {
            RemoveSceneDataFromSoundDataMap();
            AddToSoundDataMap(soundConfigMap);
        }

        private void AddToSoundDataMap(Dictionary<SoundType, SoundConfig> soundConfigMap)
        {
            var soundsConfigs = GetFModSoundConfigs(soundConfigMap);
            foreach (var soundConfig in soundsConfigs)
            {
                if (!_soundDataMap.TryAdd(soundConfig.Id, new SoundData
                {
                    EventDescription = GetEventDescription(soundConfig),
                    ParameterDefinitions = GetParameterDescriptions(soundConfig),
                }))
                    UnityEngine.Debug.LogWarning($"You try ad {soundConfig.name} from SceneSoundsConfig although this config is added to the SoundsGlobalConfig");

                _soundDataMap.TrimExcess();
            }
        }
        private void RemoveSceneDataFromSoundDataMap()
        {
            List<int> keysToRemove = _soundDataMap.Keys.Skip(_globalSoundDataCount).ToList();

            foreach (var key in keysToRemove)
            {
                _soundDataMap.Remove(key);
            }
        }
        private FModSoundConfig[] GetFModSoundConfigs(Dictionary<SoundType, SoundConfig> soundConfigMap)
        {
            var fModSoundsConfig = new FModSoundConfig[soundConfigMap.Count];

            int index = 0;
            foreach (var kvp in soundConfigMap)
            {
                fModSoundsConfig[index] = kvp.Value as FModSoundConfig;
                index++;
            }

            return fModSoundsConfig;
        }
        private EventDescription GetEventDescription(FModSoundConfig soundConfig)
        {
            return RuntimeManager.GetEventDescription(soundConfig.Name);
        }
        private PARAMETER_DESCRIPTION[] GetParameterDescriptions(FModSoundConfig soundConfig)
        {
            var parameterDescriptions = new PARAMETER_DESCRIPTION[soundConfig.Parameters.Length];

            for (int i = 0; i < parameterDescriptions.Length; i++)
            {
                var result = GetEventDescription(soundConfig)
                                 .getParameterDescriptionByName(soundConfig.Parameters[i].Parameter.Name, out var parameterDefinition);
                if (result != RESULT.OK)
                {
                    UnityEngine.Debug.LogError($"Event \"{soundConfig.Name}\" parameter \"{soundConfig.Parameters[i].Parameter.Name}\" not found");
                }

                parameterDescriptions[i] = parameterDefinition;
            }

            return parameterDescriptions;
        }
    }
}
