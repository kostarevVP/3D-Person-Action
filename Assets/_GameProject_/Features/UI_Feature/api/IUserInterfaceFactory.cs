using WKosArch.DependencyInjection;
using WKosArch.MVVM;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace WKosArch.UI_Feature
{
    public interface IUserInterfaceFactory : IDisposable
    {
        void BuildUiForScene(Dictionary<string, View> viewModelToViewMap);
        void Construct(DIContainer dIContainer, IUserInterfaceFeature userInterface);

        View GetOrCreateActiveView(UiViewModel viewModel, bool openForced = false, Transform root = null);
        UiViewModel GetOrCreateViewModel<TUiViewModel>() where TUiViewModel : UiViewModel, new();

        void Close(UiViewModel currentUiViewModel, bool forcedHide = false);
        void Hide(UiViewModel viewModel, bool forcedHide = false);
        void Close(string fullName, bool forcedHide = false);
        void Hide(string fullName, bool forcedHide = false);
    }
}