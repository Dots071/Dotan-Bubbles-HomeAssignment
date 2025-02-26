using System;

namespace Game.Models
{
    // Serializable data class for storing user preferences and game settings
    [Serializable]
    public class GameSettingsModel
    {
        public bool IsMusicOn = true;
        public bool IsSfxOn = true;
        public int Difficulty = 1;
        public int HighScore = 0;
        
    }
} 