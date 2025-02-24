using Game.ScriptableObjects;
using Game.Interfaces;

namespace Game.Models 
{
    public class GameModel : IGameModel
    {
        private ReactiveInt _roundScore;
        private int _goalScore;
        private ReactiveInt _tapsLeft;
        private ReactiveInt _timeCounter;

        public GameModel(ReactiveInt roundScore, ReactiveInt tapsLeft, ReactiveInt timeCounter)
        {
            _roundScore = roundScore;
            _goalScore = 400;
            _tapsLeft = tapsLeft;
            _timeCounter = timeCounter;
        }

        public void InitiliazeRoundData()
        {
            SetScore(0);
            _tapsLeft.Value = 20;
        }

        public void CalculateScore(int ballsExplodedCount)
        {
            int score;
            if (ballsExplodedCount < 11)
            {
                score = ballsExplodedCount;
            }
            else if (ballsExplodedCount < 21)
            {
                score = ballsExplodedCount * 2;
            }
            else
            {
                score = ballsExplodedCount * 4;
            }
            AddScore(score);
        }

        private void SetScore(int score) => _roundScore.Value = score;
        private void AddScore(int score) => _roundScore.Value += score;

        public void UpdateTaps(int amount)
        {
            _tapsLeft.Value += amount;
        }
    }
}