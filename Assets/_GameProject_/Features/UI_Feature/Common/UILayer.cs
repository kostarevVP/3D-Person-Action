using System;

namespace WKosArch.Services.UIService.Common
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