using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ObjectPoolSO", menuName = "Scriptable Objects/Object Pool")]
    public class ObjectPoolSO : ScriptableObject
    {
        [Header("Pool Configuration")]
        [Tooltip("Prefab that will be pooled.")]
        public GameObject prefab;

        [Tooltip("Initial number of instances to prewarm.")]
        public int initialPoolSize = 20;

        // Runtime pool (not serialized)
        private Queue<GameObject> pool = new Queue<GameObject>();

        /// <summary>
        /// Prewarm the pool by instantiating the initial number of objects.
        /// Call this from your loading scene, providing a parent transform if desired.
        /// </summary>
        public void PrewarmPool(Transform parent = null)
        {
            // Clear any existing pool entries.
            pool.Clear();

            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject instance = Instantiate(prefab, parent);
                instance.SetActive(false);
                pool.Enqueue(instance);
            }
        }

        /// <summary>
        /// Retrieves an object from the pool.
        /// If the pool is empty, a new instance is created.
        /// </summary>
        public GameObject GetObject(Transform parent = null)
        {
            GameObject instance;
            if (pool.Count > 0)
            {
                instance = pool.Dequeue();
            }
            else
            {
                instance = Instantiate(prefab, parent);
            }
            instance.SetActive(true);
            return instance;
        }

        /// <summary>
        /// Returns an object back to the pool.
        /// </summary>
        public void ReturnObject(GameObject instance)
        {
            instance.SetActive(false);
            pool.Enqueue(instance);
        }
    }
}
