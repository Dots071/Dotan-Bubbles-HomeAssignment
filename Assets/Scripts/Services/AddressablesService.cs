using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;




using System.Collections.Generic;


namespace Game.Services
{
    public static class AddressablesService
    {
        // Dictionary to track loaded assets.
        private static readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new Dictionary<string, AsyncOperationHandle>();

        public static async UniTask InitializeAsync()
        {
            AsyncOperationHandle initHandle = Addressables.InitializeAsync();
            try
            {
                await initHandle.ToUniTask();
                Debug.Log("[AddressablesService] Addressables initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AddressablesService] Addressables initialization failed: {ex.Message}");
            }
        }

        public static async UniTask<T> LoadAssetAsync<T>(AssetReference reference)
        {
            // Use the AssetReference's RuntimeKey as the dictionary key.
            string key = reference.RuntimeKey.ToString();
            if (_loadedAssets.ContainsKey(key))
            {
                Debug.Log($"Asset already loaded: {key}");
                // Optionally cast and return the already loaded asset
                return (T)_loadedAssets[key].Result;
            }

            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(reference);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedAssets[key] = handle;
                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load asset at address: {reference}");
                return default;
            }
        }

        public static async UniTask<SceneInstance> LoadSceneAsync(AssetReference reference, bool isAdditive)
        {
            LoadSceneMode loadMode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(reference, loadMode);
            await handle.Task.AsUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Scene loaded successfully: {reference}");
                // Optionally track scenes separately if needed.
                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load scene at address: {reference}");
                return default;
            }
        }

        public static void UnloadAsset<T>(T asset, AssetReference reference)
        {
            string key = reference.RuntimeKey.ToString();
            if (_loadedAssets.TryGetValue(key, out AsyncOperationHandle handle))
            {
                Addressables.Release(handle);
                _loadedAssets.Remove(key);
                Debug.Log($"Asset unloaded: {key}");
            }
            else
            {
                Debug.LogWarning($"Attempted to unload an asset that was not tracked: {key}");
            }
        }

        public static async UniTask UnloadSceneAsync(SceneInstance sceneInstance)
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
}


/*namespace Game.Services
{
    public static class AddressablesService
    {
        public static async UniTask InitializeAsync()
        {
            AsyncOperationHandle initHandle = Addressables.InitializeAsync();
            try
            {
                await initHandle.ToUniTask();
                Debug.Log("[AddressablesService] Addressables initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AddressablesService] Addressables initialization failed: {ex.Message}");
            }
        }

        public static async UniTask<T> LoadAssetAsync<T>(AssetReference reference)
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(reference);
            await handle.ToUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load asset at address: {reference}");
                return default;
            }
        }

        public static async UniTask<SceneInstance> LoadSceneAsync(AssetReference reference, bool isAdditive)
        {
            LoadSceneMode loadMode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(reference, loadMode);
            await handle.Task.AsUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Scene loaded successfully: {reference}");
                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load scene at address: {reference}");
                return default;
            }
        }

        public static void UnloadAsset<T>(T asset)
        {
            Addressables.Release(asset);
        }

        public static async UniTask UnloadSceneAsync(SceneInstance sceneInstance)
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
}*/
