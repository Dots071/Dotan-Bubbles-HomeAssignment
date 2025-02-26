using System;

namespace Game.Models
{
    [Serializable]
    public class GameSettingsModel
    {
        public bool IsMusicOn = true;
        public bool IsSfxOn = true;
        public int Difficulty = 1; // 0=Easy, 1=Normal, 2=Hard
        public int HighScore = 0;
        
        // Game parameters per difficulty
        public int GetTimeLimit()
        {
            return Difficulty switch
            {
                0 => 45,  // Easy
                1 => 30,  // Normal
                2 => 20,  // Hard
                _ => 30
            };
        }
        
        public int GetTapLimit()
        {
            return Difficulty switch
            {
                0 => 30,  // Easy
                1 => 20,  // Normal
                2 => 15,  // Hard
                _ => 20
            };
        }
        
        public int GetTargetScore()
        {
            return Difficulty switch
            {
                0 => 250,  // Easy
                1 => 400,  // Normal
                2 => 600,  // Hard
                _ => 400
            };
        }
    }
} 