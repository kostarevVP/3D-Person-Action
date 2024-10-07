using System;
using System.Collections.Generic;
using WKosArch.Extensions;
using WKosArch.GameState_Feature;

public class StateHandlerFeature : IStateHandlerFeature, IDisposable
{
    private List<ILoadGameState> _loadHolders = new();
    private List<ISaveGameState> _saveHolders = new();
    private readonly IGameStateProviderFeature _gameStateProvider;

    public StateHandlerFeature(IGameStateProviderFeature gameStateProvider)
    {
        _gameStateProvider = gameStateProvider;
    }

    public void AddGameStateHolder(ILoadGameState loadHolders)
    {
        if (loadHolders is ISaveGameState)
            _saveHolders.Add(_saveHolders as ISaveGameState);

        _loadHolders.Add(loadHolders);
    }

    public void InformSaveHolders()
    {
        var gameState = _gameStateProvider.GameState;

        foreach (var holder in _loadHolders)
        {
            if (holder is ISaveGameState saveHolder)
                saveHolder.SaveGameState(gameState);
        }
    }

    public void InformLoadHolders()
    {
        var gameState = _gameStateProvider.GameState;

        foreach (var holder in _loadHolders)
        {
            holder.LoadGameState(gameState);
        }
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            var gameState = _gameStateProvider.GameState;
            InformSaveHolders();
        }
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            var gameState = _gameStateProvider.GameState;
            InformSaveHolders();
        }
    }

    public void Dispose()
    {
        Clear();
    }

    private void Clear()
    {
        _loadHolders.Clear();
        _saveHolders.Clear();
    }
}
