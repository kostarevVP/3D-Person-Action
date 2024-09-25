using Assets.Game.Services.ProgressService.api;
using System;
using UnityEngine;

namespace WKosArch.Services.SaveLoadService
{
    public class SaveLoadFeature : ISaveLoadFeature
    {
        private const string GAME_PROGRESS_KEY = nameof(GAME_PROGRESS_KEY);

        public event Action OnSaveProgressStarted;

        private readonly IProgressFeature _progressService;

        public SaveLoadFeature(IProgressFeature progressService)
        {
            _progressService = progressService;
        }

        public GameProgressData LoadProgress()
        {
            var json = PlayerPrefs.GetString(GAME_PROGRESS_KEY);
            var save = JsonUtility.FromJson<GameProgressData>(json);

            return save;
        }

        public void SaveProgress()
        {
            OnSaveProgressStarted?.Invoke();

            var json = JsonUtility.ToJson(_progressService.GameProgressData);
            PlayerPrefs.SetString(GAME_PROGRESS_KEY, json);
            PlayerPrefs.Save();
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
                SaveProgress();
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                SaveProgress();
        }
    }
}