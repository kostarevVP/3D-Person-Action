using System;

namespace WKosArch.SceneManagement_Feature
{
    public interface ILoadingScreen
    {
        void Show(Action onComplete = null);
        void Hide(Action onComplete = null);
    }
}