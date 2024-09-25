using System;
using WKosArch.Domain.Features;
using WKosArch.Services.UIService.UI;

namespace WKosArch.Services.UIService
{
    public interface IUiFeature : IFeature, IDisposable
    {
        IUserInterface UI { get; }
    }
}