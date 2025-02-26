using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game.Interfaces;
using Game.ScriptableObjects;

namespace Game.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BallSpawner :  IDisposable
    {
        private readonly BallsListSO _ballPrefabs;
        private readonly GameEventAggregator _gameEventAggregator;
        private readonly Transform _spawnContainer;


        private readonly ServiceLocatorSO _serviceLocator;
        private readonly ObjectPoolSO _objectPoolSO;


        private const float _spawnInterval = 0.5f;
        private const int _maxBalls = 60;   // Get this dynamically.
        private const float _spawnMinX = -4;
        private const float _spawnMaxX = 4;
        private const float _spawnY = 9;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private int _currentBallCount = 0;

        public BallSpawner(BallsListSO ballPrefabs, GameEventAggregator gameEventAggregator,ServiceLocatorSO serviceLocatorSO, Transform spawnContainer)
        {
            _ballPrefabs = ballPrefabs;
            _gameEventAggregator = gameEventAggregator;
            _spawnContainer = spawnContainer;

            _serviceLocator = serviceLocatorSO;
            _objectPoolSO = _serviceLocator.GetService<ObjectPoolSO>();

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _gameEventAggregator.GameRoundStarted += OnRoundStarted;
        }

        private void UnSubscribeToEvents()
        {
            _gameEventAggregator.GameRoundStarted -= OnRoundStarted;
        }
        private void OnRoundStarted()
        {
            StartSpawning(_cancellationTokenSource.Token).Forget();
        }

        public async UniTaskVoid StartSpawning(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_currentBallCount < _maxBalls)
                {
                    SpawnBallFromPool();
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval), cancellationToken: cancellationToken);
            }
        }

        private void SpawnBallFromPool()
        {
            float spawnX = UnityEngine.Random.Range(_spawnMinX, _spawnMaxX);
            var newBall = _objectPoolSO.GetObject(_spawnContainer);
            var clickComponent = newBall.GetComponent<IClickableBall>();

            _gameEventAggregator.RaiseBallSpawned(clickComponent);
            _currentBallCount++;
        }

        private void SpawnBall()
        {
            float spawnX = UnityEngine.Random.Range(_spawnMinX, _spawnMaxX);
            Vector3 spawnPosition = new Vector3(spawnX, _spawnY, 0f);

            GameObject prefab = _ballPrefabs.GetRandomObject();
            GameObject newBall = UnityEngine.Object.Instantiate(prefab, spawnPosition, Quaternion.identity, _spawnContainer);

            var ball = newBall.GetComponent<IClickableBall>();
            _gameEventAggregator.RaiseBallSpawned(ball);
            _currentBallCount++;

            // TODO: subscribe to an event on the ball so that when it is removed,
            // you can decrement _currentBallCount accordingly.
        }

        public void Dispose()
        {
            UnSubscribeToEvents();
            if (_cancellationTokenSource != null) _cancellationTokenSource.Cancel();
        }
    }
}
