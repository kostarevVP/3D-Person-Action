using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using WKosArch.Common.Utils.Async;
using WKosArch.Domain.Features;
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

        public IDIContainer Container => _container ??= CreateLocalContainer();


        private bool _isReady;

        [SerializeField] private FeatureInstaller[] _featureInstallers;

        private List<IFeature> _cachedFeatures;
        private List<IFeature> _reverseCachedFeatures;

        private IDIContainer _container;

        #region Unity Lifecycle

        private void Awake()
        {
            _cachedFeatures = new List<IFeature>();
            _reverseCachedFeatures = new List<IFeature>();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            foreach (IFeature feature in _reverseCachedFeatures)
            {
                if (feature is IFocusPauseFeature focusFeature)
                    focusFeature.OnApplicationFocus(hasFocus);
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            foreach (IFeature feature in _cachedFeatures)
            {
                if (feature is IFocusPauseFeature pauseFeature)
                    pauseFeature.OnApplicationPause(pauseStatus);
            }
        }

        #endregion

        #region Lifecycle

        public virtual async UniTask InitializeAsync()
        {
            InstallFeatures();
            InitializeFeatures();

            await WaitInitializationComplete();
        }

        public virtual void Dispose()
        {
            //there are problems with Destroy and OnDestroy, so it needs to be checked
            //because when game is close this method call twice and if not call from OnDestroy
            //i have a problem with UIFactory its because static field in MonoBehaviour
            if (_container != null)
            {
                _container.Dispose();
                _container = null;
            }

            DisposeFeatures();
            IsReady = false;
        }

        #endregion

        protected abstract IDIContainer CreateLocalContainer(IDIContainer dIContainer = null);

        private void InstallFeatures()
        {
            foreach (var featureInstaller in _featureInstallers)
            {
                var createdFeature = featureInstaller.Create(Container);

                _cachedFeatures.Add(createdFeature);
            }

            _reverseCachedFeatures = _cachedFeatures;
            _reverseCachedFeatures.Reverse();
        }

        private void InitializeFeatures()
        {
            foreach (IFeature feature in _cachedFeatures)
            {
                if (feature is IAsyncFeature asyncFeature)
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

        private void DisposeFeatures()
        {
            foreach (IFeature feature in _reverseCachedFeatures)
            {
                if (feature is IAsyncFeature asyncFeature)
                    asyncFeature.DisposeAsync();
            }

            _cachedFeatures.Clear();
            _reverseCachedFeatures.Clear();

            var featureInstallers = _featureInstallers.ToList();
            featureInstallers.Reverse();

            foreach (var featuresInstaller in featureInstallers)
            {
                featuresInstaller.Dispose();
            }
        }
    }
}