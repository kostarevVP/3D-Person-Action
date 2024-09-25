using UnityEngine;
using WKosArch.DependencyInjection;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extentions;


namespace Input_Feature
{
    [CreateAssetMenu(fileName = " InputFeature_Installer", menuName = "Game/Installers/InputFeature_Installer")]
    public class InputFeature_Installer : FeatureInstaller
    {

        public override IFeature Create(IDiContainer container)
        {
            IInputFeature feature = new InputFeature();

            var inputHandler = InputHandler.CreateInstance();
            inputHandler.Inject(feature);

            RegisterFeatureAsSingleton(container, feature);

            return feature;
        }

        public override void Dispose() { }

        private void RegisterFeatureAsSingleton(IDiContainer container, IInputFeature feature)
        {
            container.RegisterSingleton(_ => feature);
            Log.PrintColor($"[InputFeature_Installer] Create and RegisterSingleton", Color.cyan);
        }
    } 
}
