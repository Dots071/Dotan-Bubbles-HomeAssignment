using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Utility.Enums;
using Game.Interfaces;

namespace Game.Views
{

    // TODO: Create an abstract ball class for common functionallity.,
    public class BallView : MonoBehaviour , IClickableBall
    {
        private Button _button;
        [SerializeField] private BallType _ballType; // move later to readonly
        public BallType BallType { get { return _ballType; } }
        public Vector2 BallPosition { get { return transform.position; } }

        public event Action<IClickableBall> OnBallClick;

        private void Awake()
        {
            _button = GetComponent<Button>();
            if (_button == null)
            {
                Debug.LogError($"BallView on {gameObject.name} is missing a Button component.");
            }
        }

        private void OnEnable()
        {
            if (_button != null)
            {
                _button.onClick.AddListener(HandleClick);
            }
        }

        private void OnDisable()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(HandleClick);
            }
        }

        private void HandleClick()
        {
            OnBallClick?.Invoke(this);
        }
    }
}

