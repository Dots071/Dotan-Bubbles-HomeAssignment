using System.Collections.Generic;
using UnityEngine;
using Game.Utility.Enums;
using Game.Interfaces;

namespace Game.ScriptableObjects
{
    // Object pooling system that manages reusable GameObjects to improve performance
    [CreateAssetMenu(fileName = "ObjectPoolSO", menuName = "Scriptable Objects/Object Pool")]
    public class ObjectPoolSO : ScriptableObject
    {
        public int initialPoolSize = 20;

        // Dictionary of pools, one for each ball type
        private Dictionary<BallType, Queue<GameObject>> _pools = new Dictionary<BallType, Queue<GameObject>>();
        private Dictionary<BallType, GameObject> _prefabsByType = new Dictionary<BallType, GameObject>();
        private Transform _poolParent;
        
        /// <summary>
        /// Prewarm a pool for a specific ball type
        /// </summary>
        public void PrewarmPool(BallType ballType, GameObject prefab, Transform parent = null)
        {
            // Create a persistent parent if one wasn't provided
            if (_poolParent == null)
            {
                GameObject poolObj = new GameObject("Ball Pools");
                Object.DontDestroyOnLoad(poolObj);
                _poolParent = poolObj.transform;
            }
             
            // Save the prefab reference for this type
            _prefabsByType[ballType] = prefab;
            
            // Initialize the pool for this type if it doesn't exist
            if (!_pools.ContainsKey(ballType))
            {
                _pools[ballType] = new Queue<GameObject>();
                
                for (int i = 0; i < initialPoolSize; i++)
                {
                    GameObject instance = Instantiate(prefab, _poolParent);
                    instance.SetActive(false);
                    _pools[ballType].Enqueue(instance);
                }
            }
        }
        
        /// <summary>
        /// Prewarm pools for all ball types in the ball list
        /// </summary>
        public void PrewarmPools(BallsListSO ballsList, Transform parent = null)
        {
            foreach (var ballPrefab in ballsList.BallPrefabs)
            {
                var clickableBall = ballPrefab.GetComponent<IClickableBall>();
                if (clickableBall != null)
                {
                    PrewarmPool(clickableBall.BallType, ballPrefab, parent);
                }
            }
        }
        
        // Clear all pools or a specific pool
        public void ClearPool(BallType? ballType = null)
        {
            if (ballType.HasValue)
            {
                // Clear specific pool
                if (_pools.TryGetValue(ballType.Value, out Queue<GameObject> pool))
                {
                    while (pool.Count > 0)
                    {
                        GameObject obj = pool.Dequeue();
                        if (obj != null)
                        {
                            Object.Destroy(obj);
                        }
                    }
                    _pools.Remove(ballType.Value);
                    _prefabsByType.Remove(ballType.Value);
                }
            }
            else
            {
                foreach (var pool in _pools.Values)
                {
                    while (pool.Count > 0)
                    {
                        GameObject obj = pool.Dequeue();
                        if (obj != null)
                        {
                            Object.Destroy(obj);
                        }
                    }
                }
                _pools.Clear();
                _prefabsByType.Clear();
            }
        }

        /// <summary>
        /// Retrieves an object from the pool for the specified ball type
        /// </summary>
        public GameObject GetObject(BallType ballType, Transform parentToSet = null)
        {
            if (!_pools.ContainsKey(ballType))
            {
                Debug.LogError($"No pool initialized for ball type: {ballType}");
                return null;
            }
            
            GameObject instance;
            if (_pools[ballType].Count > 0)
            {
                instance = _pools[ballType].Dequeue();
                
                if (instance == null)
                {
                    if (_prefabsByType.TryGetValue(ballType, out GameObject prefab))
                    {
                        instance = Instantiate(prefab, parentToSet);
                    }
                    else
                    {
                        Debug.LogError($"No prefab found for ball type: {ballType}");
                        return null;
                    }
                }
                else
                {
                    instance.transform.SetParent(parentToSet);
                }
            }
            else
            {
                // Create a new instance if pool is empty
                if (_prefabsByType.TryGetValue(ballType, out GameObject prefab))
                {
                    instance = Instantiate(prefab, parentToSet);
                }
                else
                {
                    Debug.LogError($"No prefab found for ball type: {ballType}");
                    return null;
                }
            }
            
            instance.SetActive(true);
            return instance;
        }

        /// <summary>
        /// Returns an object back to its appropriate pool based on ball type
        /// </summary>
        public void ReturnObject(GameObject instance)
        {
            if (instance == null) return;
            
            // Get the ball type from the interface instead of concrete class
            var clickableBall = instance.GetComponent<IClickableBall>();
            if (clickableBall == null)
            {
                Debug.LogWarning("Attempted to return non-ball object to ball pool.");
                return;
            }
            
            BallType ballType = clickableBall.BallType;
            
            if (!_pools.ContainsKey(ballType))
            {
                _pools[ballType] = new Queue<GameObject>();
                
                if (_poolParent == null)
                {
                    GameObject poolObj = new GameObject("Ball Pools ");
                    DontDestroyOnLoad(poolObj);
                    _poolParent = poolObj.transform;
                }
                
            }         
            instance.SetActive(false);         
            instance.transform.SetParent(_poolParent);
            _pools[ballType].Enqueue(instance);
        }
        
        private void OnDestroy()
        {
            ClearPool();
        }
    }
}
