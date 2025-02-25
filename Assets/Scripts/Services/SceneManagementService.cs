using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Services
{
    public class SceneManagerService : ISceneManager
    {
        private readonly Dictionary<AssetReference, SceneInstance> _loadedScenes = new Dictionary<AssetReference, SceneInstance>();

        public async UniTask LoadSceneAsync(AssetReference reference, bool isAdditive = false)
        {
            var sceneInstance = await AddressablesService.LoadSceneAsync(reference, isAdditive);
            _loadedScenes[reference] = sceneInstance;
        }

        public async UniTask<bool> UnloadSceneAsync(AssetReference reference)
        {
            if (_loadedScenes.TryGetValue(reference, out SceneInstance sceneInstance))
            {
                try
                {
                    await AddressablesService.UnloadSceneAsync(sceneInstance);
                    return true;
                }
                catch(Exception ex)
                {
                    Debug.LogError($"[SceneManagementService] Failed to unload scene: {reference}, error: {ex}");
                    return false;
                }
            }
            else
            {
                Debug.LogWarning($"Scene {reference} is not loaded.");
                return false;
            }
        }
    }
}
