using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using WKosArch.Common.Utils.Async;
using WKosArch.Domain.Features;
using UnityEngine;
using System;
using WKosArch.DependencyInjection;




namespace WKosArch.Domain.Contexts
{
    public abstract class Context : MonoBehaviour, IContext
    {
        public Action<Context> OnContextIsReady { get; set; }
        public Action<Context> OnContextDestroy { get; set; }

        public bool IsReady
        {
            get { return _isReady; }
            private set
            {
                if (_isReady != value)
                {
                    _isReady = value;
                    if (_isReady)
                        OnContextIsReady?.Invoke(this);
                    else
                        OnContextDestroy?.Invoke(this);
                }
            }
        }

        public IDiContainer Container => _container ??= CreateLocalContainer();


        private bool _isReady;

        [SerializeField] private FeatureInstaller[] _featureInstallers;

        private List<IFeature> _cachedFeatures;

        private IDiContainer _container;

        #region Unity Lifecycle

        private void Awake()
        {
            _cachedFeatures = new List<IFeature>();
        }

        private void OnDestroy()
        {
            Destroy();
            IsReady = false;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            foreach (IFeature serviceFeature in _cachedFeatures)
            {
                if (serviceFeature is IFocusPauseFeature focusFeature)
                    focusFeature.OnApplicationFocus(hasFocus);
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            foreach (IFeature serviceFeature in _cachedFeatures)
            {
                if (serviceFeature is IFocusPauseFeature pauseFeature)
                    pauseFeature.OnApplicationPause(pauseStatus);
            }
        }

        #endregion

        #region Lifecycle

        public virtual async UniTask InitializeAsync()
        {
            InstallServiceFeatures();
            InitializeServiceFeatures();

            await WaitInitializationComplete();
        }

        public void Destroy()
        {
            DestroyServiceFeatures();

            //there are problems with Destroy and OnDestroy, so it needs to be checked
            //because when game is close this method call twice and if not call from OnDestroy
            //i have a problem with UIFactory its because static field in Monobehaivour
            if (_container != null)
                _container.Dispose();
        }

        #endregion

        protected abstract IDiContainer CreateLocalContainer(IDiContainer dIContainer = null);

        private void InstallServiceFeatures()
        {
            foreach (var serviceFeatureInstaller in _featureInstallers)
            {
                var createdFeature = serviceFeatureInstaller.Create(Container);

                _cachedFeatures.Add(createdFeature);
            }
        }

        private void InitializeServiceFeatures()
        {
            foreach (IFeature serviceFeature in _cachedFeatures)
            {
                if (serviceFeature is IAsyncFeature asyncFeature)
                    asyncFeature.InitializeAsync();
            }
        }


        private async UniTask WaitInitializationComplete()
        {
            var asyncFeatures = new List<IAsyncFeature>();

            foreach (var feature in _cachedFeatures)
            {
                if (feature is IAsyncFeature asyncFeature)
                    asyncFeatures.Add(asyncFeature);
            }

            await UnityAwaiters.WaitUntil(() =>
                asyncFeatures.All(feature => feature.IsReady));

            IsReady = true;
        }

        private void DestroyServiceFeatures()
        {
            foreach (IFeature serviceFeature in _cachedFeatures)
            {
                if (serviceFeature is IAsyncFeature asyncFeature)
                    asyncFeature.DestroyAsync();
                if (serviceFeature is IDisposable disposable)
                    disposable.Dispose();
            }

            _cachedFeatures.Clear();

            foreach (var serviceFeaturesInstaller in _featureInstallers)
            {
                serviceFeaturesInstaller.Dispose();
            }
        }
    }
}