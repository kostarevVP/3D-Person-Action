using FMOD.Studio;
using System;
using WKosArch.Sound_Feature;

namespace WKosArch.FModSound_Feature
{
    public class FModSound : ISound
    {
        public event Action<FModSound> OnStopPlaying;
        public SoundType SoundType;

        public EventInstance EventInstance;
        public PARAMETER_ID[] IDs;

        public void StopPlaying()
        {
            OnStopPlaying?.Invoke(this);
        }
    }
}
