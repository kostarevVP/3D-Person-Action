using System;
using WKosArch.GameState_Feature;

namespace WKosArch.MVVM
{
    [Serializable]
    public class ViewModelToViewMapping : Mapping<string, View>
    {
        public ViewModelToViewMapping(string key, View value) : base(key, value) { }
    }
}