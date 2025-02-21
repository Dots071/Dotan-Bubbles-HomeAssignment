using UnityEngine;
using Cysharp.Threading.Tasks;
using Game.Interfaces;
using Game.ScriptableObjects;
using Game.Services;
using Game.Utility;

namespace Game.Boot
{
    /// <summary>
    /// The AppBoot class is responsible for showing the background image ASAP and initialize the game. 
    /// I didn't load from addressables the first to show objects to make sure the user will not see a black screen.
    /// </summary>
    public class AppBoot : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objectsToInstantiaiteOnBoot;
        [SerializeField] private ServiceLocatorSO _serviceLocator;

        [SerializeField] private ReactiveFlaot _loadingProgress;

        private IAssetLoader _assetLoaderService;
        private ISceneManager _sceneManager;

        private void Awake()
        {
            RegisterServices();
        }

        /// <summary>
        /// Bootstraps the services to the ServiceLocatorSO
        /// </summary>
        private void RegisterServices()
        {
            _assetLoaderService = new AddressablesService();
            _serviceLocator.RegisterService(_assetLoaderService);

            _sceneManager = new SceneManagerService(_assetLoaderService);
            _serviceLocator.RegisterService(_sceneManager);
        }

        private async void Start()
        {
            InstantiateLoadingSceneAssets();
            UpdateProgress(0.1f);

            await InitializeServicesAsync();
            UpdateProgress(0.2f);

            await _sceneManager.LoadSceneAsync(Constants.AddressableAssetPaths.LoadingScene, true);
            UpdateProgress(0.5f);

            // get data from playerPrefs and dummy server.
            await SetupData();
            UpdateProgress(0.7f);

            // 
            await UniTask.Delay(100);
            // preload the Menu scene
            UpdateProgress(1f);

            DisposeLoadingScene();
        }
        private async UniTask SetupData()
        {
            await UniTask.Delay(100);
        }

        private void InstantiateLoadingSceneAssets()
        {
            for (int i = 0; i < _objectsToInstantiaiteOnBoot.Length; i++)
            {
                Instantiate(_objectsToInstantiaiteOnBoot[i]);
            }

        }

        private async UniTask InitializeServicesAsync()
        {
            await _assetLoaderService.InitializeAsync();
            await UniTask.Delay(100);
        }

        private void UpdateProgress(float value)
        {
            _loadingProgress.Value = value;
        }

        private void DisposeLoadingScene()
        {
            // Start transition animation
            // Unload loading scene.
        }
    }
}



