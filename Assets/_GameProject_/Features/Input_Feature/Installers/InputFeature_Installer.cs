using UnityEngine;
using WKosArch.DependencyInjection;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;


namespace Input_Feature
{
    [CreateAssetMenu(fileName = " InputFeature_Installer", menuName = "Game/Installers/InputFeature_Installer")]
    public class InputFeature_Installer : FeatureInstaller
    {
        public override IFeature Create(IDIContainer container)
        {
            IInputFeature feature = new InputFeature();

            var inputHandler = InputHandler.CreateInstance();
            inputHandler.Inject(feature);

            BindAsSingle(container, feature);

            return feature;
        }

        public override void Dispose() { }

        private void BindAsSingle(IDIContainer container, IInputFeature feature)
        {
            container.Bind(feature).AsSingle();
            Log.PrintColor($"[InputFeature_Installer] Create and Bind as Single", Color.cyan);
        }
    } 
}
