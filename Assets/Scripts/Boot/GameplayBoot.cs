using Game.ScriptableObjects;
using UnityEngine;
using Game.Controllers;
using Game.Models;
using Game.Interfaces;
using Cysharp.Threading.Tasks;
using Game.Services;

namespace Game.Boot
{
    // Initializes and connects all gameplay components when the gameplay scene loads
    public class GameplayBoot : MonoBehaviour
    {
        [SerializeField] private ReactiveInt _score;
        [SerializeField] private ReactiveInt _taps;
        [SerializeField] private ReactiveInt _timeCounter;

        [SerializeField] private AssetReferencesSO _assetsRefrencesSO;
        [SerializeField] private BallsListSO _ballsListSO;
        [SerializeField] private GameConfigSO _gameConfig;

        [SerializeField] private Transform _ballsContainer;

        private ServiceLocatorSO _serviceLocatorSO;
        private ISceneManager _sceneManager;
        private GameEventAggregator _gameEventAggregator;
        private GameController _gameController;
        private BallSpawner _ballSpawner;
        private BallsController _ballsController;

        async void Start()
        {
            await BindObjects();
            await _sceneManager.LoadSceneAsync(_assetsRefrencesSO.GameplayHudScene, true);
            _gameEventAggregator.RaiseGameRoundStarted();

        }



        private async UniTask BindObjects()
        {
            _serviceLocatorSO = await AddressablesService.LoadAssetAsync<ServiceLocatorSO>(_assetsRefrencesSO.ServiceLocatorSO);
            _gameEventAggregator = _serviceLocatorSO.GetService<GameEventAggregator>();
            _sceneManager = _serviceLocatorSO.GetService<ISceneManager>();

            var gameModel = new GameModel(_score, _taps, _timeCounter, _gameConfig);
            _gameController = new GameController(_gameEventAggregator, gameModel);
            _ballSpawner = new BallSpawner(_ballsListSO, _serviceLocatorSO, _ballsContainer);
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