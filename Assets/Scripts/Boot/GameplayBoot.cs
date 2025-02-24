using Game.ScriptableObjects;
using UnityEngine;
using Game.Controllers;
using Game.Interfaces;

namespace Game.Boot
{
    public class GameplayBoot : MonoBehaviour
    {
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
            _gameController = new GameController(_gameEventAggregator);
            _ballSpawner = new BallSpawner(_ballsListSO, _gameEventAggregator, _ballsContainer);
            _ballsController = new BallsController(_gameEventAggregator);
        }

        private void OnDestroy()
        {
            _gameController.Dispose();
            _ballsController.Dispose();
            _ballSpawner.Dispose();
        }

    }
}