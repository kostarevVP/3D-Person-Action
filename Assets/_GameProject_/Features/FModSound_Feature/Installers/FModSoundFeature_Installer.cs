using UnityEngine;
using WKosArch.DependencyInjection;
using WKosArch.Domain.Contexts;
using WKosArch.Domain.Features;
using WKosArch.Extensions;
using WKosArch.Sound_Feature;
using WKosArch.GameState_Feature;
using WKosArch.Configs_Feature;
using WKosArch.SceneManagement_Feature;


namespace WKosArch.FModSound_Feature
{
    [CreateAssetMenu(fileName = "SoundFModSoundFeature", menuName = "Game/Installers/SoundFModSoundFeature_Installer")]
    public class FModSoundFeature_Installer : FeatureInstaller
    {
        [SerializeField]
        private SoundsSceneConfig _fModSoundGlobalConfig;
        [Space]
        [SerializeField]
        private bool _enableFModECSFactory;

        private ISceneManagementFeature _sceneManagementService;
        private IConfigsFeature _configsFeature;

        private ISoundConfigLoader _soundFeatureConfigLoader;
        private ISoundConfigLoader _fModECSFactorySoundConfigLoader;

        public override IFeature Create(DIContainer container)
        {
            _sceneManagementService = container.Resolve<ISceneManagementFeature>();
            _configsFeature = container.Resolve<IConfigsFeature>();

            Subscribe();

            GameSettingStateProxy gameSettingsState = container.Resolve<IGameStateProviderFeature>().GameSettingsState;
            SoundSettingsStateProxy soundSettingsStateProxy = gameSettingsState.SoundSettingsStateProxy.Value;

            ISoundFeature<FModSound> feature = new FModSoundFeature(soundSettingsStateProxy);
            _soundFeatureConfigLoader = feature as ISoundConfigLoader;

            if (_enableFModECSFactory)
            {
                IFModSoundECSFactory fModECSFactory = new FModSoundECSFactory();
                _fModECSFactorySoundConfigLoader = fModECSFactory as ISoundConfigLoader;
                RegisterFeatureAsSingleton(container, fModECSFactory);
            }


            LoadGlobalSoundConfig();

            RegisterFeatureAsSingleton(container, feature);

            return feature;
        }


        public override void Dispose()
        {
            Unsubscribe();
        }


        private void RegisterFeatureAsSingleton(DIContainer container, ISoundFeature<FModSound> feature)
        {
            container.RegisterFactory(_ => feature).AsSingle();
            Log.PrintColor($"[FModSoundFeature - ISoundFeature<FModSound>] Create and RegisterSingleton", Color.cyan);
        }

        private void RegisterFeatureAsSingleton(DIContainer container, IFModSoundECSFactory feature)
        {
            container.RegisterFactory(_ => feature).AsSingle();
            Log.PrintColor($"[FModSoundECSFactory - IFModECSFactory] Create and RegisterSingleton", Color.cyan);
        }

        private void LoadGlobalSoundConfig()
        {
            var soundConfigMap = ConfigUtils.CreateDictionary<SoundType, SoundConfig, SoundConfigMapping>(_fModSoundGlobalConfig.SoundConfigs);

            _soundFeatureConfigLoader.LoadSoundGlobalConfigMap(soundConfigMap);

            if (_enableFModECSFactory)
                _fModECSFactorySoundConfigLoader.LoadSoundGlobalConfigMap(soundConfigMap);
        }

        private void SceneLoaded(string scene)
        {
            LoadSceneSoundConfig(scene);
        }

        private void LoadSceneSoundConfig(string scene)
        {
            var soundConfigMap = _configsFeature.SceneSoundConfigsMap[scene];

            _soundFeatureConfigLoader.LoadSoundSceneConfigMap(soundConfigMap);

            if (_enableFModECSFactory)
                _fModECSFactorySoundConfigLoader.LoadSoundGlobalConfigMap(soundConfigMap);
        }

        private void Subscribe()
        {
            _sceneManagementService.OnSceneLoaded += SceneLoaded;
        }

        private void Unsubscribe()
        {
            _sceneManagementService.OnSceneLoaded -= SceneLoaded;
        }
    }
}