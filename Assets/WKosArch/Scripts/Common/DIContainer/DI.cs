using UnityEngine;

namespace WKosArch.DependencyInjection
{
    public static class DI
    {
        private static IDiContainer _rootDiContainer = null;

        public static void AddRootDIContainer(IDiContainer diContainer)
        {
            _rootDiContainer = diContainer;
        }

        public static TResolve Resolve<TResolve>() where TResolve : class
        {
            return _rootDiContainer.Resolve<TResolve>();
        }

        //need for not Reload Domain each time
        // ProjectSetting > Editor > Enter Play Mode Setting
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            _rootDiContainer = null;
        }
    }
}