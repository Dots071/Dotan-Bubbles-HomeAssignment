using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Game.Interfaces;
using Game.ScriptableObjects;

namespace Game.Controllers
{
    // A non-MonoBehaviour ball spawner that uses UniTask for asynchronous operations.
    public class BallSpawner :  IDisposable
    {
        private readonly BallsListSO _ballPrefabs;
        private IGameEventsAgrregator _gameEventAggregator;
        private readonly Transform _spawnContainer;


        private const float _spawnInterval = 0.5f;
        private const int _maxBalls = 60;
        private const float _spawnMinX = -4;
        private const float _spawnMaxX = 4;
        private const float _spawnY = 9;

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private int _currentBallCount = 0;

        public BallSpawner(BallsListSO ballPrefabs, IGameEventsAgrregator gameEventAggregator, Transform spawnContainer)
        {
            _ballPrefabs = ballPrefabs;
            _gameEventAggregator = gameEventAggregator;
            _spawnContainer = spawnContainer;

            StartGameWithDelay(200).Forget();
        }

        private async UniTaskVoid StartGameWithDelay(int delay)
        {
            await UniTask.Delay(delay);
            StartSpawning(_cancellationTokenSource.Token).Forget();
        }

        public async UniTaskVoid StartSpawning(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_currentBallCount < _maxBalls)
                {
                    SpawnBall();
                }
                await UniTask.Delay(TimeSpan.FromSeconds(_spawnInterval), cancellationToken: cancellationToken);
            }
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
            if (_cancellationTokenSource != null) _cancellationTokenSource.Cancel();
        }
    }
}
