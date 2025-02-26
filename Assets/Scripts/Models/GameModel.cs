using Game.ScriptableObjects;
using Game.Interfaces;
using Game.Utility;
using System;

namespace Game.Models 
{
    // Maintains game state including score, timer, and win/loss conditions
    public class GameModel : IGameModel, IDisposable
    {
        public event Action<bool, int> RoundEnded;

        private ReactiveInt _roundScore;
        private ReactiveInt _goalScore;
        private ReactiveInt _tapsLeft;
        private ReactiveInt _timeCounter;

        private GameConfigSO _gameConfigSO;

        private TimerWithCallback _timer;

        private const int _timeForRound = 30;


        public GameModel(ReactiveInt roundScore, ReactiveInt tapsLeft, ReactiveInt timeCounter, ReactiveInt goalScore, GameConfigSO config)
        {
            _gameConfigSO = config;
            _roundScore = roundScore;
            _tapsLeft = tapsLeft;
            _timeCounter = timeCounter;
            _goalScore = goalScore;

            _timer = new TimerWithCallback(_timeForRound, UpdateTimer);
            _timer.OnTimeFinished += HandleTimerFinished;
        }

        private void HandleTimerFinished()
        {
            RoundEnded?.Invoke(false, _roundScore.Value);
        }

        public void InitiliazeRoundData()
        {
            _goalScore.Value = _gameConfigSO.GetScoreGoal();
            _roundScore.Value = 0;
            _tapsLeft.Value = _gameConfigSO.GetTaps() ;
            _timeCounter.Value = _gameConfigSO.GetTimeToFinish();
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

        private void AddScore(int score)
        {
            _roundScore.Value += score;

            if (_roundScore.Value > _goalScore.Value)
            {
                RoundEnded?.Invoke(true, _roundScore.Value);
            }
        }
        private void UpdateTimer(int time) => _timeCounter.Value = time;
        public void AddTaps(int amount)
        {
            _tapsLeft.Value += amount;

            if( _tapsLeft.Value <= 0)
            {
                RoundEnded?.Invoke(false, _roundScore.Value);

            }
        }

        public void Dispose()
        {
            _timer.OnTimeFinished -= HandleTimerFinished;
            _timer.Dispose();
        }
        
    }
}