
using UnityEngine;
using Game.Services;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/Data/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public bool IsMusicOn;
        public bool IsSfxOn;
        public int GameDifficulty;
        public int HighScore;

        private PlayerPrefsService _prefsService;

        private void InitializeService()
        {
            if (_prefsService == null)
                _prefsService = new PlayerPrefsService();
        }

        public void SaveSettings()
        {
            InitializeService();

            _prefsService.Save("MusicOn", IsMusicOn);
            _prefsService.Save("SfxOn", IsSfxOn);
            _prefsService.Save("GameDifficulty", GameDifficulty);
            _prefsService.Save("HighScore", HighScore);

            PlayerPrefs.Save();
        }

        public void LoadSettings()
        {
            InitializeService();

            IsMusicOn = _prefsService.Load<bool>("MusicOn");
            IsSfxOn = _prefsService.Load<bool>("SfxOn");
            GameDifficulty = _prefsService.Load<int>("GameDifficulty");
            HighScore = _prefsService.Load<int>("HighScore");
        }
    }

}
