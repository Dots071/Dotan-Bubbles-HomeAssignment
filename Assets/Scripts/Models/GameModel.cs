using Game.ScriptableObjects;
using Game.Interfaces;
using Game.Utility;
using System;

namespace Game.Models 
{
    public class GameModel : IGameModel , IDisposable
    {
        private ReactiveInt _roundScore;
        private int _goalScore;
        private ReactiveInt _tapsLeft;
        private ReactiveInt _timeCounter;

        private TimerWithCallback _timer;

        private const int _timeForRound = 30;

        public GameModel(ReactiveInt roundScore, ReactiveInt tapsLeft, ReactiveInt timeCounter)
        {
            _roundScore = roundScore;
            _goalScore = 400;
            _tapsLeft = tapsLeft;
            _timeCounter = timeCounter;

            _timer = new TimerWithCallback(_timeForRound, UpdateTimer);
        }

        public void InitiliazeRoundData()
        {
            _roundScore.Value = 0;
            _tapsLeft.Value = 20;
            _timeCounter.Value = _timeForRound;
        }

        public void StartRoundTimer()
        {
            _timer.StartTimer();
        }

        public void StopRoundTimer()
        {
            _timer.StopTimer();
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

        private void AddScore(int score) => _roundScore.Value += score;
        private void UpdateTimer(int time) => _timeCounter.Value = time;
        public void AddTaps(int amount) => _tapsLeft.Value += amount;

        public  void Dispose()
        {
            _timer.Dispose();
        }
        
    }
}