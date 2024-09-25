using Assets.Game.Services.ProgressService.api;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;
using UnityEngine;
using WKosArch.DependencyInjection;

namespace WKosArch.Services.SaveLoadService
{
    [CreateAssetMenu(fileName = "SaveLoadFeature_Installer", menuName = "Game/Installers/SaveLoadFeature_Installer")]
    public class SaveLoadFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDiContainer container)
        {
            IProgressFeature progressFeature = container.Resolve<IProgressFeature>();

            ISaveLoadFeature feature = new SaveLoadFeature(progressFeature);

            RegisterFeatureAsSingleton(container, feature);

            return feature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, ISaveLoadFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[ISaveLoadFeature] Create and RegesterSingleton", Color.cyan);
        }
    }
}
