using System;

namespace WKosArch.UI_Feature
{
    [Serializable]
    public enum UILayer
    {
        UnderBase = 0,
        Base = 50,
        UnderPopup = 100,
        Popup = 150,
        OverPopup = 200,
    }
}