using System;
using UnityEngine;
using FMODUnity;
using WKosArch.Sound_Feature;

namespace WKosArch.FModSound_Feature
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Game/Configs/FMod/FModSoundSo")]
    public class FModSoundConfig : SoundConfig
    {
        public EventReference Name;
        public FModParameter[] Parameters;

        [NonSerialized]
        public int RuntimeIndex;
    }

    [Serializable]
    public class FModParameter
    {
        public ParamRef Parameter;

        [NonSerialized]
        public int RuntimeIndex;
    }
}
