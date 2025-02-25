
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Interfaces
{
    public interface ISceneManager
    {
        public UniTask LoadSceneAsync(AssetReference reference, bool isAdditive);
        public UniTask<bool> UnloadSceneAsync(AssetReference reference);
    }
}

