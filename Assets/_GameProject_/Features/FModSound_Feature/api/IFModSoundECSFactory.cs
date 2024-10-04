using FMOD.Studio;

namespace WKosArch.FModSound_Feature
{
    public interface IFModSoundECSFactory
    {
        EventDescription GetEventDescription(int id);
        PARAMETER_DESCRIPTION[] GetParameterDescriptions(int id);
    }
}
