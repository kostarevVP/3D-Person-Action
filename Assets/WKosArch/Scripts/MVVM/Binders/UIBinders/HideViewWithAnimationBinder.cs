using UnityEngine;
using WKosArch.Services.UIService.Common;

namespace WKosArch.MVVM.Binders
{
    public class HideViewWithAnimationBinder : ObservableBinder<bool>
    {
        [SerializeField] private GameObject _hideingGameObject;
        [Space]
        [SerializeField]
        private AnimationTransition _transitionOut = default;

        protected override async void OnPropertyChanged(bool forced)
        {
            if (!forced && _transitionOut != default)
            {
                await _transitionOut.Play();
            }

           _hideingGameObject.SetActive(false);

        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (!_hideingGameObject)
            {
                _hideingGameObject = gameObject;
            }
        }
#endif
    }
}
