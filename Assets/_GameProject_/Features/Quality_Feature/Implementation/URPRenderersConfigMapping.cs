using System;
using UnityEngine.Rendering;
using WKosArch.GameState_Feature;

namespace WKosArch.Quality_Feature
{
    [Serializable]
    public class URPRenderersConfigMapping : Mapping<RenderingQuality, RenderPipelineAsset>
    {
        public URPRenderersConfigMapping(RenderingQuality key, RenderPipelineAsset value) : base(key, value)
        {
        }
    }
}
