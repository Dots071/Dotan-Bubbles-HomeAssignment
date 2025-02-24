using Game.Interfaces;
using System;
using UnityEngine;

namespace Game.Controllers
{
    public class GameController : IDisposable
    {
        private IGameEventsAgrregator _events;
        public GameController(IGameEventsAgrregator gameEventAggregator)
        {
            _events = gameEventAggregator;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _events.PlayerMissed += OnPlayerMissed;
        }

        private void OnPlayerMissed()
        {
            Debug.Log("[GameController] player missed!");
        }

        public void Dispose()
        {
            _events.PlayerMissed -= OnPlayerMissed;
        }

        // listen to Miss event - calculate taps left and update model.
        // L
    }
}