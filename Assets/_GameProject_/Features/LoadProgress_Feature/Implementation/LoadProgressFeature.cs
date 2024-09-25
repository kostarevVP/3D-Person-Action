using Assets.Game.Services.ProgressService.api;
using Unity.Mathematics;
using WKosArch.Services.StaticDataServices;

namespace WKosArch.Features.LoadProgressFeature
{
    public class LoadProgressFeature : ILoadProgressFeature
    {
        private readonly IProgressFeature _progressService;
        private readonly ISaveLoadFeature _saveLoadService;
        private readonly IConfigDataFeature _configDataService;

        public LoadProgressFeature(IProgressFeature progressService, ISaveLoadFeature saveLoadService, IConfigDataFeature staticDataService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _configDataService = staticDataService;
        }

        public void LoadProgressOrInitNew()
        {
            _progressService.GameProgressData = _saveLoadService.LoadProgress() ?? NewProgress();

        }

        private GameProgressData NewProgress()
        {
            var progress = new GameProgressData();

            progress.SavedSceneIndex = _configDataService.GameProgressConfig.NewGameSceneIndex;

            NewSoundProgressData(progress);
            NewSceneProgressData(progress);

            return progress;
        }

        private void NewSceneProgressData(GameProgressData progress)
        {
            progress.SceneProgressData.SceneIndex = _configDataService.GameProgressConfig.NewGameSceneIndex;

            progress.SceneProgressData.PlayerProgressData.LocalTransformData = new LocalTransformData
                (new float3(0, 0.3f, 0), quaternion.identity, 1f);
        }

        private void NewSoundProgressData(GameProgressData progress)
        {
            var config = _configDataService.GameProgressConfig.SoundProgressConfig;

            progress.GlobalProgress.SoundProgressData.MasterVolume = config.MasterVolume;
            progress.GlobalProgress.SoundProgressData.MasterOn = config.MasterOn;
            progress.GlobalProgress.SoundProgressData.MusicVolume = config.MusicVolume;
            progress.GlobalProgress.SoundProgressData.MusicOn = config.MusicOn;
            progress.GlobalProgress.SoundProgressData.SfxVolume = config.SfxVolume;
            progress.GlobalProgress.SoundProgressData.SfxOn = config.SfxOn;
            progress.GlobalProgress.SoundProgressData.UiVolume = config.UiVolume;
            progress.GlobalProgress.SoundProgressData.UiOn = config.UiOn;
        }
    }
}
