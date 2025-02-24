using Game.Interfaces;
using UnityEngine;

namespace Game.Services
{
    /// <summary>
    /// A generic factory that creates instances of T using a provided prefab and parent transform.
    /// </summary>
    public class GenericFactory<T> : IFactory<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;

        public GenericFactory(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public T Create()
        {
            return Object.Instantiate(_prefab, _parent);
        }

    }
}
