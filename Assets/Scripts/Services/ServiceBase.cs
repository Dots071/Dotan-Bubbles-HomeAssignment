
using Cysharp.Threading.Tasks;

namespace Game.Services 
{
    /// <summary>
    /// An abstract base class for services to reduce duplicated functionalities.
    /// </summary>
    public abstract class ServiceBase
    {
        public bool IsInitialized { get; private set; } = false;

        public virtual async UniTask InitializeAsync()
        {
            await UniTask.CompletedTask;
            IsInitialized = true;
        }

        public virtual void Shutdown()
        {
            IsInitialized = false;
        }
    }
}
