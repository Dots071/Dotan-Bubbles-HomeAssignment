using Game.ScriptableObjects;
using UnityEngine;
using Game.Controllers;
using Game.Models;
using Game.Interfaces;
using System;
using Cysharp.Threading.Tasks;
using Game.Services;

namespace Game.Boot
{
    public class GameplayBoot : MonoBehaviour
    {
        [SerializeField] private ReactiveInt _score;
        [SerializeField] private ReactiveInt _taps;
        [SerializeField] private ReactiveInt _timeCounter;

        [SerializeField] private AssetReferencesSO _assetsRefrencesSO;
        [SerializeField] private BallsListSO _ballsListSO;
        [SerializeField] private GameEventAggregator _gameEventAggregator;

        [SerializeField] private Transform _ballsContainer;

        private ServiceLocatorSO _serviceLocatorSO;
        private GameController _gameController;
        private BallSpawner _ballSpawner;
        private BallsController _ballsController;

        async void Start()
        {
            await BindObjects();
            _gameEventAggregator.RaiseGameRoundStarted();

        }



        private async UniTask BindObjects()
        {
            _serviceLocatorSO = await AddressablesService.LoadAssetAsync<ServiceLocatorSO>(_assetsRefrencesSO.ServiceLocatorSO);
            var gameModel = new GameModel(_score, _taps, _timeCounter);
            _gameController = new GameController(_gameEventAggregator, gameModel);
            _ballSpawner = new BallSpawner(_ballsListSO, _gameEventAggregator, _serviceLocatorSO, _ballsContainer);
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