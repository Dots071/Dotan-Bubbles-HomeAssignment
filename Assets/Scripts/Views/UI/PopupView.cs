using Game.ScriptableObjects;
using UnityEngine;
using TMPro;
using Game.Services;
using UnityEngine.UI;
using System;
using Game.Interfaces;



namespace Game.Views
    {
        public class PopupView : MonoBehaviour
        {
            [SerializeField] private GameObject _popup;
            [SerializeField] private TMP_Text _title;
            [SerializeField] private AssetReferencesSO _assetReferences;

            [SerializeField] private Button _replayButton;
            [SerializeField] private Button _backButton;

            private ServiceLocatorSO _serviceLocator;
            private GameEventAggregator _gameEventAggregator;
            private ISceneManager _sceneManager;
            private const string _winText = "You Win!";
            private const string _loseText = "You Lose..";

        private async void Start()
        {
            _serviceLocator = await AddressablesService.LoadAssetAsync<ServiceLocatorSO>(_assetReferences.ServiceLocatorSO);
            _gameEventAggregator = _serviceLocator.GetService<GameEventAggregator>();
            _sceneManager = _serviceLocator.GetService<ISceneManager>();


            _gameEventAggregator.GameRoundEnded += OnGameRoundEnded;
        }

        private void OnEnable()
        {
            _replayButton.onClick.AddListener(OnReplayPressed);
            _backButton.onClick.AddListener(OnBackPressed);
            _popup.SetActive(false);
        }
        private void OnDisable()
        {
            _replayButton.onClick.RemoveListener(OnReplayPressed);
            _backButton.onClick.RemoveListener(OnBackPressed);
        }

        private void OnReplayPressed()
        {
            _sceneManager.LoadSceneAsync(_assetReferences.GameplayScene, false);
        }

        private void OnBackPressed()
        {
            _sceneManager.LoadSceneAsync(_assetReferences.MenuScene, false);
        }

        private void OnGameRoundEnded(bool isWin, int score)
            {
                var popupTitle = isWin ? _winText : _loseText;
                ShowPopup(popupTitle);
                
            }

            private void ShowPopup(string title)
            {
                _popup.SetActive(true);
                _title.text = title;
                // TODO: add tweening animation.
            }


            private void OnDestroy()
            {


            if (_gameEventAggregator != null)
                    _gameEventAggregator.GameRoundEnded -= OnGameRoundEnded;
            }
        }
    }
