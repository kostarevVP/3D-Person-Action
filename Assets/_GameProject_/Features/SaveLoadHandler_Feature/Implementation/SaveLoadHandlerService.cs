using Assets.Game.Services.ProgressService.api;
using System.Collections.Generic;

public class SaveLoadHandlerService : ISaveLoadHandlerService
{
    public bool IsReady => _isReady;

    private bool _isReady;
    private List<ILoadProgress> _loadHolders = new();
    private List<ISavedProgress> _saveHolders = new();
    private readonly IProgressFeature _progressService;
    private readonly ISaveLoadFeature _saveLoadService;

    public SaveLoadHandlerService(IProgressFeature progressService, ISaveLoadFeature saveLoadService)
    {
        _progressService = progressService;
        _saveLoadService = saveLoadService;

        _saveLoadService.OnSaveProgressStarted += InformSaveHolders;

        _isReady = true;
    }

    public void AddSaveLoadHolder(ILoadProgress loadHolders)
    {
        if (loadHolders is ISavedProgress)
            _saveHolders.Add(_saveHolders as ISavedProgress);

        _loadHolders.Add(loadHolders);
    }

    public void InformSaveHolders()
    {
        var progress = _progressService.GameProgressData;

        foreach (var holder in _loadHolders)
        {
            if(holder is ISavedProgress saveHolder)
                saveHolder.SaveProgress(progress);
        }
    }

    public void InformLoadHolders()
    {
        var progress = _progressService.GameProgressData;

        foreach (var holder in _loadHolders)
        {
            holder.LoadProgress(progress);
        }
    }

    public void Clear()
    {
        _loadHolders.Clear();
        _saveHolders.Clear();
    }
}
