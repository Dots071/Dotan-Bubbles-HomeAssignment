using System.Collections.Generic;
using UnityEngine;
using Game.Utility.Enums;
using Game.Interfaces;

namespace Game.ScriptableObjects
{
    // Manages the collection of ball prefabs and provides methods to access them by type
    [CreateAssetMenu(fileName = "BallsListSO", menuName = "Scriptable Objects/BallsListSO")]
    public class BallsListSO : ScriptableObject
    {
        public List<GameObject> BallPrefabs;
        
        // Dictionary to look up prefabs by type
        private Dictionary<BallType, List<GameObject>> _prefabsByType;

        private void Add(GameObject obj)
        {
            BallPrefabs.Add(obj);
            ClearTypeDictionary(); 
        }

        private void Remove(GameObject obj)
        {
            BallPrefabs.Remove(obj);
            ClearTypeDictionary();
        }
        
        private void BuildTypeDictionary()
        {
            _prefabsByType = new Dictionary<BallType, List<GameObject>>();
            
            foreach (var prefab in BallPrefabs)
            {
                var clickableBall = prefab.GetComponent<IClickableBall>();
                if (clickableBall != null)
                {
                    BallType type = clickableBall.BallType;
                    
                    if (!_prefabsByType.ContainsKey(type))
                    {
                        _prefabsByType[type] = new List<GameObject>();
                    }
                    
                    _prefabsByType[type].Add(prefab);
                }
            }
        }
        
        private void ClearTypeDictionary()
        {
            _prefabsByType = null;
        }

        /// <summary>
        /// Get a random ball prefab from all available types
        /// </summary>
        public GameObject GetRandomObject()
        {
            if (BallPrefabs.Count == 0) return null;
            return BallPrefabs[Random.Range(0, BallPrefabs.Count)];
        }
        
        /// <summary>
        /// Get a random ball prefab of a specific type
        /// </summary>
        public GameObject GetRandomObjectOfType(BallType type)
        {
            if (_prefabsByType == null)
            {
                BuildTypeDictionary();
            }
            
            if (_prefabsByType.TryGetValue(type, out List<GameObject> prefabs) && prefabs.Count > 0)
            {
                return prefabs[Random.Range(0, prefabs.Count)];
            }
            
            return null;
        }
        
        /// <summary>
        /// Get a random ball type from available prefabs
        /// </summary>
        public BallType GetRandomBallType()
        {
            if (_prefabsByType == null)
            {
                BuildTypeDictionary();
            }
            
            var types = new List<BallType>(_prefabsByType.Keys);
            if (types.Count > 0)
            {
                return types[Random.Range(0, types.Count)];
            }

            // return the default value.
            return 0;
        }
    }
}
