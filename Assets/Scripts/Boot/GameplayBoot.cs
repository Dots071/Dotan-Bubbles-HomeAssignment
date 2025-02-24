using Game.ScriptableObjects;
using UnityEngine;
using Game.Controllers;
using Game.Models;
using Game.Interfaces;
using System;

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

        private void Update()
        {
           if(Input.GetKey(KeyCode.Space))
            {
                var pool = _serviceLocatorSO.GetService<ObjectPoolSO>();
                var ball = pool.GetObject(_ballsContainer.transform);
                ball.transform.position = Vector3.one;
                Debug.Log($"Ball instantiated {ball.name}, position: {ball.transform.position}");
            }
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