using System;
using UnityEngine;
using Game.Interfaces;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameEventsAggregator", menuName = "Scriptable Objects/Events/Events Aggregator")]
    public class GameEventAggregator : ScriptableObject
    {
        public event Action<IClickableBall> BallSpawned;


        public void RaiseBallSpawned(IClickableBall ball)
        {
            BallSpawned?.Invoke(ball);
        }
    }
}


