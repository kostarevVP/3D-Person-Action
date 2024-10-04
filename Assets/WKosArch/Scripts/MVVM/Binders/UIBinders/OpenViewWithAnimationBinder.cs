using UnityEngine;
using WKosArch.UI_Feature;

namespace WKosArch.MVVM.Binders
{
    public class OpenViewWithAnimationBinder : ObservableBinder<bool>
    {
        [SerializeField] private GameObject _openingGameObject;
        [Space]
        [SerializeField]
        private AnimationTransition _transitionOut = default;

        protected override async void OnPropertyChanged(bool forced)
        {
            if (!forced && _transitionOut != default)
            {
                await _transitionOut.Play();
            }
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (!_openingGameObject)
            {
                _openingGameObject = gameObject;
            }
        }
#endif
    }
}
