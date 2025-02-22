using UnityEngine;
using Game.Utility.Enums;
using System;

namespace Game.Interfaces
{
    public interface IClickableBall
    {
        public BallType BallType { get; }
        public Vector2 BallPosition{ get; }

        public event Action<IClickableBall> OnBallClick;

    }
}