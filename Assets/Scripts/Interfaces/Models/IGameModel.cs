
namespace Game.Interfaces
{
    public interface IGameModel
    {
        void InitiliazeRoundData();
        void StartRoundTimer();
        void StopRoundTimer();
        void CalculateScore(int ballsExplodedAmount);
        void AddTaps(int amount);
        void Dispose();
    }
}

