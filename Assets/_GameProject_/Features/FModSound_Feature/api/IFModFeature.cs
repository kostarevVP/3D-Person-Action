using Assets.Game.Services.ProgressService.api;
using FMOD.Studio;
using System;
using WKosArch.Sound_Feature;

namespace FMod_Feature
{
    public interface IFModFeature : ISavedProgress, IDisposable
    {

        EventDescription GetEventDefinition(FModSoundConfig soundDefinition);
        PARAMETER_DESCRIPTION GetFloatParameterDescription(FModSoundConfig soundDefinition, FloatParameterDefinition parameter);
        FModSoundConfig GetSoundById(int id);
    }
}