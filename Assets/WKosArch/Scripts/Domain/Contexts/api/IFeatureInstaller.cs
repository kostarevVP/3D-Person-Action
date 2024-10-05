using WKosArch.DependencyInjection;
using WKosArch.Domain.Features;

namespace WKosArch.Domain.Contexts
{
    public interface IFeatureInstaller
    {
        IFeature Create(IDIContainer localContainer);
    }
}