using System;
using UnityEngine;

namespace Timer_Feature
{
    public class Timer
    {
        public Action<float> OnTimerValueChangedEvent;
        public Action OnTimerFinishedEvent;

        public TimerType Type { get; }
        public float InitialTime { get; private set; }
        public float RemaingTime { get; private set; }
        public float PassedTime => InitialTime - RemaingTime;
        public float Progress => Math.Clamp(PassedTime / InitialTime, 0, 1);
        public bool IsPaused { get; private set; }
        public Timer(TimerType type)
        {
            Type = type;
        }

        public Timer(TimerType type, float seconds)
        {
            Type = type;
            SetTime(seconds);
        }

        public void SetTime(float seconds)
        {
            InitialTime = seconds;
            RemaingTime = seconds;
            OnTimerValueChangedEvent?.Invoke(RemaingTime);
        }

        public void Start()
        {
            if (RemaingTime == 0f)
            {
                Debug.LogError("You trying start timer with 0 seconds");
                OnTimerFinishedEvent?.Invoke();
            }
            if (RemaingTime < 0f)
            {
                Debug.LogError("You trying start timer with negative value");
                OnTimerFinishedEvent?.Invoke();
            }
            IsPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(RemaingTime);


        }
        public void Start(float seconds)
        {
            SetTime(seconds);
            Start();
        }

        public void Pause()
        {
            IsPaused = true;
            Unsubscribe();
            OnTimerValueChangedEvent?.Invoke(RemaingTime);

        }
        public void Resume()
        {
            IsPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(RemaingTime);

        }
        public void Stop()
        {
            Unsubscribe();
            RemaingTime = 0f;
            OnTimerValueChangedEvent?.Invoke(RemaingTime);
            OnTimerFinishedEvent?.Invoke();
        }

        private void Subscribe()
        {
            switch (Type)
            {
                case TimerType.Unknown:
                    Debug.LogError("Unknown Type in timer");
                    break;
                case TimerType.UpdateTick:
                    TimerInvoker.Instance.OnUpdateTickedEvent += OnUpdateTick;
                    break;
                case TimerType.UpdateUnscaledTick:
                    TimerInvoker.Instance.OnUpdateTimeUnscaledTickedEvent += OnUpdateTick;
                    break;
                case TimerType.OneSecTick:
                    TimerInvoker.Instance.OnOneSecTickedEvent += OnSecTick;
                    break;
                case TimerType.OneSecUnscaledTick:
                    TimerInvoker.Instance.OnOneSecTimeUnscaledTickedEvent += OnSecTick;
                    break;
            }
        }


        private void Unsubscribe()
        {
            switch (Type)
            {
                case TimerType.Unknown:
                    Debug.LogError("Unknown Type in timer");
                    break;
                case TimerType.UpdateTick:
                    TimerInvoker.Instance.OnUpdateTickedEvent -= OnUpdateTick;
                    break;
                case TimerType.UpdateUnscaledTick:
                    TimerInvoker.Instance.OnUpdateTimeUnscaledTickedEvent -= OnUpdateTick;
                    break;
                case TimerType.OneSecTick:
                    TimerInvoker.Instance.OnOneSecTickedEvent -= OnSecTick;
                    break;
                case TimerType.OneSecUnscaledTick:
                    TimerInvoker.Instance.OnOneSecTimeUnscaledTickedEvent -= OnSecTick;
                    break;
            }
        }

        private void OnUpdateTick(float deltaTime)
        {
            if (IsPaused) return;

            RemaingTime -= deltaTime;
            ChekFinish();
        }
        private void OnSecTick()
        {
            if (IsPaused) return;

            RemaingTime -= 1f;
            ChekFinish();
        }

        private void ChekFinish()
        {
            if (RemaingTime <= 0)
                Stop();
            else
                OnTimerValueChangedEvent?.Invoke(RemaingTime);
        }
    }
}