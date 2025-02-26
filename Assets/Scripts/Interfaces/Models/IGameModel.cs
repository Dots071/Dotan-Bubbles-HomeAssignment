
using System;

namespace Game.Interfaces
{
    public interface IGameModel
    {
        public event Action<bool, int> RoundEnded;
        void InitiliazeRoundData();
        void StartRoundTimer();
        void StopRoundTimer();
        void CalculateScore(int ballsExplodedAmount);
        void AddTaps(int amount);
        void Dispose();
    }
}

