using R3;
using UnityEngine;
using WKosArch.Configs_Feature;

namespace WKosArch.GameState_Feature
{
    public class PlayerPrefsGameStateProviderFeature : IGameStateProviderFeature
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        private const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);

        public GameStateProxy GameState { get; private set; }
        public GameSettingStateProxy GameSettingsState { get; private set; }

        private GameState _gameStateOrigin;
        private GameSettingState _gameSettingsStateOrigin;

        private IConfigsFeature _configsFeature;

        public PlayerPrefsGameStateProviderFeature(IConfigsFeature configsFeature)
        {
            _configsFeature = configsFeature;
        }

        public Observable<GameStateProxy> LoadGameState()
        {
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                GameState = LoadGameStateFromConfig();

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);
            }

            return Observable.Return(GameState);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(_gameStateOrigin);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);

            return Observable.Return(true);
        }

        public Observable<bool> ResetGameState()
        {
            GameState = LoadGameStateFromConfig();

            return SaveGameState();
        }


        public Observable<GameSettingStateProxy> LoadGameSettingsState()
        {
            if (!PlayerPrefs.HasKey(GAME_SETTINGS_STATE_KEY))
            {
                GameSettingsState = LoadGameSettingsStateFromConfig();

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_SETTINGS_STATE_KEY);
                _gameSettingsStateOrigin = JsonUtility.FromJson<GameSettingState>(json);
                GameSettingsState = new GameSettingStateProxy(_gameSettingsStateOrigin);
            }

            return Observable.Return(GameSettingsState);
        }

        public Observable<bool> SaveGameSettingsState()
        {
            var json = JsonUtility.ToJson(_gameSettingsStateOrigin);
            PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);

            return Observable.Return(true);
        }

        public Observable<bool> ResetGameSettingsState()
        {
            GameSettingsState = LoadGameSettingsStateFromConfig();

            return SaveGameSettingsState();
        }


        private GameStateProxy LoadGameStateFromConfig()
        {
            _gameStateOrigin = _configsFeature.GameStateConfig.GameState;
            return new GameStateProxy(_gameStateOrigin);
        }
        private GameSettingStateProxy LoadGameSettingsStateFromConfig()
        {
            _gameSettingsStateOrigin = _configsFeature.GameSettingsStateConfig.GameSettingState;
            return new GameSettingStateProxy(_gameSettingsStateOrigin);
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                SaveGameState();
                SaveGameSettingsState(); 
            }
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveGameState();
                SaveGameSettingsState(); 
            }
        }
    }
}