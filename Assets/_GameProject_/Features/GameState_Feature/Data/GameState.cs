using System;

namespace WKosArch.GameState_Feature
{
    [Serializable]
    public class GameState
    {
        public PlayerState PlayerState;
        public SceneLoadingState SceneLoadingState;
    }
}