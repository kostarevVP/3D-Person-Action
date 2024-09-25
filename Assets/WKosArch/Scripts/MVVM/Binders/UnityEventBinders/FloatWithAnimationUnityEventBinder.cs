using DG.Tweening;
using TMPro;
using UnityEngine;

namespace WKosArch.MVVM.Binders
{
    public class FloatWithAnimationUnityEventBinder : UnityEventBinder<float>
    {
        [SerializeField]
        private TextMeshProUGUI _pointTMPro;  // Ensure this is the UGUI version if you're using a canvas
        [SerializeField]
        private float _animationDuration = 0.5f;  // Duration over which the number animates
        [SerializeField]
        private float _scaleMultiplier = 1.5f;  // How much the text scales up
        [SerializeField]
        private float _scaleDuration = 0.1f;  // How long each scale pulse lasts

        protected override void OnPropertyChanged(float newValue)
        {
            float initialValue = float.Parse(_pointTMPro.text);
            DOTween.Kill(_pointTMPro); // Kill previous tweens on this object to avoid overlaps

            Sequence sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(() => initialValue, x => _pointTMPro.text = Mathf.RoundToInt(x).ToString(), newValue, _animationDuration)
                        .SetEase(Ease.OutQuad))
                    .Join(_pointTMPro.transform.DOScale(Vector3.one * _scaleMultiplier, _scaleDuration)
                        .SetEase(Ease.OutBack)  // Gives a little overshoot at the end, making the effect more noticeable
                        .SetLoops(2, LoopType.Yoyo));  // Scale up and then back down

            sequence.Play();
            base.OnPropertyChanged(newValue);
        }
    }
}