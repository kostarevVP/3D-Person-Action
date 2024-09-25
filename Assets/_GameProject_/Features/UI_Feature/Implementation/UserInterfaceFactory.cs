using WKosArch;
using WKosArch.DependencyInjection;
using WKosArch.MVVM;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WKosArch.Extentions;
using WKosArch.Services.UIService;
using WKosArch.Services.UIService.Common;
using WKosArch.Services.UIService.UI;

namespace Assets._Game_.Services.UI_Service.Implementation
{
    public class UserInterfaceFactory : MonoBehaviour, IUserInterfaceFactory
    {
        private const string PrefabPath = "[INTERFACE]";

        public Dictionary<string, UiViewModel> UiViewModelsCache => _createdUiViewModelsCache;
        public Dictionary<string, View> ViewCache => _createdViewCache;



        private static UserInterfaceFactory _instance;

        [SerializeField] private UILayerContainer[] _containers;

        private Dictionary<string, UiViewModel> _createdUiViewModelsCache = new();
        private Dictionary<string, View> _createdViewCache = new();

        private IDiContainer _diContainer;
        private IUserInterface _ui;

        private UISceneConfig _uiSceneConfig;

        public void Construct(IDiContainer dIContainer, IUserInterface ui)
        {
            _diContainer = dIContainer;
            _ui = ui;
        }

        public static UserInterfaceFactory CreateInstance()
        {
            if (_instance != null)
            {
                Debug.LogWarning($"UserInterface CreateInstance _instance = {_instance}");
                return _instance;
            }

            var prefab = Resources.Load<UserInterfaceFactory>(PrefabPath);
            _instance = Instantiate(prefab);
            DontDestroyOnLoad(_instance);

            return _instance;
        }

        public void Build(UISceneConfig config)
        {
            _uiSceneConfig = config;
        }

        private View CreateView(UiViewModel uiViewModel, bool forced = false, Transform containerLayer = null)
        {
            View view = null;
            Dictionary<string, View> viewMapping = new();

            if (uiViewModel is WindowViewModel)
                viewMapping = _uiSceneConfig.WindowMappings;
            else if (uiViewModel is HudViewModel)
                viewMapping = _uiSceneConfig.HudMappings;
            else if (uiViewModel is WidgetViewModel)
                viewMapping = _uiSceneConfig.WidgetMappings;


            if (viewMapping.TryGetValue(uiViewModel.GetType().FullName, out View prefabView))
            {
                if (prefabView == null)
                {
                    Log.PrintWarning($"Couldn't open View for ({uiViewModel}). Maybe its not add to UISceneConfig for this Scene ");
                }
                else
                {
                    if (containerLayer == null)
                    {
                        containerLayer = GetLayerContainer(prefabView.Layer);
                    }
                    view = Instantiate(prefabView, containerLayer);
                    uiViewModel.Hide(true);
                    view.Bind(uiViewModel);
                    uiViewModel.Open(forced);
                }
            }
            else
            {
                Log.PrintWarning($"Couldn't find View for ({uiViewModel}). Maybe its not add to UISceneConfig for this Scene");
            }

            return view;
        }

        public UiViewModel CreateOrGetViewModel<TUiViewModel>() where TUiViewModel : UiViewModel, new()
        {
            var fullName = typeof(TUiViewModel).FullName;

            if (_createdUiViewModelsCache.TryGetValue(fullName, out UiViewModel uiViewModel))
            {
                if (uiViewModel == null)
                {
                    _createdUiViewModelsCache.Remove(fullName);

                    uiViewModel = new TUiViewModel();
                    uiViewModel.Inject(_diContainer, _ui);

                    _createdUiViewModelsCache.Add(fullName, uiViewModel);
                }
            }
            else
            {
                uiViewModel = new TUiViewModel();
                uiViewModel.Inject(_diContainer, _ui);

                _createdUiViewModelsCache.Add(fullName, uiViewModel);
            }

            return uiViewModel;
        }

        public View CreateOrGetActiveView(UiViewModel viewModel, bool forced = false, Transform containerLayer = null)
        {
            var fullName = viewModel.GetType().FullName;

            if (_createdViewCache.TryGetValue(fullName, out View view))
            {
                if (!view.isActiveAndEnabled)
                {
                    view.gameObject.SetActive(true);
                    viewModel.Open(forced);
                }
                return view;
            }
            else
            {
                view = CreateView(viewModel, forced, containerLayer);

                _createdViewCache.Add(fullName, view);
            }
             
            return view;
        }

        public void Close(string fullName, bool forcedHide = false)
        {
            _createdUiViewModelsCache[fullName].Close(forcedHide);
            _createdViewCache.Remove(fullName);
        }

        public void Hide(string fullName, bool forcedHide = false)
        {
            _createdUiViewModelsCache[fullName].Hide(forcedHide);
        }

        private Transform GetLayerContainer(UILayer layer)
        {
            return _containers.FirstOrDefault(container => container.Layer == layer)?.transform;
        }

        public void Close(UiViewModel viewModel, bool forcedHide = false)
        {
            string fullName = viewModel.GetType().FullName;
            Close(fullName, forcedHide);
        }

        public void Hide(UiViewModel viewModel, bool forcedHide = false)
        {
            string fullName = viewModel.GetType().FullName;
            Hide(fullName, forcedHide);
        }

        public void Dispose()
        {
            _createdViewCache.Clear();
            _createdUiViewModelsCache.Clear();
        }
    }
}
