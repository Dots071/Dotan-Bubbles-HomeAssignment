using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Services
{
    public class SceneManagerService : ISceneManager
    {
        private readonly IAssetLoader _addressablesService;
        private readonly Dictionary<string, SceneInstance> _loadedScenes = new Dictionary<string, SceneInstance>();

        public SceneManagerService(IAssetLoader addressablesService)
        {
            _addressablesService = addressablesService;
        }

        public async UniTask LoadSceneAsync(string sceneAddress, bool isAdditive = false)
        {
            var handle = await _addressablesService.LoadSceneAsync(sceneAddress, isAdditive);

            Debug.Log(handle);
            await UniTask.Delay(100);

            _loadedScenes[sceneAddress] = handle;
            Debug.Log($"Scene loaded successfully: {sceneAddress}");

        }

        public async UniTask<bool> UnloadSceneAsync(string sceneAddress)
        {
            if (_loadedScenes.TryGetValue(sceneAddress, out SceneInstance sceneInstance))
            {
                AsyncOperationHandle handle = Addressables.UnloadSceneAsync(sceneInstance);
                await handle.ToUniTask();

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    _loadedScenes.Remove(sceneAddress);
                    Debug.Log($"Scene unloaded successfully: {sceneAddress}");
                    return true;
                }
                else
                {
                    Debug.LogError($"Failed to unload scene at address: {sceneAddress}");
                    return false;
                }
            }
            else
            {
                Debug.LogWarning($"Scene {sceneAddress} is not loaded.");
                return false;
            }
        }
    }
}
