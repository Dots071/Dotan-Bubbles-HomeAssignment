using Game.Interfaces;
using Game.ScriptableObjects;
using System;
using UnityEngine;

namespace Game.Controllers
{
    public class GameController : IDisposable
    {
        private GameEventAggregator _events;
        private IGameModel _model;
        public GameController(GameEventAggregator gameEventAggregator, IGameModel gameModel)
        {
            _events = gameEventAggregator;
            _model = gameModel;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _events.GameRoundStarted += OnRoundStarted;
            _events.BallsExploded += OnBallsExploded;
            _events.PlayerMissed += OnPlayerMissed;
            _events.GameRoundEnded += OnRoundEnded;
        }


        private void UnSubscribeToEvents()
        {
            _events.GameRoundStarted -= OnRoundStarted;
            _events.BallsExploded -= OnBallsExploded;
            _events.PlayerMissed -= OnPlayerMissed;
            _events.GameRoundEnded -= OnRoundEnded;
        }


        private void OnRoundStarted()
        {
            _model.InitiliazeRoundData();
        }
        private void OnBallsExploded(int amount)
        {
            if (amount < 3) Debug.LogError("[GameController] amount on balls exploded is less than 3.");

            _model.CalculateScore(amount);
        }

        private void OnPlayerMissed()
        {
            Debug.Log("[GameController] player missed!");
            _model.UpdateTaps(-1);
        }
        private void OnRoundEnded(bool isWin, int score)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            UnSubscribeToEvents();
        }

        // listen to Miss event - calculate taps left and update model.
        // L
    }
}