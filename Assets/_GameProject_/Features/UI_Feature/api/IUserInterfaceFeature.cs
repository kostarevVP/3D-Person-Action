using System;
using System.Collections.Generic;
using WKosArch.Domain.Features;
using WKosArch.MVVM;

namespace WKosArch.UI_Feature
{
    public interface IUserInterfaceFeature : IFeature, IDisposable
    {
        void Build(Dictionary<string, View> viewModelToViewMap);

        /// <summary>
        /// Create or get cached any UiViewModel and Show View from WindowViewModel, HudViewModel, WidgetViewModel
        /// </summary>
        /// <typeparam name="TUiViewModel"></typeparam>
        /// <param name="hideCurrentWindow"></param>
        /// <param name="hideForced"> only WindowViewModel animation for Close - true PlayAnimation or false without animation</param>
        /// <param name="openForced"> animation for Open - true PlayAnimation or false without animation</param>
        void Show<TUiViewModel>(bool hideCurrentWindow = true, bool hideForced = false, bool openForced = false) where TUiViewModel : UiViewModel, new();
        void Back(bool hideCurrentWindow = true, bool forced = false);
        void CloseAllWindowInStack(bool withHomeWindow = false);

        void HideHud<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : HudViewModel;
        void CloseHud<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : HudViewModel;
        void HideWidget<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : WidgetViewModel;
        void CloseWidget<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : WidgetViewModel;
        void ShowAllHudInStack(bool openForced = false);
        void CloseAllHudInStack(bool forcedHide = false);
        void HideAllHudInStack(bool forcedHide = false);
    }
}