using WKosArch.Domain.Features;
using System.Collections.Generic;
using UnityEngine.Rendering;
using WKosArch.GameState_Feature;
using WKosArch.UI_Feature;
using WKosArch.Quality_Feature;
using WKosArch.Sound_Feature;
using WKosArch.MVVM;

namespace WKosArch.Configs_Feature
{
    public interface IConfigsFeature : IFeature
	{
        GameStateConfig GameStateConfig { get; }
        GameSettingsStateConfig GameSettingsStateConfig { get; }
        Dictionary<RenderingQuality, RenderPipelineAsset> RenderQualityConfigMap { get; }
        Dictionary<string, Dictionary<SoundType, SoundConfig>> SceneSoundConfigsMap { get; }
        Dictionary<string, Dictionary<string, View>> SceneUiConfigMap { get; }
    } 
}