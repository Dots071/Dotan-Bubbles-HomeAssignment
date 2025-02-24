

using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Game.Interfaces
{
    public interface IAssetLoader
    {
        public UniTask InitializeAsync();
        public UniTask<T> LoadAssetAsync<T>(string address);

        public UniTask<SceneInstance> LoadSceneAsync(string sceneAddress, bool isAdditive);
        public void UnloadAsset<T>(T asset);
        public UniTask UnloadSceneAsync(SceneInstance sceneInstance);

    }
}
