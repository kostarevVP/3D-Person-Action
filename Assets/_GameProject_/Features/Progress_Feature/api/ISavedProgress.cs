
namespace Assets.Game.Services.ProgressService.api
{
    public interface ISavedProgress : ILoadProgress
    {
        public void SaveProgress(GameProgressData progress);
    }

    public interface ILoadProgress
    {
        public void LoadProgress(GameProgressData progress);
    }

}
