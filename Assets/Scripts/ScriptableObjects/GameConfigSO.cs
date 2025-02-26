using UnityEngine;

namespace Game.ScriptableObjects
{
    // Stores game configuration values and settings that can be adjusted in the editor
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/Game Config")]
    public class GameConfigSO : ScriptableObject
    {
        public bool IsMusicOn = true;
        public bool IsSfxOn = true;
        public int Difficulty = 1;
        public int HighScore = 0;


        public int GetTimeToFinish()
        {
            return Difficulty switch
            {
                0 => 60,
                1 => 30,
                2 => 20,
                _ => 30 
            };
        }
        public int GetScoreGoal()
        {
            return Difficulty switch
            {
                0 => 200,
                1 => 400,
                2 => 800,
                _ => 400
            };
        }
        public int GetTaps()
        {
            return Difficulty switch
            {
                0 => 30,
                1 => 20,
                2 => 10,
                _ => 20
            };
        }
    }
} 