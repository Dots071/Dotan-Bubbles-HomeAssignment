using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;


// TODO: maybe hold loaded assets dictionary for validation.
public class AddressablesService : ServiceBase, IAssetLoader
{
    /// <summary>
    /// Initializes the Addressables system.
    /// </summary>
    public override async UniTask InitializeAsync()
    {
        AsyncOperationHandle initHandle = Addressables.InitializeAsync();
        await initHandle.ToUniTask();
        if (initHandle.Status == AsyncOperationStatus.Succeeded)
        {
            await base.InitializeAsync();
        }
        else
        {
            Debug.LogError("Addressables initialization failed!");
        }
    }

    /// <summary>
    /// Loads an asset of type T by its Addressable address / name.
    /// </summary>
    public async UniTask<T> LoadAssetAsync<T>(string address)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
        await handle.ToUniTask();
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogError($"Failed to load asset at address: {address}");
            return default;
        }
    }

    /// <summary>
    /// Loads a scene asynchronously by its Addressable address / name.
    /// </summary>
    public async UniTask<SceneInstance> LoadSceneAsync(string sceneAddress, LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAddress, loadMode, activateOnLoad: true);
        await handle.ToUniTask();
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogError($"Failed to load scene at address: {sceneAddress}");
            return default;
        }
    }

    /// <summary>
    /// Unloads an asset previously loaded via Addressables.
    /// </summary>
    public void UnloadAsset<T>(T asset)
    {
        Addressables.Release(asset);
    }

    /// <summary>
    /// Unloads a scene that was loaded via Addressables.
    /// </summary>
    public async UniTask UnloadSceneAsync(SceneInstance sceneInstance)
    {
        AsyncOperationHandle handle = Addressables.UnloadSceneAsync(sceneInstance);
        await handle.ToUniTask();
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Scene unloaded successfully.");
        }
        else
        {
            Debug.LogError("Failed to unload scene.");
        }
    }
}
