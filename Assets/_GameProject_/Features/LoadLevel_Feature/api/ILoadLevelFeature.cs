using Assets.Game.Services.ProgressService.api;
using System;
using WKosArch.Domain.Features;
    
namespace WKosArch.Features.LoadLevelFeature
{   
	public interface ILoadLevelFeature : IFeature, IDisposable, ISavedProgress
    {
    } 
}