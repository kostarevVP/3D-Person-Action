using System;
using UnityEngine;

namespace Timer_Feature
{
    public class TimerInvoker : MonoBehaviour
    {
        public Action<float> OnUpdateTickedEvent;
        public Action<float> OnUpdateTimeUnscaledTickedEvent;
        public Action OnOneSecTickedEvent;
        public Action OnOneSecTimeUnscaledTickedEvent;

        public static TimerInvoker Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[TimeInvoker]");
                    _instance = go.AddComponent<TimerInvoker>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private static TimerInvoker _instance;

        private float _oneSecTimer;
        private float _oneSecTimeUnscaledTimer;

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            OnUpdateTickedEvent?.Invoke(deltaTime);

            _oneSecTimer += deltaTime;
            if (_oneSecTimer >= 1f)
            {
                _oneSecTimer -= 1f;
                OnOneSecTickedEvent?.Invoke();
            }

            var unscaledDeltaTime = Time.unscaledDeltaTime;

            OnUpdateTimeUnscaledTickedEvent?.Invoke(unscaledDeltaTime);

            _oneSecTimeUnscaledTimer -= unscaledDeltaTime;
            if (_oneSecTimeUnscaledTimer >= 1f)
            {
                _oneSecTimeUnscaledTimer -= 1f;
                OnOneSecTimeUnscaledTickedEvent?.Invoke();
            }
        }
    }

}