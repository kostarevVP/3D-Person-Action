using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using UnityEngine;
using WKosArch.DependencyInjection;
using System;
using Assets.Game.Services.ProgressService.api;

namespace WKosArch.Sound_Feature
{
    [CreateAssetMenu(fileName = "SoundFeature_Installer", menuName = "Game/Installers/SoundFeature_Installer")]
    public class SoundFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDiContainer container)
        {
            var soundProgressData = container.Resolve<IProgressFeature>().GameProgressData.GlobalProgress.SoundProgressData;

            ISoundSettings feature = new SoundFeature(soundProgressData);

            RegisterFeatureAsSingleton(container, feature);

            return feature as IFeature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, ISoundSettings feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[ISoundFeature] Create and RegesterSingleton", Color.cyan);
        }
    }

    [Serializable]
    public enum SoundRealization
    {
        BuiltIn,
        FMod,
    }
}