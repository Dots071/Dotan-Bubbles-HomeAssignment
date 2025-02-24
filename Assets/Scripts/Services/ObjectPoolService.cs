using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    /// <summary>
    /// A generic object pool that pre-instantiates and recycles instances of a Component.
    /// </summary>
    public class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Queue<T> _pool;
        private readonly Transform _parent;

        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new Queue<T>(initialSize);

            // Pre-instantiate objects and disable them.
            for (int i = 0; i < initialSize; i++)
            {
                T instance = Object.Instantiate(_prefab, _parent);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }
        }

        /// <summary>
        /// Retrieves an instance from the pool, instantiating a new one if needed.
        /// </summary>
        public T Get()
        {
            T obj = _pool.Count > 0 ? _pool.Dequeue() : Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(true);
            return obj;
        }

        /// <summary>
        /// Returns an instance to the pool.
        /// </summary>
        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

}

