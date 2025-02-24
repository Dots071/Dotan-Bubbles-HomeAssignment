
using Cysharp.Threading.Tasks;

namespace Game.Interfaces
{
    public interface ISceneManager
    {
        public UniTask LoadSceneAsync(string sceneName, bool isAdditive);
        public UniTask<bool> UnloadSceneAsync(string sceneAddress);
    }
}

