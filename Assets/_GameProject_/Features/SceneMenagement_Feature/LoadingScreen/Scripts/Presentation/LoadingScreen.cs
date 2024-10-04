using System;
using UnityEngine;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace WKosArch.SceneManagement_Feature
{
    /// <summary>
    /// Class for managing loading screen animations.
    /// Uses DOTween to animate the CanvasGroup transparency.
    /// You can choose easing functions here: https://easings.net/
    /// </summary>
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        private GameObject _goContent => this.gameObject;

        [SerializeField]
        private float _delayTime;
        [Space(10)]
        [SerializeField]
        private float _fadeTime;
        [SerializeField]
        private Ease _fadeEase;
        [Space]
        [SerializeField]
        private float _revealTime;
        [SerializeField]
        private Ease _revealEase;


        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            if (_canvasGroup == null)
                throw new Exception($"Not find CanvasGroup in Children LoadingScreen{this.name} GameObject");

        }

        public async void Show(Action onComplete = null)
        {
            await UniCoroutine(RevealCanvasGroup(_revealTime));
            onComplete?.Invoke();
        }

        public async void Hide(Action onComplete = null)
        {
            //StartCoroutine(CloseLoadingScreenWithDelay(_delayTime));
            //StartCoroutine not await end of enumerator that why there is uniTask awaiter
            await UniCoroutine(CloseLoadingScreenWithDelay(_delayTime));
            await UniCoroutine(FadeCanvasGroup(_fadeTime));
            onComplete?.Invoke();
        }

        private async UniTask UniCoroutine(IEnumerator enumerator)
        {
            await enumerator;
        }

        private IEnumerator CloseLoadingScreenWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        private IEnumerator FadeCanvasGroup(float delay)
        {
            _canvasGroup.DOFade(0f, delay)
                .SetEase(_fadeEase)
                .OnComplete(() => _goContent.SetActive(false));

            yield return new WaitForSeconds(delay);

            _goContent.SetActive(false);
        }

        private IEnumerator RevealCanvasGroup(float revealTime)
        {
            _goContent.SetActive(true);

            if(_canvasGroup.alpha > 0f)
                _canvasGroup.alpha = 0f;

            _canvasGroup.DOFade(1f, revealTime)
                .SetEase(_revealEase);

            yield return new WaitForSeconds(revealTime);
        }
    }
}