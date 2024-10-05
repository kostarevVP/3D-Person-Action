﻿using WKosArch.Domain.Features;
using WKosArch.GameState_Feature;

public interface IStateHandlerFeature : IFocusPauseFeature
{
    void AddGameStateHolder(ILoadGameState loadHolders);
    void InformLoadHolders();
    void InformSaveHolders();
}

public interface ISaveGameState : ILoadGameState
{
    public void SaveGameState(GameStateProxy gameState);
}

public interface ILoadGameState
{
    public void LoadGameState(GameStateProxy gameState);
}