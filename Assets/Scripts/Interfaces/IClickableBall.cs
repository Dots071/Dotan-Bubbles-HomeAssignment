using UnityEngine;
using Game.Utility.Enums;
using System;

namespace Game.Interfaces
{
    // Interface for balls that can be clicked, defining properties and events
    public interface IClickableBall
    {
        public BallType BallType { get; }
        public Vector2 BallPosition { get; }

        public event Action<IClickableBall> BallClick;
        public event Action<IClickableBall> OnBallDestroyed;

        public void ExplodeBall();

    }
}