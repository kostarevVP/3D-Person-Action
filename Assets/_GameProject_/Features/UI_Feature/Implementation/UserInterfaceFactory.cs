using WKosArch.DependencyInjection;
using WKosArch.MVVM;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WKosArch.Extensions;

namespace WKosArch.UI_Feature
{
    public class UserInterfaceFactory : MonoBehaviour, IUserInterfaceFactory
    {
        private const string PrefabPath = "[INTERFACE]";


        private static UserInterfaceFactory _instance;

        [SerializeField] private UILayerContainer[] _containers;

        private Dictionary<string, UiViewModel> _createdUiViewModelsCache = new();
        private Dictionary<string, View> _createdViewCache = new();

        private IDIContainer _diContainer;
        private IUserInterfaceFeature _ui;

        private Dictionary<string, View> _viewModelToViewMap;

        public void Construct(IDIContainer dIContainer, IUserInterfaceFeature ui)
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

        public void BuildUiForScene(Dictionary<string, View> viewModelToViewMap)
        {
            _viewModelToViewMap = viewModelToViewMap;
        }

        private View CreateView(UiViewModel uiViewModel, bool forced = false, Transform containerLayer = null)
        {
            View view = null;

            if (_viewModelToViewMap.TryGetValue(uiViewModel.GetType().FullName, out View prefabView))
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

        public UiViewModel GetOrCreateViewModel<TUiViewModel>() where TUiViewModel : UiViewModel, new()
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

        public View GetOrCreateActiveView(UiViewModel viewModel, bool forced = false, Transform containerLayer = null)
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
                view = CreateView(viewModel , forced, containerLayer);

                _createdViewCache.Add(fullName, view);
            }

            viewModel.Transform = view.transform;

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
