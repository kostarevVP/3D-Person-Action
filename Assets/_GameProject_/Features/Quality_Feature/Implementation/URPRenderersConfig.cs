using UnityEngine;
using System.Collections.Generic;

namespace WKosArch.Quality_Feature
{
    [CreateAssetMenu(fileName = "URPRenderersConfig", menuName = "Game/Configs/QualityConfig/URPRenderersConfig")]
    public class URPRenderersConfig : ScriptableObject
    {
        public List<URPRenderersConfigMapping> URPRenderersConfigMapping; 
   }
}
