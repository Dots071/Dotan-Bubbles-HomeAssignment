
namespace Game.Interfaces
{
    public interface IGameModel
    {
        void InitiliazeRoundData();
        void CalculateScore(int ballsExplodedAmount);
        public void UpdateTaps(int amount);
    }
}

