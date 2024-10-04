using UnityEngine;
using Cysharp.Threading.Tasks;

namespace WKosArch.UI_Feature
{
    public abstract class AnimationTransition : MonoBehaviour
    {
        public bool IsPlaying { get; private set; }

        public async UniTask Play()
        {
            IsPlaying = true;

            await PlayInternal();

            IsPlaying = false;
        }

        protected abstract UniTask PlayInternal();
    }
}