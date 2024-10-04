using R3;
using System;

namespace WKosArch.GameState_Feature
{
    public class SceneLoadingStateProxy
    {
        public ReactiveProperty<int> SceneIndexToLoad;
        public ReactiveProperty<SceneState[]> SceneStates;

        public SceneLoadingStateProxy(SceneLoadingState sceneLoadingStateOrigin)
        {
            SceneIndexToLoad = new ReactiveProperty<int>(sceneLoadingStateOrigin.SceneIndexToLoad);
            SceneStates = new ReactiveProperty<SceneState[]>(sceneLoadingStateOrigin.SceneStates);

            SceneIndexToLoad.Skip(1).Subscribe(value => sceneLoadingStateOrigin.SceneIndexToLoad = value);
            SceneStates.Skip(1).Subscribe(value => sceneLoadingStateOrigin.SceneStates = value);
        }
    }
}