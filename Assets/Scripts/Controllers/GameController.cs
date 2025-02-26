using Game.Interfaces;
using Game.ScriptableObjects;
using System;
using UnityEngine;

namespace Game.Controllers
{
    // Core game logic controller that manages game state, score, and win/loss conditions
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

            // The model calculated the business logic and notifys if the round ended and how.
            _model.RoundEnded += OnRoundEnded;
        }

        private void UnSubscribeToEvents()
        {
            _events.GameRoundStarted -= OnRoundStarted;
            _events.BallsExploded -= OnBallsExploded;
            _events.PlayerMissed -= OnPlayerMissed;

            _model.RoundEnded -= OnRoundEnded;
        }


        private void OnRoundStarted()
        {
            _model.InitiliazeRoundData();
            _model.StartRoundTimer();
        }
        private void OnBallsExploded(int amount)
        {
            if (amount < 3) Debug.LogError("[GameController] amount on balls exploded is less than 3.");

            _model.CalculateScore(amount);
        }

        private void OnPlayerMissed()
        {
            Debug.Log("[GameController] player missed!");
            _model.AddTaps(-1);
        }

        private void OnRoundEnded(bool isWin, int score)
        {
            Debug.Log($"[GameController] Game ended. Win: {isWin}, Score: {score}");
            _events.RaiseGameRoundEnded(isWin, score);
            // Any other game state cleanup here
        }

        public void Dispose()
        {
            UnSubscribeToEvents();
        }

        // listen to Miss event - calculate taps left and update model.
        // L
    }
}