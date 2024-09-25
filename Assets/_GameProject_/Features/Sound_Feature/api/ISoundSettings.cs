using UnityEngine;

namespace WKosArch.Sound_Feature
{

    public interface ISoundSettings
    {
        ISoundSettingsDataProxy ValueProxy { get; }
    }

    public interface IUiSounds : ISounds
    {
        
    }

    public interface SfxSounds : ISounds
    {

    }

    public interface IMusicSounds : ISounds
    {

    }

    public interface ISounds
    {
        void Play<TSound>() where TSound : SoundSO/*, new()*/;
    }

    public class SoundSO : ScriptableObject
    {

    }
}