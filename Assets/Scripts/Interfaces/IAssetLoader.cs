

using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public interface IAssetLoader
{
    public UniTask InitializeAsync();
    public UniTask<T> LoadAssetAsync<T>(string address);

    public UniTask<SceneInstance> LoadSceneAsync(string sceneAddress, LoadSceneMode loadMode = LoadSceneMode.Single);
    public void UnloadAsset<T>(T asset);
    public UniTask UnloadSceneAsync(SceneInstance sceneInstance);

}
