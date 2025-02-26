using System;
using UnityEngine;
using Game.Utility.Enums;
using Game.Interfaces;
using UnityEngine.EventSystems;

namespace Game.Views
{
    // Visual representation of a ball that handles click interactions and animations
    public class BallView : MonoBehaviour, IClickableBall, IPointerClickHandler
    {
        [SerializeField] private BallType _ballType; // move later to readonly
        public BallType BallType { get { return _ballType; } }
        public Vector2 BallPosition { get { return transform.position; } }

        public event Action<IClickableBall> BallClick;
        public event Action<IClickableBall> OnBallDestroyed;

        public void OnPointerClick(PointerEventData eventData)
        {
            BallClick?.Invoke(this);
        }

        public void ExplodeBall()
        {
            OnBallDestroyed?.Invoke(this);
        }
    }
}

