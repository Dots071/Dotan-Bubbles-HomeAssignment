

using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Interfaces
{
    public interface IAssetLoader
    {
        public UniTask InitializeAsync();
        public UniTask<T> LoadAssetAsync<T>(AssetReference reference);

        public UniTask<SceneInstance> LoadSceneAsync(AssetReference reference, bool isAdditive);
        public void UnloadAsset<T>(T asset);
        public UniTask UnloadSceneAsync(SceneInstance sceneInstance);

    }
}
