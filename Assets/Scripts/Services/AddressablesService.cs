using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using Game.Interfaces;
using System;


// TODO: maybe hold loaded assets dictionary for validation.

namespace Game.Services
{
    public class AddressablesService : IAssetLoader
    {
        public async UniTask InitializeAsync()
        {
            AsyncOperationHandle initHandle = Addressables.InitializeAsync();
            try
            {
                // Await the operation and let any exceptions propagate.
                await initHandle.ToUniTask();
                Debug.Log("[AddressablesService] Addressables initialized successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AddressablesService] Addressables initialization failed: {ex.Message}");
            }
        }

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

        public async UniTask<SceneInstance> LoadSceneAsync(string sceneAddress, bool isAdditive)
        {
            var loadMode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAddress, loadMode);
            await handle.Task.AsUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Scene loaded successfully: {sceneAddress}");
                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load scene at address: {sceneAddress}");
                return default;
            }
        }

        /*public  bool LoadSceneAsync(string sceneAddress, bool isAdditive)
        {
            var loadMode = isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
            try
            {
                AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneAddress, loadMode);
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    //return handle.Result;
                    return true;
                }
*//*                else
                {
                    Debug.LogError($"Failed to load scene at address: {sceneAddress}");
                    return false;
                }*//*
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load scene at address: {ex}");

            }

            return false;
        }*/

        public void UnloadAsset<T>(T asset)
        {
            Addressables.Release(asset);
        }


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
}

