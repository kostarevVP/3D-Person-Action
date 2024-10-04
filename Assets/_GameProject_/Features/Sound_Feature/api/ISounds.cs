using System;
using System.Collections.Generic;
using UnityEngine;

namespace WKosArch.Sound_Feature
{
    public interface ISounds<TSound> where TSound : ISound
    {
        void PlayOnce(SoundType soundType, Vector3 position = new Vector3());
        void PlayOnceAttached(SoundType soundType, GameObject target);

        TSound Play(SoundType soundType, Vector3 position = new Vector3(), float volume = 1f);
        TSound PlayAttached(SoundType soundType, GameObject target, float volume = 1f);

        void SetVolume(TSound sound, float volume);
        void SetVolume(SoundType soundType, float volume);

        void Pause(SoundType soundType);
        void Pause(TSound sound);
        void UnPause(SoundType soundType);
        void UnPause(TSound sound);
        void Stop(SoundType soundType, bool immediate = false);
        void Stop(TSound sound, bool immediate = false);

        void SetParameter(SoundType soundType, int index, float value);
        void SetParameter(TSound sound, int index, float value);
        void SetParameters(SoundType soundType, float[] parameters);
        void SetParameters(TSound sound, float[] parameters);

        void ClearSceneCache();
    }

    public interface ISound
    {

    }

    public interface ISoundConfigLoader
    {
        void LoadSoundSceneConfigMap(Dictionary<SoundType, SoundConfig> soundsConfigMap);
        void LoadSoundGlobalConfigMap(Dictionary<SoundType, SoundConfig> soundConfigMap);
    }
}