using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class AppBoot : MonoBehaviour
{
    //TODO: download it from Addressables.
    [SerializeField] private Slider _progressBar;

    [SerializeField] private ServiceLocatorSO _serviceLocator;

    private IAssetLoader _assetLoaderService;

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
    }

    private async void Start()
    {

        await _serviceLocator.InitializeAllServicesAsync();
        UpdateProgress(0.2f);

        // get data from playerPrefs and dummy server.
        await SetupData();
        UpdateProgress(0.5f);

        await InstantiateLoadingSceneAssets();
        UpdateProgress(0.7f);

        // preload the Menu scene
        await _assetLoaderService.LoadSceneAsync("MenuSceneAddress", LoadSceneMode.Single);
        UpdateProgress(1f);

        DisposeLoadingScene();
    }
    private async UniTask SetupData()
    {
        await UniTask.Delay(100);
    }
   
    private async UniTask InstantiateLoadingSceneAssets()
    {
        await UniTask.Delay(100);

    }

    private void UpdateProgress(float value)
    {
        if (_progressBar)
        {
            _progressBar.value = value;
        }
    }

    private void DisposeLoadingScene()
    {
        // Start transition animation
        // Unload loading scene.
    }
}

