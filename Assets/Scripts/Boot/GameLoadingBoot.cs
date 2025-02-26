using Cysharp.Threading.Tasks;
using Game.Interfaces;
using Game.ScriptableObjects;
using Game.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages the loading screen, including progress bar updates and transition to the main menu
public class GameLoadingBoot : MonoBehaviour
{

    [SerializeField] private AssetReferencesSO _assetReferences;
    [SerializeField] private Slider _loadingProgress;

    private ServiceLocatorSO _serviceLocator;
    private BallsListSO _balls;
    private GameEventAggregator _gameEventAggregator;
    private ISceneManager _sceneManager;
    private IPlayerPrefsService _playerPrefsService;



    private async void Start()
    {

        await SetupData();
        UpdateProgress(0.1f);

        await RegisterServices();
        UpdateProgress(0.2f);

        await PrewarmObjectPool();
        UpdateProgress(0.4f);

        UpdateProgress(0.6f);

        // get data from playerPrefs and dummy server.
        UpdateProgress(0.7f);

        // 
        await UniTask.Delay(100);
        // preload the Menu scene
        UpdateProgress(1f);

        await DisposeLoadingScene();
    }

    /// <summary>
    /// Bootstraps the services to the ServiceLocatorSO
    /// </summary>
    private async UniTask RegisterServices()
    {
        _serviceLocator = await AddressablesService.LoadAssetAsync<ServiceLocatorSO>(_assetReferences.ServiceLocatorSO);
        _balls = await AddressablesService.LoadAssetAsync<BallsListSO>(_assetReferences.BallsSO);

        _sceneManager = new SceneManagerService();
        _serviceLocator.RegisterService(_sceneManager);

        _playerPrefsService = new PlayerPrefsService();
        _serviceLocator.RegisterService(_playerPrefsService);

        _gameEventAggregator = new GameEventAggregator();
        _serviceLocator.RegisterService(_gameEventAggregator);



        _serviceLocator.LogRegisteredServices();

    }
    private async UniTask SetupData()
    {
        await UniTask.Delay(100);
    }

    private async UniTask PrewarmObjectPool()
    {
        var ballPool = await AddressablesService.LoadAssetAsync<ObjectPoolSO>(_assetReferences.BallsObjectPool);
        _serviceLocator.RegisterService(ballPool);
        ballPool.PrewarmPools(_balls);
    }


    private async UniTask InitializeServicesAsync()
    {
        await UniTask.Delay(100);
    }

    private void UpdateProgress(float value)
    {
        if(_loadingProgress != null) _loadingProgress.value = value;
    }

    private async UniTask DisposeLoadingScene()
    {
        await _sceneManager.LoadSceneAsync(_assetReferences.MenuScene, true);

        await SceneManager.UnloadSceneAsync(0);
        //SceneManager.UnloadSceneAsync(0);
        // Start transition animation
        // Unload loading scene.
    }
}
