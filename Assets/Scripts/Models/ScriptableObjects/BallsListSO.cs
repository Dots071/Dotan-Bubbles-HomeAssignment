using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BallsListSO", menuName = "Scriptable Objects/BallsListSO")]
    public class BallsListSO : ScriptableObject
    {
        public List<GameObject> _ballPrefabs;

        public void Add(GameObject obj)
        {
            _ballPrefabs.Add(obj);
        }

        public void Remove(GameObject obj)
        {
            _ballPrefabs.Remove(obj);
        }

        public List<GameObject> GetList()
        {
            return _ballPrefabs;
        }

        public GameObject GetRandomObject()
        {
            if (_ballPrefabs.Count > 0)
            {

            }
            return _ballPrefabs[Random.Range(0, _ballPrefabs.Count)];
        }
    }

}
