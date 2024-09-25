using System;

namespace WKosArch.MVVM
{
    [Serializable]
    public class ViewModelToViewMapping
    {
        public string ViewModelTypeFullName;
        public View PrefabView;
    }
}