using Assets.Game.Services.ProgressService.api;

namespace WKosArch.Services.ProgressService
{
    public class ProgressFeature : IProgressFeature
    {
        public GameProgressData GameProgressData { get; set; }
    }
}