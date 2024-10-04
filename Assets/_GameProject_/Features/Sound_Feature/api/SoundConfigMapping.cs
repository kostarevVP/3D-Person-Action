using System;
using WKosArch.GameState_Feature;

namespace WKosArch.Sound_Feature
{
    [Serializable]
	public class SoundConfigMapping : Mapping<SoundType, SoundConfig>
	{
        public SoundConfigMapping(SoundType key, SoundConfig value) : base(key, value)
        {
        }
    } 
}
