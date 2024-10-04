using R3;
using System;

namespace WKosArch.GameState_Feature
{
    [Serializable]
    public class SceneLoadingState
    {
        public int SceneIndexToLoad;
        public SceneState[] SceneStates;
    }
}