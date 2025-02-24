using Game.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : MonoBehaviour
{
    public GameObject Prefab;
    [SerializeField] private BallsListSO _ballsListSO;
    [SerializeField] private ServiceLocatorSO _serviceLocatorSO;


    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        ObjectPoolSO runtimePool = ScriptableObject.CreateInstance<ObjectPoolSO>();
        runtimePool.prefab = _ballsListSO._ballPrefabs[0];
        runtimePool.initialPoolSize = 20;
        runtimePool.PrewarmPool();

        _serviceLocatorSO.RegisterService(runtimePool);
    }
}
