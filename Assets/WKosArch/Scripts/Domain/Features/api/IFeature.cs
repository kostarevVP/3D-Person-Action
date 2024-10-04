using Cysharp.Threading.Tasks;

namespace WKosArch.Domain.Features
{
    public interface IFeature { }

    public interface IAsyncFeature : IFeature
    {
        bool IsReady { get; }
        UniTask InitializeAsync();
        UniTask DisposeAsync();
    }

    public interface IFocusPauseFeature : IFeature
    {
        void OnApplicationFocus(bool hasFocus);
        void OnApplicationPause(bool pauseStatus);
    }
}