using Assets.Game.Services.ProgressService.api;
using System;
using UnityEngine;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using WKosArch.Services.Scenes;
using WKosArch.Services.UIService.UI;
using WKosArch.Sound_Feature;

public interface ILoadMainMenuFeature : IFeature, IDisposable, ISavedProgress
{
    
}
