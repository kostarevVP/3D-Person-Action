using UnityEngine;

namespace WKosArch.DependencyInjection
{
    public static class StaticDI
    {
        private static DIContainer _currentContainer = null;

        public static void SetCurrentContainer(DIContainer diContainer)
        {
            _currentContainer = diContainer;
        }

        public static TResolve Resolve<TResolve>() where TResolve : class
        {
            return _currentContainer.Resolve<TResolve>();
        }

        //need for not Reload Domain each time
        // ProjectSetting > Editor > Enter Play Mode Setting
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            _currentContainer = null;
        }
    }
}