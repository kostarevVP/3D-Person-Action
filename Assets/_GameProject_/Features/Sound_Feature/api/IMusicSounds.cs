using System.Diagnostics;

namespace WKosArch.Sound_Feature
{
    public interface IMusicSounds<TSound> : ISounds<TSound> where TSound : ISound
    {

    }
}