using R3;
using System;

namespace WKosArch.GameState_Feature
{
    public class SceneStateProxy
    {
        public ReactiveProperty<SceneState> SceneState { get; }

        public SceneStateProxy(SceneState sceneStateOrigin)
        {
            SceneState = new ReactiveProperty<SceneState>(sceneStateOrigin);

            SceneState.Skip(1).Subscribe(value => sceneStateOrigin = value);
        }
    }
}