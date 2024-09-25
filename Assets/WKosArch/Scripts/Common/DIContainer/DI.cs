using UnityEngine;

namespace WKosArch.DependencyInjection
{
    public static class DI
    {
        private static IDiContainer _rootDIcontainer = null;

        public static void AddRootDIContainer(IDiContainer dIcontainer)
        {
            _rootDIcontainer = dIcontainer;
        }

        public static TResolve Resolve<TResolve>() where TResolve : class
        {
            return _rootDIcontainer.Resolve<TResolve>();
        }

        //need for not Reload Domain each time
        // ProjectSetting > Editor > Enter Play Mode Setting
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            _rootDIcontainer = null;
        }
    }
}