using Game.ScriptableObjects;
using UnityEngine;
using Game.Controllers;
using Game.Models;
using Game.Interfaces;

namespace Game.Boot
{
    public class GameplayBoot : MonoBehaviour
    {
        [SerializeField] private ReactiveInt _score;
        [SerializeField] private ReactiveInt _taps;
        [SerializeField] private ReactiveInt _timeCounter;

        [SerializeField] private BallsListSO _ballsListSO;
        [SerializeField] private ServiceLocatorSO _serviceLocatorSO;
        [SerializeField] private GameEventAggregator _gameEventAggregator;

        [SerializeField] private Transform _ballsContainer;

        private GameController _gameController;
        private BallSpawner _ballSpawner;
        private BallsController _ballsController;

        void Start()
        {
            BindObjects();
        }

        private void BindObjects()
        {
            var gameModel = new GameModel(_score, _taps, _timeCounter);
            _gameController = new GameController(_gameEventAggregator, gameModel);
            _ballSpawner = new BallSpawner(_ballsListSO, _gameEventAggregator, _ballsContainer);
            _ballsController = new BallsController(_gameEventAggregator);

            _gameEventAggregator.RaiseGameRoundStarted();
        }

        private void OnDestroy()
        {
            _gameController.Dispose();
            _ballsController.Dispose();
            _ballSpawner.Dispose();
        }

    }
}