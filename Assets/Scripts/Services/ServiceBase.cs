using Cysharp.Threading.Tasks;

namespace Game.Services 
{
    /// <summary>
    /// Base class for all services with initialization and shutdown functionality.
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
