using Assets._Game_.Services.UI_Service.Implementation;
using WKosArch.DependencyInjection;
using UnityEngine;
using WKosArch.Extentions;
using WKosArch.UIService.Views.Windows;

namespace WKosArch.Services.UIService.UI
{
    public class UserInterface : IUserInterface
    {
        public WindowViewModel FocusedWindowViewModel { get; private set; }

        private IUserInterfaceFactory _uiFactory;
        private ViewModelStack<ViewModelTreeNode> _windowStack = new();
        private ViewModelStack<ViewModelTreeNode> _hudStack = new();


        public UserInterface(IDiContainer container)
        {
            _uiFactory = UserInterfaceFactory.CreateInstance();
            _uiFactory.Construct(container, this);
        }

        public void Build(UISceneConfig config)
        {
            CloseAllWindowInStack(true);
            CloseAllHudInStack();
            _uiFactory.Build(config);
        }

        public void Show<TUiViewModel>(bool hideCurrentWindow = true, bool hideForced = false, bool openForced = false) where TUiViewModel : UiViewModel, new()
        {
            var isWindowViewModel = typeof(WKosArch.WindowViewModel).IsAssignableFrom(typeof(TUiViewModel));
            if (hideCurrentWindow && FocusedWindowViewModel != null && isWindowViewModel)
                _uiFactory.Close(FocusedWindowViewModel, hideForced);

            UiViewModel uiViewModel = _uiFactory.CreateOrGetViewModel<TUiViewModel>();
            _uiFactory.CreateOrGetActiveView(uiViewModel, openForced);

            AddViewModelStack(uiViewModel);
        }

        public void Back(bool closeCurrentWindow = true, bool forced = false)
        {
            var currentUiViewModel = _windowStack.Pop().UiViewModel;

            if (IsHomeWindowType(currentUiViewModel))
            {
                _windowStack.Push(new ViewModelTreeNode(currentUiViewModel));
                OpenGameCloseWindowPopup();
                return;
            }

            if (closeCurrentWindow)
            {
                _uiFactory.Close(currentUiViewModel);
            }

            var viewModelNode = _windowStack.Pop();

            if (viewModelNode != null)
            {
                var previousUiViewModel = viewModelNode.UiViewModel;
                _uiFactory.CreateOrGetActiveView(previousUiViewModel);

                AddViewModelStack(previousUiViewModel);
            }
        }

        public void CloseAllWindowInStack(bool includeHomeWindow = false)
        {
            int stackLength = _windowStack.Length;

            for (int i = stackLength - 1; i >= 0; i--)
            {
                var viewModel = _windowStack.Pop().UiViewModel;
                bool isHomeWindow = IsHomeWindowType(viewModel);

                if (isHomeWindow && !includeHomeWindow)
                {
                    _uiFactory.CreateOrGetActiveView(viewModel);
                    AddViewModelStack(viewModel);
                }
                else
                {
                    _uiFactory.Close(viewModel, forcedHide: i != stackLength - 1);
                }
            }
        }


        public void ShowAllHudInStack(bool openForced = false)
        {
            foreach (var hudViewModel in _hudStack.ViewModelQueue)
            {
                _uiFactory.CreateOrGetActiveView(hudViewModel.UiViewModel, openForced);
            }
        }
        public void HideAllHudInStack(bool hideForce = false)
        {
            foreach (var hudViewModel in _hudStack.ViewModelQueue)
            {
                _uiFactory.Close(hudViewModel.UiViewModel.GetType().FullName, hideForce);
            }
        }
        public void CloseAllHudInStack(bool forcedHide = false)
        {
            var stackLength = _hudStack.Length;

            for (int i = 0; i < stackLength; i++)
            {
                var currentWindowViewModel = _hudStack.Pop().UiViewModel;

                _uiFactory.Close(currentWindowViewModel, forcedHide);
            }
        }


        public void CloseHud<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : HudViewModel
        {
            var fullName = typeof(TUiViewModel).FullName;
            _uiFactory.Close(fullName);
        }
        public void CloseWidget<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : WidgetViewModel
        {
            var fullName = typeof(TUiViewModel).FullName;
            _uiFactory.Close(fullName);
        }
        public void HideHud<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : HudViewModel
        {
            var fullName = typeof(TUiViewModel).FullName;
            _uiFactory.Hide(fullName);
        }
        public void HideWidget<TUiViewModel>(bool hideCurrentWindow = true, bool forced = false) where TUiViewModel : WidgetViewModel
        {
            var fullName = typeof(TUiViewModel).FullName;
            _uiFactory.Hide(fullName);
        }

        public void Dispose()
        {
            _uiFactory.Dispose();
            _windowStack.Clear();
            _hudStack.Clear();
        }


        private void AddViewModelStack(UiViewModel uiViewModel)
        {
            if (uiViewModel is WindowViewModel windowViewModel)
            {
                FocusedWindowViewModel = windowViewModel;
                _windowStack.Push(new ViewModelTreeNode(windowViewModel));
            }
            if (uiViewModel is HudViewModel hudViewModel)
            {
                _hudStack.Push(new ViewModelTreeNode(hudViewModel));
            }
        }
        private bool IsHomeWindowType(UiViewModel viewModel) =>
            typeof(IHomeWindow).IsAssignableFrom(viewModel.GetType());

        private void OpenGameCloseWindowPopup() =>
            Log.PrintColor($"Open_CloseGameWindow", Color.red);
    }
}