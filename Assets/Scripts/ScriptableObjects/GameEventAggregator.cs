using System;
using UnityEngine;
using Game.Interfaces;
using System.Xml.Serialization;

namespace Game.ScriptableObjects
{
    // Event system that allows different parts of the game to communicate without direct references
    [CreateAssetMenu(fileName = "GameEventsAggregator", menuName = "Scriptable Objects/Events/Events Aggregator")]
    public class GameEventAggregator : ScriptableObject
    {

        // Gameplay events
        public event Action GameRoundStarted;
        public event Action<IClickableBall> BallSpawned;

        public event Action<int> BallsExploded;
        public event Action TimerEnded;

        public event Action PlayerMissed;
        public event Action<bool,int> GameRoundEnded;

        public void RaiseGameRoundStarted() => GameRoundStarted?.Invoke();
        public void RaiseBallSpawned(IClickableBall ball) => BallSpawned?.Invoke(ball);
        public void RaiseBallsExploded(int amount) => BallsExploded?.Invoke(amount);
        public void RaisePlayerMissed() => PlayerMissed?.Invoke();
        public void RaiseGameRoundEnded(bool isWin, int score) => GameRoundEnded?.Invoke(isWin, score);


    }
}


