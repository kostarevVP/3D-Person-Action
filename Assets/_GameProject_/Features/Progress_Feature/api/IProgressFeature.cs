using System;
using WKosArch.Domain.Features;

namespace Assets.Game.Services.ProgressService.api
{
    public interface IProgressFeature : IFeature
    {
        GameProgressData GameProgressData { get; set; }
    }
}