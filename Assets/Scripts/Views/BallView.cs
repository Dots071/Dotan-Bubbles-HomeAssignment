using System;
using UnityEngine;
using Game.Utility.Enums;
using Game.Interfaces;
using UnityEngine.EventSystems;

namespace Game.Views
{

    // TODO: Create an abstract ball class for common functionallity.,
    public class BallView : MonoBehaviour, IClickableBall, IPointerClickHandler
    {
        [SerializeField] private BallType _ballType; // move later to readonly
        public BallType BallType { get { return _ballType; } }
        public Vector2 BallPosition { get { return transform.position; } }

        public event Action<IClickableBall> OnBallClick;



        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"click event {eventData} clickedt.");

            HandleClick();
        }

        private void HandleClick()
        {
            Debug.Log($"BallView on {gameObject.name} clickedt.");

            OnBallClick?.Invoke(this);
        }

        public void ExplodeBall()
        {
            Destroy(gameObject);
        }

    }
}

