using UnityEngine;
using Cysharp.Threading.Tasks;
using Game.Interfaces;
using Game.ScriptableObjects;
using Game.Services;
using Game.Utility;
using UnityEngine.SceneManagement;

// TODO: remove unnecessary libraries from the assembley definition also.
namespace Game.Boot
{
    /// <summary>
    /// The AppBoot class is responsible for showing the background image ASAP and initialize the game. 
    /// I didn't load from addressables the first to show objects to make sure the user will not see a black screen.
    /// </summary>
    public class AppBoot : MonoBehaviour
    {
        [SerializeField] private GameObject[] _objectsToInstantiaiteOnBoot;
        [SerializeField] private AssetReferencesSO _assetReferences;

        private async void Start()
        {
            InstantiateLoadingSceneAssets();


            var sceneManager = new SceneManagerService();
            await sceneManager.LoadSceneAsync(_assetReferences.LoadingScene, true);
        }

        private void InstantiateLoadingSceneAssets()
        {
            for (int i = 0; i < _objectsToInstantiaiteOnBoot.Length; i++)
            {
                Instantiate(_objectsToInstantiaiteOnBoot[i]);
            }

        }

    }
}



